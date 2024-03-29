﻿/****************************************************************

    PVS.MediaPlayer - Version 1.7
    January 2022, The Netherlands
    © Copyright 2022 PVS The Netherlands - licensed under The Code Project Open License (CPOL)

    PVS.MediaPlayer uses (part of) the Media Foundation .NET library by nowinskie and snarfle (https://sourceforge.net/projects/mfnet).
    Licensed under either Lesser General Public License v2.1 or BSD.  See license.txt or BSDL.txt for details (http://mfnet.sourceforge.net).

    ****************

    For use with Microsoft Windows 7 or higher*, Microsoft .NET Core 3.1, .NET 4, 5, 6 or higher and WinForms (any CPU).
    * Use of the recorders requires Windows 8 or later.

    Created with Microsoft Visual Studio.

    Article on CodeProject with information on the use of the PVS.MediaPlayer library:
    https://www.codeproject.com/Articles/109714/PVS-MediaPlayer-Audio-and-Video-Player-Library

    ****************

    The PVS.MediaPlayer library source code is divided into 8 files:

    1. Player.cs        - main source code
    2. SubClasses.cs    - various grouping and information classes
    3. Interop.cs       - unmanaged Win32 functions
    4. AudioDevices.cs  - audio devices and peak meters
    5. DisplayClones.cs - multiple video displays 
    6. CursorHide.cs    - hides the mouse cursor during inactivity
    7. Subtitles.cs     - subrip (.srt) subtitles
    8. Infolabel.cs     - movable small pop-up window

    Required references:

    System
    System.Drawing
    System.Windows.Forms

    ****************

    This file: Subtitles.cs

    Player Class
    Extension to file 'Player.cs'.

    ****************

    Thanks!

    Thank you for your comments, suggestions and 5 star votes. You keep this library alive.

    Special thanks to the creators of Media Foundation .NET for their great library.

    Special thanks to Sean Ewington and Deeksha Shenoy of CodeProject who also took care of publishing the many
    code updates and changes in the PVS.MediaPlayer articles in a friendly, fast, and highly competent manner.


    Peter Vegter
    January 2022, The Netherlands

    ****************************************************************/

#region Usings

using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;

#endregion


namespace PlexDL.Player
{
    public partial class Player
    {
        /*
            Subtitles
            Provides SubRip (.srt) subtitles by subscribing to the Player.Events.MediaSubtitleChanged event.
        */


        // ******************************** Subtitles - Fields

        #region Subtitles - Fields

        // constants
        private const int               ST_DEFAULT_DIRECTORY_DEPTH  = 0;

        // static regex
        internal static Regex           st_TimeRegex = new Regex(@"(?<start>[0-9:,]+)(\s*)-->(\s*)(?<end>[0-9:,]+)", RegexOptions.Compiled);
        internal static Regex           st_TagsRegex = new Regex("<.*?>", RegexOptions.Compiled);

        // classes
        internal sealed class FileSearchData
        {
            internal bool               Found;
            internal string             FileName;
            internal StringBuilder      FilePath = new StringBuilder(260);

            internal FileSearchData(string fileName)
            {
                FileName = Path.DirectorySeparatorChar + fileName;
            }
        }

        internal sealed class SubtitleItem
        {
            internal long   StartTime;
            internal long   EndTime;
            internal string Text;

            internal SubtitleItem(long startTime, long endTime, string text)
            {
                StartTime   = startTime;
                EndTime     = endTime;

                // remove last return and/or linefeed chars
                Text = text.TrimEnd('\r', '\n');
            }
        }

        // event args
        internal SubtitleEventArgs      st_SubtitleChangedArgs;

        // general
        internal bool                   st_SubtitlesEnabled;
        internal bool                   st_HasSubtitles;
        internal string                 st_SubtitlesName;   // filename of subtitles being used
        internal SubtitleItem[]         st_SubtitleItems;
        internal int                    st_SubTitleCount;
        private bool                    st_SubtitlesBusy;

        // find and decode subtitles file
        internal string                 st_FileName;        // set by user
        internal string                 st_Directory;       // set by user
        internal int                    st_DirectoryDepth   = ST_DEFAULT_DIRECTORY_DEPTH;
        internal Encoding               st_Encoding         = Encoding.Default;
        internal long                   st_TimeShift;
        internal bool                   st_RemoveHTMLTags   = true;
        internal bool                   st_AudioOnlyEnabled;

