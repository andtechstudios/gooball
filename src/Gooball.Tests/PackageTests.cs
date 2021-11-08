using NUnit.Framework;
using System;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Gooball.Tests
{

	public class PackageTests : GooballTests
	{
		private const string PACKAGE_VERSION = "1.2.3";

		[SetUp]
		public void InitializePackageVersion()
		{
			var package = Package.Read(ExamplePackageRoot);
			package.Version = PACKAGE_VERSION;
			Package.Write(package);
		}

		[Test]
		public void HideSamples()
		{
			Interpreter.Instance.Run(new string[] { "hide", "--in-package", ExamplePackageRoot, "Samples" });

			var regex = PathUtility.FolderRegex("Samples");
			var package = Package.Read(ExamplePackageRoot);

			Assert.IsFalse(package.Samples.Any(x => regex.IsMatch(x.Path)));
		}
	}
}
