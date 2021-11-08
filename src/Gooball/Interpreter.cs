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
			SplitArgs(args, out var toolArgs, out var passthroughArgs);
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

		private static void SplitArgs(string[] args, out string[] leftArgs, out string[] rightArgs)
		{
			int index = Array.IndexOf(args, "--");
			int count = index == -1 ? args.Length : index;

			leftArgs = args.Take(count).ToArray();
			rightArgs = args.Skip(count + 1).ToArray();
		}
	}
}
