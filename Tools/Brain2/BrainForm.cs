﻿// Sources:
//	https://dotnetbrowser.support.teamdev.com/support/solutions/articles/9000109708-saving-web-page-to-png-image
//	https://stackoverflow.com/questions/2715385/convert-webpage-to-image-from-asp-net
//	https://www.codeproject.com/Articles/12629/WYSIWYG-HTML-Editor	<== Old COM issues
//	https://github.com/UweKeim/ZetaHtmlEditControl	<== Currently used
//	https://www.telerik.com/support/kb/winforms/details/how-to-embed-chrome-browser-in-a-winforms-application	<== Chrome in Control
//	https://mkunc.com/2012/02/18/automating-chrome-browser-from-csharp/ <== Driving Chrome Dev Tools
//
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using SharpMath;
using ImageUtility;
using Renderer;

namespace Brain2 {
	public partial class BrainForm : Form {

		#region CONSTANTS

		static double	DEFAULT_OPACITY = 0.75f;	// Actually copied from Form at construction

		const float	Z_NEAR = 0.1f;
		const float	Z_FAR = 100.0f;

		#endregion

		#region NESTED TYPES

		[System.Runtime.InteropServices.StructLayout( System.Runtime.InteropServices.LayoutKind.Sequential )]
		struct CB_Main {
			public float4		_resolution;
			public float4		_mouseUV;
			public float2		_time_DeltaTime;
		}

		[System.Runtime.InteropServices.StructLayout( System.Runtime.InteropServices.LayoutKind.Sequential )]
		struct CB_Camera {
			public float4x4		_camera2World;
			public float4x4		_world2Camera;
			public float4x4		_proj2World;
			public float4x4		_world2Proj;
			public float4x4		_camera2Proj;
			public float4x4		_proj2Camera;

// 			public float4		_ZNearFar_Q_Z;
// 			public float4		_subPixelOffsets;
		}

		#endregion

		#region FIELDS

		// Database
		FichesDB					m_database = new FichesDB();

		// Display
		Device						m_device = new Device();

		ConstantBuffer< CB_Main >	m_CB_main = null;
		ConstantBuffer< CB_Camera >	m_CB_camera = null;
		Shader						m_shader_displayCube = null;
		Primitive					m_primitiveCube = null;

		DateTime					m_startTime;
		DateTime					m_lastFrameTime;

		// Modeless forms
		PreferencesForm				m_preferenceForm = null;
		FicheEditorForm				m_ficheEditorForm = null;

		#endregion

		#region METHODS

		/// <summary>
		/// Main loop
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void Application_Idle(object sender, EventArgs e) {
			if ( !Visible || m_device == null )
				return;

			DateTime	frameTime = DateTime.Now;
			float		totalTime = (float) (frameTime - m_startTime).TotalSeconds;
			float		deltaTime = (float) (frameTime - m_lastFrameTime).TotalSeconds;
			m_lastFrameTime = frameTime;

			m_CB_main.m._resolution.Set( Width, Height, 1.0f / Width, 1.0f / Height );
			m_CB_main.m._mouseUV.Set( (float) MousePosition.X / Width, (float) MousePosition.Y / Height, 0, 0 );
			m_CB_main.m._time_DeltaTime.Set( totalTime, deltaTime );
			m_CB_main.UpdateData();

			// Animate camera
			float	phi = Mathf.TWOPI * totalTime / 8.0f;	// 8 seconds for a full rotation
			float	theta = Mathf.HALFPI * (1.0f + 0.25f * Mathf.Sin( Mathf.TWOPI * totalTime / 6.0f ));	// 6 seconds for a full up/down cycle
			float3	wsTargetPosition = float3.Zero;
			float3	wsAt = new float3( Mathf.Cos( phi ) * Mathf.Sin( theta ), Mathf.Sin( phi ) * Mathf.Sin( theta ), Mathf.Cos( theta ) );
			SetCamera( wsTargetPosition - 5.0f * wsAt, wsTargetPosition, float3.UnitY, Mathf.ToRad( 90.0f ) );

			m_device.Clear( float4.Zero );
			m_device.ClearDepthStencil( m_device.DefaultDepthStencil, 1, 0, true, false );

			//////////////////////////////////////////////////////////////////////////
			// Display pipo cube
			if ( m_shader_displayCube.Use() ) {
				m_device.SetRenderStates( RASTERIZER_STATE.CULL_BACK, DEPTHSTENCIL_STATE.READ_WRITE_DEPTH_LESS, BLEND_STATE.DISABLED );
				m_device.SetRenderTarget( m_device.DefaultTarget, m_device.DefaultDepthStencil );
				m_primitiveCube.Render( m_shader_displayCube );
			}

			m_device.Present( false );
		}

