/****************************************************************

    PVS.MediaPlayer - Version 1.0
    September 2020, The Netherlands
    © Copyright 2020 PVS The Netherlands - licensed under The Code Project Open License (CPOL)

    PVS.MediaPlayer uses (part of) the Media Foundation .NET library by nowinskie and snarfle (https://sourceforge.net/projects/mfnet).
    Licensed under either Lesser General Public License v2.1 or BSD.  See license.txt or BSDL.txt for details (http://mfnet.sourceforge.net).

    ****************

    For use with Microsoft Windows 7 or higher, Microsoft .NET Framework version 4.0 or higher and WinForms (any CPU).
    Created with Microsoft Visual Studio.

    Article on CodeProject with information on the use of the PVS.MediaPlayer library:
    https://www.codeproject.com/Articles/109714/PVS-MediaPlayer-Audio-and-Video-Player-Library

    ****************

    The PVS.MediaPlayer library source code is divided into 8 files:

    1. Player.cs        - main source code
    2. SubClasses.cs    - various grouping and information classes
    3. Interop.cs       - unmanaged Win32 functions
    4. AudioDevices.cs  - audio devices and peak meters
    5. DisplayClones.cs - multiple video displays 
    6. CursorHide.cs    - hides the mouse cursor during inactivity
    7. Subtitles.cs     - subrip (.srt) subtitles
    8. Infolabel.cs     - custom ToolTip

    Required references:
    System
    System.Drawing
    System.Windows.Forms

    ****************

    This file: InfoLabel.cs

    Info Label Class

    ****************

    Many thanks to Microsoft (Windows, .NET Framework, Visual Studio and others), all the people
    writing about programming on the internet (a great source for ideas and solving problems),
    the websites publishing those or other writings about programming, the people responding to the
    PVS.MediaPlayer articles with comments and suggestions and, of course, the people at CodeProject.

    Special thanks to the creators of Media Foundation .NET for their great library.

    Special thanks to Sean Ewington and Deeksha Shenoy of CodeProject who also took care of publishing the many
    code updates and changes in the PVS.MediaPlayer articles in a friendly, fast, and highly competent manner.

    Peter Vegter
    September 2020, The Netherlands

    ****************************************************************/

#region Usings

using System;
using System.Drawing;
using System.Windows.Forms;

#endregion


namespace PlexDL.Player
{
    /// <summary>
    /// Represents a pop-up window that displays a short text. One instance of this class is sufficient to use infolabels throughout an application, but if desired, more can be created.
    /// </summary>
    [CLSCompliant(true)]
    public sealed class InfoLabel : HideObjectMembers, IDisposable
    {

        // ******************************** Info Label - Fields

        #region Info Label - Fields

        #region Constants

        // Default border margins - also minimum size
        private const int       IL_TEXT_MARGIN_LEFT         = 2;
        private const int       IL_TEXT_MARGIN_RIGHT        = 1;
        private const int       IL_TEXT_MARGIN_TOP          = 1;
        private const int       IL_TEXT_MARGIN_BOTTOM       = 1;

        // Maximum border margin per side
        private const int       IL_BORDER_MARGIN_MAXIMUM    = 150;

        // Default border thickness
        private const int       IL_BORDER_THICKNESS_LEFT    = 1;
        private const int       IL_BORDER_THICKNESS_TOP     = 1;
        private const int       IL_BORDER_THICKNESS_RIGHT   = 1;
        private const int       IL_BORDER_THICKNESS_BOTTOM  = 1;

        // Maximum border thickness per side
        private const int       IL_BORDER_THICKNESS_MAXIMUM = 150;

        // Text label resize - if label size change is greater also resize infolabel form
        private const int       IL_TEXT_ROOM_WIDTH          = 3;
        private const int       IL_TEXT_ROOM_HEIGHT         = 2;

        // Default duration and fading steps
        private const int       IL_DURATION                 = 1000;
        private const int       IL_DURATION_MINIMUM         = 10;
        private const double    IL_FADE_OUT_VALUE           = 0.2;

        #endregion

        private sealed class InfoForm : Form
        {
            #region Fields

            internal enum BackgroundMode
            {
                Solid,
                Brush,
                Image
            }

            internal sealed class DisplayLabel : Label
            {
                // Prevent mouseclick activation / detection
                protected override void WndProc(ref Message m)
                {
                    const int WM_MOUSEACTIVATE = 0x0021;
                    const int MA_NOACTIVATEANDEAT = 0x0004;
                    const int WM_NCHITTEST = 0x0084;
                    const int HTTRANSPARENT = (-1);

                    if (m.Msg == WM_MOUSEACTIVATE) m.Result = (IntPtr)MA_NOACTIVATEANDEAT;
                    else if (m.Msg == WM_NCHITTEST) m.Result = (IntPtr)HTTRANSPARENT;
                    else base.WndProc(ref m);
                }
            }

            //private InfoLabel       _base;
            internal DisplayLabel   Label;

            internal Region         FormRegion;
            internal bool           RoundCorners;

