using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Gooball
{
	/// <summary>
	/// A manifest file of a Unity package.
	/// </summary>  
	[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
	public partial class PackageManifest
	{
		[JsonProperty("name", Order = -13, NullValueHandling = NullValueHandling.Include)]
		public string Name { get; set; }

		[JsonProperty("version", Order = -12, NullValueHandling = NullValueHandling.Include)]
		public string Version { get; set; }

		[JsonProperty("displayName", Order = -11, NullValueHandling = NullValueHandling.Include)]
		public string DisplayName { get; set; }

		[JsonProperty("description", Order = -10, NullValueHandling = NullValueHandling.Include)]
		public string Description { get; set; }

		[JsonProperty("unity", Order = -9, NullValueHandling = NullValueHandling.Include)]
		public string Unity { get; set; }

		[JsonProperty("unityRelease", Order = -8)]
		public string UnityRelease { get; set; }

		[JsonProperty("dependencies", Order = -7)]
		public Dictionary<string, JToken> Dependencies { get; set; }

		[JsonProperty("keywords", Order = -6)]
		public string[] Keywords { get; set; }

		[JsonProperty("type", Order = -5)]
		public string Type { get; set; }

		[JsonProperty("author", Order = -4)]
		public Author Author { get; set; }

		[JsonProperty("repository", Order = -3)]
		public string Repository { get; set; }

		[JsonProperty("license", Order = -2)]
		public string License { get; set; }

		[JsonProperty("samples", Order = -1)]
		public Sample[] Samples { get; set; }
	}

	public partial class Author
	{
		[JsonProperty("name", Order = -3)]
		public string Name { get; set; }

		[JsonProperty("email", Order = -2)]
		public string Email { get; set; }

		[JsonProperty("url", Order = -1)]
		public string Url { get; set; }
	}

	public partial class Sample
	{
		[JsonProperty("displayName", Order = -3)]
		public string DisplayName { get; set; }

		[JsonProperty("description", Order = -2)]
		public string Description { get; set; }

		[JsonProperty("path", Order = -1)]
		public string Path { get; set; }
	}
}