		void	SetCamera( float3 _wsPosition, float3 _wsTargetPosition, float3 _wsUp, float _FOV ) {
			m_CB_camera.m._camera2World.BuildRotLeftHanded( _wsPosition, _wsTargetPosition, _wsUp );
			m_CB_camera.m._camera2Proj.BuildProjectionPerspective( _FOV, (float) Width / Height, Z_NEAR, Z_FAR );

			m_CB_camera.m._proj2Camera = m_CB_camera.m._camera2Proj.Inverse;
			m_CB_camera.m._world2Camera = m_CB_camera.m._camera2World.Inverse;
			m_CB_camera.m._proj2World = m_CB_camera.m._proj2Camera * m_CB_camera.m._camera2World;
			m_CB_camera.m._world2Proj = m_CB_camera.m._world2Camera * m_CB_camera.m._camera2Proj;

			m_CB_camera.UpdateData();
		}

		#region Fiche Creation

		public void	CreateFiche( IDataObject _data ) {
			try {



// Debug formats
Debug( "_____________________________" );
Debug( "Listing paste & drop formats:" );
foreach ( string format in _data.GetFormats() ) {
	object data = _data.GetData( format );
	Debug( format + " => " + (data!=null ? data.ToString() : "<null>") );
}
Debug( "_____________________________" );
Hide();

				// Ask factory to create the best fiche for our data
				Fiche	fiche = Data2Fiche.CreateFiche( _data );

Show();

				// Start edition
				m_ficheEditorForm.EditedFiche = fiche;
				m_ficheEditorForm.Show( this );

			} catch ( Exception _e ) {
				MessageBox( "An error occurred while creating fiche!", _e );
			}
		}

		#endregion

		#region Init / Exit

		public BrainForm() {
			InitializeComponent();

			DEFAULT_OPACITY = this.Opacity;

			try {

				// Create the modeless forms
				m_preferenceForm = new PreferencesForm( this );
				m_preferenceForm.RootDBFolderChanged += preferenceForm_RootDBFolderChanged;
				m_preferenceForm.Visible = false;

				m_ficheEditorForm = new FicheEditorForm( this );
				m_ficheEditorForm.Visible = false;

				// Parse fiches and load database
				DirectoryInfo	rootDBFolder = new DirectoryInfo( m_preferenceForm.RootDBFolder );
				if ( !rootDBFolder.Exists ) {
					rootDBFolder.Create();
					rootDBFolder.Refresh();

					int	waitCount = 0;
					while ( !rootDBFolder.Exists ) {
						System.Threading.Thread.Sleep( 100 );
						if ( waitCount++ > 10 )	// Wait for a full second
							throw new Exception( "Failed to create root DB folder \"" + rootDBFolder + "\"! Time elapsed..." );
					}
				}

				m_database.LoadDatabase( rootDBFolder );

			} catch ( Exception _e ) {
// 				MessageBox( "Error when creating forms and loading database!\r\n\n", _e );
// 				Close();
// //				Application.Exit();
			}

			try {
				// Attempt to retrieve containing monitor
				IntPtr	hMonitor;
				Screen	screen;
				Interop.GetMonitorFromPosition( Control.MousePosition, out screen, out hMonitor );

				// Rescale window to fullscreen overlay
				this.SetDesktopBounds( screen.Bounds.X, screen.Bounds.Y, screen.Bounds.Width, screen.Bounds.Height );

				// Create fullscreen windowed device
				m_device.Init( this.Handle, false, true );

				m_CB_main = new ConstantBuffer<CB_Main>( m_device, 0 );
				m_CB_camera = new ConstantBuffer<CB_Camera>( m_device, 1 );
				m_CB_camera.m._camera2World = new float4x4();
				m_CB_camera.m._camera2Proj = new float4x4();

				// Create primitives and shaders
				m_shader_displayCube = new Shader( m_device, new System.IO.FileInfo( "./Shaders/DisplayCube.hlsl" ), VERTEX_FORMAT.P3N3, "VS", null, "PS", null );

				BuildCube();

				m_startTime = DateTime.Now;
				Application.Idle += Application_Idle;

				// Register Win+X to toggle visibility
//				Interop.RegisterHotKey( this, Interop.NativeModifierKeys.Win | NativeModifierKeys.Shift, Keys.X );
				Interop.RegisterHotKey( this, Interop.NativeModifierKeys.Win, Keys.X );

				Focus();

			} catch ( Exception _e ) {
				MessageBox( "Error when creating D3D device!\r\n\n", _e );
				Close();
//				Application.Exit();
			}
		}