            internal Brush          BorderBrush = new SolidBrush(SystemColors.WindowFrame);
            internal BackgroundMode BackMode = BackgroundMode.Solid;
            internal Brush          BackBrush;
            internal bool           Transparent;

            private bool            _disposed;

            #endregion

            #region Main

            public InfoForm()
            {
                DoubleBuffered          = true;
                ShowInTaskbar           = false;
                MinimumSize             = new Size(32, 20);

                StartPosition           = FormStartPosition.Manual;
                FormBorderStyle         = FormBorderStyle.None;
                BackgroundImageLayout   = ImageLayout.Stretch;

                ForeColor               = SystemColors.InfoText;
                BackColor               = SystemColors.Info;

                Label = new DisplayLabel
                {
                    BackColor   = Color.Transparent,
                    TextAlign   = ContentAlignment.MiddleCenter,
                    AutoSize    = true,
                    MinimumSize = new Size(32, 20),
                    Left        = IL_BORDER_THICKNESS_LEFT + IL_TEXT_MARGIN_LEFT,
                    Top         = IL_BORDER_THICKNESS_TOP + IL_TEXT_MARGIN_TOP
                };
                Controls.Add(Label);
            }

            protected override bool ShowWithoutActivation
            {
                get { return true; }
            }

            // has to be top window because of player overlay.canfocus option
            protected override CreateParams CreateParams
            {
                get
                {
                    const int WS_EX_TOPMOST = 0x0008;

                    CreateParams cp = base.CreateParams;
                    cp.ExStyle |= WS_EX_TOPMOST;
                    return cp;
                }
            }

            // Prevent mouseclick activation / detection
            protected override void WndProc(ref Message m)
            {
                const int WM_MOUSEACTIVATE = 0x0021;
                const int MA_NOACTIVATEANDEAT = 0x0004;
                const int WM_NCHITTEST = 0x0084;
                const int HTTRANSPARENT = (-1);

                if (m.Msg == WM_MOUSEACTIVATE) m.Result = (IntPtr)MA_NOACTIVATEANDEAT;
                else if (m.Msg == WM_NCHITTEST) m.Result = (IntPtr)HTTRANSPARENT;
                else base.WndProc(ref m);
            }

            protected override void Dispose(bool disposing)
            {
                if (!_disposed)
                {
                    _disposed = true;
                    if (disposing)
                    {
                        if (FormRegion != null) FormRegion.Dispose();
                        if (BorderBrush != null) BorderBrush.Dispose();
                        if (BackBrush != null) BackBrush.Dispose();

                        if (Label != null) Label.Dispose();
                    }
                }
                base.Dispose(disposing);
            }

            #endregion

            #region Paint

            // 'regular paint' and draw border
            protected override void OnPaint(PaintEventArgs e)
            {
                base.OnPaint(e);

                // Draw border
                e.Graphics.FillRegion(BorderBrush, FormRegion);
            }

            // draw 'regular' or background brush
            protected override void OnPaintBackground(PaintEventArgs e)
            {
                if (BackMode == BackgroundMode.Brush && !Transparent) e.Graphics.FillRectangle(BackBrush, ClientRectangle);
                else base.OnPaintBackground(e);
            }

            #endregion
        }

        private InfoForm        il_InfoForm;

        private ContentAlignment il_Alignment               = ContentAlignment.TopCenter;
        private int             il_OffsetX;
        private int             il_OffsetY;

        private Color           il_BorderColor              = SystemColors.WindowFrame;
        private Padding         il_BorderThickness          = new Padding(IL_BORDER_THICKNESS_LEFT, IL_BORDER_THICKNESS_TOP, IL_BORDER_THICKNESS_RIGHT, IL_BORDER_THICKNESS_BOTTOM);
        private int             il_BorderThicknessWidth     = IL_BORDER_THICKNESS_LEFT + IL_BORDER_THICKNESS_RIGHT;
        private int             il_BorderThicknessHeight    = IL_BORDER_THICKNESS_TOP + IL_BORDER_THICKNESS_BOTTOM;

        private Padding         il_TextMargins              = new Padding(IL_TEXT_MARGIN_LEFT, IL_TEXT_MARGIN_TOP, IL_TEXT_MARGIN_RIGHT, IL_TEXT_MARGIN_BOTTOM);
        private int             il_TextMarginsWidth         = IL_TEXT_MARGIN_LEFT + IL_TEXT_MARGIN_RIGHT;
        private int             il_TextMarginsHeight        = IL_TEXT_MARGIN_TOP + IL_TEXT_MARGIN_BOTTOM;

        private Control         il_Control;
        private Form            il_BaseForm;

        private Size            il_OldTextSize              = Size.Empty;
        private Color           il_OldBackColor;
        private Image           il_OldImage;

        private bool            il_Busy;

        private Timer           il_Timer;
        private int             il_Duration                 = IL_DURATION;
        private double          il_FadeOutValue             = IL_FADE_OUT_VALUE;
        private bool            il_Fading;

