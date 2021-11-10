using CommandLine;
using System.Threading.Tasks;

namespace Andtech.Gooball
{
	internal class BuildCommand : UnityProjectCommand<BuildCommand.Options>
	{
		[Verb("build", HelpText = "Build a Unity project.")]
		internal class Options : UnityProjectOptions { }

		public static Task OnParseAsync(Options options) => new BuildCommand().RunAsync(options);

		protected override UnityStartInfo CreateStartInfo(Options options) => UnityStartInfo.Build(options, Interpreter.Instance.PassthroughArgs);
	}
}
