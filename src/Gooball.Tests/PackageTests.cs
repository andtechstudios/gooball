using NUnit.Framework;
using System.IO;
using System.Linq;

namespace Gooball.Tests {

	public class PackageTests : GooballTests {
		private const string PACKAGE_VERSION = "1.2.3";

		[SetUp]
		public void InitializePackageVersion() {
			var package = Package.Read(ExamplePackageRoot);
			package.Version = PACKAGE_VERSION;
			Package.Write(package);
		}

		[TearDown]
		public void RevertPackageVersion() {
			var package = Package.Read(ExamplePackageRoot);
			package.Version = PACKAGE_VERSION;
			Package.Write(package);
		}

		[Test]
		public void GetPackageVersion() {
			var stream = OpenTestStream();

			Interpreter.Instance.Run(new string[] { "package", "get-version", ExamplePackageRoot });

			Assert.AreEqual(PACKAGE_VERSION, stream.ToString().Trim());
		}

		[Test]
		public void BumpMajorVersion() {
			Interpreter.Instance.Run(new string[] { "package", "bump", "--major", ExamplePackageRoot });

			IsVersion("2.0.0");
		}

		[Test]
		public void BumpMinorVersion() {
			Interpreter.Instance.Run(new string[] { "package", "bump", "--minor", ExamplePackageRoot });

			IsVersion("1.3.0");
		}

		[Test]
		public void BumpPatchVersion() {
			Interpreter.Instance.Run(new string[] { "package", "bump", "--patch", ExamplePackageRoot });

			IsVersion("1.2.4");
		}

		[Test]
		public void HideSamples() {
			Interpreter.Instance.Run(new string[] { "package", "ignore-folder", ExamplePackageRoot, Path.Combine(ExamplePackageRoot, "Samples") });

			var regex = PathUtility.FolderRegex("Samples");
			var package = Package.Read(ExamplePackageRoot);

			Assert.IsFalse(package.Samples.Any(x => regex.IsMatch(x.Path)));
		}

		private void IsVersion(string expected) {
			var package = Package.Read(ExamplePackageRoot);

			Assert.AreEqual(expected, package.Version);
		}
	}
}
