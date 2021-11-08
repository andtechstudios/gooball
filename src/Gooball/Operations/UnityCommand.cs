using CommandLine;
using System;

namespace Gooball
{

	internal class OpenCommand
	{
		[Verb("open", isDefault: true, HelpText = "Open a Unity project with the Unity editor.")]
		public class Options : UnityProjectOptions
		{
		}

		public static void OnParse(Options options)
		{
			var project = Project.Read(options.ProjectPath);
			var args = new UnityArgs(Interpreter.Instance.PassthroughArgs);
			var exitCode = new UnityProcess(args).Open(project);

			Environment.Exit(exitCode);
		}
	}

	internal class BuildCommand
	{
		[Verb("build", HelpText = "Build a Unity project.")]
		internal class Options : UnityProjectOptions
		{
		}

		public static void OnParse(Options options)
		{
			var project = Project.Read(options.ProjectPath);
			var args = new UnityArgs(Interpreter.Instance.PassthroughArgs);
			var exitCode = new UnityProcess(args).Build(project);

			Environment.Exit(exitCode);
		}
	}

	internal class TestCommand
	{
		[Verb("test", HelpText = "Run tests on Unity project.")]
		internal class Options : UnityProjectOptions
		{
			[Option("test-results", HelpText = "The path where Unity should save the result file.")]
			public string TestResults { get; set; }
		}

		public static void OnParse(Options options)
		{
			var project = Project.Read(options.ProjectPath);
			var argss = new UnityArgs(Interpreter.Instance.PassthroughArgs);
			var exitCode = new UnityProcess(args).Test(project);

			Environment.Exit(exitCode);
		}
	}
}
