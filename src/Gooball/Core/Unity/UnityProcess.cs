using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

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

		public async Task RunAsync()
		{
			var helper = new UnityInstallationHelper();
			var version = VersionUtility.Parse(startInfo.PreferredEditorVersion);
			var editor = helper.GetBestEditor(version);

			var arguments = new List<string>(startInfo.Args);

			var hadRequestedLogFile = ArgumentUtility.TryGetOption(startInfo.Args, "logFile", out var logFilePath);
			var isUsingTempLogFile = startInfo.Verbose && !hadRequestedLogFile;
			var isLogging = hadRequestedLogFile || isUsingTempLogFile;
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
				var cts = new CancellationTokenSource();

				if (isLogging)
				{
					var logger = new LogDumper(logFilePath);
					logger.Listen(cts.Token);
				}

				using (var process = new Process())
				{
					process.StartInfo.FileName = editor.ExecutablePath;
					process.StartInfo.UseShellExecute = false;
					process.StartInfo.RedirectStandardOutput = true;
					process.StartInfo.Arguments = argsString;
					process.Start();

					process.WaitForExit();

					ExitCode = process.ExitCode;
				}

				cts.Cancel();
			}

			if (isUsingTempLogFile)
			{
				File.Delete(logFilePath);
			}
		}
	}
}
