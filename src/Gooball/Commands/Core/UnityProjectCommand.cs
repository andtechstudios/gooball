using System;
using System.Threading.Tasks;

namespace Andtech.Gooball
{

	internal abstract class UnityProjectCommand<T> where T : UnityProjectOptions
	{

		public async Task RunAsync(T options)
		{
			var startInfo = CreateStartInfo(options);
			var project = Project.Read(options.ProjectPath);
			startInfo.PreferredEditorVersion = project.EditorVersion;
			var process = new UnityProcess(startInfo);
			await process.RunAsync();

			Environment.Exit(process.ExitCode);
		}

		protected abstract UnityStartInfo CreateStartInfo(T options);
	}
}
