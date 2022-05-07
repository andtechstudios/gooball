using Andtech.Common;
using CommandLine;
using System;
using System.Threading.Tasks;

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
			await Wrap<OpenCommand.Options>(new OpenCommand().OnParseAsync);
			await Wrap<BuildCommand.Options>(new BuildCommand().OnParseAsync);
			await Wrap<TestCommand.Options>(new TestCommand().OnParseAsync);
			await Wrap<RunCommand.Options>(new RunCommand().OnParseAsync);
			await Wrap<ListCommand.Options>(new ListCommand().OnParseAsync);
			await Wrap<HideCommand.Options>(new HideCommand().OnParseAsync);
			await Wrap<SetVersionCommand.Options>(new SetVersionCommand().OnParseAsync);
			await Wrap<ServeCommand.Options>(new ServeCommand().OnParseAsync);

			async Task Wrap<T>(Func<T, Task> action) where T : GooballOptions => await result.WithParsedAsync<T>(x => OnParse<T>(x, action));

			async Task OnParse<T>(T options, Func<T, Task> action) where T : GooballOptions
			{
				Log.Verbosity = options.Verbosity;

				// Initialize session
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

				// Continue as normal
				try
				{
					await action(options);
				}
				catch (ProjectNotFoundException)
				{
					Log.Error.WriteLine($"The directory at '{options.ProjectPath}' is not a Unity project.", ConsoleColor.Red, Verbosity.minimal);
					Environment.Exit(1);
				}
				catch (Exception ex)
				{
					Console.Error.WriteLine(ex);
					Environment.Exit(1);
				}
			}
		}
	}
}
