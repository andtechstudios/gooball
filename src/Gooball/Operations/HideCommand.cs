using CommandLine;
using System;

namespace Gooball
{

	internal class HideCommand
	{
		[Verb("hide", HelpText = "Hide assets from the Unity asset database.")]
		public class Options
		{
			[Option("in-package", HelpText = "Hides the path in a package manifest instead of the filesystem.")]
			public bool PackageMode { get; set; }
		}

		public static void OnParse(Options options)
		{

		}
	}
}
