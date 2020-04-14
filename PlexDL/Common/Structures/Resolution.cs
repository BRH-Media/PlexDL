namespace PlexDL.Common.Structures
{
    public class Resolution
    {
        public int Width { get; set; } = 0;
        public int Height { get; set; } = 0;

        public string ResolutionString(bool incStandard = true)
        {
            string std = StandardFromFrameHeight();
            if (incStandard && !string.Equals(std, string.Empty))
                return Width + "x" + Height + " (" + std + ")";
            else
                return Width + "x" + Height;
        }

        public string ResolutionShorthand()
        {
            return Height + "p";
        }

        public string StandardFromFrameHeight()
        {
            if (Height > 0)
            {
                foreach (string[] s in ResolutionStandards.Standards)
                    if (string.Equals(Height.ToString(), s[0]))
                        return s[1];
                return "";
            }
            else
                return "";
        }
    }
}