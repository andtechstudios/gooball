using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace Gooball.Tests {

	public class TransformationTests : GooballTests {

		[Test]
		public void HideFolder() {
			var folderPath = Path.Combine(ExamplePackageRoot, "Samples");
			var metafilePath = Path.ChangeExtension(folderPath, ".meta");

			Interpreter.Instance.Run(new string[] { "transform", "hide-folder", folderPath });

			Assert.IsFalse(Directory.Exists(folderPath));
			Assert.IsTrue(Directory.Exists(folderPath + "~"));
			Assert.IsFalse(File.Exists(metafilePath));
		}

		[Test]
		public void InjectOperations() {
			var filePath = Path.Combine(ExampleProjectRoot, "Assets/Scripts/Script.cs");
			var headerPath = Path.Combine("TestFiles", "preamble.txt");

			Interpreter.Instance.Run(new string[] { "transform", "inject", "--headerfile", headerPath, filePath });

			var header = File.ReadAllText(headerPath);
			var contents = File.ReadAllText(filePath);

			var headerLines = header.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
			var fileLines = contents.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);

			foreach (var line in headerLines) {
				Assert.IsTrue(fileLines.Any(x => x.Contains(line)));
			}
		}
	}
}
