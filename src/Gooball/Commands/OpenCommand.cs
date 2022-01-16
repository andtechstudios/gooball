using CommandLine;
using System;
using System.Threading.Tasks;

namespace Andtech.Gooball
{

	internal class OpenCommand
	{
		[Verb("open", isDefault: true, HelpText = "Open a Unity project with a Unity editor.")]
		public class Options : GooballOptions { }

		public async Task OnParseAsync(Options options)
		{
			var startInfo = UnityStartInfo.Open(options);
			var process = new UnityProcess(startInfo);
			try
			{
				await process.RunAsync();
			}
			catch
			{
				Environment.ExitCode = process.ExitCode;
				throw;
			}
		}
	}
}
