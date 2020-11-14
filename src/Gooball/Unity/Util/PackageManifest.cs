using System.Collections.Generic;

namespace Gooball {

	/// <summary>
	/// A manifest file of a custom Unity package.
	/// </summary>
	public class PackageManifest {
		public string Name { get; set; }
		public string Version { get; set; }
		public string DisplayName { get; set; }
		public string Description { get; set; }
		public string Unity { get; set; }
		public PackageAuthor Author { get; set; }
		public List<PackageSample> Samples { get; set; }

		public class PackageAuthor {
			public string Name { get; set; }
			public string Email { get; set; }
		}

		public class PackageSample {
			public string DisplayName { get; set; }
			public string Description { get; set; }
			public string Path { get; set; }
		}
	}
}
