using PlexDL.Common.Components.Forms;
using PlexDL.Common.Enums;
using PlexDL.Common.Globals;
using PlexDL.Common.Security;
using System;
using System.ComponentModel;
using System.IO;
using System.Reflection;

// ReSharper disable SwitchStatementHandlesSomeKnownEnumValuesWithDefault

namespace PlexDL.UI.Forms
{
    internal partial class About : DoubleBufferedForm
    {
        public About()
        {
            InitializeComponent();

            var title = $"About {AssemblyTitle}";

            lblProductName.Text = AssemblyProduct;
            lblVersion.Text = $@"Version {AssemblyVersion}";
            lblCopyright.Text = AssemblyCopyright;
            lblCompanyName.Text = AssemblyCompany;
            lblGlobalGuid.Text = $"GUID: {GlobalGuid}";
            txtDescription.Text = AssemblyDescription;

            switch (BuildState.State)
            {
                case DevStatus.InDevelopment:
                    Text = $"{title} - Developer Build";
                    break;

                case DevStatus.InBeta:
                    Text = $"{title} - Beta Testing Build";
                    break;

                case DevStatus.ProductionReady:
                    Text = title;
                    break;
            }
        }

        [Localizable(false)]
        public sealed override string Text
        {
            get => base.Text;
            set => base.Text = value;
        }

        public string AssemblyTitle
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly()
                    .GetCustomAttributes(typeof(AssemblyTitleAttribute), false);

                if (attributes.Length <= 0)
                    return Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);

                var titleAttribute = (AssemblyTitleAttribute)attributes[0];

                return titleAttribute.Title != ""
                    ? titleAttribute.Title
                    : Path.GetFileNameWithoutExtension(Assembly.GetExecutingAssembly().CodeBase);
            }
        }

        public string AssemblyVersion => Assembly.GetExecutingAssembly().GetName().Version.ToString();

        public string AssemblyDescription
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly()
                    .GetCustomAttributes(typeof(AssemblyDescriptionAttribute), false);
                return attributes.Length == 0 ? "" : ((AssemblyDescriptionAttribute)attributes[0]).Description;
            }
        }

        public string AssemblyProduct
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly()
                    .GetCustomAttributes(typeof(AssemblyProductAttribute), false);
                return attributes.Length == 0 ? "" : ((AssemblyProductAttribute)attributes[0]).Product;
            }
        }

        public string AssemblyCopyright
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly()
                    .GetCustomAttributes(typeof(AssemblyCopyrightAttribute), false);
                return attributes.Length == 0 ? "" : ((AssemblyCopyrightAttribute)attributes[0]).Copyright;
            }
        }

        public string AssemblyCompany
        {
            get
            {
                var attributes = Assembly.GetExecutingAssembly()
                    .GetCustomAttributes(typeof(AssemblyCompanyAttribute), false);
                return attributes.Length == 0 ? "" : ((AssemblyCompanyAttribute)attributes[0]).Company;
            }
        }

        public string GlobalGuid => GuidHandler.GetGlobalGuid().ToString();

        private void About_Load(object sender, EventArgs e)
        {
        }
    }
}