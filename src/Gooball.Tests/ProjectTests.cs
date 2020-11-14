using NUnit.Framework;

namespace Gooball.Tests {

	public class ProjectTests : GooballTests {
		private const string PROJECT_VERSION = "1.2.3";
		private const string EDITOR_VERSION = "2020.1.3f1";

		[Test]
		public void GetProjectVersion() {
			var stream = OpenTestStream();

			Interpreter.Instance.Run(new string[] { "project", "get-version", ExampleProjectRoot });

			Assert.AreEqual(PROJECT_VERSION, stream.ToString().Trim());
		}

		[Test]
		public void GetEditorVersion() {
			var project = Project.Read(ExampleProjectRoot);

			Interpreter.Instance.Run(new string[] { "project", "get-editor-version", ExampleProjectRoot });

			Assert.AreEqual(EDITOR_VERSION, project.EditorVersion);
		}
	}
}
