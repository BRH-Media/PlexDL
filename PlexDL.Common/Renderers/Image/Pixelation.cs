using System.Drawing;

namespace PlexDL.Common.Renderers.Image
{
    /// <summary>
    /// Provides methods to pixelate a Bitmap image
    /// </summary>
    public static class Pixelation
    {
        /// <summary>
        /// Pixelate the provided bitmap image based on a rectangle object
        /// </summary>
        /// <param name="image">The image to pixelate</param>
        /// <param name="blurSize">The size of the pixelation blur</param>
        /// <param name="rectangle">The rectangular bitmap canvas</param>
        /// <returns>A new pixelated bitmap</returns>
        public static Bitmap Pixelate(Bitmap image, Rectangle rectangle, int blurSize)
        {
            var pixelated = new Bitmap(image.Width, image.Height);

            // make an exact copy of the bitmap provided
            using (var graphics = Graphics.FromImage(pixelated))
            {
                graphics.DrawImage(image, new Rectangle(0, 0, image.Width, image.Height),
                    new Rectangle(0, 0, image.Width, image.Height), GraphicsUnit.Pixel);
            }

            // look at every pixel in the rectangle while making sure we're within the image bounds
            for (var xx = rectangle.X; xx < rectangle.X + rectangle.Width && xx < image.Width; xx += blurSize)
                for (var yy = rectangle.Y; yy < rectangle.Y + rectangle.Height && yy < image.Height; yy += blurSize)
                {
                    var offsetX = blurSize / 2;
                    var offsetY = blurSize / 2;

                    // make sure that the offset is within the boundary of the image
                    while (xx + offsetX >= image.Width) offsetX--;
                    while (yy + offsetY >= image.Height) offsetY--;

                    // get the pixel color in the center of the soon to be pixelated area
                    var pixel = pixelated.GetPixel(xx + offsetX, yy + offsetY);

                    // for each pixel in the pixelate size, set it to the center color
                    for (var x = xx; x < xx + blurSize && x < image.Width; x++)
                        for (var y = yy; y < yy + blurSize && y < image.Height; y++)
                            pixelated.SetPixel(x, y, pixel);
                }

            return pixelated;
        }

        /// <summary>
        /// Pixelate the provided bitmap image based on its existing sizing information
        /// </summary>
        /// <param name="image">The image to pixelate</param>
        /// <param name="blurSize">The size of the pixelation blur</param>
        /// <returns>A new pixelated bitmap</returns>
        public static Bitmap Pixelate(Bitmap image, int blurSize) =>
            Pixelate(image, new Rectangle(0, 0, image.Width, image.Height), blurSize);
    }
}