using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Gooball.Tests {

	public class PackageTests : GooballTests {
		private const string PACKAGE_VERSION = "1.2.3";

		[SetUp]
		public void InitializePackageVersion() {
			var package = Package.Read(ExamplePackageRoot);
			package.Version = PACKAGE_VERSION;
			Package.Write(package);
		}

		[Test]
		public void GetPackageVersion() {
			Action action = () => Interpreter.Instance.Run(new string[] { "package", "get-version", ExamplePackageRoot });

			AssertConsoleEquals(PACKAGE_VERSION, action);
		}

		[Test]
		public void GetPackageVersionImplicit() {
			Action action = () => {
				Directory.SetCurrentDirectory(ExamplePackageRoot);
				Interpreter.Instance.Run(new string[] { "package", "get-version" });
			};

			AssertConsoleEquals(PACKAGE_VERSION, action);
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
			Interpreter.Instance.Run(new string[] { "package", "ignore-folder", ExamplePackageRoot, "Samples" });

			var regex = PathUtility.FolderRegex("Samples");
			var package = Package.Read(ExamplePackageRoot);

			Assert.IsFalse(package.Samples.Any(x => regex.IsMatch(x.Path)));
		}

		[Test]
		public void SerializeNullValue() {
			Interpreter.Instance.Run(new string[] { "package", "bump", "--major", ExamplePackageRoot });

			var manifestPath = Path.Combine(ExamplePackageRoot, "package.json");
			var text = File.ReadAllText(manifestPath);
			var regex = new Regex("\"type\"/s*:");

			Console.WriteLine(text);

			Assert.IsFalse(regex.IsMatch(text));
		}

		private void IsVersion(string expected) {
			var package = Package.Read(ExamplePackageRoot);

			Assert.AreEqual(expected, package.Version);
		}
	}
}
