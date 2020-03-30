namespace PlexDL.Common
{
    public static class DefaultStringVariables
    {
        public static StringVariable ContentTitle { get; set; } = new StringVariable("%TITLE%", null);
        public static StringVariable FileName { get; set; } = new StringVariable("%FILE%", null);
        public static StringVariable TokenHash { get; set; } = new StringVariable("%TOKEN_MD5%", null);
        public static StringVariable ServerHash { get; set; } = new StringVariable("%SERVER_MD5%", null);
        public static StringVariable ServerIp { get; set; } = new StringVariable("%SERVER_IP%", null);
        public static StringVariable ServerPort { get; set; } = new StringVariable("%SERVER_PORT%", null);
    }
}