        private double          il_Opacity                  = 1;

        private bool            il_Disposed;

        #endregion;


        // ******************************** Info Label - Main

        #region Info Label - Main

        /// <summary>
        /// Initializes a new instance of the PVS.MediaPlayer.InfoLabel class.
        /// </summary>
        public InfoLabel()
        {
            il_InfoForm = new InfoForm(); // this);
            SetBorder();

            il_Timer = new Timer();
            il_Timer.Tick += TimerTick;

        }

        /// <summary>
        /// Remove the infolabel and clean up any resources being used.
        /// </summary>
        public void Dispose()
        {
            if (!il_Disposed)
            {
                il_Disposed = true;

                if (il_Timer != null) { il_Timer.Dispose(); il_Timer = null; }
                if (il_InfoForm != null)
                {
                    try
                    {
                        il_InfoForm.Visible = false;
                        il_InfoForm.Dispose();
                    }
                    catch { /* ignore */ }
                    il_InfoForm = null;
                }

                il_Control = null;
                if (il_BaseForm != null)
                {
                    try
                    {
                        il_BaseForm.FormClosing -= BaseForm_FormClosing;
                        il_BaseForm.Deactivate -= BaseForm_HideLabel;
                        il_BaseForm.Move -= BaseForm_HideLabel;
                    }
                    catch { /* ignore */ }
                    il_BaseForm = null;
                }
            }
        }

        #endregion


        // ******************************** Info Label - TimerTick (Fading) / Reset Fading

        #region Info Label - TimerTick (Fading) / Reset Fading

        private void TimerTick(object sender, EventArgs e)
        {
            if (il_Fading)
            {
                il_InfoForm.Opacity -= il_FadeOutValue;
                if (il_InfoForm.Opacity <= 0)
                {
                    il_InfoForm.Visible = false;
                    ResetFading();
                }
            }
            else
            {
                il_Timer.Interval = 50;
                il_InfoForm.Opacity -= il_FadeOutValue;
                il_Fading = true;
            }
        }

        private void ResetFading()
        {
            il_Timer.Stop();
            il_InfoForm.Opacity = il_Opacity;
            il_Fading = false;
        }

        #endregion


        // ******************************** Info Label - Set InfoLabel Size / Border / BaseForm Deactivated

        #region Info Label - Set InfoLabel Size / Border / BaseForm Deactivated

        private void SetSize()
        {
            il_InfoForm.Size = new Size
                (il_InfoForm.Label.Width + il_BorderThicknessWidth + il_TextMarginsWidth,
                il_InfoForm.Label.Height + il_BorderThicknessHeight + il_TextMarginsHeight);

            //if (_infoForm.RoundCorners) SetRegion();
            ////else if (_infoForm.Visible) _infoForm.Invalidate();

            SetBorder();
        }

        private void SetBorder()
        {
            if (il_InfoForm.FormRegion != null) il_InfoForm.FormRegion.Dispose();
            if (il_InfoForm.Region != null) il_InfoForm.Region.Dispose();

            if (il_InfoForm.RoundCorners)
            {
                // Form region
                IntPtr handle1 = SafeNativeMethods.CreateRoundRectRgn(0, 0, il_InfoForm.Width + 1, il_InfoForm.Height + 1, 4, 4);
                il_InfoForm.FormRegion = Region.FromHrgn(handle1);
                il_InfoForm.Region = il_InfoForm.FormRegion.Clone();

                // Border region
                IntPtr handle2 = SafeNativeMethods.CreateRoundRectRgn(BorderThickness.Left, BorderThickness.Top, il_InfoForm.Width - BorderThickness.Right + 1, il_InfoForm.Height - BorderThickness.Bottom + 1, 3, 3);
                il_InfoForm.FormRegion.Exclude(Region.FromHrgn(handle2));

                SafeNativeMethods.DeleteObject(handle1); // is this necessary?
                SafeNativeMethods.DeleteObject(handle2);
            }
            else
            {
                il_InfoForm.Region = null;

                // Form region
                il_InfoForm.FormRegion = new Region(new Rectangle(0, 0, il_InfoForm.Width, il_InfoForm.Height));

                // Border region
                il_InfoForm.FormRegion.Exclude(new Rectangle(
                    BorderThickness.Left,
                    BorderThickness.Top,
                    il_InfoForm.Width - BorderThickness.Left - BorderThickness.Right,
                    il_InfoForm.Height - BorderThickness.Top - BorderThickness.Bottom));
            }
        }

        private void BaseForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            Hide(false);

            il_Control = null;
            if (il_BaseForm != null)
            {
                try
                {
                    il_BaseForm.FormClosing -= BaseForm_FormClosing;
                    il_BaseForm.Deactivate -= BaseForm_HideLabel;
                    il_BaseForm.Move -= BaseForm_HideLabel;
                }
                catch { /* ignore */ }
                il_BaseForm = null;
            }
        }

        // when form is deactivated or moved
        private void BaseForm_HideLabel(object sender, EventArgs e)
        {
            Hide(false);
        }

