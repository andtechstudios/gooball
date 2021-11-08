using NUnit.Framework;
using System;
using System.IO;
using System.Linq;

namespace Gooball.Tests
{

	public class HideTests : BaseGooballTests
	{

		[Test]
		public void HideAsset()
		{
			var folderPath = Path.Combine(ExamplePackageRoot, "Samples");
			var metafilePath = Path.ChangeExtension(folderPath, ".meta");

			Interpreter.Instance.Run(new string[] { "hide", folderPath });

			Assert.IsFalse(Directory.Exists(folderPath));
			Assert.IsTrue(Directory.Exists(folderPath + "~"));
			Assert.IsFalse(File.Exists(metafilePath));
		}

		[Test]
		public void HideAssetInPackage()
		{
			Interpreter.Instance.Run(new string[] { "hide", "--in-package", ExamplePackageRoot, "Samples" });

			var regex = PathUtility.FolderRegex("Samples");
			var package = Package.Read(ExamplePackageRoot);

			Assert.IsFalse(package.Samples.Any(x => regex.IsMatch(x.Path)));
		}
	}
}
