using NUnit.Framework;
using System;
using System.IO;

namespace Gooball.Tests {

	public class EditorTests : GooballTests {
		private static readonly string[] EDITORS = new string[] {
			Path.Combine(ExampleEditorInstallRoot, "2019.1.0"),
			Path.Combine(ExampleEditorInstallRoot, "2020.1.3f1"),
			Path.Combine(ExampleEditorInstallRoot, "2020.2.0")
		};

		[Test]
		public void ListEditors() {
			var stream = OpenTestStream();

			Interpreter.Instance.Run(new string[] { "editor", "list", "--install-path", ExampleEditorInstallRoot });

			CollectionAssert.AreEquivalent(EDITORS, stream.ToString().Split(stream.NewLine, StringSplitOptions.RemoveEmptyEntries));
		}
	}
}
