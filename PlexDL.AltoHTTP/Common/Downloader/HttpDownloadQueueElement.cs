﻿// ReSharper disable NonReadonlyMemberInGetHashCode

namespace PlexDL.AltoHTTP.Common.Downloader
{
    /// <summary>
    ///     Struct to store the information for download operations
    /// </summary>
    public struct HttpDownloadQueueElement
    {
        /// <summary>
        ///     Checks if two objects are the same
        /// </summary>
        /// <param name="obj">Target object to compare</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj is HttpDownloadQueueElement el)
                return el.Url == Url
                       && el.Destination == Destination
                       && el.Completed == Completed
                       && el.Id == Id;

            //default
            return false;
        }

        /// <summary>
        ///     Get the unique hash code of the object
        /// </summary>
        /// <returns></returns>
        public override int GetHashCode()
        {
            return Id.GetHashCode()
                   ^ Url.GetHashCode()
                   ^ Destination.GetHashCode()
                   ^ Completed.GetHashCode();
        }

        /// <summary>
        ///     Download object ID
        /// </summary>
        public string Id;

        /// <summary>
        ///     Url source string
        /// </summary>
        public string Url;

        /// <summary>
        ///     Destination file path to save the data
        /// </summary>
        public string Destination;

        /// <summary>
        ///     Boolean value specifies if the download is completed
        /// </summary>
        public bool Completed;
    }
}