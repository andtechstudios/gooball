using System.Diagnostics;
using Andtech.Common;
using CliWrap;
using CliWrap.Buffered;

namespace Andtech.Gooball
{

	internal class UnityProcess
	{
		public int ExitCode { get; set; }
		public bool EnableWSLMode { get; set; }

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

					process.StartInfo.FileName = startInfo.Editor.ExecutablePath;
					process.StartInfo.Arguments = argsString;
					if (!string.IsNullOrEmpty(Environment.GetEnvironmentVariable("WSLENV")))
					{
						if (EnableWSLMode)
						{
							var wslArgs = new List<string>()
							{
								"-w",
								startInfo.Editor.ExecutablePath,
							};
							var result = await Cli.Wrap("wslpath")
								.WithArguments(wslArgs)
								.ExecuteBufferedAsync(cts.Token);

							process.StartInfo.Arguments = $"Start-Process '{result.StandardOutput.Trim()}' -Wait -ArgumentList '{argsString}'";
							process.StartInfo.FileName = "powershell.exe";
						}
					}

					Log.WriteLine($"{process.StartInfo.FileName} {process.StartInfo.Arguments}", Verbosity.verbose);
					process.Start();
					await process.WaitForExitAsync(cancellationToken: cts.Token);
					Console.WriteLine($"[Gooball] Unity process ended with exit code {process.ExitCode}");

					cts.Cancel();
					cts.Dispose();

					ExitCode = process.ExitCode;
				}
			}
		}
	}
}