        // used with get current subtitle
        internal bool                   st_SubtitleOn;
        internal bool                   st_SubtitleForce;
        internal int                    st_CurrentIndex     = -1;
        private long                    st_CurrentStart;
        private long                    st_CurrentEnd;

        #endregion


        // ******************************** Subtitles - Start / Exists / Stop / Eventhandler / Find

        #region Subtitles - Start / Exists / Stop / Position Eventhandler / Find

        internal void Subtitles_Start(bool refresh)
        {
            if (st_HasSubtitles) Subtitles_Stop();

			//if (st_SubtitlesEnabled && _playing && !_webcamMode && (_hasVideo || st_AudioOnlyEnabled))
			if (st_SubtitlesEnabled && _fileMode && (_hasVideo || st_AudioOnlyEnabled))
			{
				string subtitlesFile = Subtitles_Exists();
				if (subtitlesFile.Length > 0)
				{
					if (Subtitles_GetFromFile(subtitlesFile))
					{
						st_CurrentIndex = 0;
						_mediaPositionChanged += Subtitles_MediaPositionChanged;

						st_HasSubtitles = true;
						st_SubtitlesName = subtitlesFile;

						if (refresh) OnMediaPositionChanged();
					}
				}
			}
        }

        // check subtitles file exists (just the file (name), no contents check)
        internal string Subtitles_Exists()
        {
            if (!_fileMode) return string.Empty;
            if (st_HasSubtitles) return st_SubtitlesName;

            string searchFile;
            string initialDirectory;

            if (!string.IsNullOrWhiteSpace(st_FileName)) searchFile = st_FileName;
            else searchFile = Media.GetName(MediaName.FileNameWithoutExtension) + SUBTITLES_FILE_EXTENSION;

            if (!string.IsNullOrWhiteSpace(st_Directory)) initialDirectory = st_Directory;
            else initialDirectory = Media.GetName(MediaName.DirectoryName);

            return Subtitles_FindFile(searchFile, initialDirectory, st_DirectoryDepth);
            // TODO check contents of file (Count > 0)
        }

        internal void Subtitles_Stop()
        {
            if (st_HasSubtitles)
            {
                this._mediaPositionChanged -= Subtitles_MediaPositionChanged;

                if (st_SubtitleOn)
                {
                    st_SubtitleChangedArgs._index    = -1;
                    st_SubtitleChangedArgs._subtitle = string.Empty;
                    _mediaSubtitleChanged(this, st_SubtitleChangedArgs);

                    st_SubtitleOn   = false;
                }

                st_HasSubtitles     = false;
                st_SubtitlesName    = string.Empty;

                //st_LastPosition     = 0;
                st_CurrentIndex     = -1;
                st_CurrentStart     = 0;
                st_CurrentEnd       = 0;

                st_FileName         = string.Empty;
                st_TimeShift        = 0;

                Subtitles_Dispose();
            }
        }

        // returns current subtitle string
        private void Subtitles_MediaPositionChanged(object sender, PositionEventArgs e)
        {
            if (!st_SubtitlesBusy)
            {
                st_SubtitlesBusy = true;

                // get player position + synchronize offset
                long position = e.FromBegin - st_TimeShift;
                if (position < 0) position = 0;

                if (st_SubtitleForce || position < st_CurrentStart || position >= st_CurrentEnd)
                {
                    bool backwards = position < st_CurrentStart;

                    if (Subtitle_Find(position, st_CurrentIndex, backwards, out int index))
                    {
                        if (!st_SubtitleOn || (index != st_CurrentIndex || st_SubtitleForce))
                        {
                            st_CurrentIndex = index;

                            st_SubtitleOn = true;
                            st_CurrentStart = st_SubtitleItems[index].StartTime;
                            st_CurrentEnd = st_SubtitleItems[index].EndTime;

                            st_SubtitleChangedArgs._index = index;
                            st_SubtitleChangedArgs._subtitle = st_SubtitleItems[index].Text;
                            _mediaSubtitleChanged(this, st_SubtitleChangedArgs);
                        }
                    }
                    else
                    {
                        if (st_SubtitleOn || (index != st_CurrentIndex || st_SubtitleForce))
                        {
                            st_CurrentIndex = index;

                            if (st_CurrentIndex < 1) st_CurrentStart = 0;
                            else st_CurrentStart = st_SubtitleItems[st_CurrentIndex].EndTime;

                            if (st_CurrentIndex > st_SubtitleItems.Length - 1) st_CurrentEnd = this.Length;
                            else st_CurrentEnd = st_SubtitleItems[st_CurrentIndex].StartTime;

                            if (st_SubtitleOn)
                            {
                                st_SubtitleOn = false;
                                st_SubtitleChangedArgs._index = -1;
                                st_SubtitleChangedArgs._subtitle = string.Empty;
                                _mediaSubtitleChanged(this, st_SubtitleChangedArgs);
                            }
                        }
                    }
                    st_SubtitleForce = false;
                }
                st_SubtitlesBusy = false;
            }
        }

