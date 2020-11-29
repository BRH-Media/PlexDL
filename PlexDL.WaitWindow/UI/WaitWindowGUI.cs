using System;
using System.Windows.Forms;

// ReSharper disable InconsistentNaming

namespace PlexDL.WaitWindow.UI
{
    /// <summary>
    ///     The dialogue displayed by a WaitWindow instance.
    /// </summary>
    public partial class WaitWindowGUI
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

        // Counts the amount of dots appended for the animation (max 3)
        private int _dotCount;

        // The text to display while working
        private string _labelText = @"Processing";

        // The delegate responsible for offloading the work
        private delegate T FunctionInvoker<out T>();

        public WaitWindowGUI(WaitWindow parent)
        {
            //
            // The InitializeComponent() call is required for Windows Forms designer support.
            //
            InitializeComponent();

            // The calling parent handler
            _parent = parent;

            // Position the window in the top right of the main screen.
            Top = Screen.PrimaryScreen.WorkingArea.Top + 32;
            Left = Screen.PrimaryScreen.WorkingArea.Right - Width - 32;
        }

        internal void SetMessage(string message)
        {
            // Set GUI label working text
            MessageLabel.Text = message;

            // Set internal working text variable
            _labelText = message;
        }

        internal void Cancel()
        {
            // Invoke form Close() on the GUI thread
            Invoke(new MethodInvoker(Close), null);
        }

        protected override void OnShown(EventArgs e)
        {
            // Execute default OnShown event handler
            base.OnShown(e);

            // Create Delegate
            var threadController = new FunctionInvoker<object>(DoWork);

            // Execute on secondary thread.
            threadController.BeginInvoke(WorkComplete, threadController);
        }

        private object DoWork()
        {
            // Invoke the worker method and return the result.
            var e = new WaitWindowEventArgs(_parent, _parent.Args);
            _parent.WorkerMethod?.Invoke(this, e);

            // Return the worker result
            return e.Result;
        }

        private void WorkComplete(IAsyncResult results)
        {
            // Don't try and finish up if we're already done
            if (IsDisposed)
                return;

            // Check if a thread invoke is required before performing any actions
            if (InvokeRequired)
                // Invoke this function on another thread
                Invoke(new WaitWindow.MethodInvoker<IAsyncResult>(WorkComplete), results);
            else
            {
                // Capture the result
                try
                {
                    Result = ((FunctionInvoker<object>)results.AsyncState).EndInvoke(results);
                }
                catch (Exception ex)
                {
                    // Grab the Exception for rethrowing after the WaitWindow has closed.
                    Error = ex;
                }

                // When the work is done, we can close the form
                Close();
            }
        }

        private void WaitWindowGUI_Load(object sender, EventArgs e)
        {
            // Dot counter is reset
            _dotCount = 0;

            // Dot animation timer is started
            tmrDots.Start();
        }

        private void WaitWindowGUI_FormClosing(object sender, FormClosingEventArgs e)
        {
            // Dot animation timer is stopped
            tmrDots.Stop();
        }

        private void TmrDots_Tick(object sender, EventArgs e)
        {
            // Check dot count (max 3)
            if (_dotCount < 3)
            {
                // Add new dot to counter
                _dotCount++;

                // Add new dot to working text
                MessageLabel.Text += '.';
            }
            else
            {
                // Reset dot counter
                _dotCount = 0;

                // Remove all dots from working text
                MessageLabel.Text = _labelText;
            }
        }
    }
}