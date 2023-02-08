using System.Diagnostics;
using Andtech.Common;

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
				var fileName = $"log-{DateTime.UtcNow.ToBinary()}.txt";
				logFilePath = Path.Combine(startInfo.Project.Path, "Logs", fileName);
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
				if (File.Exists(logFilePath))
				{
					File.Delete(logFilePath);
				}

				using (var process = new Process())
				{
					var cts = new CancellationTokenSource();
					if (startInfo.Follow)
					{
						Log.WriteLine($"Will tail logfile at '{logFilePath}'...", Verbosity.verbose);
						var tail = new Tail(logFilePath);
						tail?.RunAsync(cancellationToken: cts.Token);
					}

					Log.WriteLine($"{startInfo.Editor.ExecutablePath} {argsString}", Verbosity.verbose);
					process.StartInfo.FileName = startInfo.Editor.ExecutablePath;
					process.StartInfo.Arguments = argsString;
					process.Start();
					await process.WaitForExitAsync(cancellationToken: cts.Token);

					cts.Cancel();
					cts.Dispose();

					ExitCode = process.ExitCode;
				}
			}
		}
	}
}