        #endregion


        // ******************************** Info Label - Public Members

        // ******************************** Info Label - Show / Hide

        #region Info Label - Show / Hide

        /// <summary>
        /// Shows the infolabel with the specified text and settings.
        /// </summary>
        /// <param name="text">A string containing the new infolabel text.</param>
        /// <param name="control">The form or control to display the infolabel for.</param>
        /// <param name="location">The location, in pixels, relative to the upper-left corner of the associated control, to display the infolabel.</param>
        public int Show(string text, Control control, Point location)
        {
            return Show(text, control, location, il_Alignment, il_Duration);
        }

        /// <summary>
        /// Shows the infolabel with the specified text and settings.
        /// </summary>
        /// <param name="text">A string containing the new infolabel text.</param>
        /// <param name="control">The form or control to display the infolabel for.</param>
        /// <param name="location">The location, in pixels, relative to the upper-left corner of the associated control, to display the infolabel.</param>
        /// <param name="duration">The duration, in milliseconds, to display the infolabel.</param>
        public int Show(string text, Control control, Point location, int duration)
        {
            return Show(text, control, location, il_Alignment, duration);
        }

        /// <summary>
        /// Shows the infolabel with the specified text and settings.
        /// </summary>
        /// <param name="text">A string containing the new infolabel text.</param>
        /// <param name="control">The form or control to display the infolabel for.</param>
        /// <param name="location">The location, in pixels, relative to the upper-left corner of the associated control, to display the infolabel.</param>
        /// <param name="alignment">The duration, in milliseconds, to display the infolabel.</param>
        public int Show(string text, Control control, Point location, ContentAlignment alignment)
        {
            return Show(text, control, location, alignment, il_Duration);
        }

        /// <summary>
        /// Shows the infolabel with the specified text and settings.
        /// </summary>
        /// <param name="text">A string containing the new infolabel text.</param>
        /// <param name="control">The form or control to display the infolabel for.</param>
        /// <param name="location">The location, in pixels, relative to the upper-left corner of the associated control, to display the infolabel.</param>
        /// <param name="alignment">The alignment of the infolabel relative to the specified location.</param>
        /// <param name="duration">The duration, in milliseconds, to display the infolabel.</param>
        public int Show(string text, Control control, Point location, ContentAlignment alignment, int duration)
        {
            HResult result = HResult.S_OK;

            if (!il_Busy)
            {
                il_Busy = true;

                if (string.IsNullOrWhiteSpace(text)
                    || control  == null
                    || location == null
                    || duration < IL_DURATION_MINIMUM
                    || control.FindForm() != Form.ActiveForm)
                {
                    result  = HResult.E_INVALIDARG;
                    il_Busy = false;
                    return (int)result;
                }

                il_InfoForm.Label.Text = text;
                if (il_InfoForm.Label.AutoSize && (Math.Abs(il_InfoForm.Label.Width - il_OldTextSize.Width) > IL_TEXT_ROOM_WIDTH || Math.Abs(il_InfoForm.Label.Height - il_OldTextSize.Height) > IL_TEXT_ROOM_HEIGHT))
                {
                    il_OldTextSize = il_InfoForm.Label.Size;
                    SetSize();
                }

                Point position = control.PointToScreen(location);
                switch (alignment)
                {
                    case ContentAlignment.TopLeft:
                        position.X -= il_InfoForm.Width - il_OffsetX;
                        position.Y -= il_InfoForm.Height - il_OffsetY;
                        break;
                    case ContentAlignment.TopCenter:
                        position.X -= (int)(il_InfoForm.Width * 0.5) - il_OffsetX;
                        position.Y -= il_InfoForm.Height - il_OffsetY;
                        break;
                    case ContentAlignment.TopRight:
                        position.X += il_OffsetX;
                        position.Y -= il_InfoForm.Height - il_OffsetY;
                        break;

                    case ContentAlignment.MiddleLeft:
                        position.X -= il_InfoForm.Width - il_OffsetX;
                        position.Y -= (int)(il_InfoForm.Height * 0.5) - il_OffsetY;
                        break;
                    case ContentAlignment.MiddleCenter:
                        position.X -= (int)(il_InfoForm.Width * 0.5) - il_OffsetX;
                        position.Y -= (int)(il_InfoForm.Height * 0.5) - il_OffsetY;
                        break;
                    case ContentAlignment.MiddleRight:
                        position.X += il_OffsetX;
                        position.Y -= (int)(il_InfoForm.Height * 0.5) - il_OffsetY;
                        break;

                    case ContentAlignment.BottomLeft:
                        position.X -= il_InfoForm.Width - il_OffsetX;
                        position.Y += il_OffsetY;
                        break;
                    case ContentAlignment.BottomCenter:
                        position.X -= (int)(il_InfoForm.Width * 0.5) - il_OffsetX;
                        position.Y += il_OffsetY;
                        break;
                    case ContentAlignment.BottomRight:
                        position.X += il_OffsetX;
                        position.Y += il_OffsetY;
                        break;
                    default:
                        break;
                }

                // check if within all screens
                Rectangle screen = SystemInformation.VirtualScreen;
                if (position.X < screen.X) position.X = screen.X;
                else if (position.X + il_InfoForm.Width > screen.Right) position.X = screen.Right - il_InfoForm.Width;
                if (position.Y < screen.Y) position.Y = screen.Y;
                else if (position.Y + il_InfoForm.Height > screen.Bottom) position.Y = screen.Bottom - il_InfoForm.Height;

                il_InfoForm.Location = position;

                if (il_Control != control)
                {
                    il_Control  = control;
                    Form form   = control.FindForm();
                    if (il_BaseForm != form)
                    {
                        if (il_BaseForm != null)
                        {
                            try
                            {
                                il_BaseForm.FormClosing -= BaseForm_FormClosing;
                                il_BaseForm.Deactivate  -= BaseForm_HideLabel;
                                il_BaseForm.Move        -= BaseForm_HideLabel;
                            }
                            catch { /* ignore */ }
                            il_BaseForm = null;
                        }
                        if (form != null)
                        {
                            il_BaseForm = form;
                            il_BaseForm.FormClosing += BaseForm_FormClosing;
                            il_BaseForm.Deactivate  += BaseForm_HideLabel;
                            il_BaseForm.Move        += BaseForm_HideLabel;
                        }
                    }
                }

                if (il_Fading) ResetFading();
                il_Timer.Stop();
                il_Timer.Interval = duration;
                il_Timer.Start();

                if (!il_InfoForm.Visible) il_InfoForm.Visible = true;
                il_InfoForm.Invalidate();

                il_Busy = false;
            }
            else result = HResult.ERROR_BUSY;

            return (int)result;
        }

