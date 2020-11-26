using System;
using System.Windows.Forms;

namespace PlexDL.WaitWindow.UI
{
    /// <summary>
    ///     The dialogue displayed by a WaitWindow instance.
    /// </summary>
    public partial class WaitWindowGui
    {
        /// <summary>
        /// The last exception to occur in the WaitWindow
        /// </summary>
        public Exception Error;

        /// <summary>
        /// The thread object return
        /// </summary>
        public object Result;

        //  Internal variables
        private readonly WaitWindow _parent;

        private int _dotCount;
        private string _labelText = @"Processing";

        //the delegate responsible for offloading the work
        private delegate T FunctionInvoker<out T>();

        public WaitWindowGui(WaitWindow parent)
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();

            //  The calling parent handler
            _parent = parent;

            //	Position the window in the top right of the main screen.
            Top = Screen.PrimaryScreen.WorkingArea.Top + 32;
            Left = Screen.PrimaryScreen.WorkingArea.Right - Width - 32;
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

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            //   Create Delegate
            var threadController = new FunctionInvoker<object>(DoWork);

            //   Execute on secondary thread.
            threadController.BeginInvoke(WorkComplete, threadController);
        }

        private object DoWork()
        {
            //	Invoke the worker method and return the result.
            var e = new WaitWindowEventArgs(_parent, _parent._Args);
            _parent._WorkerMethod?.Invoke(this, e);

            return e.Result;
        }

        private void WorkComplete(IAsyncResult results)
        {
            //  Don't try and finish up if we're already done
            if (IsDisposed)
                return;

            if (InvokeRequired)
                Invoke(new WaitWindow.MethodInvoker<IAsyncResult>(WorkComplete), results);
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

                // When the work is done, we can close the form
                Close();
            }
        }

        private void WaitWindowGUI_Load(object sender, EventArgs e)
        {
            _dotCount = 0;
            tmrDots.Start();
        }

        private void WaitWindowGUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            tmrDots.Stop();
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
    }
}