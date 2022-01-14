using Andtech.Common;
using CommandLine;

namespace Andtech.Gooball
{

	internal class GooballOptions
	{
		[Option("verbosity", HelpText = "Verbosity of logging")]
		public Verbosity Verbosity { get; set; }
		[Option("install-path", HelpText = "Location of Unity editor executables.")]
		public string InstallPath { get; set; }

		[Option('n', "dry-run", HelpText = "Dry run this command", Hidden = false)]
		public bool DryRun { get; set; }
		[Option("execute-method", HelpText = "Execute the static method as soon as Unity opens the project, and after the optional Asset server update is complete.")]
		public string ExecuteMethod { get; set; }
		[Option("build-target", HelpText = "Select an active build target before loading a project.")]
		public string BuildTarget { get; set; }
		[Option("log-file", HelpText = "Specify where Unity writes the Editor or Windows/Linux/OSX standalone log file.")]
		public string LogFile { get; set; }
		[Option("forget-project-path", HelpText = "Don't save your current Project into the Unity launcher/hub history.")]
		public bool ForgetProjectPath { get; set; }
		[Option("no-graphics", HelpText = "When you run this in batch mode, it does not initialize the graphics device.")]
		public bool NoGraphics { get; set; }
		[Option("editor-version", HelpText = "Describe which Unity Editor version to launch.")]
		public string EditorVersion { get; set; }

		[Value(0, Required = false, Default = "./", HelpText = "The path to the Unity project.")]
		public string ProjectPath { get; set; }
		[Option('f', "follow", HelpText = "Output appended data as the Unity log grows. (same as dumplog)")]
		public bool Follow { get; set; }
		[Option("dumplog", HelpText = "Output appended data as the Unity log grows. (same as follow)")]
		public bool DumpLog { get; set; }
	}
}
