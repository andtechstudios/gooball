using CommandLine;

namespace Andtech.Gooball
{
	internal class BuildCommand : UnityProjectCommand<BuildCommand.Options>
	{
		[Verb("build", HelpText = "Build a Unity project.")]
		internal class Options : UnityProjectOptions { }

		protected override UnityStartInfo CreateStartInfo(Options options) => UnityStartInfo.Build(options, Interpreter.Instance.PassthroughArgs);
	}
}
