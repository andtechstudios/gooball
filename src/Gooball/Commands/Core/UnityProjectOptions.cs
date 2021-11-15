using CommandLine;

namespace Andtech.Gooball
{

	internal class UnityProjectOptions : BaseUnityOptions
	{
		[Value(0, Required = false, Default = "./", HelpText = "The path to the Unity project.")]
		public string ProjectPath { get; set; }
		[Option('f', "follow", HelpText = "Output appended data as the Unity log grows. (same as dumplog)")]
		public bool Follow { get; set; }
		[Option("dumplog", HelpText = "Output appended data as the Unity log grows. (same as follow)")]
		public bool DumpLog { get; set; }
	}
}
