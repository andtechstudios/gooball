using Andtech.Common;
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
			var arguments = new List<string>(startInfo.Args);

			var isUsingExplicitLogFile = ArgumentUtility.TryGetOption(arguments, "logFile", out var logFilePath);
			var isUsingTempLogFile = startInfo.Follow && !isUsingExplicitLogFile;
			var isLogging = isUsingExplicitLogFile || isUsingTempLogFile;
			if (isUsingTempLogFile)
			{
				var fileName = $"LogFile-{DateTime.UtcNow.ToBinary()}.txt";
				logFilePath = Path.Combine(startInfo.Project.Path, fileName);
				arguments.Add("-logFile");
				arguments.Add(logFilePath);
			}

			var argsString = string.Join(" ", arguments.Select(x => $"\"{x}\""));

			if (startInfo.DryRun)
			{
				Console.WriteLine($"[DRY RUN] {startInfo.Editor.ExecutablePath} {argsString}");
			}
			else
			{
				var cts = new CancellationTokenSource();

				if (startInfo.Follow)
				{
					try
					{
						File.WriteAllText(logFilePath, string.Empty);
					}
					catch { }

					var tail = new Tail(logFilePath);
					tail.Listen(cancellationToken: cts.Token);
				}

				Log.WriteLine($"{startInfo.Editor.ExecutablePath} {argsString}", Verbosity.verbose);

				using (var process = new Process())
				{
					process.StartInfo.FileName = startInfo.Editor.ExecutablePath;
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
