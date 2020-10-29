using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace PlexDL.Common.Components
{
    public class DoubleBufferedForm : Form
    {
        [Category(@"Behavior")]
        [DisplayName(@"DoubleBuffered")]
        [Description(@"Indicates whether the control will be double buffered.")]
        public new bool DoubleBuffered { get; set; } = true;

        [Category(@"Behavior")]
        [DisplayName(@"DoubleBufferedOverride")]
        [Description(@"Performs a graphics repaint by slightly changing the dimensions. Avoids clashes with Media Foundation.")]
        public bool DoubleBufferedHack { get; set; } = false;

        /// <summary>
        /// This is only set to false on runtime; useful for avoiding double buffering while designing
        /// </summary>
        internal bool InDesignMode { get; set; } = true;

        //double-buffering override for the entire form
        protected override CreateParams CreateParams
        {
            get
            {
                //already existing parameters
                var cp = base.CreateParams;

                //turn on WS_EX_COMPOSITED only if enabled by the bool
                if (DoubleBuffered && !InDesignMode)
                    cp.ExStyle |= 0x02000000;

                //return parameters (existing and modified)
                return cp;
            }
        }

        //standard constructor
        public DoubleBufferedForm()
        {
            //on-shown event
            Shown += DoubleBufferGraphicsHack;
        }

        private void DoubleBufferGraphicsHack(object sender, EventArgs e)
        {
            //on runtime, this gets fired. The designer cannot fire events.
            InDesignMode = false;

            var senderValid = sender.GetType() == typeof(DoubleBufferedForm);
            if (senderValid && DoubleBufferedHack && DoubleBuffered)
            {
                Width++;
                Width--;
            }
        }
    }
}