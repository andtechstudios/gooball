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
		public string[] Args { get; private set; }
		public string[] PassthroughArgs { get; private set; }

		public static Interpreter Instance { get; private set; }

		static Interpreter()
		{
			Instance = new Interpreter();
		}

		private Interpreter() { }

		public void Run(string[] args)
		{
			ArgumentUtility.SplitArgs(args, out var gooArgs, out var passthroughArgs);
			Args = gooArgs;
			PassthroughArgs = passthroughArgs;

			Parser.Default.ParseArguments<OpenUnity.Options, BuildUnity.Options, TestUnity.Options>(gooArgs)
				.WithParsed<OpenUnity.Options>(OpenUnity.OnParse)
				.WithParsed<BuildUnity.Options>(BuildUnity.OnParse)
				.WithParsed<TestUnity.Options>(TestUnity.OnParse)
			;
		}
	}
}
