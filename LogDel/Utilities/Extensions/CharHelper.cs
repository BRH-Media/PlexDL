using System.Collections.Generic;
using System.Linq;

namespace LogDel.Utilities.Extensions
{
    public static class CharHelper
	{
		public static string CleanLogDel(this string line)
		{
			var clean = line;
			char[] bannedChars =
			{
				'#', '!', '\n' //newline causes parsing problems
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
		{
			return line.Select(CleanLogDel).ToArray();
		}
	}
}