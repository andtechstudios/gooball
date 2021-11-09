using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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

		public void Start()
		{
			var helper = new UnityInstallationHelper();
			var version = VersionUtility.Parse(startInfo.PreferredEditorVersion);
			var editor = helper.GetBestEditor(version);

			ExitCode = Execute(editor, startInfo);
		}

		private static int Execute(UnityEditor editor, UnityStartInfo startInfo)
		{
			int exitCode = 0;
			var arguments = new List<string>(startInfo.Args);

			var hadRequestedLogFile = ArgumentUtility.TryGetOption(startInfo.Args, "logFile", out var logFilePath);
			var isUsingTempLogFile = startInfo.Verbose && !hadRequestedLogFile;
			if (isUsingTempLogFile)
			{
				logFilePath = Path.GetTempFileName();
				arguments.Add("-logFile");
				arguments.Add(logFilePath);
			}

			var argsString = string.Join(" ", arguments.Select(x => $"\"{x}\""));

			if (isUsingTempLogFile)
			{
				Console.ForegroundColor = ConsoleColor.Yellow;
				Console.WriteLine($"Logfile at {logFilePath}");
				Console.ResetColor();
			}

			if (startInfo.DryRun)
			{
				Console.WriteLine($"[DRY RUN] {editor.ExecutablePath} {argsString}");
			}
			else
			{
				using (var process = new Process())
				{
					process.StartInfo.FileName = editor.ExecutablePath;
					process.StartInfo.UseShellExecute = false;
					process.StartInfo.RedirectStandardOutput = true;
					process.StartInfo.Arguments = argsString;
					process.Start();

					process.WaitForExit();

					exitCode = process.ExitCode;
				}
			}

			if (isUsingTempLogFile)
			{
				Console.WriteLine(File.ReadAllText(logFilePath));
				File.Delete(logFilePath);
			}

			return exitCode;
		}
	}
}
