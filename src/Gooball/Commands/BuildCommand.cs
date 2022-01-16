using CommandLine;
using System;
using System.Threading.Tasks;

namespace Andtech.Gooball
{

	internal class BuildCommand
	{
		[Verb("build", HelpText = "Build a Unity project.")]
		internal class Options : GooballOptions { }

		public async Task OnParseAsync(Options options)
		{
			var startInfo = UnityStartInfo.Build(options);
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