		protected override void Dispose(bool disposing) {
			if( disposing && components != null ) {
				components.Dispose();
			}
			
			Interop.UnregisterHotKey( Handle, 0 );

			m_primitiveCube.Dispose();

			m_shader_displayCube.Dispose();

			m_CB_main.Dispose();
			m_CB_camera.Dispose();

			m_device.Dispose();

			base.Dispose(disposing);
		}

		#region Primitives Creation

		/// <summary>
		/// Build the cube primitive
		/// </summary>
		void	BuildCube() {
			float3[]	Normals = new float3[6] {
				-float3.UnitX,
				float3.UnitX,
				-float3.UnitY,
				float3.UnitY,
				-float3.UnitZ,
				float3.UnitZ,
			};

			float3[]	Tangents = new float3[6] {
				float3.UnitZ,
				-float3.UnitZ,
				float3.UnitX,
				-float3.UnitX,
				-float3.UnitX,
				float3.UnitX,
			};

			VertexP3N3[]	vertices = new VertexP3N3[6*4];
			uint[]			indices = new uint[2*6*3];

			for ( int FaceIndex=0; FaceIndex < 6; FaceIndex++ ) {
				float3	N = Normals[FaceIndex];
				float3	T = Tangents[FaceIndex];
				float3	B = N.Cross( T );

				vertices[4*FaceIndex+0] = new VertexP3N3() {
					P = N - T + B,
					N = N,
//					T = T,
//					B = B,
//					UV = new float2( 0, 0 )
				};
				vertices[4*FaceIndex+1] = new VertexP3N3() {
					P = N - T - B,
					N = N,
// 					T = T,
// 					B = B,
// 					UV = new float2( 0, 1 )
				};
				vertices[4*FaceIndex+2] = new VertexP3N3() {
					P = N + T - B,
					N = N,
// 					T = T,
// 					B = B,
// 					UV = new float2( 1, 1 )
				};
				vertices[4*FaceIndex+3] = new VertexP3N3() {
					P = N + T + B,
					N = N,
// 					T = T,
// 					B = B,
// 					UV = new float2( 1, 0 )
				};

				indices[2*3*FaceIndex+0] = (uint) (4*FaceIndex+0);
				indices[2*3*FaceIndex+1] = (uint) (4*FaceIndex+1);
				indices[2*3*FaceIndex+2] = (uint) (4*FaceIndex+2);
				indices[2*3*FaceIndex+3] = (uint) (4*FaceIndex+0);
				indices[2*3*FaceIndex+4] = (uint) (4*FaceIndex+2);
				indices[2*3*FaceIndex+5] = (uint) (4*FaceIndex+3);
			}

			m_primitiveCube = new Primitive( m_device, (uint) vertices.Length, VertexP3N3.FromArray( vertices ), indices, Primitive.TOPOLOGY.TRIANGLE_LIST, VERTEX_FORMAT.P3N3 );

// 			VertexP3N3[]	vertices = new VertexP3N3[4*6];
// 			uint[]			indices = new uint[6*2*3];
// 			for ( uint face=0; face < 6; face++ ) {
// 				float3	N = (1 - 2 * (face & 1)) * new float3( (face >> 1) == 0 ? 1 : 0, (face >> 1) == 1 ? 1 : 0, (face >> 1) == 2 ? 1 : 0 );
// 				float3	T = (1 - 2 * (face & 1)) * new float3( (face >> 1) == 1 ? 1 : 0, (face >> 1) == 2 ? 1 : 0, (face >> 1) == 0 ? 1 : 0 );
// 				float3	B = T.Cross( N );
// 
// 				for ( uint v=0; v < 4; v++ ) {
// 					vertices[4*face+v].N = N;
// 					vertices[4*face+v].P = N + (2.0f*(v & 1)-1) * T + (2.0f*((v >> 1) & 1)-1) * B;
// 				}
// 
// 				indices[3*(2*face+0)+0] = 4*face + 0;
// 				indices[3*(2*face+0)+1] = 4*face + 1;
// 				indices[3*(2*face+0)+2] = 4*face + 2;
// 				indices[3*(2*face+1)+0] = 4*face + 2;
// 				indices[3*(2*face+1)+1] = 4*face + 1;
// 				indices[3*(2*face+1)+2] = 4*face + 3;
// 			}
// 
// 			m_primitiveCube = new Primitive( m_device, 6*4, VertexP3N3.FromArray( vertices ), indices, Primitive.TOPOLOGY.TRIANGLE_LIST, VERTEX_FORMAT.P3N3 );
		}

