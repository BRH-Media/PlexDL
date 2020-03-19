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

        #endregion Fields (Playlist Class)

        internal Playlist(Player player)
        {
            _base = player;
        }

        /// <summary>
        /// Opens a playlist as a list of file names. Returns null if no or an empty playlist is found.
        /// </summary>
        /// <param name="playlist">The path and file name of the playlist. Supported file types are .m3u and .m3u8.</param>
        public string[] Open(string playlist)
        {
            List<string> fileNames = null;

            if (string.IsNullOrEmpty(playlist))
            {
                _base._lastError = HResult.ERROR_INVALID_NAME;
            }
            else
            {
                var validExtension = false;
                var m3u8 = false;

                var extension = Path.GetExtension(playlist);
                if (extension.Length == 0)
                {
                    playlist += ".m3u";
                    validExtension = true;
                }
                else if (string.Equals(extension, ".m3u", StringComparison.OrdinalIgnoreCase) ||
                         string.Equals(extension, ".ppl", StringComparison.OrdinalIgnoreCase))
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
                        var playListPath = Path.GetDirectoryName(playlist);
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
                                if (line != string.Empty && line[0] != '#')
                                {
                                    // get absolute path...
                                    if (line[1] != ':' && !line.Contains(@"://") && !line.Contains(@":\\"))
                                        fileNames.Add(Path.GetFullPath(Path.Combine(playListPath, line)));
                                    else fileNames.Add(line);
                                }
                            }

                            _base._lastError = Player.NO_ERROR;
                        }
                        catch (Exception e) { _base._lastError = (HResult)Marshal.GetHRForException(e); }

                        if (file != null) file.Close();
                    }
                    else
                    {
                        _base._lastError = HResult.ERROR_FILE_NOT_FOUND;
                    }
                }
                else
                {
                    _base._lastError = HResult.ERROR_INVALID_NAME;
                }
            }

            if (fileNames == null || fileNames.Count == 0) return null;
            return fileNames.ToArray();
        }

        /// <summary>
        /// Saves a list of file names in the specified playlist. If the playlist already exists, it is overwritten.
        /// </summary>
        /// <param name="playlist">The path and file name of the playlist. Supported file types are .m3u and .m3u8.</param>
        /// <param name="fileNames">The list of media file names to save in the specified playlist.</param>
        /// <param name="relativePaths">A value indicating whether to use relative (to the playlist) paths with the saved file names.</param>
        public int Save(string playlist, string[] fileNames, bool relativePaths)
        {
            if (string.IsNullOrEmpty(playlist) || fileNames == null || fileNames.Length == 0)
            {
                _base._lastError = HResult.E_INVALIDARG;
            }
            else
            {
                var validExtension = false;
                var m3u8 = false;

                var extension = Path.GetExtension(playlist);
                if (extension.Length == 0)
                {
                    playlist += ".m3u";
                    validExtension = true;
                }
                else if (string.Equals(extension, ".m3u", StringComparison.OrdinalIgnoreCase) ||
                         string.Equals(extension, ".ppl", StringComparison.OrdinalIgnoreCase))
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
                        var count = fileNames.Length;
                        var relPaths = new string[count];
                        for (var i = 0; i < count; ++i)
                            relPaths[i] = GetRelativePath(playlist, fileNames[i]);

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
                else
                {
                    _base._lastError = HResult.ERROR_INVALID_NAME;
                }
            }

            return (int)_base._lastError;
        }

        // Taken from: https://stackoverflow.com/questions/275689/how-to-get-relative-path-from-absolute-path
        // Thanks Dave!
        private string GetRelativePath(string fromPath, string toPath)
        {
            var fromUri = new Uri(fromPath);
            var toUri = new Uri(toPath);

            if (fromUri.Scheme != toUri.Scheme)
                return toPath;

            var relativeUri = fromUri.MakeRelativeUri(toUri);
            var relativePath = Uri.UnescapeDataString(relativeUri.ToString());

            if (toUri.Scheme.Equals("file", StringComparison.InvariantCultureIgnoreCase))
                relativePath = relativePath.Replace(Path.AltDirectorySeparatorChar, Path.DirectorySeparatorChar);

            return relativePath;
        }
    }
}