using NUnit.Framework;
using System;
using System.IO;

namespace Gooball.Tests
{

	public class EditorTests : BaseGooballTests
	{
		private static readonly string[] EDITORS = new string[] {
			Path.Combine(ExampleEditorInstallRoot, "2019.1.0"),
			Path.Combine(ExampleEditorInstallRoot, "2020.1.3f1"),
			Path.Combine(ExampleEditorInstallRoot, "2020.2.0")
		};

		[Test]
		public void ListInstalls()
		{
			Action action = () => Interpreter.Instance.Run(new string[] { "list", "--install-path", ExampleEditorInstallRoot });

			AssertConsoleEquals(EDITORS, action);
		}
	}
}