		#endregion

		#endregion

		#region Window Modes

		/// <summary>
		/// Shows the window as a fullscreen overlay where the mouse cursor is standing
		/// </summary>
		public new void	Show() {
			bool	mouseInBounds = Bounds.Contains( Control.MousePosition );
			if ( Visible && mouseInBounds )
				return;	// No change...

			if ( !mouseInBounds ) {
				// Retrieve new boundaries and resize & move viewport accordingly
				try {
					IntPtr	hMonitor;
					Screen	screen;
					Interop.GetMonitorFromPosition( Control.MousePosition, out screen, out hMonitor );

					// Rescale window to fullscreen overlay
					this.SetDesktopBounds( screen.Bounds.X, screen.Bounds.Y, screen.Bounds.Width, screen.Bounds.Height );
					m_device.ResizeSwapChain( (uint) screen.Bounds.Width, (uint) screen.Bounds.Height );

					this.TopMost = true;

				} catch ( Exception _e ) {
					MessageBox( "Error while changing window's location and size!", _e );
				}
			}

			base.Show();
//			Capture = true;
		}

		public new void	Hide() {
			if ( !Visible )
				return;	// No change...

			// Also hide any open form
			m_preferenceForm.Hide();
			m_ficheEditorForm.Hide();

//			Capture = false;
			base.Hide();
		}

		protected override void OnLostFocus(EventArgs e) {
			base.OnLostFocus(e);
			ExitFishing();
		}

		#endregion

		#region Fishing Mode

		bool	m_fishing = false;

		/// <summary>
		/// Enters "fiche fishing"
		/// </summary>
		void	EnterFishing() {
			if ( m_fishing )
				return;

			this.Opacity = 0.1;	// 10% opacity only
// 			this.TransparencyKey = this.BackColor;	// This will make the mouse messages go straight through the overlay
// 			Capture = true;

			m_fishing = true;
		}

		void	ExitFishing() {
			if ( !m_fishing )
				return;

			this.Opacity = DEFAULT_OPACITY;
// 			this.TransparencyKey = Color.Transparent;
// 			Capture = false;

			m_fishing = false;
		}

