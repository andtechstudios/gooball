using CommandLine;

namespace Gooball
{

	internal class BaseUnityOptions
	{
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
	}
}
