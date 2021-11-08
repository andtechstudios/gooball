using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gooball
{

	public class VersionSelectionHelper
	{
		private readonly IEnumerable<Version> versions;

		public VersionSelectionHelper(IEnumerable<Version> versions)
		{
			this.versions = versions;
		}

		public Version GetBestVersion(Version heuristic)
		{
			if (versions.Any(x => x == heuristic))
			{
				return heuristic;
			}

			var nextHigher = HigherThan(heuristic)
				.OrderBy(x => x.Major).ThenBy(x => x.Minor).ThenBy(x => x.Build)
				.FirstOrDefault();
			if (nextHigher != null)
			{
				return nextHigher;
			}

			var nextLower = LowerThan(heuristic)
				.OrderBy(x => x.Major).OrderBy(x => x.Minor).OrderBy(x => x.Build)
				.LastOrDefault();

			return nextLower;
		}

		private IEnumerable<Version> HigherThan(Version heuristic)
		{
			return versions.Where(x =>
				   x.Major > heuristic.Major ||
				   x.Major >= heuristic.Major && x.Minor > heuristic.Minor ||
				   x.Major >= heuristic.Major && x.Minor >= heuristic.Minor && x.Build > heuristic.Build);
		}

		private IEnumerable<Version> LowerThan(Version heuristic)
		{
			return versions.Where(x =>
				x.Major < heuristic.Major ||
				x.Major <= heuristic.Major && x.Minor < heuristic.Minor ||
				x.Major <= heuristic.Major && x.Minor <= heuristic.Minor && x.Build < heuristic.Build);
		}
	}
}
