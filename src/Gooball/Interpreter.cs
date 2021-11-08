using CommandLine;
using System;
using System.Linq;
using System.Reflection;

namespace Gooball
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
				.WithParsed<OpenCommand.Options>(OpenCommand.OnParse)
				.WithParsed<BuildCommand.Options>(BuildCommand.OnParse)
				.WithParsed<TestCommand.Options>(TestCommand.OnParse)
				.WithParsed<RunCommand.Options>(RunCommand.OnParse)
				.WithParsed<ListCommand.Options>(ListCommand.OnParse)
				.WithParsed<HideCommand.Options>(HideCommand.OnParse)
			;
		}
	}
}
