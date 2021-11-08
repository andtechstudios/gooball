using System;

namespace Gooball
{

	public enum VersionFlag
	{
		None,
		Major,
		Minor,
		Build,
		Revision
	}

	internal static class VersionExtensions
	{

		public static Version Increment(this Version version, VersionFlag mask)
		{
			switch (mask)
			{
				case VersionFlag.Major:
					return new Version(version.Major + 1, 0, 0);
				case VersionFlag.Minor:
					return new Version(version.Major, version.Minor + 1, 0);
				case VersionFlag.Build:
					return new Version(version.Major, version.Minor, version.Build + 1);
				case VersionFlag.Revision:
					return new Version(version.Major, version.Minor, version.Build, version.Revision + 1);
				default:
					return version;
			}
		}

		public static int GetSubVersion(this Version version, VersionFlag subVersion)
		{
			switch (subVersion)
			{
				case VersionFlag.Major:
					return version.Major;
				case VersionFlag.Minor:
					return version.Minor;
				case VersionFlag.Build:
					return version.Build;
				case VersionFlag.Revision:
					return version.Revision;
			}

			return -1;
		}
	}
}
