using CommandLine;
using System.Threading.Tasks;

namespace Andtech.Gooball
{
	internal class OpenCommand : UnityProjectCommand<OpenCommand.Options>
	{
		[Verb("open", isDefault: true, HelpText = "Open a Unity project with a Unity editor.")]
		public class Options : UnityProjectOptions { }

		public static Task OnParseAsync(Options options) => new OpenCommand().RunAsync(options);

		protected override UnityStartInfo CreateStartInfo(Options options) => UnityStartInfo.Open(options, Interpreter.Instance.PassthroughArgs);
	}
}
