﻿//////////////////////////////////////////////////////////////////////////
/// This is an adaptation of the iOS LaTeX library by Kostub Deshmukh
/// Initial GitHub project: https://github.com/kostub/iosMath
///
//////////////////////////////////////////////////////////////////////////
///
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpTeX
{
	/// <summary>
	/// Generates an image from a LaTeX expression
	///  Accepts either a string in LaTeX or an `MTMathList` to display. Use `MTMathList` directly only if you are building it programmatically (e.g. using an editor), otherwise using LaTeX is the preferable method.
	///  The math display is centered vertically in the label. The default horizontal alignment is is left. This can be changed by setting `textAlignment`. The math is default displayed in *Display* mode. This can be changed using `labelMode`.
	///  When created it uses `[MTFontManager defaultFont]` as its font. This can be changed using the `font` parameter.
	/// </summary>
	///  
    public class ImageLaTeX {

		#region NESTED TYPES

		/// <summary>
		/// Different display styles supported by the `MTMathUILabel`.
		/// </summary>
		/// <remarks> The only significant difference between the two modes is how fractions and limits on large operators are displayed.</remarks>
		public enum Mode {
			Display,			// Display mode. Equivalent to $$ in TeX
			Text				// Text mode. Equivalent to $ in TeX.
		}

		/// <summary>
		/// Horizontal text alignment
		/// </summary>
		public enum TextAlignment {
			Left,			// Align left.
			Center,			// Align center.
			Right,			// Align right.
		}

		#endregion

		#region FIELDS

		AtomsList		m_atoms = null;
		string			m_LaTeX = null;
		ParseException	m_error = null;

		#endregion

		#region PROPERTIES

		public string	LaTeX {
			get { return m_LaTeX; }
			set {
				if ( value == m_LaTeX )
					return;

				m_LaTeX = value;
				m_error = null;
				m_atoms = null;

				// Attempt conversion
				try {
					m_atoms = LaTeXStringToAtomsList.BuildFromString( m_LaTeX );
					DisplayAtoms( m_atoms );
				} catch ( ParseException _e ) {
					m_error = _e;
				}

			}
		}

		/// <summary>
		/// The `MTMathList` to render. Setting this will remove any `latex` that has already been set. If `latex` has been set, this will return the parsed `MTMathList` if the `latex` parses successfully.
		/// Use this setting if the `MTMathList` has been programmatically constructed, otherwise it is preferred to use `latex`.
		/// </summary>
		public AtomsList		Atoms {
			get { return m_atoms; }
			set {
				if ( value == m_atoms )
					return;

				m_LaTeX = null;
				m_atoms = value;

				try {
					DisplayAtoms( m_atoms );
				} catch ( Exception _e ) {
					throw _e;
				}
			}
		}

// /** The latex string to be displayed. Setting this will remove any `mathList` that
//  has been set. If latex has not been set, this will return the latex output for the
//  `mathList` that is set.
//  @see error */
// @property (nonatomic, nullable) IBInspectable NSString* latex;
// 
// /** The MTFont to use for rendering. */
// @property (nonatomic, nonnull) MTFont* font;
// 
// /** Convenience method to just set the size of the font without changing the fontface. */
// @property (nonatomic) IBInspectable CGFloat fontSize;
// 
// /** This sets the text color of the rendered math formula. The default color is black. */
// @property (nonatomic, nonnull) IBInspectable MTColor* textColor;
// 
// /** The minimum distance from the margin of the view to the rendered math. This value is
//  `UIEdgeInsetsZero` by default. This is useful if you need some padding between the math and
//  the border/background color. sizeThatFits: will have its returned size increased by these insets.
//  */
// @property (nonatomic) IBInspectable MTEdgeInsets contentInsets;
// 
// /** The Label mode for the label. The default mode is Display */
// @property (nonatomic) MTMathUILabelMode labelMode;
// 
// /** Horizontal alignment for the text. The default is align left. */
// @property (nonatomic) MTTextAlignment textAlignment;
// 
// /** The internal display of the MTMathUILabel. This is for advanced use only. */
// @property (nonatomic, readonly, nullable) MTMathListDisplay* displayList;

		#endregion

		#region METHODS

		public ImageLaTeX() {

// 	self.layer.geometryFlipped = YES;  // For ease of interaction with the CoreText coordinate system.
//     // default font size
//     _fontSize = 20;
//     _contentInsets = MTEdgeInsetsZero;
//     _labelMode = kMTMathUILabelModeDisplay;
//     MTFont* font = [MTFontManager fontManager].defaultFont;
//     self.font = font;
//     _textAlignment = kMTTextAlignmentLeft;
//     _displayList = nil;
//     _displayErrorInline = true;
//     self.backgroundColor = [MTColor clearColor];
//     
//     _textColor = [MTColor blackColor];
//     _errorLabel = [[MTLabel alloc] init];
//     _errorLabel.hidden = YES;
//     _errorLabel.layer.geometryFlipped = YES;
//     _errorLabel.textColor = [MTColor redColor];
//     [self addSubview:_errorLabel];
		}

		void	DisplayAtoms( AtomsList _atoms ) {

		}

		#endregion
    }
}
