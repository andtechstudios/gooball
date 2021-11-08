using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Gooball
{

	class ArgumentUtility
	{

		public static void SplitArgs(string[] args, out string[] leftArgs, out string[] rightArgs, string terminator = "--")
		{
			int index = Array.IndexOf(args, terminator);
			int count = index == -1 ? args.Length : index;

			leftArgs = args.Take(count).ToArray();
			rightArgs = args.Skip(count + 1).ToArray();
		}

		public static bool TryGetOption(string[] args, string switchText, out string value)
		{
			var regex = new Regex($"(-|--)?{switchText}", RegexOptions.IgnoreCase);
			for (int i = 0; i < args.Length; i++)
			{
				if (regex.IsMatch(args[i]))
				{
					value = args[i + 1];
					return true;
				}
			}

			value = string.Empty;
			return false;
		}

		public static bool HasFlag(string[] args, string switchText)
		{
			var regex = new Regex($"(-|--)?{switchText}", RegexOptions.IgnoreCase);
			for (int i = 0; i < args.Length; i++)
			{
				if (regex.IsMatch(args[i]))
				{
					return true;
				}
			}

			return false;
		}
	}
}
