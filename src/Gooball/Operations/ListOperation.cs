using CommandLine;
using System;

namespace Gooball
{

	internal class ListCommand
	{
		[Verb("list", HelpText = "List installed editors.")]
		public class Options
		{
		}

		public static void OnParse(Options options)
		{
			var installationHelper = new UnityInstallationHelper();
			foreach (var editor in installationHelper.GetInstalledEditors())
			{
				Console.WriteLine(editor);
			}
		}
	}
}
