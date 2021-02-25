using Newtonsoft.Json;

namespace Gooball
{
	/// <summary>
	/// A manifest file of a Unity package.
	/// </summary>  
	[JsonObject(ItemNullValueHandling = NullValueHandling.Ignore)]
	public partial class PackageManifest
	{
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("version")]
		public string Version { get; set; }

		[JsonProperty("displayName")]
		public string DisplayName { get; set; }

		[JsonProperty("description")]
		public string Description { get; set; }

		[JsonProperty("unity")]
		public string Unity { get; set; }

		[JsonProperty("unityRelease")]
		public string UnityRelease { get; set; }

		[JsonProperty("repository")]
		public string Repository { get; set; }

		[JsonProperty("license")]
		public string License { get; set; }

		[JsonProperty("dependencies")]
		public Dependencies Dependencies { get; set; }

		[JsonProperty("keywords")]
		public string[] Keywords { get; set; }

		[JsonProperty("author")]
		public Author Author { get; set; }

		[JsonProperty("samples")]
		public Sample[] Samples { get; set; }

		[JsonProperty("type")]
		public string Type { get; set; }
	}

	public partial class Author
	{
		[JsonProperty("name")]
		public string Name { get; set; }

		[JsonProperty("email")]
		public string Email { get; set; }

		[JsonProperty("url")]
		public string Url { get; set; }
	}

	public partial class Dependencies
	{
		[JsonProperty("com.andtech.core")]
		public string ComAndtechCore { get; set; }
	}

	public partial class Sample
	{
		[JsonProperty("displayName")]
		public string DisplayName { get; set; }

		[JsonProperty("Description")]
		public string Description { get; set; }

		[JsonProperty("path")]
		public string Path { get; set; }
	}
}