        private bool Subtitle_Find(double time, int startIndex, bool backwards, out int index)
        {
            bool found = false;
            index = 0;

            if (st_HasSubtitles)
            {
                if (backwards)
                {
                    for (int i = startIndex; i >= 0; i--)
                    {
                        if (time < st_SubtitleItems[i].EndTime)
                        {
                            if (time >= st_SubtitleItems[i].StartTime)
                            {
                                index = i;
                                found = true;
                                break;
                            }
                        }
                        else
                        {
                            index = i;
                            break;
                        }
                    }
                }
                else
                {
                    int itemCount = st_SubtitleItems.Length;
                    index = itemCount - 1;
                    for (int i = startIndex; i < itemCount; i++)
                    {
                        if (time >= st_SubtitleItems[i].StartTime)
                        {
                            if (time < st_SubtitleItems[i].EndTime)
                            {
                                index = i;
                                found = true;
                                break;
                            }
                        }
                        else
                        {
                            index = i;
                            break;
                        }
                    }
                }
            }
            return found;
        }

        #endregion


        // ******************************** Subtitles - Find File / Search File - also used by Media.GetChaptersFile

        #region Subtitles - Find File / Search File - also used by Media.GetChaptersFile and Playlist.GetChaptersBaseFile

        internal string Subtitles_FindFile(string fileName, string initialDirectory, int directoryDepth)
        {
            if (string.IsNullOrWhiteSpace(fileName)
                || fileName.IndexOfAny(Path.GetInvalidFileNameChars()) >= 0
                || !Directory.Exists(initialDirectory))
                return string.Empty;

            FileSearchData data = new FileSearchData(fileName);
            Subtitles_SearchFile(initialDirectory, directoryDepth, data);
            return data.Found ? data.FilePath.ToString() : string.Empty;
        }

        private void Subtitles_SearchFile(string directory, int depth, FileSearchData data)
        {
            try
            {
                data.FilePath.Length = 0;
                data.FilePath.Append(directory).Append(data.FileName);
                data.Found = File.Exists(data.FilePath.ToString());

                if (!data.Found && --depth >= 0)
                {
                    string[] directories = Directory.GetDirectories(directory);
                    for (int i = 0; i < directories.Length; i++)
                    {
                        Subtitles_SearchFile(directories[i], depth, data);
                        if (data.Found) return;
                    }
                }
            }
            catch { /* ignored */ }
        }

        #endregion


        // ******************************** Subtitles - Get From File / Count / Dispose

        #region Subtitles - Get From File / Count / Dispose

