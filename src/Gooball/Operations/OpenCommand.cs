using CommandLine;

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
			System.Console.WriteLine("Open");
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
			System.Console.WriteLine("Build");
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
			System.Console.WriteLine("Test");
		}
	}
}
