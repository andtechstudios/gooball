using Andtech.Common;
using CommandLine;
using System.Diagnostics;
using System.IO;
using Andtech.Common;
using CommandLine;

namespace Andtech.Gooball
{

	internal class ServeCommand
	{
		[Verb("serve", HelpText = "Start a local web server.")]
		internal class Options : BaseOptions
		{
			[Option(Default = 8080)]
			public int Port { get; set; }
		}

		public async Task OnParseAsync(Options options)
		{
			var editor = Session.Instance.GetEditor();
			var serverBinary = Path.Combine(editor.Path, "Editor", "Data", "PlaybackEngines", "WebGLSupport", "BuildTools", "SimpleWebServer.exe");

			if (!File.Exists(serverBinary))
			{
				Log.Error.WriteLine("SimpleWebServer not found. Is WebGL support installed for this version of Unity?");

				return;
			}

			Log.WriteLine($"{serverBinary} {options.ProjectPath} {options.Port}", Verbosity.diagnostic);

			var process = Process.Start(serverBinary, $"\"{options.ProjectPath}\" \"{options.Port}\"");
			await process.WaitForExitAsync();
		}
	}
}
