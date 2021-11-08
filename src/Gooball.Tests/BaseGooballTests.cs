using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Andtech.Gooball.Tests
{

	public class BaseGooballTests
	{
		protected static readonly string ExampleProjectRoot = @"Example Unity Project";
		protected static readonly string ExamplePackageRoot = @"Example Unity Project/Assets/Standard Assets/Example Package";
		protected static readonly string ExampleEditorInstallRoot = @"Editor Installations/Unity/Hub/Editor";

		[SetUp]
		public void Initialize()
		{
			var testDirectory = Path.Combine(TestContext.CurrentContext.TestDirectory, "TestFiles");
			Directory.SetCurrentDirectory(testDirectory);
		}

		private TextWriter OpenOut()
		{
			var writer = new StringWriter();
			Console.SetOut(writer);

			return writer;
		}

		private void CloseOut()
		{
			var standardOutput = new StreamWriter(Console.OpenStandardOutput())
			{
				AutoFlush = true
			};
			Console.SetOut(standardOutput);
		}

		protected void AssertConsoleEquals(string expected, Action action)
		{
			var stream = OpenOut();
			action();
			CloseOut();

			Assert.AreEqual(expected, stream.ToString().Trim());
		}

		protected void AssertConsoleEquals(IEnumerable<string> expected, Action action)
		{
			var stream = OpenOut();
			action();
			CloseOut();

			var lines =
				from line in stream.ToString().Split(stream.NewLine, StringSplitOptions.RemoveEmptyEntries)
				select line.Trim();
			CollectionAssert.AreEquivalent(expected, lines);
		}
	}
}
