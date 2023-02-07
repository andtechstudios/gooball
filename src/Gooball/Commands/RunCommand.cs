using System;
using System.Threading.Tasks;
using Andtech.Common;
using CommandLine;

namespace Andtech.Gooball
{

	internal class RunCommand
	{
		[Verb("run", HelpText = "Run arbitrary Unity commands.")]
		public class Options : BaseOptions { }

		public async Task OnParseAsync(Options options)
		{
			var startInfo = UnityStartInfo.Run(options);
			var process = new UnityProcess(startInfo);
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
