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

        private Player _base;
        private List<TaskbarItem> _taskbarItems;
        internal TaskbarProgressMode _progressMode;

        private class TaskbarItem
        {
            internal Form Form;
            internal IntPtr Handle;
        }

        #endregion Fields (TaskbarProgress Class)

        internal TaskbarProgress(Player player)
        {
            _base = player;

            if (!Player._taskbarProgressEnabled)
            {
                _taskbarItems = new List<TaskbarItem>(4);
                Player.TaskbarInstance = (TaskbarIndicator.ITaskbarList3)new TaskbarIndicator.TaskbarInstance();
                Player._taskbarProgressEnabled = true;
                _progressMode = TaskbarProgressMode.Progress;

                _base._lastError = Player.NO_ERROR;
            }
            else
            {
                _taskbarItems = new List<TaskbarItem>(4);
            }
        }

        #region Public - Taskbar Progress methods and properties

        /// <summary>
        /// Adds a taskbar progress indicator to the player.
        /// </summary>
        /// <param name="form">The form whose taskbar item is added as a progress indicator. Multiple forms and duplicates are allowed.</param>
        public int Add(Form form)
        {
            if (Player._taskbarProgressEnabled)
            {
                lock (_taskbarItems)
                {
                    if (form != null) // && form.ShowInTaskbar)
                    {
                        // check if already exists
                        bool exists = false;
                        for (int i = 0; i < _taskbarItems.Count; i++)
                        {
                            if (_taskbarItems[i].Form == form)
                            {
                                exists = true;
                                break;
                            }
                        }

                        if (!exists)
                        {
                            TaskbarItem item = new TaskbarItem();
                            item.Form = form;
                            item.Handle = form.Handle;

                            _taskbarItems.Add(item);

                            if (_base._playing)
                            {
                                if (_base._paused)
                                {
                                    Player.TaskbarInstance.SetProgressState(item.Handle, TaskbarStates.Paused);
                                    SetValue(_base.PositionX);
                                }
                                else if (!_base._fileMode)
                                {
                                    Player.TaskbarInstance.SetProgressState(item.Handle, TaskbarStates.Indeterminate);
                                }
                            }

                            _base._hasTaskbarProgress = true;
                            _base.StartMainTimerCheck();
                        }

                        _base._lastError = Player.NO_ERROR;
                    }
                    else _base._lastError = HResult.S_FALSE;
                }
            }
            else _base._lastError = HResult.S_FALSE;

            return (int)_base._lastError;
        }

        /// <summary>
        /// Removes a taskbar progress indicator from the player.
        /// </summary>
        /// <param name="form">The form whose taskbar progress indicator is removed.</param>
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
                            if (_taskbarItems[index].Form == form || _taskbarItems[index].Form == null)
                            {
                                if (_taskbarItems[index].Form != null)
                                {
                                    Player.TaskbarInstance.SetProgressState(_taskbarItems[index].Handle, TaskbarStates.NoProgress);
                                }

                                _taskbarItems.RemoveAt(index);
                            }
                        }

                        if (_taskbarItems.Count == 0)
                        {
                            _base._hasTaskbarProgress = false;
                            _base.StopMainTimerCheck();
                            _taskbarItems = new List<TaskbarItem>(4);
                        }
                    }
                }

                _base._lastError = Player.NO_ERROR;
            }
            else _base._lastError = HResult.S_FALSE;

            return (int)_base._lastError;
        }

        /// <summary>
        /// Removes all taskbar progress indicators from the player.
        /// </summary>
        public int RemoveAll()
        {
            if (Player._taskbarProgressEnabled)
            {
                if (_base._hasTaskbarProgress)
                {
                    _base._hasTaskbarProgress = false;
                    _base.StopMainTimerCheck();
                    SetState(TaskbarStates.NoProgress);
                    _taskbarItems = new List<TaskbarItem>(4);
                }

                _base._lastError = Player.NO_ERROR;
            }
            else _base._lastError = HResult.S_FALSE;

            return (int)_base._lastError;
        }

        /// <summary>
        /// Removes all taskbar progress indicators from the player.
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
                _base._lastError = Player.NO_ERROR;
                return _taskbarItems == null ? 0 : _taskbarItems.Count;
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
                        result[i] = _taskbarItems[i].Form;
                    }

                    _base._lastError = Player.NO_ERROR;
                }
                else _base._lastError = HResult.S_FALSE;

                return result;
            }
        }

        /// <summary>
        /// Gets or sets the mode (track or progress) of the taskbar progress indicator (default: TaskbarProgressMode.Progress).
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

        #endregion Public - Taskbar Progress methods and properties

        #region Private - SetValue / SetState

        internal void SetValue(long progressValue)
        {
            long pos = progressValue;
            long total;

            if (!_base._fileMode)
            {
                total = 1;
                pos = 1;
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
                if (_taskbarItems[i].Form != null)
                {
                    try { Player.TaskbarInstance.SetProgressValue(_taskbarItems[i].Handle, (ulong)pos, (ulong)total); }
                    catch { _taskbarItems[i].Form = null; }
                }
            }
        }

        internal void SetState(TaskbarStates taskbarState)
        {
            for (int i = 0; i < _taskbarItems.Count; i++)
            {
                if (_taskbarItems[i].Form != null)
                {
                    try { Player.TaskbarInstance.SetProgressState(_taskbarItems[i].Handle, taskbarState); }
                    catch { _taskbarItems[i].Form = null; }
                }
            }
        }

        #endregion Private - SetValue / SetState
    }
}