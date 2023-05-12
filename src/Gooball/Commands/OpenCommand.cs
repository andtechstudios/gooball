using System;
using System.Threading.Tasks;
using Andtech.Common;
using CommandLine;

namespace Andtech.Gooball
{

	internal class OpenCommand
	{
		[Verb("open", isDefault: true, HelpText = "Open a Unity project with a Unity editor.")]
		public class Options : BaseOptions { }

		public async Task OnParseAsync(Options options)
		{
			var startInfo = UnityStartInfo.Open(options);
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
