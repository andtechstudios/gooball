using CommandLine;

namespace Gooball {

	[Verb("build", HelpText = "Build a Unity project.")]
	internal class UnityBuildOperation {
		[Value(0, Required = true, MetaName = "project-path", HelpText = "The path to the Unity project.")]
		public string ProjectPath { get; set; }

		[Option('e', "editor", HelpText = "A specific version of the Unity editor to use")]
		public string EditorPath { get; set; }
	}

	[Verb("test", HelpText = "Test a Unity project.")]
	internal class UnityTestOperation {
		[Value(0, Required = true, MetaName = "project-path", HelpText = "The path to the Unity project.")]
		public string ProjectPath { get; set; }

		[Option('e', "editor", HelpText = "A specific version of the Unity editor to use")]
		public string EditorPath { get; set; }

	}

	internal static class BuildOperation {

		[Operation(typeof(UnityBuildOperation))]
		public static void OnParse(UnityBuildOperation options) {
			var unityArgs = new UnityArgs(Interpreter.Instance.PassthroughArgs);
			var project = Project.Read(options.ProjectPath);
			new UnityRunner(unityArgs).Build(project);
		}
	}

	internal static class TestOperation {

		[Operation(typeof(UnityTestOperation))]
		public static void OnParse(UnityTestOperation options) {
			var unityArgs = new UnityArgs(Interpreter.Instance.PassthroughArgs);
			var project = Project.Read(options.ProjectPath);
			new UnityRunner(unityArgs).Test(project);
		}
	}
}
