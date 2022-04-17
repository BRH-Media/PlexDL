using System;
using System.Windows.Forms;

// ReSharper disable InvertIf
// ReSharper disable InconsistentNaming

namespace UIHelpers
{
    public static class UIMessages
    {
        public static DialogResult ThreadSafeMessage(string msg, string title = @"Message", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.Information)
        {
            //all currently open Windows Forms (will not work on startup due to this)
            var openForms = Application.OpenForms;

            //make sure that there is a form to invoke first
            if (openForms.Count > 0)
            {
                //the first form will be 'Home'
                var form = openForms[0];

                //thread-safe logic
                if (form.InvokeRequired)
                {
                    //invoke and return result
                    return (DialogResult)form.Invoke(
                        new Func<DialogResult>(() => MessageBox.Show(msg, title, buttons, icon))
                        );
                }

                //no invoke required
                return MessageBox.Show(msg, title, buttons, icon);
            }

            //default
            return DialogResult.None;
        }

        public static void Info(string msg, string title = @"Message")
            => ThreadSafeMessage(msg, title);

        public static void Error(string msg, string title = @"Error")
            => ThreadSafeMessage(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error);

        public static void Warning(string msg, string title = @"Warning")
            => ThreadSafeMessage(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);

        public static bool Question(string msg, string title = @"Question")
            => ThreadSafeMessage(msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
    }
}