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
				Tail tail = null;
				if (startInfo.Follow)
				{
					Log.WriteLine($"Will tail logfile at '{logFilePath}'...", Verbosity.verbose);
					if (File.Exists(logFilePath))
					{
						File.Delete(logFilePath);
					}
					tail = new Tail(logFilePath);
				}

				Log.WriteLine($"{startInfo.Editor.ExecutablePath} {argsString}", Verbosity.verbose);

				using (var process = new Process())
				{
					process.StartInfo.FileName = startInfo.Editor.ExecutablePath;
					process.StartInfo.Arguments = argsString;

					var cts = new CancellationTokenSource();
					process.Start();
					var mainTask = process.WaitForExitAsync(cancellationToken: cts.Token);
					var tailTask = tail?.RunAsync(cancellationToken: cts.Token);

					await Task.WhenAny(mainTask, tailTask);

					cts.Cancel();
					cts.Dispose();

					ExitCode = process.ExitCode;
				}
			}
		}
	}
}
