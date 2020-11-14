using CommandLine;

namespace Gooball {

	[Verb("inject", HelpText = "Inject text into a Unity source code file.")]
	internal class InjectOptions {
		[Value(0, Required = true, MetaName = "file-path", HelpText = "The path to the target file.")]
		public string FilePath { get; set; }
		[Option('h', "headerfile", HelpText = "The preamble text to inject.")]
		public string HeaderFilePath { get; set; }
	}

	internal static class InjectOperation {

		[Operation(typeof(InjectOptions))]
		public static void OnParse(InjectOptions options) {
			new Injector() { HeaderFilePath = options.HeaderFilePath }.Prepend(options.FilePath);
		}
	}
}
