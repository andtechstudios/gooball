using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Andtech.Gooball
{

	internal class UnityProcess
	{
		public int ExitCode { get; set; }

		private readonly UnityStartInfo startInfo;

		public UnityProcess(UnityStartInfo startInfo)
		{
			this.startInfo = startInfo;
		}

		private int Execute(UnityEditor editor, IEnumerable<string> args)
		{
			var argsString = string.Join(" ", args.Select(WrapArgument));

			if (startInfo.DryRun)
			{
				Console.WriteLine($"[DRY RUN] {editor.ExecutablePath} {argsString}");

				return 0;
			}

			using (var process = new Process())
			{
				process.StartInfo.FileName = editor.ExecutablePath;
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.RedirectStandardOutput = true;
				process.StartInfo.Arguments = argsString;
				process.Start();

				process.WaitForExit();

				return process.ExitCode;
			}

			string WrapArgument(string arg)
			{
				return $"\"{arg}\"";
			}
		}

		public void Start()
		{
			var helper = new UnityInstallationHelper();
			var version = VersionUtility.Parse(startInfo.PreferredEditorVersion);
			var editor = helper.GetBestEditor(version);

			ExitCode = Execute(editor, startInfo.Args);
		}
	}
}
