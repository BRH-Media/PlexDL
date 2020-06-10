using System;
using System.Collections.Generic;

namespace PlexDL.WaitWindow
{
    /// <summary>
    ///     Provides data for the WaitWindow events.
    /// </summary>
    public class WaitWindowEventArgs : EventArgs
    {
        /// <summary>
        ///     Initialises a new intance of the WaitWindowEventArgs class.
        /// </summary>
        /// <param name="GUI">The associated WaitWindow instance.</param>
        /// <param name="args">A list of arguments to be passed.</param>
        public WaitWindowEventArgs(WaitWindow GUI, List<object> args)
        {
            Window = GUI;
            Arguments = args;
        }

        public WaitWindow Window { get; }

        public List<object> Arguments { get; }

        public object Result { get; set; }
    }
}