﻿using System;
using System.Windows.Forms;

namespace PlexDL.Common
{
    public static class UnhandledExceptionHandler
    {
        public static void Handler(object sender, UnhandledExceptionEventArgs e)
        {
            Exception obj = (Exception)e.ExceptionObject;
            MessageBox.Show("An unhandled exception has occurred. Please report this issue on GitHub, including any relevant logs or other information.\n\n" +
                obj.ToString(), "Unhandled Exception", MessageBoxButtons.OK, MessageBoxIcon.Error);
            PlexDL.UI.Home.recordException(obj.Message, "UnhandledException");
        }
    }
}