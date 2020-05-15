using System.Data;

namespace PlexDL.Common.Globals
{
    public static class RowGet
    {
        public static DataRow GetDataRowTbl(DataTable table, int index)
        {
            //MessageBox.Show(Flags.IsTVShow.ToString());
            //MessageBox.Show(table.Rows[index].ItemArray.Length.ToString());
            return table.Rows[index];
        }

        public static DataRow GetDataRowContent(int index)
        {
            return GetDataRowTbl(GlobalTables.ReturnCorrectTable(), index);
        }

        public static DataRow GetDataRowSeries(int index)
        {
            return GetDataRowTbl(GlobalTables.SeasonsTable, index);
        }

        public static DataRow GetDataRowAlbums(int index)
        {
            return GetDataRowTbl(GlobalTables.AlbumsTable, index);
        }

        public static DataRow GetDataRowEpisodes(int index)
        {
            return GetDataRowTbl(GlobalTables.EpisodesTable, index);
        }

        public static DataRow GetDataRowTracks(int index)
        {
            return GetDataRowTbl(GlobalTables.TracksTable, index);
        }

        public static DataRow GetDataRowLibrary(int index)
        {
            return GetDataRowTbl(GlobalTables.SectionsTable, index);
        }
    }
}