using System;
using System.Collections.Generic;
using System.Linq;

namespace Gooball
{

	internal class ArgumentUtility
	{

		internal static void SplitArgs(IList<string> args, out string[] leftArgs, out string[] rightArgs)
		{
			int index = args.IndexOf("--");
			int count = index == -1 ? args.Count : index;

			leftArgs = args.Take(count).ToArray();
			rightArgs = args.Skip(count + 1).ToArray();
		}

		internal static void SplitArgs(string[] args, out string[] leftArgs, out string[] rightArgs)
		{
			int index = Array.IndexOf(args, "--");
			int count = index == -1 ? args.Length : index;

			leftArgs = args.Take(count).ToArray();
			rightArgs = args.Skip(count + 1).ToArray();
		}
	}
}
