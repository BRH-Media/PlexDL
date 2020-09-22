using PlexDL.Common.Logging;
using System;
using System.Data;

namespace PlexDL.Common.Globals.Providers
{
    public static class DataProvider
    {
        public static DataSet TitlesProvider { get; set; } = new DataSet(@"TitlesProvider");
        public static DataSet FilteredProvider { get; set; } = new DataSet(@"FilteredProvider");
        public static DataSet SectionsProvider { get; set; } = new DataSet(@"SectionsProvider");
        public static DataSet SeasonsProvider { get; set; } = new DataSet(@"SeasonsProvider");
        public static DataSet EpisodesProvider { get; set; } = new DataSet(@"EpisodesProvider");
        public static DataSet TracksProvider { get; set; } = new DataSet(@"TracksProvider");
        public static DataSet AlbumsProvider { get; set; } = new DataSet(@"Albums");

        public static void DoFill(this DataSet data, DataTable rawData, DataTable viewData)
        {
            //clean all data for a new DataSet
            data = new DataSet(data.DataSetName);

            //table name fixes
            rawData.TableName = @"RawData";
            rawData.Namespace = @"DataProvider";
            viewData.TableName = @"ViewData";
            viewData.Namespace = @"DataProvider";

            //add the data
            data.Tables.Add(rawData);
            data.Tables.Add(viewData);
        }

        /// <summary>
        /// This is a VERY dangerous operation; it will clear all DataSets regardless of the circumstance.
        /// </summary>
        public static void DropAllData()
        {
            ClearAllViews();
            ClearAllRawData();
        }

        public static void ClearAllViews()
        {
            try
            {
                TitlesProvider.ClearViewTable();
                FilteredProvider.ClearViewTable();
                SectionsProvider.ClearViewTable();
                SeasonsProvider.ClearViewTable();
                EpisodesProvider.ClearViewTable();
                TracksProvider.ClearViewTable();
                AlbumsProvider.ClearViewTable();
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, @"ClearAllViewsError");
            }
        }

        public static void ClearAllRawData()
        {
            try
            {
                TitlesProvider.ClearRawTable();
                FilteredProvider.ClearRawTable();
                SectionsProvider.ClearRawTable();
                SeasonsProvider.ClearRawTable();
                EpisodesProvider.ClearRawTable();
                TracksProvider.ClearRawTable();
                AlbumsProvider.ClearRawTable();
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, @"ClearAllRawDataError");
            }
        }

        public static void ClearViewTable(this DataSet data)
        {
            if (data != null)
                if (data.Tables.Contains(@"ViewData"))
                    data.Tables.Remove(@"ViewData");
        }

        public static void ClearRawTable(this DataSet data)
        {
            if (data != null)
                if (data.Tables.Contains(@"RawData"))
                    data.Tables.Remove(@"RawData");
        }

        public static void SetViewTable(this DataSet data, DataTable ViewData)
        {
            try
            {
                //if the dataset is null, then initialise it
                if (data == null) data = new DataSet(@"AutoFill");

                //remove it if it already exists
                if (data.Tables.Contains(@"ViewData"))
                    data.Tables.Remove(@"ViewData");

                //copy the structure to avoid inheritance problems
                var toAdd = ViewData.Copy();

                //table name fix
                toAdd.TableName = @"ViewData";
                toAdd.Namespace = @"DataProvider";

                //commit the new table to the DataSet
                data.Tables.Add(toAdd);
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, @"SetViewDataError");
            }
        }

        public static void SetRawTable(this DataSet data, DataTable RawData)
        {
            try
            {
                //if the dataset is null, then initialise it
                if (data == null) data = new DataSet(@"AutoFill");

                //remove it if it already exists
                if (data.Tables.Contains(@"RawData"))
                    data.Tables.Remove(@"RawData");

                //copy the structure to avoid inheritance problems
                var toAdd = RawData.Copy();

                //table name fix
                toAdd.TableName = @"RawData";
                toAdd.Namespace = @"DataProvider";

                //commit the new table to the DataSet
                data.Tables.Add(toAdd);
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, @"SetRawDataError");
            }
        }

        public static DataTable GetViewTable(this DataSet data)
        {
            try
            {
                if (data.Tables.Contains(@"ViewData"))
                    return data.Tables[@"ViewData"];
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, @"RetrieveViewDataError");
            }

            //default
            return null;
        }

        public static DataTable GetRawTable(this DataSet data)
        {
            try
            {
                if (data.Tables.Contains(@"RawData"))
                    return data.Tables[@"RawData"];
            }
            catch (Exception ex)
            {
                LoggingHelpers.RecordException(ex.Message, @"RetrieveRawDataError");
            }

            //default
            return null;
        }
    }
}