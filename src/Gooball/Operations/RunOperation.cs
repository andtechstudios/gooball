using CommandLine;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Andtech.Gooball
{

	internal class RunCommand
	{
		[Verb("run", HelpText = "Run arbitrary Unity commands.")]
		public class Options : BaseUnityOptions
		{
			public IEnumerable<string> Arguments { get; set; }
		}

		public static void OnParse(Options options)
		{
			var editorHelper = new UnityInstallationHelper();
			var editor = editorHelper.GetInstalledEditors().First();

			var startInfo = new UnityStartInfo(Interpreter.Instance.PassthroughArgs)
			{
				DryRun = options.DryRun,
				PreferredEditorVersion = Path.GetFileName(editor.VersionRaw)
			};
			var process = new UnityProcess(startInfo);
			process.Start();

			Environment.Exit(process.ExitCode);
		}
	}
}
