using Newtonsoft.Json;
using System.Collections.Generic;

namespace Gooball {

	/// <summary>
	/// A manifest file of a custom Unity package.
	/// </summary>
	public class PackageManifest {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("version")]
		public string Version { get; set; }
        [JsonProperty("displayName")]
		public string DisplayName { get; set; }
		[JsonProperty("description")]
		public string Description { get; set; }
		[JsonProperty("author")]
		public PackageAuthor Author { get; set; }
		[JsonProperty("samples")]
		public List<PackageSample> Samples { get; set; }

		public class PackageAuthor {
			[JsonProperty("name")]
			public string Name { get; set; }
			[JsonProperty("email")]
			public string Email { get; set; }
		}

		public class PackageSample {
			[JsonProperty("displayName")]
			public string DisplayName { get; set; }
			[JsonProperty("Description")]
			public string Description { get; set; }
			[JsonProperty("path")]
			public string Path { get; set; }
		}
    }
}
