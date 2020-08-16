﻿using LogDel.Enums;
using System;

namespace LogDel
{
    public static class Vars
    {
        public static LogSecurity Protected { get; set; } = LogSecurity.Unprotected;
        public static string LogDirectory { get; set; } = $"{Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)}\\.plexdl\\logs";
    }
}