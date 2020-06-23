using System.Windows.Forms;

namespace UIHelpers
{
    public static class UIMessages
    {
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