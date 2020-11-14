using CommandLine;
using System;

namespace Gooball {

	[Verb("editor", HelpText = "Commands for working with installations of the Unity editor.")]
	internal class EditorOptions {
		[Value(0, Required = true, MetaName = "command", HelpText = "The editor action to perform.")]
		public string Command { get; set; }
		[Option("install-path", HelpText = "The editor installation location.")]
		public string Path { get; set; }
	}

	internal static class EditorOperation {

		[Operation(typeof(EditorOptions))]
		public static void OnParse(EditorOptions options) {
			switch (options.Command) {
				case "list":
					ListEditors();
					break;
			}

			void ListEditors() {
				UnityInstallationHelper helper;
				if (options.Path is null) {
					helper = new UnityInstallationHelper();
				}
				else {
					helper = new UnityInstallationHelper(options.Path);
				}
				var editors = helper.GetInstalledEditors();
				Console.WriteLine(string.Join(Environment.NewLine, editors));
			}
		}
	}
}
