using NUnit.Framework;
using System.IO;
using System.Linq;

namespace Andtech.Gooball.Tests
{

	public class HideTests : BaseGooballTests
	{

		[Test]
		public void HideDirectoryAsset()
		{
			var assetPath = Path.Combine(ExamplePackageRoot, "Samples");

			Interpreter.Instance.Run(new string[] { "hide", assetPath });

			Assert.IsFalse(Directory.Exists(assetPath));
			Assert.IsTrue(Directory.Exists(assetPath + "~"));
			Assert.IsFalse(File.Exists(assetPath + ".meta"));
		}

		[Test]
		public void HideFileAsset()
		{
			var assetPath = Path.Combine(ExamplePackageRoot, "README.md");

			Interpreter.Instance.Run(new string[] { "hide", assetPath });

			Assert.IsFalse(File.Exists(assetPath));
			Assert.IsTrue(File.Exists(assetPath + "~"));
			Assert.IsFalse(File.Exists(assetPath + ".meta"));
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
