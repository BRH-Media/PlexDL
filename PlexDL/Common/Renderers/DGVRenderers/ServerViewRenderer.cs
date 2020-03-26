using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using PlexDL.PlexAPI;

namespace PlexDL.Common.Renderers.DGVRenderers
{
    public static class ServerViewRenderer
    {
        public static void RenderView(List<Server> data, DataGridView target)
        {
            var Name = new DataColumn("Name", typeof(string));
            var Address = new DataColumn("Address", typeof(string));
            var Port = new DataColumn("Port", typeof(string));

            var dgvBind = new DataTable("Servers");
            dgvBind.Columns.Add(Name);
            dgvBind.Columns.Add(Address);
            dgvBind.Columns.Add(Port);

            foreach (var r1 in data)
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