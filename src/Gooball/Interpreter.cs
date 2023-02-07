using Andtech.Common;
using CommandLine;

namespace Andtech.Gooball
{

	public class Interpreter
	{
		public static readonly Interpreter Instance = new Interpreter();

		public void Run(string[] args) => RunAsync(args).Wait();

		public async Task RunAsync(string[] args)
		{
			ArgumentUtility.SplitArgs(args, out var toolArgs, out var passthroughArgs);

			var result = Parser.Default.ParseArguments<OpenCommand.Options, BuildCommand.Options, TestCommand.Options, RunCommand.Options, ListCommand.Options, HideCommand.Options, SetVersionCommand.Options, ServeCommand.Options>(toolArgs);
			result.WithParsed<BaseOptions>(PreParse);

			await result.WithParsedAsync<OpenCommand.Options>(new OpenCommand().OnParseAsync);
			await result.WithParsedAsync<BuildCommand.Options>(new BuildCommand().OnParseAsync);
			await result.WithParsedAsync<TestCommand.Options>(new TestCommand().OnParseAsync);
			await result.WithParsedAsync<RunCommand.Options>(new RunCommand().OnParseAsync);
			await result.WithParsedAsync<ListCommand.Options>(new ListCommand().OnParseAsync);
			await result.WithParsedAsync<HideCommand.Options>(new HideCommand().OnParseAsync);
			await result.WithParsedAsync<SetVersionCommand.Options>(new SetVersionCommand().OnParseAsync);
			await result.WithParsedAsync<ServeCommand.Options>(new ServeCommand().OnParseAsync);

			void PreParse(BaseOptions options)
			{
				Log.Verbosity = options.Verbosity;

				Session.Instance = new Session
				{
					ToolArgs = toolArgs,
					PassthroughArgs = passthroughArgs,
					InstallationHelper = new UnityInstallationHelper(options.InstallPath),
				};
				if (!string.IsNullOrEmpty(options.EditorVersion))
				{
					Session.Instance.PreferredEditorVersion = options.EditorVersion;
				}
			}
		}
	}
}
