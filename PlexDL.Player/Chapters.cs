using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that is used to group together the Chapters methods and properties of the PVS.MediaPlayer.Player class.
    /// </summary>
    //[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
#pragma warning disable CA1001 // Types that own disposable fields should be disposable
    public sealed class Chapters : HideObjectMembers
#pragma warning restore CA1001 // Types that own disposable fields should be disposable
    {
        #region Fields (Chapters Class)

        private const string    NO_TITLE_INDICATOR      = "#";
        private const int       CHAPTERS_FILE_MAX_SIZE  = 10240;
        private const string    ROOT_ATOM_TYPES         = "ftyp,moov,mdat,pdin,moof,mfra,stts,stsc,stsz,meta,free,skip";
        private const string    IGNORE_EXTENSIONS       = ".chap.srt.m3u.m3u8.ppl.txt.inf.cfg.exe.dll";

        private Player          _base;

        // Chapters file
        private string          _chapDirectory;
        private string          _chapFileName;
        private static Regex    _parser   = new Regex(@"^(?<start>[0-9:,.]+)(\s*)(-(\s*))?(?<end>[0-9:,.]+)?(\s*)(?<title>.+)", RegexOptions.Compiled);

        private byte[]          MOOV_ATOM = { (byte)'m', (byte)'o', (byte)'o', (byte)'v' };
        private byte[]          TRAK_ATOM = { (byte)'t', (byte)'r', (byte)'a', (byte)'k' };
        private byte[]          TREF_ATOM = { (byte)'t', (byte)'r', (byte)'e', (byte)'f' };
        private byte[]          CHAP_ATOM = { (byte)'c', (byte)'h', (byte)'a', (byte)'p' };
        private byte[]          TKHD_ATOM = { (byte)'t', (byte)'k', (byte)'h', (byte)'d' };
        private byte[]          MDIA_ATOM = { (byte)'m', (byte)'d', (byte)'i', (byte)'a' };
        private byte[]          MINF_ATOM = { (byte)'m', (byte)'i', (byte)'n', (byte)'f' };
        private byte[]          STBL_ATOM = { (byte)'s', (byte)'t', (byte)'b', (byte)'l' };
        private byte[]          STTS_ATOM = { (byte)'s', (byte)'t', (byte)'t', (byte)'s' };
        private byte[]          STCO_ATOM = { (byte)'s', (byte)'t', (byte)'c', (byte)'o' };
        private byte[]          UDTA_ATOM = { (byte)'u', (byte)'d', (byte)'t', (byte)'a' };
        private byte[]          CHPL_ATOM = { (byte)'c', (byte)'h', (byte)'p', (byte)'l' };

        private FileStream      _reader;
        private long            _fileLength;
        private long            _atomEnd;
        private long            _moovStart;
        private long            _moovEnd;
        private byte[]          _buffer;

        #endregion

        internal Chapters(Player player)
        {
            _base = player;
        }

        /* Get Chapters From Media */

        /*
            Thanks to Zeugma440, https://github.com/Zeugma440/atldotnet/wiki/Focus-on-Chapter-metadata
            A great help to defeat the ugly QuickTime chapters beast.
        */

        /// <summary>
        /// Returns chapter information from the playing media file. Supported file formats: .mp4, .m4a, .m4b, .m4v, .mkv, .mka and .webm (and maybe others). This method does not evaluate file extensions but the actual content of files.
        /// </summary>
        /// <param name="chapters_I">When this method returns, contains the chapter information of the media stored in the QuickTime (mp4 types) or Matroska (mkv types) format or null.</param>
        /// <param name="chapters_II">When this method returns, contains the chapter information of the media stored the Nero (mp4 types) format or null.</param>
        public int FromMedia(out MediaChapter[] chapters_I, out MediaChapter[] chapters_II)
        {
            if (_base._fileMode && !_base._imageMode) return FromMedia(_base._fileName, out chapters_I, out chapters_II);

            chapters_I = null;
            chapters_II = null;

            _base._lastError = HResult.MF_E_NOT_AVAILABLE;
            return (int)_base._lastError;
        }

        /// <summary>
        /// Returns chapter information from the specified media file. Supported file formats: .mp4, .m4a, .m4b, .m4v, .mkv, .mka and .webm (and maybe others). This method does not evaluate file extensions but the actual content of files.
        /// </summary>
        /// <param name="fileName">The path and name of the media file whose chapter information is to be obtained.</param>
        /// <param name="chapters_I">When this method returns, contains the chapter information of the media file stored in the QuickTime (mp4 types) or Matroska (mkv types) format or null.</param>
        /// <param name="chapters_II">When this method returns, contains the chapter information of the media file stored in the Nero (mp4 types) format or null.</param>
        public int FromMedia(string fileName, out MediaChapter[] chapters_I, out MediaChapter[] chapters_II)
        {
            chapters_I = null;
            chapters_II = null;
            int fileType = 0;    // 0 = none, 1 = mp4, 2 = mkv

            if (string.IsNullOrWhiteSpace(fileName)) _base._lastError = HResult.E_INVALIDARG;
            else if (!File.Exists(fileName)) _base._lastError = HResult.ERROR_FILE_NOT_FOUND;
            else
            {
                _base._lastError = HResult.MF_E_NOT_AVAILABLE;
                try
                {
                    byte[] buffer = new byte[16];
                    _reader = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    if (_reader.Length > 1000)
                    {
                        _reader.Read(buffer, 0, 8);
                        if ((ROOT_ATOM_TYPES.IndexOf(Encoding.ASCII.GetString(new byte[] { buffer[4], buffer[5], buffer[6], buffer[7] }), StringComparison.Ordinal) >= 0))
                        {
                            fileType = 1;
                            _base._lastError = Player.NO_ERROR;
                        }
                        else if (buffer[0] == 0x1A && buffer[1] == 0x45 && buffer[2] == 0xDF && buffer[3] == 0xA3)
                        {
                            fileType = 2;
                            _base._lastError = Player.NO_ERROR;
                        }
                    }
                }
                catch (Exception e) { _base._lastError = (HResult)Marshal.GetHRForException(e); }
            }

            if (_base._lastError == Player.NO_ERROR)
            {
                _fileLength = _reader.Length;
                _reader.Position = 0;

                if (fileType == 1)
                {
                    chapters_I = GetQuickTimeChapters();
                    if (_moovStart != 0) chapters_II = GetNeroChapters();
                }
                else //if (fileType == 2)
                {
                    chapters_I = GetMatroskaChapters();
                }
            }

            if (_reader != null)
            {
                _reader.Dispose();
                _reader = null;
            }

            return (int)_base._lastError;
        }

        private MediaChapter[] GetQuickTimeChapters()
        {
            MediaChapter[] chapters = null;
            byte[] buffer = new byte[256];

            try
            {
                long index = FindAtom(MOOV_ATOM, 0, _fileLength);
                if (index > 0)
                {
                    bool found = false;

                    _moovStart = index;
                    _moovEnd = _atomEnd;
                    long moovIndex = index;
                    long moovEnd = _atomEnd;

                    long oldIndex;
                    long oldEnd;

                    int trackCounter = 0;
                    int trackNumber = 0;

                    while (!found && index < moovEnd)
                    {
                        oldEnd = _atomEnd;

                        // walk the "moov" atom
                        index = FindAtom(TRAK_ATOM, index, _atomEnd);
                        if (index > 0)
                        {
                            oldIndex = _atomEnd;
                            trackCounter++;

                            // walk the "trak" atom
                            index = FindAtom(TREF_ATOM, index, _atomEnd);
                            if (index > 0)
                            {
                                index = FindAtom(CHAP_ATOM, index, _atomEnd);
                                if (index > 0)
                                {
                                    _reader.Read(buffer, 0, 4);
                                    trackNumber = buffer[0] << 24 | buffer[1] << 16 | buffer[2] << 8 | buffer[3];
                                    found = true;

                                    index = oldIndex;
                                    _reader.Position = index;
                                    _atomEnd = oldEnd;

                                    break; // break while
                                }
                            }
                            index = oldIndex;
                            _reader.Position = index;
                            _atomEnd = oldEnd;
                        }
                        else // no more trak atoms - break not really necessary (?)
                        {
                            break;
                        }
                    }

                    if (found)
                    {
                        // get the chapters track
                        int count = trackNumber - trackCounter;
                        if (count < 0)
                        {
                            count = trackNumber;
                            index = moovIndex;
                            _reader.Position = index;
                            _atomEnd = _moovEnd;
                        }
                        for (int i = 0; i < count && index > 0; i++)
                        {
                            index = FindAtom(TRAK_ATOM, index, _atomEnd);
                            if (i < count - 1)
                            {
                                index = _atomEnd;
                                _reader.Position = index;
                                _atomEnd = _moovEnd;
                            }
                        }

                        if (index > 0)
                        {
                            // walk the "trak" atom
                            oldIndex = index;
                            oldEnd = _atomEnd;
                            index = FindAtom(TKHD_ATOM, index, _atomEnd);
                            if (index > 0)
                            {
                                index = oldIndex;
                                _reader.Position = index;
                                _atomEnd = oldEnd;
                                index = FindAtom(MDIA_ATOM, index, _atomEnd);
                                if (index > 0)
                                {
                                    oldIndex = index;

                                    // get time scale
                                    index += 20;
                                    _reader.Position = index;
                                    _reader.Read(buffer, 0, 4);
                                    int timeScale = buffer[0] << 24 | buffer[1] << 16 | buffer[2] << 8 | buffer[3];
                                    if (timeScale == 0) timeScale = 1;

                                    index = oldIndex;
                                    _reader.Position = index;
                                    index = FindAtom(MINF_ATOM, index, _atomEnd);
                                    if (index > 0)
                                    {
                                        index = FindAtom(STBL_ATOM, index, _atomEnd);
                                        if (index > 0)
                                        {
                                            oldIndex = index;
                                            oldEnd = _atomEnd;
                                            index = FindAtom(STTS_ATOM, index, _atomEnd);
                                            if (index > 0)
                                            {
                                                // get chapter start times (durations)
                                                index += 4;
                                                _reader.Position = index;
                                                _reader.Read(buffer, 0, 4);
                                                int startTimeCounter = buffer[0] << 24 | buffer[1] << 16 | buffer[2] << 8 | buffer[3];

                                                if (startTimeCounter > 0)
                                                {
                                                    int chapterCount = 1;
                                                    int startTime = 0;

                                                    List<int> startTimes = new List<int>
                                                    {
                                                        0
                                                    };
                                                    while (startTimeCounter > 1)
                                                    {
                                                        _reader.Read(buffer, 0, 8);
                                                        int sampleCount = buffer[0] << 24 | buffer[1] << 16 | buffer[2] << 8 | buffer[3];
                                                        chapterCount += sampleCount;
                                                        startTime += (buffer[4] << 24 | buffer[5] << 16 | buffer[6] << 8 | buffer[7]) / timeScale;
                                                        for (int i = 1; i <= sampleCount; i++)
                                                        {
                                                            startTimes.Add(i * startTime);
                                                        }
                                                        startTimeCounter -= sampleCount;
                                                    }

                                                    index = oldIndex;
                                                    _reader.Position = index;
                                                    _atomEnd = oldEnd;
                                                    index = FindAtom(STCO_ATOM, index, _atomEnd);
                                                    if (index > 0)
                                                    {
                                                        // get chapter titles
                                                        index += 4;
                                                        _reader.Position = index;
                                                        _reader.Read(buffer, 0, 4);
                                                        int entries = buffer[0] << 24 | buffer[1] << 16 | buffer[2] << 8 | buffer[3];
                                                        if (entries == chapterCount)
                                                        {
                                                            chapters = new MediaChapter[chapterCount];
                                                            for (int i = 0; i < chapterCount; i++)
                                                            {
                                                                _reader.Read(buffer, 0, 4);
                                                                int offset1 = buffer[0] << 24 | buffer[1] << 16 | buffer[2] << 8 | buffer[3];

                                                                index = _reader.Position;
                                                                _reader.Position = offset1;

                                                                _reader.Read(buffer, 0, 2);
                                                                int len = buffer[0] << 8 | buffer[1];

                                                                _reader.Read(buffer, 0, len);
                                                                chapters[i] = new MediaChapter
                                                                {
                                                                    _title = new string[] { Encoding.UTF8.GetString(buffer, 0, len) },
                                                                    _startTime = TimeSpan.FromSeconds(startTimes[i])
                                                                };

                                                                _reader.Position = index;
                                                            }
                                                        }
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch { chapters = null; }

            if (chapters != null)
            {
                for (int i = 1; i < chapters.Length; i++)
                {
                    if (chapters[i - 1]._endTime == TimeSpan.Zero)
                    {
                        chapters[i - 1]._endTime = chapters[i]._startTime;
                    }
                }
            }

            return chapters;
        }

        private MediaChapter[] GetNeroChapters()
        {
            MediaChapter[] chapters = null;
            byte[] buffer = new byte[256];

            long index = _moovStart; // retrieved at GetChapters
            _reader.Position = index;
            long moovEnd = _moovEnd;
            _atomEnd = moovEnd;

            try
            {
                while (index < moovEnd)
                {
                    long oldIndex;
                    long oldEnd = _atomEnd;

                    index = FindAtom(UDTA_ATOM, index, _atomEnd);
                    if (index > 0)
                    {
                        oldIndex = _atomEnd;
                        index = FindAtom(CHPL_ATOM, index, _atomEnd);
                        if (index > 0)
                        {
                            index += 5;
                            _reader.Position = index;
                            _reader.Read(buffer, 0, 4);
                            int count = buffer[0] << 24 | buffer[1] << 16 | buffer[2] << 8 | buffer[3];
                            chapters = new MediaChapter[count];
                            int length;

                            for (int i = 0; i < count; i++)
                            {
                                _reader.Read(buffer, 0, 9);

                                chapters[i] = new MediaChapter
                                {
                                    _startTime = TimeSpan.FromTicks(((long)(buffer[0] << 24 | buffer[1] << 16 | buffer[2] << 8 | buffer[3]) << 32)
                                                                    | ((buffer[4] << 24 | buffer[5] << 16 | buffer[6] << 8 | buffer[7]) & 0xffffffff))
                                };
                                length = buffer[8];
                                _reader.Read(buffer, 0, length);
                                chapters[i]._title = new string[] { Encoding.UTF8.GetString(buffer, 0, length) };
                            }
                            break; // chapters found and done
                        }
                        else // chapters not found, check for more udta atoms
                        {
                            index = oldIndex;
                            _reader.Position = index;
                            _atomEnd = oldEnd;
                        }
                    }
                    else // no more udta atoms - chapters not present and done
                    {
                        break;
                    }
                }
            }
            catch { chapters = null; }

            if (chapters != null)
            {
                for (int i = 1; i < chapters.Length; i++)
                {
                    if (chapters[i - 1]._endTime == TimeSpan.Zero)
                    {
                        chapters[i - 1]._endTime = chapters[i]._startTime;
                    }
                }
            }

            return chapters;
        }

        private long FindAtom(byte[] type, long startIndex, long endIndex)
        {
            long index = startIndex;
            long end = endIndex - 8;
            byte[] buffer = new byte[16];

            while (index < end)
            {
                _reader.Read(buffer, 0, 8);
                long atomSize = buffer[0] << 24 | buffer[1] << 16 | buffer[2] << 8 | buffer[3];
                if (atomSize < 2)
                {
                    if (atomSize == 0) atomSize = _fileLength - index;
                    else // size == 1
                    {
                        _reader.Read(buffer, 8, 8);
                        atomSize = ((long)((buffer[8] << 24) | (buffer[9] << 16) | (buffer[10] << 8) | buffer[11]) << 32)
                                   | ((buffer[12] << 24 | buffer[13] << 16 | buffer[14] << 8 | buffer[15]) & 0xffffffff);
                    }
                }

                if (buffer[4] == type[0] && buffer[5] == type[1] && buffer[6] == type[2] && buffer[7] == type[3])
                {
                    _atomEnd = index + atomSize;
                    return _reader.Position; // found
                }

                index += atomSize;
                _reader.Position = index;
            }
            return 0; // not found
        }

        // don't do: _reader.Position += GetDataSize(); etc.
        private MediaChapter[] GetMatroskaChapters()
        {
            MediaChapter[] mkvChapters = null;
            bool found = false;

            try
            {
                _buffer = new byte[256];

                // "EBML Header"
                int idLength = GetElementID();
                if (idLength == 4 && _buffer[0] == 0x1A && _buffer[1] == 0x45 && _buffer[2] == 0xDF && _buffer[3] == 0xA3)
                {
                    // skip
                    _reader.Position = GetDataSize() + _reader.Position;

                    // "Segment"
                    idLength = GetElementID();
                    if (idLength == 4 && _buffer[0] == 0x18 && _buffer[1] == 0x53 && _buffer[2] == 0x80 && _buffer[3] == 0x67)
                    {
                        GetDataSize();
                        long segmentStart = _reader.Position;

                        // "SeekHead" (Meta Seek Info)
                        idLength = GetElementID();
                        if (idLength == 4 && _buffer[0] == 0x11 && _buffer[1] == 0x4D && _buffer[2] == 0x9B && _buffer[3] == 0x74)
                        {
                            long seekEnd = GetDataSize() + _reader.Position;
                            while (_reader.Position < seekEnd)
                            {
                                // "Seek"
                                _reader.Read(_buffer, 0, 2);
                                if (_buffer[0] == 0x4D && _buffer[1] == 0xBB)
                                {
                                    long nextSeek = GetDataSize() + _reader.Position;

                                    // "SeekId"
                                    _reader.Read(_buffer, 0, 2);
                                    if (_buffer[0] == 0x53 && _buffer[1] == 0xAB)
                                    {
                                        // "Chapters"
                                        if (GetDataSize() == 4)
                                        {
                                            _reader.Read(_buffer, 0, 4);
                                            if (_buffer[0] == 0x10 && _buffer[1] == 0x43 && _buffer[2] == 0xA7 && _buffer[3] == 0x70)
                                            {
                                                found = true;
                                                break;
                                            }
                                        }
                                        _reader.Position = nextSeek;
                                    }
                                    else break;
                                }
                                else break;
                            }

                            if (found)
                            {
                                found = false;

                                // "SeekPosition" of "Chapters"
                                _reader.Read(_buffer, 0, 2);
                                if (_buffer[0] == 0x53 && _buffer[1] == 0xAC)
                                {
                                    // get position of "Chapters"
                                    long dataSize = GetDataSize();
                                    _reader.Read(_buffer, 0, (int)dataSize);

                                    long offset = 0;
                                    for (int i = 0; i < dataSize; i++) offset = (offset << 8) + _buffer[i];
                                    _reader.Position = segmentStart + offset;

                                    // "Chapters"
                                    idLength = GetElementID();
                                    if (idLength == 4 && _buffer[0] == 0x10 && _buffer[1] == 0x43 && _buffer[2] == 0xA7 && _buffer[3] == 0x70)
                                    {
                                        dataSize = GetDataSize();

                                        // "EditionEntry"
                                        _reader.Read(_buffer, 0, 2);
                                        if (_buffer[0] == 0x45 && _buffer[1] == 0xB9)
                                        {
                                            // find first "ChapterAtom"
                                            long chapterEnd = GetDataSize() + _reader.Position;
                                            while (!found && _reader.Position < chapterEnd)
                                            {
                                                idLength = GetElementID();
                                                if (idLength == 1 && _buffer[0] == 0xB6)
                                                {
                                                    _reader.Position--;
                                                    found = true;
                                                }
                                                else dataSize = GetDataSize();
                                            }

                                            if (found)
                                            {
                                                // parse all "ChapterAtom"
                                                List<MediaChapter> chapters = new List<MediaChapter>();
                                                do
                                                {
                                                    idLength = GetElementID();
                                                    if (idLength == 1 && _buffer[0] == 0xB6)
                                                    {
                                                        dataSize = GetDataSize();
                                                        long nextChapter = _reader.Position + dataSize;
                                                        MediaChapter chapter = GetChapter(dataSize);
                                                        if (chapter != null)
                                                        {
                                                            chapters.Add(chapter);
                                                            _reader.Position = nextChapter;
                                                        }
                                                        else found = false;
                                                    }
                                                    else found = false;
                                                }
                                                while (found && _reader.Position < chapterEnd);

                                                if (found) mkvChapters = chapters.ToArray();
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch { }

            if (mkvChapters != null)
            {
                for (int i = 1; i < mkvChapters.Length; i++)
                {
                    if (mkvChapters[i - 1]._endTime == TimeSpan.Zero)
                    {
                        mkvChapters[i - 1]._endTime = mkvChapters[i]._startTime;
                    }
                }
            }

            return mkvChapters;
        }

        private int GetElementID()
        {
            int length = _reader.ReadByte();

            if ((length & 0x80) != 0) length = 1;
            else if ((length & 0x40) != 0) length = 2;
            else if ((length & 0x20) != 0) length = 3;
            else length = 4;

            _reader.Position--;
            _reader.Read(_buffer, 0, length);

            return length;
        }

        private long GetDataSize()
        {
            byte mask = 0x7F;
            int length = _reader.ReadByte();
            _buffer[0] = (byte)length;

            // length == 1 less than true length
            for (int i = 0; i < 8; i++)
            {
                if ((length & 0x80) != 0)
                {
                    length = i;
                    break;
                }
                length <<= 1;
                mask >>= 1;
            }

            _buffer[0] &= mask;
            long result = _buffer[0];

            if (length > 0)
            {
                _reader.Read(_buffer, 1, length);
                for (int i = 0; i <= length; i++)
                {
                    result = (result << 8) + _buffer[i];
                }
            }
            return result;
        }

        private MediaChapter GetChapter(long length)
        {
            MediaChapter chapter = null;
            List<string> languages = new List<string>();
            List<string> titles = new List<string>();
            long startTime = 0;
            long endTime = 0;
            byte id0, id1;

            try
            {
                long chapterEnd = _reader.Position + length;
                while (_reader.Position < chapterEnd)
                {
                    long idLength = GetElementID();
                    id0 = _buffer[0];
                    long dataSize = GetDataSize();

                    if (idLength == 1 && id0 == 0x80) // chapter display
                    {
                        long displayEnd = _reader.Position + dataSize;
                        while (_reader.Position < displayEnd)
                        {
                            idLength = GetElementID();
                            id0 = _buffer[0]; id1 = _buffer[1];
                            dataSize = GetDataSize();
                            _reader.Read(_buffer, 0, (int)dataSize);

                            if (idLength == 1 && id0 == 0x85)
                            {
                                titles.Add(Encoding.UTF8.GetString(_buffer, 0, (int)dataSize));
                            }
                            else if (idLength == 2 && id0 == 0x43 && id1 == 0x7C)
                            {
                                languages.Add(Encoding.UTF8.GetString(_buffer, 0, (int)dataSize));
                            }
                        }
                    }
                    else
                    {
                        _reader.Read(_buffer, 0, (int)dataSize);
                        if (idLength == 1 && id0 == 0x91) // time start
                        {
                            startTime = 0;
                            for (int i = 0; i < dataSize; i++)
                            {
                                startTime = (startTime << 8) + _buffer[i];
                            }
                        }
                        else if (idLength == 1 && id0 == 0x92) // time end
                        {
                            endTime = 0;
                            for (int i = 0; i < dataSize; i++)
                            {
                                endTime = (endTime << 8) + _buffer[i];
                            }
                        }
                    }
                }
                if (titles.Count > 0 && titles.Count == languages.Count)
                {
                    chapter = new MediaChapter
                    {
                        _title = titles.ToArray(),
                        _language = languages.ToArray(),
                        _startTime = TimeSpan.FromTicks(startTime / 100),
                        _endTime = TimeSpan.FromTicks(endTime / 100)
                    };
                }
            }
            catch { chapter = null; }
            return chapter;
        }


        /* Get Chapters From Text File */

        /// <summary>
        /// Gets or sets the initial directory to search for chapter files with the Player.Chapters.FromFile method (default: string.Empty (the directory of the playing media)).
        /// </summary>
        public string Directory
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                if (_chapDirectory == null) _chapDirectory = string.Empty;
                return _chapDirectory;
            }
            set
            {
                _base._lastError = HResult.E_INVALIDARG;
                if (!string.IsNullOrWhiteSpace(value) && System.IO.Directory.Exists(value))
                {
                    try
                    {
                        _chapDirectory = Path.GetDirectoryName(value);
                        _base._lastError = Player.NO_ERROR;
                    }
                    catch (Exception e)
                    {
                        _chapDirectory = string.Empty;
                        _base._lastError = (HResult)Marshal.GetHRForException(e);
                    }
                }
                else _chapDirectory = string.Empty;
            }
        }

        /// <summary>
        /// Gets or sets the file name (without path and extension) of the chapters file to search for with the Player.Chapters.FromFile method (default: string.Empty (the file name of the playing media)). Reset to string.Empty after the Player.Chapters.FromFile method is used.
        /// </summary>
        public string FileName
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                if (_chapFileName == null) _chapFileName = string.Empty;
                return _chapFileName;
            }
            set
            {
                if (value != null)
                {
                    value = value.Trim();
                    _chapFileName = value;
                    _base._lastError = Player.NO_ERROR;
                }
                else
                {
                    _chapFileName = string.Empty;
                    _base._lastError = HResult.E_INVALIDARG;
                }
            }
        }

        /// <summary>
        /// Gets the number of chapters in the playing media (only applicable to chapters played with the Player.Play method).
        /// </summary>
        public int Count
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _base._chapterMode ? _base._mediaChapters.Length : 0;
            }
        }

        /// <summary>
        /// Gets or sets the (zero-based) index of the media chapter being played (only applicable to chapters played with the Player.Play method). 
        /// </summary>
        public int Index
        {
            get
            {
                if (_base._chapterMode)
                {
                    _base._lastError = Player.NO_ERROR;
                    return _base._chapterIndex;
                }
                _base._lastError = HResult.MF_E_NOT_AVAILABLE;
                return 0;
            }
            set
            {
                if (_base._chapterMode)
                {
                    if (value >= 0 && value < _base._mediaChapters.Length)
                    {
                        // set index -1 lower because AV_EndOfMedia first increases index
                        _base._chapterIndex = value - 1;
                        SetChapter();
                    }
                    else _base._lastError = HResult.MF_E_OUT_OF_RANGE;
                }
                else _base._lastError = HResult.MF_E_NOT_AVAILABLE;
            }
        }

        /// <summary>
        /// Plays the next media chapter (only applicable to chapters played with the Player.Play method).
        /// </summary>
        public int Next()
        {
            if (_base._chapterMode)
            {
                if (_base._chapterIndex < _base._mediaChapters.Length - 1)
                {
                    // set index -1 lower because AV_EndOfMedia first increases index
                    SetChapter();
                }
                else _base._lastError = HResult.MF_E_OUT_OF_RANGE;
            }
            else _base._lastError = HResult.MF_E_NOT_AVAILABLE;
            return (int)_base._lastError;
        }

        /// <summary>
        /// Plays the previous media chapter (only applicable to chapters played with the Player.Play method).
        /// </summary>
        public int Previous()
        {
            if (_base._chapterMode)
            {
                if (_base._chapterIndex > 0)
                {
                    // set index -1 lower because AV_EndOfMedia first increases index
                    _base._chapterIndex -= 2;
                    SetChapter();
                }
                else _base._lastError = HResult.MF_E_OUT_OF_RANGE;
            }
            else _base._lastError = HResult.MF_E_NOT_AVAILABLE;
            return (int)_base._lastError;
        }

        /// <summary>
        /// Returns the playing media chapter or null if not available (only applicable to chapters played with the Player.Play method).
        /// </summary>
        public MediaChapter GetChapter()
        {
            MediaChapter chapter = null;

            if (_base._chapterMode)
            {
                int index = _base._chapterIndex;
                if (index >= 0 && index < _base._mediaChapters.Length)
                {
                    chapter = _base._mediaChapters[index];
                    _base._lastError = Player.NO_ERROR;
                }
                else _base._lastError = HResult.MF_E_OUT_OF_RANGE;
            }
            else _base._lastError = HResult.MF_E_NOT_AVAILABLE;

            return chapter;
        }

        /// <summary>
        /// Returns the media chapter with the specified index or null if not available (only applicable to chapters played with the Player.Play method).
        /// </summary>
        /// <param name="index">The (zero-based) index of the chapter to be retrieved.</param>
        public MediaChapter GetChapter(int index)
        {
            MediaChapter chapter = null;

            if (_base._chapterMode)
            {
                if (index >= 0 && index < _base._mediaChapters.Length)
                {
                    chapter = _base._mediaChapters[index];
                }
                else _base._lastError = HResult.MF_E_OUT_OF_RANGE;
            }
            else _base._lastError = HResult.MF_E_NOT_AVAILABLE;

            return chapter;
        }

        /// <summary>
        /// Returns the playing media chapters or null if not available (only applicable to chapters played with the Player.Play method).
        /// </summary>
        public MediaChapter[] GetChapters()
        {
            MediaChapter[] chapters = null;

            if (_base._chapterMode)
            {
                chapters = _base._mediaChapters;
                _base._lastError = Player.NO_ERROR;
            }
            else _base._lastError = HResult.MF_E_NOT_AVAILABLE;

            return chapters;
        }

        private void SetChapter()
        {
            _base._repeatChapterCount = 0;
            bool repeat = _base._repeatChapter;
            _base._repeatChapter = false; // needed for logic at AV_EndOfMedia
            _base.AV_EndOfMedia();
            _base._repeatChapter = repeat;

            _base._lastError = Player.NO_ERROR;
        }

        /// <summary>
        /// Returns chapter information of the playing media from a chapters text file. The information is obtained from a file with the same name as the playing media file but with the extension ".chap" located in the same folder as the media file or in one of the folders contained therein. See also: Player.Chapters.Directory and Player.Chapters.FileName.
        /// </summary>
        public MediaChapter[] FromFile()
        {
            if (_base._fileMode)
            {
                string path;
                string fileName;

                if (!string.IsNullOrWhiteSpace(_chapFileName))
                {
                    fileName = _chapFileName + Player.CHAPTERS_FILE_EXTENSION;
                    _chapFileName = string.Empty;
                }
                else fileName = Path.Combine(Path.GetFileNameWithoutExtension(_base._fileName), Player.CHAPTERS_FILE_EXTENSION);

                if (!string.IsNullOrWhiteSpace(_chapDirectory)) path = _base.Subtitles_FindFile(fileName, _chapDirectory, 1);
                else path = _base.Subtitles_FindFile(fileName, Path.GetDirectoryName(_base._fileName), 1);

                return GetChaptersFile(path);
            }
            _base._lastError = HResult.MF_E_NOT_AVAILABLE;
            return null;
        }

        /// <summary>
        /// Returns chapter information from the specified chapters text file.
        /// </summary>
        /// <param name="fileName">The path and file name of the chapters text file.</param>
        public MediaChapter[] FromFile(string fileName)
        {
            if (!string.IsNullOrWhiteSpace(fileName))
            {
                return GetChaptersFile(fileName);
            }

            _base._lastError = HResult.ERROR_INVALID_NAME;
            return null;
        }

        private MediaChapter[] GetChaptersFile(string path)
        {
            MediaChapter[] chapters = null;
            _base._lastError = HResult.MF_E_NOT_AVAILABLE;

            if (path.Length > 0 && File.Exists(path) && new FileInfo(path).Length < CHAPTERS_FILE_MAX_SIZE)
            {
                try
                {
                    string[] lines = File.ReadAllLines(path);
                    int count = lines.Length;
                    if (count > 0)
                    {
                        Match match;
                        bool error      = false;
                        int trueCount   = 0;

                        chapters = new MediaChapter[count];

                        for (int i = 0; i < count && !error; i++)
                        {
                            lines[i] = lines[i].Trim();
                            if (lines[i].Length > 0 && lines[i][0] != '#')
                            {
                                match = _parser.Match(lines[i]);
                                if (match.Success)
                                {
                                    chapters[trueCount] = new MediaChapter();
                                    if (TimeSpan.TryParse(match.Groups["start"].Value, out chapters[trueCount]._startTime))
                                    {
#pragma warning disable CA1806 // Do not ignore method results
                                        if (match.Groups["end"].Value != string.Empty) TimeSpan.TryParse(match.Groups["end"].Value, out chapters[trueCount]._endTime);
#pragma warning restore CA1806 // Do not ignore method results
                                        chapters[trueCount]._title = new string[1];

                                        if (match.Groups["title"].Value != null && match.Groups["title"].Value.Length == 1 && (match.Groups["title"].Value[0] >= '0' && match.Groups["title"].Value[0] <= '9')) chapters[trueCount]._title[0] = NO_TITLE_INDICATOR;
                                        else chapters[trueCount]._title[0] = match.Groups["title"].Value;

                                        if (trueCount > 0 && chapters[trueCount - 1]._endTime == TimeSpan.Zero)
                                        {
                                            chapters[trueCount - 1]._endTime = chapters[trueCount]._startTime;
                                        }

                                        trueCount++;
                                    }
                                    else error = true;
                                }
                                else error = true;
                            }
                        }

                        if (error || trueCount == 0) chapters = null;
                        else
                        {
                            if (trueCount != count) Array.Resize(ref chapters, trueCount);
                            _base._lastError = Player.NO_ERROR;
                        }
                    }
                }
                catch { /* ignored */ }
            }

            return chapters;
        }


        // Get Base Media File

        /// <summary>
        /// Returns the path and file name of the media file (located in the same or parent(!) directory) belonging to the specified chapters text file or null if not found.
        /// </summary>
        /// <param name="fileName">The path and file name of the chapters text file.</param>
#pragma warning disable CA1822 // Mark members as static
        public string GetMediaFile(string fileName)
#pragma warning restore CA1822 // Mark members as static
        {
            string result = null;

            if (!string.IsNullOrWhiteSpace(fileName))
            {
                try
                {
                    string mediaName = Path.GetFileNameWithoutExtension(fileName);
                    if (!string.IsNullOrWhiteSpace(mediaName))
                    {
                        mediaName = mediaName.Trim();

                        // search home directory
                        string directory = Path.GetDirectoryName(fileName);
                        result = GetBaseFile(directory, mediaName);

                        // search parent directory
                        if (result == null)
                        {
                            DirectoryInfo parent = System.IO.Directory.GetParent(directory);
                            if (parent.FullName != null) result = GetBaseFile(parent.FullName, mediaName);
                        }
                    }
                }
                catch { /* ignored */ }
            }
            return result;
        }

        private static string GetBaseFile(string directory, string fileName)
        {
            string result = null;
            try
            {
                IEnumerable<string> baseFiles = System.IO.Directory.EnumerateFiles(directory, fileName + ".*");
                foreach (string baseFile in baseFiles)
                {
                    string extension = Path.GetExtension(baseFile);
                    if (IGNORE_EXTENSIONS.IndexOf(extension, StringComparison.OrdinalIgnoreCase) == -1)
                    {
                        result = Path.Combine(directory, baseFile);
                        break;
                    }
                }
            }
            catch { /* ignored */ }
            return result;
        }


        /* Write Chapters To Text File */

        /// <summary>
        /// Saves the specified chapters to the specified chapters text file. If the chapters text file already exists, it is overwritten.
        /// </summary>
        /// <param name="fileName">The path and file name of the chapters text file. The file extension is set to ".chap".</param>
        /// <param name="chapters">The chapters to save to the chapters text file.</param>
        public int ToFile(string fileName, MediaChapter[] chapters)
        {
            return ToFile(fileName, chapters, 0);
        }

        /// <summary>
        /// Saves the specified chapters to the specified chapters text file. If the chapters text file already exists, it is overwritten.
        /// </summary>
        /// <param name="fileName">The path and file name of the chapters text file. The file extension is set to ".chap".</param>
        /// <param name="chapters">The chapters to save to the chapters text file.</param>
        /// <param name="titleIndex">The index of the chapter title to save to the chapters text file (if multiple languages are used, default: 0).</param>
        public int ToFile(string fileName, MediaChapter[] chapters, int titleIndex)
        {
            if (string.IsNullOrWhiteSpace(fileName) || chapters == null) _base._lastError = HResult.E_INVALIDARG;
            else
            {
                StringBuilder text = new StringBuilder(1024);
                try
                {
                    text.AppendLine("# " + Path.GetFileNameWithoutExtension(fileName));
                    int initLength = text.Length;
                    for (int i = 0; i < chapters.Length; i++)
                    {
                        if (chapters[i] != null)
                        {
                            text.Append(chapters[i]._startTime.ToString(Player.CHAPTERs_TIME_FORMAT));
                            if (chapters[i]._endTime != TimeSpan.Zero) text.Append(" - ").Append(chapters[i]._endTime.ToString(Player.CHAPTERs_TIME_FORMAT));
                            if (chapters[i]._title == null || string.IsNullOrWhiteSpace(chapters[i]._title[0])) text.AppendLine(" #");
                            else
                            {
                                if (titleIndex < 0 || titleIndex >= chapters[i]._title.Length) text.Append(' ').AppendLine(chapters[i]._title[0]);
                                else text.Append(' ').AppendLine(chapters[i]._title[titleIndex]);
                            }
                        }
                    }
                    if (text.Length > initLength)
                    {
                        File.WriteAllText(Path.ChangeExtension(fileName, Player.CHAPTERS_FILE_EXTENSION), text.ToString());
                        _base._lastError = Player.NO_ERROR;
                    }
                    else _base._lastError = HResult.E_INVALIDARG;
                }
                catch (Exception e) { _base._lastError = (HResult)Marshal.GetHRForException(e); }
                text.Length = 0;
            }
            return (int)_base._lastError;
        }
    }
}