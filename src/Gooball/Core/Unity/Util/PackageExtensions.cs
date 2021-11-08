using System;

namespace Gooball
{

	public static class PackageExtensions
	{

		public static void Increment(this Package package, VersionFlag mask)
		{
			Bump();

			void Bump()
			{
				var previous = new Version(package.Version);
				var next = previous.Increment(mask);
				package.Version = next.ToString();
			}
		}
	}
}
