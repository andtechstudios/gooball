using CommandLine;
using System;

namespace Gooball
{

	internal static class ProjectOperation
	{

		public static void OnParse(object options)
		{
			var projectPath = string.Empty;
			var project = Project.Read(projectPath);

			switch (string.Empty)
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
				var exitCode = new UnityProcess(unityArgs).Open(project);

				Environment.Exit(exitCode);
			}

			void Build()
			{
				var unityArgs = new UnityArgs(Interpreter.Instance.PassthroughArgs);
				var exitCode = new UnityProcess(unityArgs).Build(project);

				Environment.Exit(exitCode);
			}

			void Test()
			{
				var unityArgs = new UnityArgs(Interpreter.Instance.PassthroughArgs);
				var exitCode = new UnityProcess(unityArgs).Test(project);

				Environment.Exit(exitCode);
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
