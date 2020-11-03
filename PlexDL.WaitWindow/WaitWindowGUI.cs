using PlexDL.Common.Components;
using System;
using System.ComponentModel;
using System.Windows.Forms;
using PlexDL.Common.Components.Controls;

namespace PlexDL.WaitWindow
{
    /// <summary>
    ///     The dialogue displayed by a WaitWindow instance.
    /// </summary>
    public partial class WaitWindowGui
    {
        private readonly WaitWindow _parent;
        internal Exception Error;

        private int _dotCount;

        internal object Result;

        private IContainer components;

        private string _labelText = "";

        public Label MessageLabel;
        private CircularProgressBar _progressMain;
        private Timer _tmrDots;

        public WaitWindowGui(WaitWindow parent)
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();

            _parent = parent;

            //	Position the window in the top right of the main screen.
            Top = Screen.PrimaryScreen.WorkingArea.Top + 32;
            Left = Screen.PrimaryScreen.WorkingArea.Right - Width - 32;
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            //   Create Delegate
            var threadController = new FunctionInvoker<object>(DoWork);

            //   Execute on secondary thread.
            threadController.BeginInvoke(WorkComplete, threadController);
        }

        internal object DoWork()
        {
            //	Invoke the worker method and return any results.
            var e = new WaitWindowEventArgs(_parent, _parent._Args);
            _parent._WorkerMethod?.Invoke(this, e);

            return e.Result;
        }

        private void WorkComplete(IAsyncResult results)
        {
            //don't try and finish up if we're already done
            if (IsDisposed) return;

            if (InvokeRequired)
            {
                Invoke(new WaitWindow.MethodInvoker<IAsyncResult>(WorkComplete), results);
            }
            else
            {
                //	Capture the result
                try
                {
                    Result = ((FunctionInvoker<object>)results.AsyncState).EndInvoke(results);
                }
                catch (Exception ex)
                {
                    //	Grab the Exception for rethrowing after the WaitWindow has closed.
                    Error = ex;
                }

                Close();
            }
        }

        internal void SetMessage(string message)
        {
            MessageLabel.Text = message;
            _labelText = message;
        }

        internal void Cancel()
        {
            Invoke(new MethodInvoker(Close), null);
        }

        private void WaitWindowGUI_Load(object sender, EventArgs e)
        {
            _dotCount = 0;
            _tmrDots.Start();
        }

        private void WaitWindowGUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            _tmrDots.Stop();
        }

        private void TmrDots_Tick(object sender, EventArgs e)
        {
            if (_dotCount < 3)
            {
                _dotCount++;
                MessageLabel.Text += '.';
            }
            else
            {
                _dotCount = 0;
                MessageLabel.Text = _labelText;
            }
        }

        private delegate T FunctionInvoker<T>();
    }
}