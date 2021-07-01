using System.Collections.Generic;
using System.Linq;

// ReSharper disable ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator

namespace LogDel.Utilities.Extensions
{
    public static class CharacterHelper
    {
        public static string CleanLogDel(this string line)
        {
            var clean = line;

            char[] bannedChars =
            {
                '#', '!', '\n', '\r' //newline characters cause parsing problems
			};

            foreach (var c in line)
                if (bannedChars.Contains(c))
                {
                    var index = clean.IndexOf(c);
                    clean = clean.Remove(index, 1);
                }

            return clean;
        }

        public static string[] CleanLogDel(this IEnumerable<string> line)
            => line.Select(CleanLogDel).ToArray();
    }
}