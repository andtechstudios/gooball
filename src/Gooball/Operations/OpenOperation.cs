using CommandLine;

namespace Gooball
{

	internal class OpenUnity
	{
		[Verb("open", isDefault: true, HelpText = "Open a Unity project with the Unity editor.")]
		public class Options : BaseUnityOptions
		{
		}

		public static void OnParse(Options options)
		{
			System.Console.WriteLine("Open");
		}
	}

	internal class BuildUnity
	{
		[Verb("build", HelpText = "Open a Unity project with the Unity editor.")]
		internal class Options : BaseUnityOptions
		{
		}

		public static void OnParse(Options options)
		{
			System.Console.WriteLine("Build");
		}
	}

	internal class TestUnity
	{
		[Verb("test", HelpText = "Open a Unity project with the Unity editor.")]
		internal class Options : BaseUnityOptions
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
