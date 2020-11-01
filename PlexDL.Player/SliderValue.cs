using System;
using System.Drawing;
using System.Windows.Forms;

namespace PlexDL.Player
{
    /// <summary>
    /// A static class that provides location information for values on a slider (trackbar).
    /// </summary>
    [CLSCompliant(true)]
    public static class SliderValue
    {
        #region Fields (Slider Value Class))

        // standard .Net TrackBar track margins (pixels between border and begin/end of track)
        private const int SLIDER_LEFT_MARGIN    = 13;
        private const int SLIDER_RIGHT_MARGIN   = 14;
        private const int SLIDER_TOP_MARGIN     = 13;
        private const int SLIDER_BOTTOM_MARGIN  = 14;

        #endregion

        /// <summary>
        /// Returns the slider value at the specified location on the specified slider (trackbar).
        /// </summary>
        /// <param name="slider">The slider whose value should be obtained.</param>
        /// <param name="location">The relative x- and y-coordinates on the slider.</param>
        public static int FromPoint(TrackBar slider, Point location)
        {
            return FromPoint(slider, location.X, location.Y);
        }

        /// <summary>
        /// Returns the slider value at the specified location on the specified slider (trackbar).
        /// </summary>
        /// <param name="slider">The slider whose value should be obtained.</param>
        /// <param name="x">The relative x-coordinate on the slider (for horizontal oriented sliders).</param>
        /// <param name="y">The relative y-coordinate on the slider (for vertical oriented sliders).</param>
        public static int FromPoint(TrackBar slider, int x, int y)
        {
            if (slider == null) return 0;

            float pos;
            if (slider.Orientation == Orientation.Horizontal)
            {
                if (x <= SLIDER_LEFT_MARGIN) pos = 0;
                else if (x >= slider.Width - SLIDER_LEFT_MARGIN) pos = 1;
                else pos = (float)(x - SLIDER_LEFT_MARGIN) / (slider.Width - (SLIDER_LEFT_MARGIN + SLIDER_RIGHT_MARGIN));
            }
            else
            {
                if (y <= SLIDER_TOP_MARGIN) pos = 1;
                else if (y >= slider.Height - SLIDER_TOP_MARGIN) pos = 0;
                else pos = 1 - (float)(y - SLIDER_TOP_MARGIN) / (slider.Height - (SLIDER_TOP_MARGIN + SLIDER_BOTTOM_MARGIN));
            }
            //pos = (pos * (slider.Maximum - slider.Minimum)) + slider.Minimum;
            return (int)(pos * (slider.Maximum - slider.Minimum)) + slider.Minimum;
        }

        /// <summary>
        /// Returns the location of the specified value on the specified slider (trackbar).
        /// </summary>
        /// /// <param name="slider">The slider whose value location should be obtained.</param>
        /// <param name="value">The value of the slider.</param>
        public static Point ToPoint(TrackBar slider, int value)
        {
            Point result = Point.Empty;
            if (slider != null)
            {
                double pos = 0;
                if (value > slider.Minimum)
                {
                    if (value >= slider.Maximum) pos = 1;
                    else pos = (double)(value - slider.Minimum) / (slider.Maximum - slider.Minimum);
                }
                if (slider.Orientation == Orientation.Horizontal) result.X = (int)(pos * (slider.Width - (SLIDER_LEFT_MARGIN + SLIDER_RIGHT_MARGIN)) + 0.5) + SLIDER_LEFT_MARGIN;
                else result.Y = (int)(pos * (slider.Height - (SLIDER_TOP_MARGIN + SLIDER_BOTTOM_MARGIN)) + 0.5) + SLIDER_TOP_MARGIN;
            }
            return result;
        }
    }
}