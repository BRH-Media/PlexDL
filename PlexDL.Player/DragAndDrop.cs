using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace PlexDL.Player
{
    /// <summary>
    /// A class that is used to group together the Drag-and-drop methods of the PVS.MediaPlayer.Player class.
    /// </summary>
    [CLSCompliant(true)]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public sealed class DragAndDrop : HideObjectMembers
    {
        #region Fields (DragAndDrop Class)

        private Player              _base;
        private IDropTargetHelper   _dropHelper;
        private Win32Point          _location;

        #endregion

        internal DragAndDrop(Player player)
        {
            _base = player;
        }

        /// <summary>
        /// Enables drag-and-drop ghost images to be displayed when dragged over the specified form. Can be used when no actual drag-and-drop functionality is implemented (otherwise use Player.DragAndDrop.DragEnter etc.).
        /// </summary>
        /// <param name="form">The form on which drag-and-drop ghost images should be enabled.</param>
        public int Add(Form form)
        {
            if (form != null)
            {
                form.AllowDrop = true;

                form.DragEnter += Form_DragEnter;
                form.DragOver += Form_DragOver;
                form.DragLeave += Form_DragLeave;
                form.DragDrop += Form_DragDrop;

                _base._lastError =Player.NO_ERROR;
            }
            else _base._lastError = HResult.E_INVALIDARG;
            return (int)_base._lastError;
        }

        /// <summary>
        /// Disables drag-and-drop ghost images to be displayed when dragged over the specified form (if enabled with Player.DragAndDrop.Add).
        /// </summary>
        /// <param name="form">The form on which drag-and-drop ghost images should be disabled.</param>
        public int Remove(Form form)
        {
            if (form != null)
            {
                form.AllowDrop = false;

                form.DragEnter -= Form_DragEnter;
                form.DragOver -= Form_DragOver;
                form.DragLeave -= Form_DragLeave;
                form.DragDrop -= Form_DragDrop;

                _base._lastError = Player.NO_ERROR;
            }
            else _base._lastError = HResult.E_INVALIDARG;
            return (int)_base._lastError;
        }

        private void Form_DragEnter(object sender, DragEventArgs e) { DragEnter(e); }
        private void Form_DragOver(object sender, DragEventArgs e) { DragOver(e); }
        private void Form_DragLeave(object sender, EventArgs e) { DragLeave(); }
        private void Form_DragDrop(object sender, DragEventArgs e) { DragDrop(e); }

        /// <summary>
        /// Enables drag-and-drop ghost images to be displayed when dragged over a control (for example, a form). Add to the OnDragEnter method (or DragEnter event handler) of a control. Always use all 4 Player.DragAndDrop methods: DragEnter, DragOver, DragLeave and DragDrop.
        /// </summary>
        /// <param name="e">The event arguments received with the OnDragEnter method or the DragEnter event handler (just pass them to this method).</param>
        public void DragEnter(DragEventArgs e)
        {
            if (e != null)
            {
                _location.x = e.X; _location.y = e.Y;
                _dropHelper = (IDropTargetHelper)new DragDropHelper();
                _dropHelper.DragEnter(IntPtr.Zero, (System.Runtime.InteropServices.ComTypes.IDataObject)e.Data, ref _location, (int)e.Effect);

                //_base._lastError = Player.NO_ERROR;
            }
            //else _base._lastError = HResult.E_INVALIDARG;
            //return (int)_base._lastError;
        }

        /// <summary>
        /// Enables drag-and-drop ghost images to be displayed when dragged over a control (for example, a form). Add to the OnDragOver method (or DragOver event handler) of a control. Always use all 4 Player.DragAndDrop methods: DragEnter, DragOver, DragLeave and DragDrop.
        /// </summary>
        /// <param name="e">The event arguments received with the OnDragOver method or the DragOver event handler (just pass them to this method).</param>
        public void DragOver(DragEventArgs e)
        {
            if (e != null)
            {
                _location.x = e.X; _location.y = e.Y;
                _dropHelper.DragOver(ref _location, (int)e.Effect);

                //_base._lastError = Player.NO_ERROR;
            }
            //else _base._lastError = HResult.E_INVALIDARG;
            //return (int)_base._lastError;
        }

        /// <summary>
        /// Enables drag-and-drop ghost images to be displayed when dragged over a control (for example, a form). Add to the OnDragLeave method (or DragLeave event handler) of a control. Always use all 4 Player.DragAndDrop methods: DragEnter, DragOver, DragLeave and DragDrop.
        /// </summary>
        public void DragLeave()
        {
            _dropHelper.DragLeave();
            Marshal.ReleaseComObject(_dropHelper);
            _dropHelper = null;

            //_base._lastError = Player.NO_ERROR;
            //return (int)_base._lastError;
        }

        /// <summary>
        /// Enables drag-and-drop ghost images to be displayed when dragged over a control (for example, a form). Add to the OnDragDrop method (or DragDrop event handler) of a control. Always use all 4 Player.DragAndDrop methods: DragEnter, DragOver, DragLeave and DragDrop.
        /// </summary>
        /// <param name="e">The event arguments received with the OnDragDrop method or the DragDrop event handler (just pass them to this method).</param>
        public void DragDrop(DragEventArgs e)
        {
            if (e != null)
            {
                _location.x = e.X; _location.y = e.Y;
                _dropHelper.Drop((System.Runtime.InteropServices.ComTypes.IDataObject)e.Data, ref _location, (int)e.Effect);
                Marshal.ReleaseComObject(_dropHelper);
                _dropHelper = null;

                //_base._lastError = Player.NO_ERROR;
            }
            //else _base._lastError = HResult.E_INVALIDARG;
            //return (int)_base._lastError;
        }

    }
}