using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that is used to group together the Playlist methods of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class Playlist : HideObjectMembers
    {
        #region Fields (Playlist Class)

        private Player _base;

        #endregion

        internal Playlist(Player player)
        {
            _base = player;
        }

        /// <summary>
        /// Returns a list of path and file names from the specified m3u type playlist file or null if none are found.
        /// </summary>
        /// <param name="playlist">The path and file name of the playlist file. Supported file types are .m3u and .m3u8.</param>
        public string[] Open(string playlist)
        {
            List<string> fileNames = null;

            if (string.IsNullOrWhiteSpace(playlist))
            {
                _base._lastError = HResult.ERROR_INVALID_NAME;
            }
            else
            {
                bool validExtension = false;
                bool m3u8 = false;

                string extension = Path.GetExtension(playlist);
                if (extension.Length == 0)
                {
                    playlist += ".m3u";
                    validExtension = true;
                }
                else if (string.Equals(extension, ".m3u", StringComparison.OrdinalIgnoreCase) || (string.Equals(extension, ".ppl", StringComparison.OrdinalIgnoreCase)))
                {
                    validExtension = true;
                }
                else if (string.Equals(extension, ".m3u8", StringComparison.OrdinalIgnoreCase))
                {
                    validExtension = true;
                    m3u8 = true;
                }

                if (validExtension)
                {
                    if (File.Exists(playlist))
                    {
                        StreamReader file = null;
                        string playListPath = Path.GetDirectoryName(playlist);
                        string line;

                        fileNames = new List<string>(16);

                        try
                        {
                            if (m3u8) file = new StreamReader(playlist, Encoding.UTF8);
                            else file = new StreamReader(playlist); // something wrong with Encoding.Default?
                            while ((line = file.ReadLine()) != null)
                            {
                                line = line.TrimStart();
                                // skip if line is empty, #extm3u, #extinf info or # comment
                                //if (line != string.Empty && line[0] != '#')
                                if (line.Length > 0 && line[0] != '#')
                                {
                                    // get absolute path...
                                    if (line[1] != ':' && !line.Contains(@"://") && !line.Contains(@":\\")) fileNames.Add(Path.GetFullPath(Path.Combine(playListPath, line)));
                                    else fileNames.Add(line);
                                }
                            }
                            _base._lastError = Player.NO_ERROR;
                        }
                        catch (Exception e) { _base._lastError = (HResult)Marshal.GetHRForException(e); }

                        if (file != null) file.Close();
                    }
                    else _base._lastError = HResult.ERROR_FILE_NOT_FOUND;
                }
                else _base._lastError = HResult.ERROR_INVALID_NAME;
            }

            if (fileNames == null || fileNames.Count == 0) return null;
            return fileNames.ToArray();
        }

        /// <summary>
        /// Saves the specified file name list as an m3u type playlist file. If the playlist file already exists, it is overwritten.
        /// </summary>
        /// <param name="playlist">The path and file name of the playlist. Supported file types are .m3u and .m3u8.</param>
        /// <param name="fileNames">The list of media file names to save to the specified playlist file.</param>
        /// <param name="relativePaths">A value that indicates whether to use relative (to the playlist) paths with the saved file names.</param>
        public int Save(string playlist, string[] fileNames, bool relativePaths)
        {
            if (string.IsNullOrWhiteSpace(playlist) || fileNames == null || fileNames.Length == 0)
            {
                _base._lastError = HResult.E_INVALIDARG;
            }
            else
            {
                bool validExtension = false;
                bool m3u8 = false;

                string extension = Path.GetExtension(playlist);
                if (extension.Length == 0)
                {
                    playlist += ".m3u";
                    validExtension = true;
                }
                else if (string.Equals(extension, ".m3u", StringComparison.OrdinalIgnoreCase) || (string.Equals(extension, ".ppl", StringComparison.OrdinalIgnoreCase)))
                {
                    validExtension = true;
                }
                else if (string.Equals(extension, ".m3u8", StringComparison.OrdinalIgnoreCase))
                {
                    validExtension = true;
                    m3u8 = true;
                }

                if (validExtension)
                {
                    if (relativePaths)
                    {
                        int count = fileNames.Length;
                        string[] relPaths = new string[count];
                        for (int i = 0; i < count; ++i)
                        {
                            relPaths[i] = GetRelativePath(playlist, fileNames[i]);
                        }
                        fileNames = relPaths;
                    }
                    try
                    {
                        if (m3u8) File.WriteAllLines(playlist, fileNames, Encoding.UTF8);
                        else File.WriteAllLines(playlist, fileNames);
                        _base._lastError = Player.NO_ERROR;
                    }
                    catch (Exception e) { _base._lastError = (HResult)Marshal.GetHRForException(e); }
                }
                else _base._lastError = HResult.ERROR_INVALID_NAME;
            }
            return (int)_base._lastError;
        }

        // Taken from: https://stackoverflow.com/questions/275689/how-to-get-relative-path-from-absolute-path
        // Thanks Dave!
        private static string GetRelativePath(string fromPath, string toPath)
        {
            if (string.IsNullOrWhiteSpace(toPath)) return string.Empty;

            Uri fromUri, toUri;

            try
            {
                fromUri = new Uri(fromPath);
                toUri = new Uri(toPath);

                if (fromUri.Scheme != toUri.Scheme) return toPath;
            }
            catch { return toPath; }

            Uri relativeUri = fromUri.MakeRelativeUri(toUri);
            string relativePath = Uri.UnescapeDataString(relativeUri.ToString());

            if (toUri.Scheme.Equals("file", StringComparison.InvariantCultureIgnoreCase))
            {
                relativePath = relativePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);
            }
            return relativePath;
        }

    }
}