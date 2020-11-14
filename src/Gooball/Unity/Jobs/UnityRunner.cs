using System.Collections.Generic;
using System.Diagnostics;

namespace Gooball {

	internal class UnityRunner {
		public readonly UnityArgs Args;

		public UnityRunner(UnityArgs args) {
			Args = args;
		}

		public void Build(Project project) {
			var helper = new UnityInstallationHelper();
			var args = GetDefaultArgs(project);

			var editorPath = helper.GetBestEditor(project.EditorVersion);
			Run(editorPath, project.Path, string.Join(' ', args));
		}

		public void Test(Project project) {
			var helper = new UnityInstallationHelper();
			var args = GetDefaultArgs(project);
			if (!Args.RunTests) {
				args.Enqueue($"-runTests");
			}

			var editorPath = helper.GetBestEditor(project.EditorVersion);
			Run(editorPath, project.Path, string.Join(' ', args));
		}

		private Queue<string> GetDefaultArgs(Project project) {
			var args = new Queue<string>();
			if (!Args.BatchMode) {
				args.Enqueue($"-batchMode");
			}
			if (!Args.Quit) {
				args.Enqueue($"-quit");
			}
			if (Args.ProjectPath is null) {
				args.Enqueue($"-projectPath \"{project.Path}\"");
			}
			foreach (var argument in Args) {
				args.Enqueue(argument);
			}

			return args;
		}

		private void Run(string editorPath, string projectPath, string args) {
			using (var process = new Process()) {
				process.StartInfo.FileName = editorPath;
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.RedirectStandardOutput = true;
				process.StartInfo.Arguments = args;
				process.Start();

				process.WaitForExit();
			}
		}
	}
}
