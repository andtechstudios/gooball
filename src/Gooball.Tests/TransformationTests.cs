using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace Gooball.Tests
{

	public class TransformationTests : GooballTests
	{

		[Test]
		public void HideFolder()
		{
			var folderPath = Path.Combine(ExamplePackageRoot, "Samples");
			var metafilePath = Path.ChangeExtension(folderPath, ".meta");

			Interpreter.Instance.Run(new string[] { "hide", folderPath });

			Assert.IsFalse(Directory.Exists(folderPath));
			Assert.IsTrue(Directory.Exists(folderPath + "~"));
			Assert.IsFalse(File.Exists(metafilePath));
		}
	}
}
