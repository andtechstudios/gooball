using CommandLine;
using System;

namespace Gooball
{

	[Verb("project", HelpText = "Commands for working with Unity projects.")]
	internal class ProjectOptions
	{
		[Value(0, Required = true, MetaName = "command", HelpText = "The project action to perform.")]
		public string Command { get; set; }
		[Value(1, Required = false, Default = "./", HelpText = "The path to the Unity project.")]
		public string ProjectPath { get; set; }

		// Run, Test options
		[Option("editor", HelpText = "A specific version of the Unity editor to use")]
		public string EditorPath { get; set; }
	}

	internal static class ProjectOperation
	{

		[Operation(typeof(ProjectOptions))]
		public static void OnParse(ProjectOptions options)
		{
			var projectPath = options.ProjectPath;
			var project = Project.Read(projectPath);

			switch (options.Command)
			{
				case "open":
					Open();
					break;
				case "build":
					Build();
					break;
				case "test":
					Test();
					break;
				case "get-version":
					GetProjectVersion();
					break;
				case "get-editor-version":
					GetProjectEditorVersion();
					break;
			}

			void Open()
			{
				var unityArgs = new UnityArgs(Interpreter.Instance.PassthroughArgs);
				new UnityRunner(unityArgs).Open(project);
			}

			void Build()
			{
				var unityArgs = new UnityArgs(Interpreter.Instance.PassthroughArgs);
				new UnityRunner(unityArgs).Build(project);
			}

			void Test()
			{
				var unityArgs = new UnityArgs(Interpreter.Instance.PassthroughArgs);
				new UnityRunner(unityArgs).Test(project);
			}

			void GetProjectVersion()
			{
				Console.WriteLine(project.Version);
			}

			void GetProjectEditorVersion()
			{
				Console.WriteLine(project.EditorVersion);
			}
		}
	}
}
