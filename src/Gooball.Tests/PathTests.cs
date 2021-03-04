using NUnit.Framework;

namespace Gooball.Tests
{

	public class PathTests
	{

		[Test]
		public static void FindRootedFolder()
		{
			var regex = PathUtility.FolderRegex("Samples");

			Assert.IsTrue(regex.IsMatch(@"Samples"));
		}

		[Test]
		public static void RejectDeepFolder()
		{
			var regex = PathUtility.FolderRegex("Samples");

			Assert.IsFalse(regex.IsMatch(@"Costco/Samples"));
			Assert.IsFalse(regex.IsMatch(@"Audio/Projects/Samples"));
		}
	}
}
