/*
 * Created by SharpDevelop.
 * User: mjackson
 * Date: 05/03/2010
 * Time: 09:36
 *
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace PlexDL.WaitWindow
{
    /// <summary>
    /// Displays a window telling the user to wait while a process is executing.
    /// </summary>
    public sealed class WaitWindow
    {
        /// <summary>
        /// Shows a wait window with the text 'Please wait...' while executing the passed method.
        /// </summary>
        /// <param name="workerMethod">Pointer to the method to execute while displaying the wait window.</param>
        /// <returns>The result argument from the worker method.</returns>
        public static object Show(EventHandler<WaitWindowEventArgs> workerMethod)
        {
            return Show(workerMethod, null);
        }

        /// <summary>
        /// Shows a wait window with the specified text while executing the passed method.
        /// </summary>
        /// <param name="workerMethod">Pointer to the method to execute while displaying the wait window.</param>
        /// <param name="message">The text to display.</param>
        /// <returns>The result argument from the worker method.</returns>
        public static object Show(EventHandler<WaitWindowEventArgs> workerMethod, string message)
        {
            var instance = new WaitWindow();
            return instance.Show(workerMethod, message, new List<object>());
        }

        /// <summary>
        /// Shows a wait window with the specified text while executing the passed method.
        /// </summary>
        /// <param name="workerMethod">Pointer to the method to execute while displaying the wait window.</param>
        /// <param name="message">The text to display.</param>
        /// <param name="args">Arguments to pass to the worker method.</param>
        /// <returns>The result argument from the worker method.</returns>
        public static object Show(EventHandler<WaitWindowEventArgs> workerMethod, string message, params object[] args)
        {
            var arguments = new List<object>();
            arguments.AddRange(args);

            var instance = new WaitWindow();
            return instance.Show(workerMethod, message, arguments);
        }

        #region Instance implementation

        private WaitWindow()
        {
        }

        private WaitWindowGUI _GUI;

        internal delegate void MethodInvoker<T>(T parameter1);

        internal EventHandler<WaitWindowEventArgs> _WorkerMethod;
        internal List<object> _Args;

        /// <summary>
        /// Updates the message displayed in the wait window.
        /// </summary>
        public string Message
        {
            set => _GUI.Invoke(new MethodInvoker<string>(_GUI.SetMessage), value);
        }

        /// <summary>
        /// Cancels the work and exits the wait windows immediately
        /// </summary>
        public void Cancel()
        {
            _GUI.Invoke(new MethodInvoker(_GUI.Cancel), null);
        }

        private object Show(EventHandler<WaitWindowEventArgs> workerMethod, string message, List<object> args)
        {
            //	Validate Parameters
            if (workerMethod == null)
                throw new ArgumentException(@"No worker method has been specified.", "workerMethod");
            _WorkerMethod = workerMethod;

            _Args = args;

            if (string.IsNullOrEmpty(message))
                message = "Please wait...";

            //	Set up the window
            _GUI = new WaitWindowGUI(this);
            _GUI.SetMessage(message);

            //	Call it
            _GUI.ShowDialog();

            var result = _GUI._Result;

            //	clean up
            var _Error = _GUI._Error;
            _GUI.Dispose();

            //	Return result or throw and exception
            if (_Error != null)
                throw _Error;
            return result;
        }

        #endregion Instance implementation
    }
}