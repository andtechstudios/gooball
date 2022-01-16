using CommandLine;
using System;
using System.Threading.Tasks;

namespace Andtech.Gooball
{

	internal class RunCommand
	{
		[Verb("run", HelpText = "Run arbitrary Unity commands.")]
		public class Options : GooballOptions { }

		public async Task OnParseAsync(Options options)
		{
			var startInfo = UnityStartInfo.Run(options);
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
