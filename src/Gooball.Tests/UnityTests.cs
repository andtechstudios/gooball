using NUnit.Framework;
using System;
using System.IO;

namespace Gooball.Tests {

	public class UnityTests : GooballTests {
		private static readonly string[] EDITORS = new string[] {
			Path.Combine(ExampleEditorInstallRoot, "2019.1.0"),
			Path.Combine(ExampleEditorInstallRoot, "2020.1.3f1"),
			Path.Combine(ExampleEditorInstallRoot, "2020.2.0")
		};

		[Test]
		public void ListInstalls() {
			var stream = OpenTestStream();

			Interpreter.Instance.Run(new string[] { "unity", "list-installs", "--install-path", ExampleEditorInstallRoot });

			CollectionAssert.AreEquivalent(EDITORS, stream.ToString().Split(stream.NewLine, StringSplitOptions.RemoveEmptyEntries));
		}
	}
}
