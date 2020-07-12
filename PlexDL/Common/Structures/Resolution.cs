namespace PlexDL.Common.Structures
{
    public class Resolution
    {
        public int Width { get; set; } = 0;
        public int Height { get; set; } = 0;
        public string Framerate { get; set; } = @"Unknown";
        public string VideoStandard { get; set; } = @"";

        public string ResolutionString(bool incStandard = true)
        {
            var std = string.IsNullOrEmpty(VideoStandard) ? StandardFromFrameHeight() : VideoStandard;
            if (incStandard && !string.Equals(std, string.Empty))
                return Width + "x" + Height + " (" + std + ")";
            return Width + "x" + Height;
        }

        public string ResolutionShorthand()
        {
            return Height + "p";
        }

        public string StandardFromFrameHeight()
        {
            return ResolutionStandards.StdFromHeight(Height.ToString());
        }
    }
}