/****************************************************************

    PVS.MediaPlayer - Version 0.98.1
    December 2019, The Netherlands
    © Copyright 2019 PVS The Netherlands - licensed under The Code Project Open License (CPOL)

    PVS.MediaPlayer uses (part of) the Media Foundation .NET library by nowinskie and snarfle (https://sourceforge.net/projects/mfnet).
    Licensed under either Lesser General Public License v2.1 or BSD.  See license.txt or BSDL.txt for details (http://mfnet.sourceforge.net).

    ****************

    For use with Microsoft Windows 7 or higher, Microsoft .NET Framework version 2.0 or higher and WinForms any CPU.
    Created with Microsoft Visual Studio.

    Article on CodeProject with information on the use of the PVS.MediaPlayer library:
    https://www.codeproject.com/Articles/109714/PVS-MediaPlayer-Audio-and-Video-Player-Library

    ****************

    The PVS.MediaPlayer library source code is divided into 8 files:

    1. Player.cs        - main source code
    2. SubClasses.cs    - various grouping and information classes
    3. Interop.cs       - unmanaged Win32 functions
    4. PeakMeter.cs     - audio peak level values
    5. DisplayClones.cs - multiple video displays
    6. CursorHide.cs    - hides the mouse cursor during inactivity
    7. Subtitles.cs     - subrip (.srt) subtitles
    8. Infolabel.cs     - custom ToolTip

    Required references:
    System
    System.Drawing
    System.Windows.Forms

    ****************

    This file: CursorHide.cs

    Player Class
    Extension to file 'Player.cs'.

    ****************

    Many thanks to Microsoft (Windows, .NET Framework, Visual Studio and others), all the people
    writing about programming on the internet (a great source for ideas and solving problems),
    the websites publishing those or other writings about programming, the people responding to the
    PVS.MediaPlayer articles with comments and suggestions and, of course, the people at CodeProject.

    Special thanks to the creators of Media Foundation .NET for their great library.

    Special thanks to Sean Ewington and Deeksha Shenoy of CodeProject who also took care of publishing the many
    code updates and changes in the PVS.MediaPlayer articles in a friendly, fast, and highly competent manner.

    Peter Vegter
    December 2019, The Netherlands

    ****************************************************************/

#region Usings

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

#endregion Usings

namespace PVS.MediaPlayer
{
    public partial class Player
    {
        /*
            Automatically hides (after not having moved for a few seconds) and unhides (when moved or clicked) the mouse cursor when playing media.
            A MessageFilter is used to detect mouse events.
        */

        // ******************************** Cursor Hide - Item Class / MessageFilter

        #region Cursor Hide - Item Class / MessageFilter

        private sealed class CH_Item
        {
            internal Form _form;
            internal Player _player;

            internal CH_Item(Form form, Player player)
            {
                _form = form;
                _player = player;
            }
        }

        private sealed class CH_MessageFilter : IMessageFilter
        {
            private const int WM_MOUSEMOVE = 0x0200;
            private const int WM_MOUSEWHEEL = 0x020A;

            public bool PreFilterMessage(ref Message m)
            {
                if (ch_Disabled || ch_Busy) return false;

                if (m.Msg == WM_MOUSEMOVE)
                {
                    ch_Moved = DateTime.Now;
                    if (ch_Hidden)
                    {
                        Point _cursorHideNewPosition = Cursor.Position;
                        if (Math.Abs(_cursorHideNewPosition.X - ch_OldPosition.X) >= 2 || Math.Abs(_cursorHideNewPosition.Y - ch_OldPosition.Y) >= 2)
                        {
                            Cursor.Show();
                            ch_Hidden = false;
                            ch_Timer.Start();

                            //if (_mediaCursorHideChanged != null)
                            //{
                            //    ch_EventArgs._reason = CursorChangedReason.MouseMoved;
                            //    CH_RaiseEvent(Form.ActiveForm);
                            //}
                        }
                        else ch_OldPosition = _cursorHideNewPosition;
                    }
                }
                else if (m.Msg > WM_MOUSEMOVE && m.Msg <= WM_MOUSEWHEEL)
                {
                    ch_Moved = DateTime.Now;
                    if (ch_Hidden)
                    {
                        Cursor.Show();
                        ch_Hidden = false;
                        ch_Timer.Start();

                        //if (_mediaCursorHideChanged != null)
                        //{
                        //    ch_EventArgs._reason = CursorChangedReason.MouseAction;
                        //    CH_RaiseEvent(Form.ActiveForm);
                        //}
                    }
                }
                return false;
            }
        }

