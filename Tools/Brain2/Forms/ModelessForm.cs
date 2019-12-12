﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Brain2 {

	public abstract partial class ModelessForm : Form {

		#region CONSTANTS

		const int	BORDER_SIZE = 8;

		#endregion

		#region FIELDS

		protected BrainForm	m_owner;

		protected bool		m_sizeable = false;
		protected int		m_hitArea = Interop.HTCAPTION;

		protected System.Drawing.Size		m_relativeLocation = new System.Drawing.Size( -1, -1 );

		#endregion

		#region PROPERTIES

		public abstract Keys	SHORTCUT_KEY { get; }

		#endregion

		#region METHODS

		public ModelessForm( BrainForm _owner ) {
			m_owner = _owner;

			InitializeComponent();
		}

// 		public new void	Show() {
// 			base.Show();
// 			Capture = true;
// 
// 			if ( m_relativeLocation.Width < 0 ) {
// 				// Initial condition => Reset to center
// 				m_relativeLocation.Width = (m_owner.Width - Width) / 2;
// 				m_relativeLocation.Height = (m_owner.Height - Height) / 2;
// 			}
// 
// 			Point	newLocation = m_owner.Location + m_relativeLocation;
// 			newLocation.X = Math.Max( 0, Math.Min( m_owner.Width - Width, newLocation.X ) );
// 			newLocation.Y = Math.Max( 0, Math.Min( m_owner.Height - Width, newLocation.Y ) );
// 			this.Location = newLocation;
// 		}
// 
// 		public new void Hide() {
// 			Capture = false;
// 			base.Hide();
// 		}

		protected override void OnShown(EventArgs e) {
//			Capture = true;

			if ( m_relativeLocation.Width < 0 ) {
				// Initial condition => Reset to center
				m_relativeLocation.Width = (m_owner.Width - Width) / 2;
				m_relativeLocation.Height = (m_owner.Height - Height) / 2;
			}

			Point	newLocation = m_owner.Location + m_relativeLocation;
					newLocation.X = Math.Max( m_owner.Left, Math.Min( m_owner.Right - Width, newLocation.X ) );
					newLocation.Y = Math.Max( m_owner.Top, Math.Min( m_owner.Bottom - Height, newLocation.Y ) );

			this.Location = newLocation;

			base.OnShown(e);
		}

		public new void Hide() {
			Capture = false;
			base.Hide();
		}

		protected override void OnLocationChanged(EventArgs e) {
			if ( Visible ) {
	 			// Update relative location
	 			m_relativeLocation.Width = Left - m_owner.Left;
	 			m_relativeLocation.Height = Top - m_owner.Top;

//System.Diagnostics.Debug.WriteLine( "Relative location = " + m_relativeLocation );
			}

			base.OnLocationChanged(e);
		}

		protected override void OnKeyDown(KeyEventArgs e) {
			if ( e.KeyCode == Keys.Escape || e.KeyCode == SHORTCUT_KEY ) {
				Hide();
			}

			base.OnKeyDown(e);
		}

// 		private Point			m_mouseDownFormLocation;
// 		private Point			m_mouseDownPosition;
// 		private MouseButtons	m_mouseDownButtons = MouseButtons.None;

		protected override void OnMouseDown(MouseEventArgs e) {
			base.OnMouseDown(e);
// 			m_mouseDownFormLocation = this.Location;
// 			m_mouseDownPosition = e.Location;
// 			m_mouseDownButtons = e.Button;

			// Simulate a click on caption bar that Windows normally uses to move windows
			// From https://stackoverflow.com/questions/1592876/make-a-borderless-form-movable
			if ( e.Button == MouseButtons.Left ) {
				Interop.ReleaseCapture();
				Interop.SendMessage( Handle, Interop.WM_NCLBUTTONDOWN, m_hitArea, 0 );
			}
		}

 		protected override void OnMouseMove(MouseEventArgs e) {

// 			if ( m_mouseDownButtons == MouseButtons.Left ) {
// 				// Move form
// 				Size	delta = new Size( e.Location.X - m_mouseDownPosition.X, e.Location.Y - m_mouseDownPosition.Y );
// 				this.Location = m_mouseDownFormLocation + delta;
// 			}

			m_hitArea = Interop.HTCAPTION;
			if ( m_sizeable ) {
				int	borderLeft = e.X < BORDER_SIZE ? 1 : 0;
				int	borderRight = e.X > Width-BORDER_SIZE ? 2 : 0;
				int	borderTop = e.Y < BORDER_SIZE ? 4 : 0;
				int	borderBottom = e.Y > Height-BORDER_SIZE ? 8 : 0;

				switch ( borderLeft | borderRight | borderTop | borderBottom ) {
					case 1: m_hitArea = Interop.HTLEFT; Cursor = Cursors.SizeWE; break;
					case 2: m_hitArea = Interop.HTRIGHT; Cursor = Cursors.SizeWE; break;
					case 4: m_hitArea = Interop.HTTOP; Cursor = Cursors.SizeNS; break;
					case 8: m_hitArea = Interop.HTBOTTOM; Cursor = Cursors.SizeNS; break;

					case 1 + 4: m_hitArea = Interop.HTTOPLEFT; Cursor = Cursors.SizeNWSE; break;
					case 1 + 8: m_hitArea = Interop.HTBOTTOMLEFT; Cursor = Cursors.SizeNESW; break;
					case 2 + 4: m_hitArea = Interop.HTTOPRIGHT; Cursor = Cursors.SizeNESW; break;
					case 2 + 8: m_hitArea = Interop.HTBOTTOMRIGHT; Cursor = Cursors.SizeNWSE; break;

					default: Cursor = Cursors.Default; break;
				}
			}

			base.OnMouseMove(e);
		}

// 		protected override void OnMouseUp(MouseEventArgs e) {
// 			base.OnMouseUp(e);
// //			m_mouseDownButtons = MouseButtons.None;
// 		}

		#endregion

		#region EVENTS

		#endregion
	}
}