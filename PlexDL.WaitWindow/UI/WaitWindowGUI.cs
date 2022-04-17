using PlexDL.Common.Components.Forms;
using System;
using System.Windows.Forms;

// ReSharper disable InconsistentNaming

namespace PlexDL.WaitWindow.UI
{
    public class WaitWindowGUI : DoubleBufferedForm
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
        internal readonly WaitWindow _parent;

        // The text to display while working
        internal string _labelText = @"Processing";

        // The delegate responsible for offloading the work
        internal delegate T FunctionInvoker<out T>();

        public WaitWindowGUI()
        {
            // In order for the designer to work, this needs to be here unfortunately.
        }

        public WaitWindowGUI(WaitWindow parent)
        {
            // Set the parent controller
            _parent = parent;

            // Position the window in the top right of the main screen.
            Top = Screen.PrimaryScreen.WorkingArea.Top + 32;
            Left = Screen.PrimaryScreen.WorkingArea.Right - Width - 32;
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

        public void SetMessage(string message)
        {
            // Set global
            _labelText = message;
        }

        public void Cancel()
        {
            // Invoke form Close() on the GUI thread
            Invoke(new MethodInvoker(Close), null);
        }

        public object DoWork()
        {
            // Invoke the worker method and return the result.
            var e = new WaitWindowEventArgs(_parent, _parent.Args);
            _parent.WorkerMethod?.Invoke(this, e);

            // Return the worker result
            return e.Result;
        }

        public void WorkComplete(IAsyncResult results)
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
    }
}