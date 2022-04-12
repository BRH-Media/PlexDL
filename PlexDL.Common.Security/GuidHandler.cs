using PlexDL.Common.Security.Protection;
using System;
using System.IO;

// ReSharper disable InvertIf

namespace PlexDL.Common.Security
{
    public static class GuidHandler
    {
        private static readonly string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private static string GuidFileLocation { get; } = $@"{AppData}\.plexdl\.guid";

        /// <summary>
        /// Gets the global GUID. Returns a new random GUID and stores it if one doesn't already exist.
        /// </summary>
        /// <param name="forceNew">Whether or not to force a new GUID instead of grabbing from persistent storage</param>
        /// <returns></returns>
        public static Guid GetGlobalGuid(bool forceNew = false)
        {
            //final return value
            Guid value;

            //make sure we can load from persistent storage
            if (!forceNew && File.Exists(GuidFileLocation))
            {
                var read = StoredGlobalGuid();

                //final return value must run a verification check
                value =
                    //returned GUID invalid?
                    read.IsEmpty()
                        ? NewGlobalGuid()
                        : read;
            }
            else
            {
                //replace the file with new 20-byte entropy data and return the result
                value = NewGlobalGuid();
            }

            //return the final value
            return value;
        }

        /// <summary>
        /// Generates a new random global GUID and if storeNew is true, it replaces the existing GUID in persistent storage.
        /// </summary>
        /// <param name="storeNew">Whether or not to replace the existing GUID in persistent storage</param>
        /// <returns></returns>
        public static Guid NewGlobalGuid(bool storeNew = true)
        {
            try
            {
                //generate a new GUID
                var newGuid = Guid.NewGuid();

                //should we replace the existing GUID?
                if (storeNew)
                {
                    //store the new GUID encrypted via WDPAPI
                    var handler = new ProtectedFile(GuidFileLocation);
                    handler.WriteAllText(newGuid.ToString());
                }

                //return the newly generated GUID
                return newGuid;
            }
            catch
            {
                //ignore all errors
            }

            //default (all zeroes)
            return Guid.Empty;
        }

        /// <summary>
        /// Load the global GUID from persistent storage; returns Guid.Empty on failure.
        /// </summary>
        /// <returns></returns>
        public static Guid StoredGlobalGuid()
        {
            try
            {
                if (File.Exists(GuidFileLocation))
                {
                    //decrypt and read the GUID from persistent storage
                    var handler = new ProtectedFile(GuidFileLocation);
                    var guidString = handler.ReadAllText();

                    //is it a valid string?
                    if (!string.IsNullOrEmpty(guidString))
                    {
                        //validate the GUID and return the GUID struct if successful
                        if (Guid.TryParse(guidString, out var r))
                            return r;
                    }
                }
            }
            catch
            {
                //ignore all errors
            }

            //default (all zeroes)
            return Guid.Empty;
        }

        public static bool IsEmpty(this Guid g)
        {
            return g == Guid.Empty;
        }
    }
}