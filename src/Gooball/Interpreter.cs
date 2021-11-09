using CommandLine;

namespace Andtech.Gooball
{

	/// <summary>
	/// Gooball command line interpreter.
	/// </summary>
	public class Interpreter
	{
		public string[] ToolArgs { get; private set; }
		public string[] PassthroughArgs { get; private set; }

		public static Interpreter Instance { get; private set; }

		static Interpreter()
		{
			Instance = new Interpreter();
		}

		private Interpreter() { }

		public void Run(string[] args)
		{
			ArgumentUtility.SplitArgs(args, out var toolArgs, out var passthroughArgs);
			ToolArgs = toolArgs;
			PassthroughArgs = passthroughArgs;

			Parser.Default.ParseArguments<OpenCommand.Options, BuildCommand.Options, TestCommand.Options, RunCommand.Options, ListCommand.Options, HideCommand.Options>(toolArgs)
				.WithParsed<OpenCommand.Options>(options => new OpenCommand().OnParse(options))
				.WithParsed<BuildCommand.Options>(options => new BuildCommand().OnParse(options))
				.WithParsed<TestCommand.Options>(options => new TestCommand().OnParse(options))
				.WithParsed<RunCommand.Options>(RunCommand.OnParse)
				.WithParsed<ListCommand.Options>(ListCommand.OnParse)
				.WithParsed<HideCommand.Options>(HideCommand.OnParse)
			;
		}
	}
}
