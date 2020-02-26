using PlexDL.PlexAPI;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace PlexDL.Common.Renderers.DGVRenderers
{
    public class ServerViewRenderer
    {
        public static void RenderView(List<Server> data, DataGridView target)
        {
            DataColumn Name = new DataColumn("Name", typeof(string));
            DataColumn Address = new DataColumn("Address", typeof(string));
            DataColumn Port = new DataColumn("Port", typeof(string));

            DataTable dgvBind = new DataTable("Servers");
            dgvBind.Columns.Add(Name);
            dgvBind.Columns.Add(Address);
            dgvBind.Columns.Add(Port);

            foreach (Server r1 in data)
            {
                dgvBind.Rows.Add(r1.name, r1.address, r1.port.ToString());
            }

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