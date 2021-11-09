using CommandLine;
using System.Threading.Tasks;

namespace Andtech.Gooball
{
	internal class TestCommand : UnityProjectCommand<TestCommand.Options>
	{
		[Verb("test", HelpText = "Run tests on Unity project.")]
		internal class Options : UnityProjectOptions
		{
			[Option("test-results", HelpText = "The path where Unity should save the result file.")]
			public string TestResults { get; set; }
		}

		public static Task OnParseAsync(Options options) => new TestCommand().RunAsync(options);

		protected override UnityStartInfo CreateStartInfo(Options options) => UnityStartInfo.Test(options, Interpreter.Instance.PassthroughArgs);
	}
}
