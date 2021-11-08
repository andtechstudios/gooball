using CommandLine;
using System;

namespace Gooball
{

	[Verb("unity", HelpText = "Project-independent Unity commands.")]
	internal class UnityOptions
	{
		[Value(0, Required = true, MetaName = "command", HelpText = "The editor action to perform.")]
		public string Command { get; set; }

		// List-Installs options
		[Option("install-path", HelpText = "The editor installation location.")]
		public string Path { get; set; }
	}

	internal static class UnityOperation
	{

		public static void OnParse(UnityOptions options)
		{
			switch (options.Command)
			{
				case "list-installs":
					ListEditorInstalls();
					break;
			}

			void ListEditorInstalls()
			{
				UnityInstallationHelper helper;
				if (options.Path is null)
				{
					helper = new UnityInstallationHelper();
				}
				else
				{
					helper = new UnityInstallationHelper(options.Path);
				}
				var editors = helper.GetInstalledEditors();
				Console.WriteLine(string.Join(Environment.NewLine, editors));
			}
		}
	}
}
