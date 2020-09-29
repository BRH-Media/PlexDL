using PlexDL.PlexAPI;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using PlexDL.MyPlex;

namespace PlexDL.Common.Renderers.DGVRenderers
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
                    dgvBind.Rows.Add(r1.name, r1.address, r1.port.ToString(), r1.accessToken);
                else
                    dgvBind.Rows.Add(r1.name, r1.address, r1.port.ToString());

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