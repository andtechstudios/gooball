using CommandLine;

namespace Andtech.Gooball
{

	internal class UnityProjectOptions : BaseUnityOptions
	{
		[Value(0, Required = false, Default = "./", HelpText = "The path to the Unity project.")]
		public string ProjectPath { get; set; }
	}
}
