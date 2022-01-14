using CommandLine;
using System;
using System.Threading.Tasks;

namespace Andtech.Gooball
{

	internal class TestCommand
	{
		[Verb("test", HelpText = "Run tests on Unity project.")]
		internal class Options : GooballOptions
		{
			[Option("test-results", HelpText = "The path where Unity should save the result file.")]
			public string TestResults { get; set; }
		}

		public async Task OnParseAsync(Options options)
		{
			var startInfo = UnityStartInfo.Test(options);
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
