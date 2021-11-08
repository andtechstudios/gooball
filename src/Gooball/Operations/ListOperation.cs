using CommandLine;
using System;

namespace Gooball
{

	internal class ListCommand
	{
		[Verb("list", HelpText = "List installed editors.")]
		public class Options
		{
			[Option("install-path", HelpText = "Location of Unity editor executables.")]
			public string InstallPath { get; set; }
		}

		public static void OnParse(Options options)
		{
			var installPath = options.InstallPath ?? null;
			var installationHelper = new UnityInstallationHelper(installPath);
			foreach (var editor in installationHelper.GetInstalledEditors())
			{
				Console.WriteLine(editor);
			}
		}
	}
}
