using PlexDL.Common.Globals.Providers;
using System.Data;

namespace PlexDL.Common.Globals
{
    public static class RowGet
    {
        public static DataRow GetDataRowTbl(DataTable table, int index)
        {
            //UIMessages.Info(Flags.IsTVShow.ToString());
            //UIMessages.Info(table.Rows[index].ItemArray.Length.ToString());
            return table.Rows[index];
        }

        public static DataRow GetDataRowContent(int index)
        {
            return GetDataRowTbl(TableManager.ReturnCorrectTable(), index);
        }

        public static DataRow GetDataRowSeries(int index)
        {
            return GetDataRowTbl(DataProvider.SeasonsProvider.GetRawTable(), index);
        }

        public static DataRow GetDataRowAlbums(int index)
        {
            return GetDataRowTbl(DataProvider.AlbumsProvider.GetRawTable(), index);
        }

        public static DataRow GetDataRowEpisodes(int index)
        {
            return GetDataRowTbl(DataProvider.EpisodesProvider.GetRawTable(), index);
        }

        public static DataRow GetDataRowTracks(int index)
        {
            return GetDataRowTbl(DataProvider.TracksProvider.GetRawTable(), index);
        }

        public static DataRow GetDataRowLibrary(int index)
        {
            return GetDataRowTbl(DataProvider.SectionsProvider.GetRawTable(), index);
        }
    }
}