		#endregion

		#region Global Shortcut Management

		// From https://stackoverflow.com/questions/2450373/set-global-hotkeys-using-c-sharp

		/// <summary>
		/// Overridden to get the notifications.
		/// </summary>
		/// <param name="m"></param>
		protected override void WndProc( ref Message m ) {

			switch ( m.Msg ) {
				case Interop.WM_HOTKEY:
					Keys						key = (Keys) (((int)m.LParam >> 16) & 0xFFFF);
					Interop.NativeModifierKeys	modifiers = (Interop.NativeModifierKeys) ((int)m.LParam & 0xFFFF);

					// Toggle visibility
					if ( Visible )
						Hide();
					else
						Show();
					break;

				case Interop.WM_KEYDOWN:
// 					if ( m.HWnd == m_ficheEditorForm.Handle ) {
// 						System.Diagnostics.Debug.WriteLine( "Bisou!" );
// 					}
					break;

				default:
					System.Diagnostics.Debug.WriteLine( "Message: " + m );
					break;
			}

			if ( !m_fishing ) {
				base.WndProc(ref m);	// Process messages normally
			} else {
				// In "fishing mode", dispatch messages to background windows
				switch ( m.Msg ) {
					case 0x7f:
					case 0x84:	// (WM_NCHITTEST) hwnd=0xaa1b64 wparam=0x0 lparam=0x34c040e result=0x0
					case 0x20:	// (WM_SETCURSOR)
						break;

					default:
						System.Diagnostics.Debug.WriteLine( "Message: " + m );
						break;
				}

//				if ( m.Msg == Interop.WM_KEYUP || m.Msg == Interop.WM_KEYDOWN || m.Msg == Interop.WM_SYSKEYUP || m.Msg == Interop.WM_SYSKEYDOWN ) {
				if ( m.Msg == Interop.WM_KEYUP || m.Msg == Interop.WM_SYSKEYUP ) {
					if ( ((Keys) m.WParam.ToInt32()) == Keys.Menu ) {
						System.Diagnostics.Debug.WriteLine( "========================= EXIT FISHING! =========================" );
						ExitFishing();
					}
				}

				DispatchMessageToBackgroundWindows( m );
			}
		}

		#endregion

		#region Background Windows Management

		void	DispatchMessageToBackgroundWindows( Message m ) {

			StringBuilder	sb = new StringBuilder( 1000 );

			IntPtr[]	handles = Interop.GetWindowHandles();
			foreach ( IntPtr handle in handles ) {
				try {
					if ( handle == Handle )
						continue;
					if ( !Interop.IsWindowVisible( handle ) )
						continue;

// 					int	length = GetWindowTextLength( handle );
// 					if ( length > 0 ) {
// 						sb.Clear();
// 						GetWindowText( handle, sb, sb.Capacity );
// 						System.Diagnostics.Debug.WriteLine( sb );
// 					}

 					Control	F = Control.FromHandle( handle );
					if ( F != null ) {
						System.Diagnostics.Debug.WriteLine( "WEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEEE!" );
					}
// 					if ( F == null || F == this )//|| !F.Bounds.Contains( Control.MousePosition ) )
// 						continue;	// Not a target...
//
// 					Application.OpenForms
// 
// 					System.Windows.Forms.Form.FromHandle
// 
// 					System.Diagnostics.Debug.WriteLine( "Possible target = " + F.Name );

				} catch ( Exception _e ) {
					System.Diagnostics.Debug.WriteLine( "GNA!" + _e.Message );
				}
			}

		}

		#endregion

		#region Forms

		void	ToggleShowPreferences() {
			if ( m_preferenceForm.Visible )
				m_preferenceForm.Hide();
			else
				m_preferenceForm.Show( this );
		}

		void	ToggleShowFicheEditor() {
			if ( m_ficheEditorForm.Visible )
				m_ficheEditorForm.Hide();
			else
				m_ficheEditorForm.Show( this );
		}

		#endregion

		#region Helpers

