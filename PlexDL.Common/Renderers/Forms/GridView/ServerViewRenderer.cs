using PlexDL.MyPlex;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace PlexDL.Common.Renderers.Forms.GridView
{
    public static class ServerViewRenderer
    {
        public static void RenderView(List<Server> data, bool renderTokenColumn, DataGridView target)
        {
            var name = new DataColumn("Name", typeof(string));
            var address = new DataColumn("Address", typeof(string));
            var port = new DataColumn("Port", typeof(string));
            var token = new DataColumn("Token", typeof(string));

            var dgvBind = new DataTable("Servers");

            dgvBind.Columns.Add(name);
            dgvBind.Columns.Add(address);
            dgvBind.Columns.Add(port);

            if (renderTokenColumn)
                dgvBind.Columns.Add(token);

            foreach (var r1 in data)
                if (renderTokenColumn)
                    dgvBind.Rows.Add(r1.Name, r1.Address, r1.Port.ToString(), r1.AccessToken);
                else
                    dgvBind.Rows.Add(r1.Name, r1.Address, r1.Port.ToString());

            if (target.InvokeRequired)
            {
                target.BeginInvoke((MethodInvoker)delegate
               {
                   target.DataSource = dgvBind;
                   Methods.SortingEnabled(target, false);
                   target.Refresh();
               });
            }
            else
            {
                target.DataSource = dgvBind;
                Methods.SortingEnabled(target, false);
                target.Refresh();
            }
        }
    }
}