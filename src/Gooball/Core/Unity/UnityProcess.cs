using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Gooball
{

	internal class UnityProcess
	{
		public int ExitCode { get; set; }

		private readonly UnityStartInfo startInfo;

		public UnityProcess(UnityStartInfo startInfo)
		{
			this.startInfo = startInfo;
		}

		private int Execute(string editorPath, IEnumerable<string> args)
		{
			var argsString = string.Join(" ", args.Select(WrapArgument));

			if (startInfo.DryRun)
			{
				Console.WriteLine($"[DRY RUN] {editorPath} {argsString}");

				return 0;
			}

			using (var process = new Process())
			{
				process.StartInfo.FileName = editorPath;
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
			var editorPath = helper.GetBestEditor(startInfo.PreferredEditorVersion);

			ExitCode = Execute(editorPath, startInfo.Args);
		}
	}
}