        #endregion Cursor Hide - Item Class / MessageFilter

        // ******************************** Cursor Hide - Fields

        #region Cursor Hide - Fields

        private const int CH_DEFAULT_HIDE_DELAY_SEC = 3;

        internal static Timer ch_Timer;
        private static bool ch_Busy;
        internal static int ch_Delay = CH_DEFAULT_HIDE_DELAY_SEC; // default hide delay seconds
        private static DateTime ch_Moved;
        private static Point ch_OldPosition;
        private static bool ch_Active;
        internal static bool ch_Disabled;
        private static int ch_DisabledCount;
        internal static bool ch_Hidden;
        private static CH_MessageFilter ch_MessageFilter;
        private static List<CH_Item> ch_Items;
        private static bool ch_ModalThread;

        #endregion Cursor Hide - Fields

        // ******************************** Cursor Hide - Main

        #region Cursor Hide - Main

        /// <summary>
        /// Provides access to the cursor hide settings of the player (for example Player.CursorHide.Add).
        /// </summary>
        public CursorHide CursorHide
        {
            get
            {
                if (_cursorHideClass == null) _cursorHideClass = new CursorHide(this);
                return _cursorHideClass;
            }
        }

        #endregion Cursor Hide - Main

        // ******************************** Cursor Hide - Add / Remove / Has Items / Form Closed Handler

        #region Cursor Hide - Add / Remove / Has Items / Form Closed Handler

        internal static HResult CH_AddItem(Form form, Player player) //, Control display)
        {
            HResult result = NO_ERROR;

            if (form != null)
            {
                int index = -1;
                if (ch_Items == null)
                {
                    ch_Items = new List<CH_Item>(8);
                }
                else
                {
                    int count = ch_Items.Count;
                    for (int i = 0; i < count; i++)
                    {
                        if (ch_Items[i]._form == form && ch_Items[i]._player == player)
                        {
                            index = i;
                            break;
                        }
                    }
                }

                if (index == -1)
                {
                    CH_Item item = new CH_Item(form, player);
                    ch_Items.Add(item);
                    form.FormClosed += CH_FormClosedHandler;

                    if (!ch_Active)
                    {
                        CH_Start();
                    }
                }
            }
            else result = HResult.S_FALSE;
            return result;
        }

        internal static HResult CH_RemoveItem(Form form, Player player) //, Control display)
        {
            HResult result = NO_ERROR;

            if (ch_Items != null)
            {
                int count = ch_Items.Count;
                for (int i = 0; i < count; i++)
                {
                    if (ch_Items[i]._form == form && ch_Items[i]._player == player)
                    {
                        ch_Items[i]._form.FormClosed -= CH_FormClosedHandler;
                        ch_Items.RemoveAt(i);
                        if (ch_Active && ch_Items.Count <= 0)
                        {
                            CH_Stop();
                        }
                        break;
                    }
                }
            }
            else result = HResult.S_FALSE;
            return result;
        }

        internal static void CH_RemovePlayerItems(Player player)
        {
            if (player != null)
            {
                if (ch_Items != null)
                {
                    for (int i = ch_Items.Count - 1; i >= 0; --i)
                    {
                        if (ch_Items[i]._player == player)
                        {
                            ch_Items[i]._form.FormClosed -= CH_FormClosedHandler;
                            ch_Items.RemoveAt(i);
                        }
                    }

                    if (ch_Active && ch_Items.Count <= 0)
                    {
                        CH_Stop();
                    }
                }
            }
        }

        internal static bool CH_HasItems(Player player)
        {
            bool result = false;

            if (player != null)
            {
                if (ch_Items != null)
                {
                    for (int index = ch_Items.Count - 1; index >= 0; --index)
                    {
                        if (ch_Items[index]._player == player)
                        {
                            result = true;
                            break;
                        }
                    }
                }
            }
            return result;
        }

        private static void CH_FormClosedHandler(object sender, FormClosedEventArgs e)
        {
            if (ch_Items != null)
            {
                Form form = (Form)sender;

                for (int i = ch_Items.Count - 1; i >= 0; --i)
                {
                    if (ch_Items[i]._form == form)
                    {
                        form.FormClosed -= CH_FormClosedHandler;
                        ch_Items.RemoveAt(i);
                    }
                }

                if (ch_Active && ch_Items.Count <= 0)
                {
                    CH_Stop();
                }
            }
        }

        #endregion Cursor Hide - Add / Remove / Has Items / Form Closed Handler

        // ******************************** Cursor Hide - Show Cursor / Hide Cursor (Forced)

