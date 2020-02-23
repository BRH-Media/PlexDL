using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace PlexDL.Common.Renderers
{
    public class GenericRenderer
    {
        public static void RenderView(RenderStruct info, DataGridView target)
        {
            if ((info != null) && (target != null))
            {
                List<string> currentColumns = new List<string>();
                List<string> currentCaption = new List<string>();
                List<string> wantedColumns = info.WantedColumns;
                List<string> wantedCaption = info.WantedCaption;

                DataTable dgvBind = new DataTable();
                DataView viewBind = new DataView(info.Data);

                //check if appropriate columns are part of the table; then we can verify and add them to the view.
                foreach (DataColumn c in info.Data.Columns)
                {
                    if (wantedColumns.Contains(c.ColumnName))
                    {
                        int index = wantedColumns.IndexOf(c.ColumnName);
                        string caption = wantedCaption[index];
                        c.Caption = caption;
                        currentCaption.Add(caption);
                        currentColumns.Add(c.ColumnName);
                    }
                }

                currentColumns = Methods.OrderMatch(wantedColumns, currentColumns);

                dgvBind = viewBind.ToTable(false, currentColumns.ToArray());

                if (target.InvokeRequired)
                {
                    target.BeginInvoke((MethodInvoker)delegate
                    {
                        target.DataSource = dgvBind;
                        Methods.SetHeaderText(target, info.Data);
                        target.Refresh();
                    });
                }
                else
                {
                    target.DataSource = dgvBind;
                    Methods.SetHeaderText(target, info.Data);
                    target.Refresh();
                }
            }
        }
    }
}