        /// <summary>
        /// Hides the infolabel.
        /// </summary>
        /// <param name="fade">A value that indicates whether a fade effect should be used to hide the infolabel.</param>
        public void Hide(bool fade)
        {
            if (il_InfoForm.Visible)
            {
                if (fade)
                {
                    if (!il_Fading)
                    {
                        il_Timer.Stop();
                        il_Timer.Interval   = 50;
                        il_Timer.Start();
                        il_InfoForm.Opacity -= il_FadeOutValue;
                        il_Fading = true;
                    }
                }
                else
                {
                    il_InfoForm.Visible = false;
                    ResetFading();
                }
            }
        }

        #endregion


        // ******************************** Info Label - Active / Text / Size / Location / Image

        #region Info Label - Active / Text / Size / Location / Image

        /// <summary>
        /// Gets a value that indicates whether the infolabel is active (visible).
        /// </summary>
        public bool Active
        {
            get { return il_InfoForm.Visible; }
        }

        /// <summary>
        /// Gets or sets the text associated with the infoLabel.
        /// </summary>
        public string Text
        {
            get { return il_InfoForm.Label.Text; }
            set
            {
                il_InfoForm.Label.Text = value;
                if (il_InfoForm.Label.AutoSize) SetSize();
            }
        }

        /// <summary>
        /// Gets or sets the margin between the text area and the border of each side in the infolabel (when set, use -1 to keep an existing value - default all: 0).
        /// </summary>
        public Padding TextMargin
        {
            get
            {
                // adjust for minimum margin values
                return new Padding(
                    il_TextMargins.Left - IL_TEXT_MARGIN_LEFT,
                    il_TextMargins.Top - IL_TEXT_MARGIN_TOP,
                    il_TextMargins.Right - IL_TEXT_MARGIN_RIGHT,
                    il_TextMargins.Bottom - IL_TEXT_MARGIN_BOTTOM);
            }

            set
            {
                if (value.Left > IL_BORDER_MARGIN_MAXIMUM) value.Left = IL_BORDER_MARGIN_MAXIMUM;
                else if (value.Left < 0) value.Left = il_TextMargins.Left;
                else value.Left += IL_TEXT_MARGIN_LEFT; // adjust for minimum margin values

                if (value.Top > IL_BORDER_MARGIN_MAXIMUM) value.Top = IL_BORDER_MARGIN_MAXIMUM;
                else if (value.Top < 0) value.Top = il_TextMargins.Top;
                else value.Top += IL_TEXT_MARGIN_TOP;

                if (value.Right > IL_BORDER_MARGIN_MAXIMUM) value.Right = IL_BORDER_MARGIN_MAXIMUM;
                else if (value.Right < 0) value.Right = il_TextMargins.Right;
                else value.Right += IL_TEXT_MARGIN_RIGHT;

                if (value.Bottom > IL_BORDER_MARGIN_MAXIMUM) value.Bottom = IL_BORDER_MARGIN_MAXIMUM;
                else if (value.Bottom < 0) value.Bottom = il_TextMargins.Bottom;
                else value.Bottom += IL_TEXT_MARGIN_BOTTOM;

                if (value != il_TextMargins)
                {
                    il_TextMargins = value;
                    il_TextMarginsWidth = value.Left + value.Right;
                    il_TextMarginsHeight = value.Top + value.Bottom;

                    il_InfoForm.Label.Location = new Point(value.Left + il_BorderThickness.Left, value.Top + il_BorderThickness.Top);
                    SetSize();
                }
            }
        }

