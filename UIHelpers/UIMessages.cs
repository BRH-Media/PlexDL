using System.Windows.Forms;

// ReSharper disable InvertIf
// ReSharper disable InconsistentNaming

namespace UIHelpers
{
    public static class UIMessages
    {
        public static void ThreadSafeMessage(string msg, string title = @"Message", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.Information)
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
                    form.BeginInvoke((MethodInvoker)delegate
                    {
                        MessageBox.Show(msg, title, buttons, icon);
                    });
                }
                else
                    MessageBox.Show(msg, title, buttons, icon);
            }
        }

        public static void Info(string msg, string title = @"Message")
        {
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Error(string msg, string title = @"Error")
        {
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void Warning(string msg, string title = @"Warning")
        {
            MessageBox.Show(msg, title, MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        public static bool Question(string msg, string title = @"Question")
        {
            return MessageBox.Show(msg, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }
    }
}