		public static void	MessageBox( string _message, Exception _e ) {
			MessageBox( _message + _e.Message, MessageBoxIcon.Error );
		}
		public static void	MessageBox( string _message ) {
			MessageBox( _message, MessageBoxIcon.Information );
		}
		public static void	MessageBox( string _message, MessageBoxButtons _buttons ) {
			MessageBox( _message, _buttons, MessageBoxIcon.Information );
		}
		public static void	MessageBox( string _message, MessageBoxIcon _icon ) {
			MessageBox( _message, MessageBoxButtons.OK, _icon );
		}
		public static void	MessageBox( string _message, MessageBoxButtons _buttons, MessageBoxIcon _icon ) {
			System.Windows.Forms.MessageBox.Show( _message, "Brain 2", _buttons, _icon );
		}

		public static void	Debug( string _text ) {
			System.Diagnostics.Debug.WriteLine( _text );
		}

		#endregion

		#endregion

		#region EVENTS

// 		protected override bool ProcessKeyPreview(ref Message m) {
// 			return base.ProcessKeyPreview(ref m);
// 		}

		protected override void OnShown(EventArgs e) {
			base.OnShown(e);
			Focus();
		}

		protected override void OnKeyDown(KeyEventArgs e) {
			base.OnKeyDown(e);
			switch ( e.KeyCode ) {
				case Keys.Escape:
					Hide();
					break;

				case Keys.V:
					if ( e.Control ) {
//MessageBox( "PASTE!" );
						CreateFiche( Clipboard.GetDataObject() );
					}
					break;

				case Keys.R:
					m_device.ReloadModifiedShaders();
					break;

// @TODO! Fix fishing!
//	=> Global hook + make entire form transparent so back windows get messages?
//	=> Or make opaque + forward messages to back windows if possible?
// 				case Keys.Menu:
// 					if ( e.Alt )
// 						EnterFishing();
// 					break;
			}

			if ( e.KeyCode == m_preferenceForm.SHORTCUT_KEY ) {
				ToggleShowPreferences();
			} else if ( e.KeyCode == m_ficheEditorForm.SHORTCUT_KEY ) {
				ToggleShowFicheEditor();
			}

//Debug( e.KeyCode.ToString() );
		}

		protected override void OnKeyUp(KeyEventArgs e) {
			base.OnKeyUp(e);
			switch ( e.KeyCode ) {
				case Keys.Alt | Keys.Menu:
					ExitFishing();
					break;
			}

			ExitFishing();
		}

		protected override void OnVisibleChanged(EventArgs e) {
			base.OnVisibleChanged(e);
			timerDisplay.Enabled = Visible;
		}

		private void notifyIcon_MouseUp(object sender, MouseEventArgs e) {
			if ( e.Button != MouseButtons.Left )
				return;

			Show();
			Focus();
		}

		private void BrainForm_MouseUp(object sender, MouseEventArgs e) {
			if ( !this.Bounds.Contains( Control.MousePosition ) ) {
				Hide();
			}
		}

		private void preferenceForm_RootDBFolderChanged(object sender, EventArgs e) {
			try {
				m_database.Rebase( new DirectoryInfo( m_preferenceForm.RootDBFolder ) );
			} catch ( Exception _e ) {
				MessageBox( "Some errors occurred while rebasing fiches database!", _e );
			}
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e) {
			Close();
		}

		#region Drag'n Drop

		private void BrainForm_QueryContinueDrag(object sender, QueryContinueDragEventArgs e) {
			e.Action = DragAction.Continue;
		}

		private void BrainForm_DragOver(object sender, DragEventArgs e) {
			e.Effect = e.AllowedEffect;
		}

		private void BrainForm_DragDrop(object sender, DragEventArgs e) {
			CreateFiche( e.Data );
		}

		private void BrainForm_GiveFeedback(object sender, GiveFeedbackEventArgs e) {
		}

		private void BrainForm_DragEnter(object sender, DragEventArgs e) {
		}

		#endregion

		#endregion
	}
}