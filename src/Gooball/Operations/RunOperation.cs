using CommandLine;
using System;

namespace Gooball
{

	internal class RunCommand
	{
		[Verb("run", HelpText = "Run arbitrary Unity commands.")]
		public class Options : BaseUnityOptions
		{
			[Option("project-path", HelpText = "The path to the Unity project.")]
			public string ProjectPath { get; set; }
		}

		public static void OnParse(Options options)
		{

		}
	}
}
