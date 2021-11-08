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
			ArgsHelper.SplitArgs(args, out var gooArgs, out var passthroughArgs);
			Args = gooArgs;
			PassthroughArgs = passthroughArgs;

			var types = LoadVerbs();
			var operations = LoadOperations();

			Parser.Default.ParseArguments(gooArgs, types)
				.WithParsed(OnParse);

			void OnParse(object options)
			{
				var type = options.GetType();

				var operation = operations.First(x => x.GetCustomAttribute<OperationAttribute>().OptionType == type);
				operation.Invoke(null, new object[] { options });
			}
		}

		private static Type[] LoadVerbs()
		{
			return Assembly
				.GetExecutingAssembly()
				.GetTypes()
				.Where(t => t.GetCustomAttribute<VerbAttribute>() != null)
				.ToArray();
		}

		private static MethodInfo[] LoadOperations()
		{
			return Assembly
				.GetExecutingAssembly()
				.GetTypes()
				.SelectMany(t => t.GetMethods())
				.Where(m => m.GetCustomAttribute<OperationAttribute>() != null)
				.ToArray();
		}
	}
}
