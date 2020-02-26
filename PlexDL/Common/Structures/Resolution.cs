namespace PlexDL.Common.Structures
{
    public class Resolution
    {
        public int Width { get; set; } = 0;
        public int Height { get; set; } = 0;

        public string ResolutionString()
        {
            return Width + "x" + Height;
        }

        public string ResolutionShorthand()
        {
            return Height + "p";
        }
    }
}