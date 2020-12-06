using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace Gooball {

	internal class UnityRunner {
		public readonly UnityArgs Args;

		public UnityRunner(UnityArgs args) {
			Args = args;
		}

		public void Open(Project project) {
			var helper = new UnityInstallationHelper();
			var args = new Queue<string>(Args);
			if (Args.ProjectPath is null) {
				args.Enqueue($"-projectPath \"{project.Path}\"");
			}

			var editorPath = helper.GetBestEditor(project.EditorVersion);
			Run(editorPath, string.Join(' ', args), false);
		}

		public void Build(Project project) {
			var helper = new UnityInstallationHelper();
			var args = GetDefaultArgsBatch(project);

			var editorPath = helper.GetBestEditor(project.EditorVersion);
			Run(editorPath, string.Join(' ', args));
		}

		public void Test(Project project) {
			var helper = new UnityInstallationHelper();
			var args = GetDefaultArgsBatch(project);
			if (!Args.RunTests) {
				args.Enqueue($"-runTests");
			}

			var editorPath = helper.GetBestEditor(project.EditorVersion);
			Run(editorPath, string.Join(' ', args));
		}

		private Queue<string> GetDefaultArgsBatch(Project project) {
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

		public void Execute() {
			var helper = new UnityInstallationHelper();
			var args = Args;

			var editorPath = UnityInstallationHelper.GetExecutablePath(helper.GetInstalledEditors().First());
			Run(editorPath, string.Join(' ', args));
		}

		private void Run(string editorPath, string args, bool waitForExit = true) {
			using (var process = new Process()) {
				process.StartInfo.FileName = editorPath;
				process.StartInfo.UseShellExecute = false;
				process.StartInfo.RedirectStandardOutput = true;
				process.StartInfo.Arguments = args;
				process.Start();

				if (waitForExit)
					process.WaitForExit();
			}
		}
	}
}