        private bool Subtitles_GetFromFile(string fileName)
        {
            bool result = false;

            if (!string.IsNullOrWhiteSpace(fileName))
            {
                StreamReader reader = new StreamReader(fileName, st_Encoding, true);
				int count = Subtitles_Count(reader);
                reader.Close();

                if (count > 0)
                {
                    bool    error       = false;
                    int     readStep    = 0;
                    Match   m;
                    string  line;

                    TimeSpan startTime  = TimeSpan.Zero;
                    TimeSpan endTime    = TimeSpan.Zero;
                    string  text        = "";

                    int index           = 0;
                    st_SubtitleItems    = new SubtitleItem[count];

                    // use new reader because of problems with files with BOM
                    reader = new StreamReader(fileName, st_Encoding, true);
                    while ((line = (reader.ReadLine())) != null && !error)
                    {
						line = line.Trim();
                        if (string.IsNullOrWhiteSpace(line)) continue;

                        int testId;
                        switch (readStep)
                        {
                            case 0: // Id
#pragma warning disable IDE0059 // Unnecessary assignment of a value
                                if (int.TryParse(line, out testId))
                                {
                                    readStep = 1;
                                }
                                break;
                            case 1: // Time
                                m = st_TimeRegex.Match(line);
                                if (m.Success)
                                {
                                    if (!TimeSpan.TryParse(m.Groups["start"].Value.Replace(",", "."), out startTime)) error = true;
                                    if (!TimeSpan.TryParse(m.Groups["end"].Value.Replace(",", "."), out endTime)) error = true;
                                }
                                else error = true;
								readStep = 2;
                                break;
                            case 2: // Text
                                if (int.TryParse(line, out testId)) // if not id (subtitle number)
#pragma warning restore IDE0059 // Unnecessary assignment of a value
                                {
                                    if (st_RemoveHTMLTags) st_SubtitleItems[index++] = new SubtitleItem(startTime.Ticks, endTime.Ticks, st_TagsRegex.Replace(text, string.Empty));
                                    else st_SubtitleItems[index++] = new SubtitleItem(startTime.Ticks, endTime.Ticks, text);
                                    text = "";
                                    readStep = 1;
                                }
                                else
                                {
                                    text += line + "\r\n";
                                }
                                break;
                        }
                    }
                    if (!error)
                    {
						if (text.Length > 0)
                        {
                            if (st_RemoveHTMLTags) st_SubtitleItems[index++] = new SubtitleItem(startTime.Ticks, endTime.Ticks, st_TagsRegex.Replace(text, string.Empty));
                            else st_SubtitleItems[index++] = new SubtitleItem(startTime.Ticks, endTime.Ticks, text);
                        }
                        else
                        {
                            st_SubtitleItems[index++] = new SubtitleItem(0, 0, string.Empty);
                        }
                        st_SubTitleCount = count;

                        if (st_SubtitleItems[st_SubTitleCount - 1] == null)
                        {
                            if (st_SubTitleCount > 1) st_SubtitleItems[st_SubTitleCount - 1] = st_SubtitleItems[st_SubTitleCount - 2];
                            else st_SubtitleItems[st_SubTitleCount - 1] = new SubtitleItem(0, 0, string.Empty);
                        }

                        result = true;
                    }
                    reader.Close();
                }
            }
            return result;
        }

        // ... also for dimensioning the subtitles array
        private static int Subtitles_Count(StreamReader reader)
        {
            bool error          = false;
            string line;
            int readStep        = 0;
            int count           = 0;

			while ((line = (reader.ReadLine())) != null && !error)
            {
                line = line.Trim();
                if (string.IsNullOrWhiteSpace(line)) continue;

                int testId;
                switch (readStep)
                {
                    case 0: // Id
#pragma warning disable IDE0059 // Unnecessary assignment of a value
                        if (int.TryParse(line, out testId)) readStep = 1;
                        break;
                    case 1: // Time
                        Match m = st_TimeRegex.Match(line);
                        if (m.Success)
                        {
                            if (!TimeSpan.TryParse(m.Groups["start"].Value.Replace(",", "."), out TimeSpan startTime)) error = true;
                            if (!TimeSpan.TryParse(m.Groups["end"].Value.Replace(",", "."), out TimeSpan endTime)) error = true;
                            ++count;
                        }
                        else error = true;
                        readStep = 2;
                        break;
                    case 2: // Text
                        if (int.TryParse(line, out testId))
#pragma warning restore IDE0059 // Unnecessary assignment of a value
                        {
                            readStep = 1;
                        }
                        break;
                }
            }
			//reader.DiscardBufferedData();
			//reader.BaseStream.Position = 0;
            return error ? 0 : count;
        }

        private void Subtitles_Dispose()
        {
            if (st_SubtitleItems != null)
            {
                for (int i = 0; i < st_SubtitleItems.Length; i++)
                {
                    st_SubtitleItems[i] = null;
                }

                st_SubtitleItems = null;
                st_SubTitleCount = 0;
            }
        }

        #endregion
    }
}