        /// <summary>
        /// Gets the current size of the (variable sized) infoLabel.
        /// </summary>
        public Size Size
        {
            get { return il_InfoForm.Size; }
        }

        /// <summary>
        /// Gets the true location (top-left screen coordinates) of the infoLabel (when visible).
        /// </summary>
        public Point Location
        {
            get
            {
                if (il_InfoForm.Visible) return il_InfoForm.Location;
                return Point.Empty;
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the infolabel is automatically resized to display the entire text default: true).
        /// </summary>
        public bool AutoSize
        {
            get { return il_InfoForm.Label.AutoSize; }
            set
            {
                if (value != il_InfoForm.Label.AutoSize)
                {
                    il_InfoForm.Label.AutoSize = value;
                    if (value && il_InfoForm.Visible) il_InfoForm.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the height and width of the text area of the infolabel. When set, the autosize property of the infolabel is set to false.
        /// </summary>
        public Size TextSize
        {
            get { return il_InfoForm.Label.Size; }
            set
            {
                if (value.Width < 32) value.Width = 32;
                if (value.Height < 20) value.Height = 20;
                il_InfoForm.Label.AutoSize = false;
                il_InfoForm.Label.Size = value;
                if (il_InfoForm.Visible) il_InfoForm.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the alignment of the text in the text area of the infolabel (default: MiddleCenter).
        /// </summary>
        public ContentAlignment TextAlign
        {
            get { return il_InfoForm.Label.TextAlign; }
            set
            {
                if (value != il_InfoForm.Label.TextAlign)
                {
                    il_InfoForm.Label.TextAlign = value;
                    if (il_InfoForm.Visible) il_InfoForm.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the image that is displayed in the text area of the infolabel.
        /// </summary>
        public Image Image
        {
            get { return il_InfoForm.Label.Image; }
            set
            {
                if (value != il_InfoForm.Label.Image)
                {
                    il_InfoForm.Label.Image = value;
                    if (il_InfoForm.Visible) il_InfoForm.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the alignment of the image that is displayed in the text area of the infolabel (default: MiddleCenter).
        /// </summary>
        public ContentAlignment ImageAlign
        {
            get { return il_InfoForm.Label.ImageAlign; }
            set
            {
                if (value != il_InfoForm.Label.ImageAlign)
                {
                    il_InfoForm.Label.ImageAlign = value;
                    if (il_InfoForm.Visible) il_InfoForm.Invalidate();
                }
            }
        }

        #endregion


        // ******************************** Info Label - Font / Colors / Border / RoundedCorners / BackImage

        #region Info Label - Font / Colors / Border / RoundedCorners / BackImage

        /// <summary>
        /// Gets or sets the font of the text displayed in the infolabel (default: Label.DefaultFont).
        /// </summary>
        public Font Font
        {
            get { return il_InfoForm.Label.Font; }
            set
            {
                if (value != null)
                {
                    try
                    {
                        il_InfoForm.Label.Font = value;
                        if (il_InfoForm.Label.AutoSize) SetSize();
                    }
                    catch { /* ignore */ }
                }
            }
        }

        /// <summary>
        /// Gets or sets the size of the font of the text displayed in the infolabel (default: Label.DefaultFont.Size).
        /// </summary>
        public float FontSize
        {
            get { return il_InfoForm.Label.Font.Size; }
            set
            {
                if (value <= 6) value = 6;
                else if (value > 128) value = 128;

                try
                {
                    il_InfoForm.Label.Font = new Font(il_InfoForm.Label.Font.FontFamily, value);
                    if (il_InfoForm.Label.AutoSize) SetSize();
                }
                catch { /* ignore */ }
            }
        }

        /// <summary>
        /// Gets or sets the style of the font of the text displayed in the infolabel (default: FontStyle.Regular).
        /// </summary>
        public FontStyle FontStyle
        {
            get { return il_InfoForm.Label.Font.Style; }
            set
            {
                try
                {
                    il_InfoForm.Label.Font = new Font(il_InfoForm.Label.Font, value);
                    if (il_InfoForm.Label.AutoSize) SetSize();
                }
                catch { /* ignore */ }
            }
        }

        /// <summary>
        /// Gets or sets a value that determines whether to use the Graphics class (GDI+) or the TextRenderer class (GDI) to render the text in the infolabel (default: false (GDI+)).
        /// </summary>
        public bool UseCompatibleTextRendering
        {
            get { return il_InfoForm.Label.UseCompatibleTextRendering; }
            set { il_InfoForm.Label.UseCompatibleTextRendering = value; }
        }

        /// <summary>
        /// Gets or sets the color of the text displayed in the infolabel (default: SystemColors.InfoText).
        /// </summary>
        public Color ForeColor
        {
            get { return il_InfoForm.Label.ForeColor; }
            set
            {
                if (value != il_InfoForm.ForeColor)
                {
                    try
                    {
                        il_InfoForm.ForeColor = value;
                        if (il_InfoForm.Visible) il_InfoForm.Invalidate();
                    }
                    catch { /* ignore */ }
                }
            }
        }

        /// <summary>
        /// Gets or sets the background color of the infolabel (see also BackBrush - default: SystemColors.Info).
        /// </summary>
        public Color BackColor
        {
            get { return il_InfoForm.BackColor; }
            set
            {
                if (value == Color.Transparent)
                {
                    if (!il_InfoForm.Transparent)
                    {
                        il_OldBackColor = il_InfoForm.BackColor;
                        if (il_InfoForm.BackMode == InfoForm.BackgroundMode.Image)
                        {
                            il_OldImage = il_InfoForm.BackgroundImage;
                            il_InfoForm.BackgroundImage = null;
                        }
                        il_InfoForm.TransparencyKey = il_InfoForm.BackColor;
                        il_InfoForm.Transparent = true;
                    }
                }
                else
                {
                    try
                    {
                        il_InfoForm.BackColor = value;
                        if (il_InfoForm.Transparent)
                        {
                            il_InfoForm.TransparencyKey = value;
                        }
                        else
                        {
                            if (il_InfoForm.BackMode == InfoForm.BackgroundMode.Image)
                            {
                                il_InfoForm.BackgroundImage = null;
                                il_OldImage = null;
                            }
                            else if (il_InfoForm.BackMode == InfoForm.BackgroundMode.Brush)
                            {
                                il_InfoForm.BackBrush.Dispose();
                                il_InfoForm.BackBrush = null;
                            }
                            il_InfoForm.BackMode = InfoForm.BackgroundMode.Solid;
                        }
                    }
                    catch { /* ignore */ }
                }
                if (il_InfoForm.Visible) il_InfoForm.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the brush that is used to fill the background of the infolabel (see also BackColor).
        /// </summary>
        public Brush BackBrush
        {
            get { return il_InfoForm.BackBrush; }
            set
            {
                if (value != null)
                {
                    if (il_InfoForm.BackBrush != null) il_InfoForm.BackBrush.Dispose();
                    try
                    {
                        if (il_InfoForm.BackMode == InfoForm.BackgroundMode.Image)
                        {
                            il_InfoForm.BackgroundImage = null;
                            il_OldImage = null;
                        }
                        il_InfoForm.BackBrush = value;
                        il_InfoForm.BackMode = InfoForm.BackgroundMode.Brush;
                    }
                    catch
                    {
                        il_InfoForm.BackBrush = null;
                        il_InfoForm.BackMode = InfoForm.BackgroundMode.Solid;
                    }

                    if (!il_InfoForm.Transparent && il_InfoForm.Visible) il_InfoForm.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets the color of the border of the infolabel (see also BorderBrush - default: SystemColors.WindowFrame).
        /// </summary>
        public Color BorderColor
        {
            get { return il_BorderColor; }

            set
            {
                if (il_InfoForm.BorderBrush != null) il_InfoForm.BorderBrush.Dispose();

                il_BorderColor = value;
                il_InfoForm.BorderBrush = new SolidBrush(value);
                if (il_InfoForm.Visible) il_InfoForm.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the brush that is used to fill the border of the infolabel (see also BorderColor).
        /// </summary>
        public Brush BorderBrush
        {
            get { return il_InfoForm.BorderBrush; }
            set
            {
                if (value != null)
                {
                    if (il_InfoForm.BorderBrush != null) il_InfoForm.BorderBrush.Dispose();
                    il_InfoForm.BorderBrush = value;
                    if (il_InfoForm.Visible) il_InfoForm.Invalidate();
                }
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the corners of the infolabel are rounded (default: false).
        /// </summary>
        public bool RoundedCorners
        {
            get { return il_InfoForm.RoundCorners; }
            set
            {
                if (value != il_InfoForm.RoundCorners)
                {
                    il_InfoForm.RoundCorners = value;
                    SetBorder();
                }
            }
        }

        /// <summary>
        /// Gets or sets the thickness of each side of the border of the infolabel (when set, use -1 to keep an existing value - default all: 1).
        /// </summary>
        public Padding BorderThickness
        {
            get { return il_BorderThickness; }
            set
            {
                if (value.Left > IL_BORDER_THICKNESS_MAXIMUM) value.Left = IL_BORDER_THICKNESS_MAXIMUM;
                else if (value.Left < 0) value.Left = il_BorderThickness.Left;

                if (value.Top > IL_BORDER_THICKNESS_MAXIMUM) value.Top = IL_BORDER_THICKNESS_MAXIMUM;
                else if (value.Top < 0) value.Top = il_BorderThickness.Top;

                if (value.Right > IL_BORDER_THICKNESS_MAXIMUM) value.Right = IL_BORDER_THICKNESS_MAXIMUM;
                else if (value.Right < 0) value.Right = il_BorderThickness.Right;

                if (value.Bottom > IL_BORDER_THICKNESS_MAXIMUM) value.Bottom = IL_BORDER_THICKNESS_MAXIMUM;
                else if (value.Bottom < 0) value.Bottom = il_BorderThickness.Bottom;

                if (value != il_BorderThickness)
                {
                    il_BorderThickness = value;
                    il_BorderThicknessWidth = value.Left + value.Right;
                    il_BorderThicknessHeight = value.Top + value.Bottom;

                    il_InfoForm.Label.Location = new Point(value.Left + il_TextMargins.Left, value.Top + il_TextMargins.Top);
                    SetSize();
                }
            }
        }

        /// <summary>
        /// Gets or sets the background image displayed in the infolabel.
        /// </summary>
        public Image BackImage
        {
            get { return il_InfoForm.BackgroundImage; }
            set
            {
                il_InfoForm.BackgroundImage = value;
                if (value == null)
                {
                    il_OldImage = null;
                    il_InfoForm.BackMode = InfoForm.BackgroundMode.Solid;
                }
                else
                {
                    il_InfoForm.BackMode = InfoForm.BackgroundMode.Image;
                }

                if (il_InfoForm.Visible) il_InfoForm.Invalidate();
            }
        }

        /// <summary>
        /// Gets or sets the background image layout of the infolabel (default: Stretch).
        /// </summary>
        public ImageLayout BackImageLayout
        {
            get { return il_InfoForm.BackgroundImageLayout; }
            set
            {
                il_InfoForm.BackgroundImageLayout = value;
                if (il_InfoForm.Visible && il_InfoForm.BackMode == InfoForm.BackgroundMode.Image) il_InfoForm.Invalidate();
            }

        }

        #endregion


        // ******************************** Info Label - Duration / Alignment / FadeOut Speed / Opacity / Transparent

        #region Info Label - Duration / Alignment / FadeOut Speed / Opacity / Transparent

        /// <summary>
        /// Gets or sets the duration, in milliseconds, to display the infolabel, that is used when not specified in the show method - default value: 1000.
        /// </summary>
        public int Duration
        {
            get { return il_Duration; }
            set
            {
                if (value < IL_DURATION_MINIMUM) value = IL_DURATION_MINIMUM;
                il_Duration = value;
            }
        }

        /// <summary>
        /// Gets or sets the infolabel alignment that is used when not specified in the show method - default value: TopCenter.
        /// </summary>
        public ContentAlignment Align
        {
            get { return il_Alignment; }
            set { il_Alignment = value; }
        }

        /// <summary>
        /// Gets of sets the alignment location offset, in pixels, of the infolabel - default values: x = 0, y = 0.
        /// </summary>
        public Point AlignOffset
        {
            get { return new Point (il_OffsetX, il_OffsetY); }
            set
            {
                il_OffsetX = value.X; // any value
                il_OffsetY = value.Y;
            }
        }

        /// <summary>
        /// Gets or sets the speed of the fade out effect of the infolabel, values 1 (slow) to 100 (fast) - default value: 20.
        /// </summary>
        public int FadeOutSpeed
        {
            get { return (int)(il_FadeOutValue * 100); }
            set
            {
                if (value < 1) value = 1;
                else if (value > 100) value = 100;
                il_FadeOutValue = value * 0.01;
            }
        }

        /// <summary>
        /// Gets or sets the opacity level of the infolabel, values 0.0 (full transparency) to 1.0 (no transparency) - default value: 1.0.
        /// </summary>
        public double Opacity
        {
            get { return il_Opacity; }
            set
            {
                if (value < 0.0) value = 0.0;
                else if (value > 1.0) value = 1.0;
                il_Opacity = value;
                if (!il_Fading) il_InfoForm.Opacity = value;
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates whether the background of the infolabel is transparent - default value: false.
        /// </summary>
        public bool Transparent
        {
            get { return il_InfoForm.Transparent; }
            set
            {
                if (value != il_InfoForm.Transparent)
                {
                    il_InfoForm.Transparent = value;
                    if (value)
                    {
                        il_OldBackColor = il_InfoForm.BackColor;
                        if (il_InfoForm.BackMode == InfoForm.BackgroundMode.Image)
                        {
                            il_OldImage = il_InfoForm.BackgroundImage;
                            il_InfoForm.BackgroundImage = null;
                        }
                        il_InfoForm.TransparencyKey = il_InfoForm.BackColor;
                    }
                    else
                    {
                        il_InfoForm.BackColor = il_OldBackColor;
                        if (il_InfoForm.BackMode == InfoForm.BackgroundMode.Image)
                        {
                            il_InfoForm.BackgroundImage = il_OldImage;
                            il_OldImage = null;
                        }
                        il_InfoForm.TransparencyKey = Color.Empty;
                    }
                    if (il_InfoForm.Visible) il_InfoForm.Invalidate();
                }
            }
        }

        #endregion

    }
}

