using NUnit.Framework;
using System;
using System.IO;

namespace Gooball {

	public class GooballTests {
		protected static readonly string ExampleProjectRoot = @"TestFiles/Example Unity Project";
		protected static readonly string ExamplePackageRoot = @"TestFiles/Example Unity Project/Assets/Standard Assets/Example Package";
		protected static readonly string ExampleEditorInstallRoot = @"TestFiles/Editor Installations/Unity/Hub/Editor";

		[TearDown]
		public void RestoreConsole() {
			var standardOutput = new StreamWriter(Console.OpenStandardOutput()) {
				AutoFlush = true
			};
			Console.SetOut(standardOutput);
		}

		protected TextWriter OpenTestStream() {
			var writer = new StringWriter();
			Console.SetOut(writer);
			Console.SetError(writer);

			return writer;
		}
	}
}