        #region Cursor Hide - Show Cursor / Hide Cursor (Forced)

        internal static void CH_ShowCursor()
        {
            if (ch_Active && ch_Hidden)
            {
                Cursor.Show();
                ch_Hidden = false;
                ch_Moved = DateTime.Now;
                ch_Timer.Start();
            }
        }

        internal static void CH_HideCursor()
        {
            if (ch_Active && !ch_Hidden)
            {
                ch_Timer.Stop();
                ch_OldPosition = Cursor.Position;
                Cursor.Hide();
                ch_Hidden = true;
            }
        }

        #endregion Cursor Hide - Show Cursor / Hide Cursor (Forced)

        // ******************************** Cursor Hide - Start / Stop / Disable / Modal Thread

        #region Cursor Hide - Start / Stop / Disable / Modal Thread

        private static void CH_Start()
        {
            if (!ch_Active)
            {
                if (ch_Timer == null)
                {
                    ch_Timer = new Timer();
                    ch_Timer.Interval = ch_Delay == 1 ? 500 : 100; // _cursorHideDelay * 500;
                    ch_Timer.Tick += CH_TimerTick;

                    ch_MessageFilter = new CH_MessageFilter();
                }

                ch_Moved = DateTime.Now;
                ch_Timer.Start();
                Application.AddMessageFilter(ch_MessageFilter);

                Application.EnterThreadModal += CH_EnterThreadModal;
                Application.LeaveThreadModal += CH_LeaveThreadModal;

                ch_Active = true;
            }
        }

        private static void CH_Stop()
        {
            if (ch_Active)
            {
                ch_Timer.Dispose();
                ch_Timer = null;

                Application.RemoveMessageFilter(ch_MessageFilter);
                ch_MessageFilter = null;

                ch_Items.Clear();
                ch_Items = null;

                ch_Disabled = false;
                ch_DisabledCount = 0;

                Application.EnterThreadModal -= CH_EnterThreadModal;
                Application.LeaveThreadModal -= CH_LeaveThreadModal;
                ch_ModalThread = false;

                ch_Active = false;

                if (ch_Hidden)
                {
                    Cursor.Show();
                    ch_Hidden = false;
                }
            }
        }

        internal static bool CH_Disabled
        {
            set
            {
                if (ch_Active)
                {
                    if (value)
                    {
                        ch_DisabledCount++;
                        ch_Disabled = true;

                        if (ch_Hidden)
                        {
                            Cursor.Show();
                            ch_Hidden = false;
                            ch_Timer.Start();

                            //if (_mediaCursorHideChanged != null) CH_RaiseEvent(Form.ActiveForm);
                        }
                    }
                    else
                    {
                        ch_DisabledCount--;
                        if (ch_DisabledCount <= 0)
                        {
                            ch_DisabledCount = 0;
                            ch_Moved = DateTime.Now;
                            ch_Disabled = false;
                        }
                    }
                }
            }
        }

        private static void CH_EnterThreadModal(object sender, EventArgs e)
        {
            if (!ch_ModalThread && !ch_Disabled)
            {
                //ch_EventArgs._reason = CursorChangedReason.ModalDialog; // alt. way to set _reason
                CH_Disabled = true;
                ch_ModalThread = true;
            }
        }

        private static void CH_LeaveThreadModal(object sender, EventArgs e)
        {
            if (ch_ModalThread)
            {
                if (ch_Disabled) CH_Disabled = false;
                ch_ModalThread = false;
            }
        }

        #endregion Cursor Hide - Start / Stop / Disable / Modal Thread

        // ******************************** Cursor Hide - Timer Tick

        #region Cursor Hide - Timer Tick

        private static void CH_TimerTick(object sender, EventArgs e)
        {
            if (ch_Disabled || ch_Busy) return;
            ch_Busy = true;

            if (!ch_Hidden && Form.ActiveForm != null)
            {
                if ((DateTime.Now - ch_Moved).TotalSeconds >= ch_Delay)
                {
                    int count = ch_Items.Count;
                    for (int i = 0; i < count; i++)
                    {
                        if (ch_Items[i]._form == Form.ActiveForm)
                        {
                            if (ch_Items[i]._player.Playing)
                            {
                                ch_Timer.Stop();
                                ch_OldPosition = Cursor.Position;
                                Cursor.Hide();
                                ch_Hidden = true;
                                break;
                            }
                        }
                    }
                }
            }
            ch_Busy = false;
        }

        #endregion Cursor Hide - Timer Tick
    }
}