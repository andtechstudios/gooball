using System;
using System.Threading.Tasks;
using Andtech.Common;
using CommandLine;

namespace Andtech.Gooball
{

	internal class TestCommand
	{
		[Verb("test", HelpText = "Run tests on Unity project.")]
		internal class Options : BaseOptions
		{
			[Option("test-results", HelpText = "The path where Unity should save the result file.")]
			public string TestResults { get; set; }
		}

		public async Task OnParseAsync(Options options)
		{
			var startInfo = UnityStartInfo.Test(options);
			var process = new UnityProcess(startInfo)
			{
				EnableWSLMode = !options.DisablePowershellShim,
			};
			try
			{
				await process.RunAsync();
			}
			catch (ProjectNotFoundException)
			{
				Log.Error.WriteLine($"The directory at '{options.ProjectPath}' is not a Unity project.", ConsoleColor.Red, Verbosity.minimal);
				Environment.Exit(1);
			}
			catch
			{
				Environment.ExitCode = process.ExitCode;
				throw;
			}
		}
	}
}
