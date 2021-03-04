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
	}
}
