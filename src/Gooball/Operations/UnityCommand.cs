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
			var startInfo = UnityStartInfo.Open(options, Interpreter.Instance.PassthroughArgs);
			startInfo.PreferredEditorVersion = project.EditorVersion;
			var process = new UnityProcess(startInfo);
			process.Start();

			Environment.Exit(process.ExitCode);
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
			var startInfo = UnityStartInfo.Build(options, Interpreter.Instance.PassthroughArgs);
			startInfo.PreferredEditorVersion = project.EditorVersion;
			var process = new UnityProcess(startInfo);
			process.Start();

			Environment.Exit(process.ExitCode);
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
			var startInfo = UnityStartInfo.Test(options, Interpreter.Instance.PassthroughArgs);
			startInfo.PreferredEditorVersion = project.EditorVersion;
			var process = new UnityProcess(startInfo);
			process.Start();

			Environment.Exit(process.ExitCode);
		}
	}
}
