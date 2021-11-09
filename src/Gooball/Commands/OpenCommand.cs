using CommandLine;

namespace Andtech.Gooball
{
	internal class OpenCommand : UnityProjectCommand<OpenCommand.Options>
	{
		[Verb("open", isDefault: true, HelpText = "Open a Unity project with a Unity editor.")]
		public class Options : UnityProjectOptions { }

		protected override UnityStartInfo CreateStartInfo(Options options) => UnityStartInfo.Open(options, Interpreter.Instance.PassthroughArgs);
	}
}
