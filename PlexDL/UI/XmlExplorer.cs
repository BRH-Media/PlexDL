using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace PlexDL.UI
{
    public partial class XmlExplorer : Form
    {
        public XmlDocument Xml { get; set; }

        public XmlExplorer()
        {
            InitializeComponent();
        }

        private void XmlExplorer_Load(object sender, EventArgs e)
        {
            if (Xml != null)
            {
                var t = NiceXml(Xml);

                txtXml.Clear();
                txtXml.Text = t;
            }
        }

        private void BtnOK_Click(object sender, EventArgs e)
        {
            Close();
        }

        public static void ShowXmlExplorer(XmlDocument doc)
        {
            var frm = new XmlExplorer
            {
                Xml = doc
            };
            frm.ShowDialog();
        }

        private static string NiceXml(XmlNode xml)
        {
            var result = "";

            var mStream = new MemoryStream();
            var writer = new XmlTextWriter(mStream, Encoding.Unicode);

            try
            {
                writer.Formatting = Formatting.Indented;

                // Write the XML into a formatting XmlTextWriter
                xml.WriteContentTo(writer);
                writer.Flush();
                mStream.Flush();

                // Have to rewind the MemoryStream in order to read
                // its contents.
                mStream.Position = 0;

                // Read MemoryStream contents into a StreamReader.
                var sReader = new StreamReader(mStream);

                // Extract the text from the StreamReader.
                var formattedXml = sReader.ReadToEnd();

                result = formattedXml;
            }
            catch (XmlException)
            {
                // Handle the exception
            }

            mStream.Close();
            writer.Close();

            return result;
        }
    }
}