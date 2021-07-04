using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Forms;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that is used to group together the Taskbar Progress methods and properties of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class TaskbarProgress : HideObjectMembers
    {
        #region Fields (TaskbarProgress Class)

        private Player                      _base;
        private List<Form>                  _taskbarItems;
        internal TaskbarProgressMode        _progressMode;
        private TaskbarProgressState        _taskbarState = TaskbarProgressState.NoProgress;

        #endregion

        internal TaskbarProgress(Player player)
        {
            _base = player;

            if (!Player._taskbarProgressEnabled)
            {
                Player.TaskbarInstance = (TaskbarIndicator.ITaskbarList3)new TaskbarIndicator.TaskbarInstance();
                Player._taskbarProgressEnabled = true;
            }
            _taskbarItems    = new List<Form>(4);
            _progressMode    = TaskbarProgressMode.Progress;

            _base._lastError = Player.NO_ERROR;
        }

        #region Public - Taskbar Progress methods and properties

        /// <summary>
        /// Adds a taskbar progress indicator to the player.
        /// </summary>
        /// <param name="form">The form whose taskbar item should be added as a progress indicator.</param>
        public int Add(Form form)
        {
            if (Player._taskbarProgressEnabled)
            {
                lock (_taskbarItems)
                {
                    if (form != null)
                    {
                        // check if already exists
                        bool exists = false;
                        for (int i = 0; i < _taskbarItems.Count; i++)
                        {
                            if (_taskbarItems[i] == form)
                            {
                                exists = true;
                                break;
                            }
                        }

                        if (!exists)
                        {
                            _taskbarItems.Add(form);
                            if (_base._playing)
                            {
                                if (_base._paused)
                                {
                                    Player.TaskbarInstance.SetProgressState(form.Handle, TaskbarProgressState.Paused);
                                    SetValue(_base.PositionX);
                                }
                                else if (!_base._fileMode || _base._liveStreamMode)
                                {
                                    Player.TaskbarInstance.SetProgressState(form.Handle, TaskbarProgressState.Indeterminate);
                                }
                            }
                            _base._hasTaskbarProgress = true;
                            _base.StartMainTimerCheck();
                        }
                        _base._lastError = Player.NO_ERROR;
                    }
                    else _base._lastError = HResult.E_INVALIDARG;
                }
            }
            else _base._lastError = HResult.MF_E_NOT_AVAILABLE;
            return (int)_base._lastError;
        }

        /// <summary>
        /// Removes a taskbar progress indicator from the player.
        /// </summary>
        /// <param name="form">The form whose taskbar progress indicator should be removed.</param>
        public int Remove(Form form)
        {
            if (Player._taskbarProgressEnabled)
            {
                if (_base._hasTaskbarProgress && form != null)
                {
                    lock (_taskbarItems)
                    {
                        for (int index = _taskbarItems.Count - 1; index >= 0; index--)
                        {
                            if (_taskbarItems[index] == form || _taskbarItems[index] == null)
                            {
                                if (_taskbarItems[index] != null)
                                {
                                    Player.TaskbarInstance.SetProgressState(_taskbarItems[index].Handle, TaskbarProgressState.NoProgress);
                                }
                                _taskbarItems.RemoveAt(index);
                            }
                        }

                        if (_taskbarItems.Count == 0)
                        {
                            _taskbarItems = new List<Form>(4);
                        }
                    }
                }
                _base._lastError = Player.NO_ERROR;
            }
            else _base._lastError = HResult.MF_E_NOT_AVAILABLE;
            return (int)_base._lastError;
        }

        /// <summary>
        /// Removes all taskbar progress indicators from the player. Same as Player.TaskbarProgress.Clear.
        /// </summary>
        public int RemoveAll()
        {
            if (Player._taskbarProgressEnabled)
            {
                if (_base._hasTaskbarProgress)
                {
                    _base._hasTaskbarProgress = false;
                    _base.StopMainTimerCheck();
                    SetState(TaskbarProgressState.NoProgress);
                    _taskbarItems    = new List<Form>(4);
                }
                _base._lastError = Player.NO_ERROR;
            }
            else _base._lastError = HResult.MF_E_NOT_AVAILABLE;
            return (int)_base._lastError;
        }

        /// <summary>
        /// Removes all taskbar progress indicators from the player. Same as Player.TaskbarProgress.RemoveAll.
        /// </summary>
        public int Clear()
        {
            return RemoveAll();
        }

        /// <summary>
        /// Gets the number of taskbar progress indicators of the player.
        /// </summary>
        public int Count
        {
            get
            {
                int count = 0;

                if (_taskbarItems == null)      count = _taskbarItems.Count;

                _base._lastError = Player.NO_ERROR;
                return count;
            }
        }

        /// <summary>
        /// Gets a list of the forms that have a taskbar progress indicator of the player.
        /// </summary>
        public Form[] List
        {
            get
            {
                Form[] result = null;
                if (_taskbarItems != null)
                {
                    int count = _taskbarItems.Count;
                    result = new Form[count];
                    for (int i = 0; i < count; i++)
                    {
                        result[i] = _taskbarItems[i];
                    }
                    _base._lastError = Player.NO_ERROR;
                }
                else _base._lastError = HResult.MF_E_NOT_AVAILABLE;
                return result;
            }
        }

        /// <summary>
        /// Gets or sets the mode (track or progress) of the player's taskbar progress indicator (default: TaskbarProgressMode.Progress).
        /// </summary>
        public TaskbarProgressMode Mode
        {
            get
            {
                _base._lastError = Player.NO_ERROR;
                return _progressMode;
            }
            set
            {
                _progressMode = value;
                if (_base._hasTaskbarProgress && _base.Playing && _base._fileMode) SetValue(_base.PositionX);
                _base._lastError = Player.NO_ERROR;
            }
        }

        /// <summary>
        /// Gets or sets a value that indicates how the player's progress indicator is displayed in the taskbar button. Changes when the player's playback status changes.
        /// </summary>
        public TaskbarProgressState State
        {
            get
            {
                if (_base._hasTaskbarProgress)
                {
                    _base._lastError = Player.NO_ERROR;
                    return _taskbarState;
                }
                else
                {
                    _base._lastError = HResult.MF_E_NOT_AVAILABLE;
                    return TaskbarProgressState.NoProgress;
                }
            }
            set
            {
                if (_base._hasTaskbarProgress)
                {
                    SetState(value);
                    if (!_base._fileMode || _base._liveStreamMode) SetValue(1);
                    _base._lastError = Player.NO_ERROR;
                }
                else _base._lastError = HResult.MF_E_NOT_AVAILABLE;
            }
        }

        /// <summary>
        /// Updates all taskbar progress indicators of the player. Only for use in special cases.
        /// </summary>
        public int Update()
        {
            if (_base._hasTaskbarProgress && _base.Playing)
            {
                SetValue(_base.PositionX);
                if (_base._paused) SetState(TaskbarProgressState.Paused);
            }
            _base._lastError = Player.NO_ERROR;
            return (int)_base._lastError;
        }

        #endregion

        #region Private - SetValue / SetState

        internal void SetValue(long progressValue)
        {
            long pos = progressValue;
            long total;

            if (_taskbarState == TaskbarProgressState.Indeterminate) return;

            if (!_base._fileMode || _base._liveStreamMode)
            {
                pos     = 1;
                total   = 1;
            }
            else
            {
                if (_progressMode == TaskbarProgressMode.Track)
                {
                    total = _base._mediaLength;
                }
                else
                {
                    if (pos < _base._startTime)
                    {
                        total = _base._stopTime == 0 ? _base._mediaLength : _base._stopTime;
                    }
                    else
                    {
                        if (_base._stopTime == 0) total = _base._mediaLength - _base._startTime;
                        else
                        {
                            if (pos <= _base._stopTime) total = _base._stopTime - _base._startTime;
                            else total = _base._mediaLength - _base._startTime;
                        }
                        pos -= _base._startTime;
                    }
                }
            }

            for (int i = 0; i < _taskbarItems.Count; i++)
            {
                if (_taskbarItems[i] != null)
                {
                    try { Player.TaskbarInstance.SetProgressValue(_taskbarItems[i].Handle, (ulong)pos, (ulong)total); }
                    catch { _taskbarItems[i] = null; }
                }
            }
        }

        internal void SetState(TaskbarProgressState taskbarState)
        {
            //if (_taskbarItems.Count > 0)
            {
                _taskbarState = taskbarState;
                for (int i = 0; i < _taskbarItems.Count; i++)
                {
                    if (_taskbarItems[i] != null)
                    {
                        try { Player.TaskbarInstance.SetProgressState(_taskbarItems[i].Handle, taskbarState); }
                        catch { _taskbarItems[i] = null; }
                    }
                }
            }
        }

        #endregion
    }
}