using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Andtech.Gooball
{

	public class VersionUtility
	{
		private static readonly string VersionExpression = @"(?<major>[0-9a-fA-F]+)(\.(?<minor>[0-9a-fA-F]+))(\.(?<build>[0-9a-fA-F]+))?(\.(?<revision>[0-9a-fA-F]+))?";

		public static Version Parse(string input)
		{
			var match = Regex.Match(input, VersionExpression);
			int.TryParse(match.Groups["major"].Value, out var major);
			int.TryParse(match.Groups["minor"].Value, out var minor);
			int.TryParse(match.Groups["build"].Value, out var build);
			int.TryParse(match.Groups["revision"].Value, out var revision);

			return new Version(major, minor, build, revision);
		}
	}
}
