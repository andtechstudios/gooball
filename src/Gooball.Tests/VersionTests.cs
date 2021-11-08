using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace Gooball.Tests
{

	public class VersionTests
	{
		private static readonly List<Version> Versions = new List<Version>
		{
			new Version(1, 1, 1),
			new Version(1, 5, 5),
			new Version(2, 1, 1),
			new Version(2, 5, 5),
			new Version(3, 1, 1),
			new Version(3, 5, 5),
		};

		[Test]
		public void GetExact()
		{
			var versionHelper = new VersionSelectionHelper(Versions);
			var current = new Version(2, 1, 1);
			var best = versionHelper.GetBestVersion(current);

			Assert.AreEqual(current, best);
		}

		[Test]
		public void GetUpgradeMinor()
		{
			var versionHelper = new VersionSelectionHelper(Versions);
			var current = new Version(2, 2, 1);
			var best = versionHelper.GetBestVersion(current);

			Assert.AreEqual(new Version(2, 5, 5), best);
		}

		[Test]
		public void GetUpgradeMajor()
		{
			var versionHelper = new VersionSelectionHelper(Versions);
			var current = new Version(2, 6, 1);
			var best = versionHelper.GetBestVersion(current);

			Assert.AreEqual(new Version(3, 1, 1), best);
		}

		[Test]
		public void GetDowngradeMajor()
		{
			var versionHelper = new VersionSelectionHelper(Versions);
			var current = new Version(4, 0, 0);
			var best = versionHelper.GetBestVersion(current);

			Assert.AreEqual(new Version(3, 5, 5), best);
		}

		[Test]
		public void GetDowngradeMinor()
		{
			var versionHelper = new VersionSelectionHelper(Versions);
			var current = new Version(3, 6, 0);
			var best = versionHelper.GetBestVersion(current);

			Assert.AreEqual(new Version(3, 5, 5), best);
		}
	}
}
