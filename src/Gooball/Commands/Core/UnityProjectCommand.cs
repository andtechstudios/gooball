using CommandLine;
using System;

namespace Andtech.Gooball
{

	internal abstract class UnityProjectCommand<T> where T : UnityProjectOptions
	{

		public void OnParse(T options)
		{
			var startInfo = CreateStartInfo(options);
			var project = Project.Read(options.ProjectPath);
			startInfo.PreferredEditorVersion = project.EditorVersion;
			var process = new UnityProcess(startInfo);
			process.Start();

			Environment.Exit(process.ExitCode);
		}

		protected abstract UnityStartInfo CreateStartInfo(T options);
	}
}
