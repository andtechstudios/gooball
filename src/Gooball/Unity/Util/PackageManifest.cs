using Newtonsoft.Json;

namespace Gooball
{
    /// <summary>
    /// A manifest file of a Unity package.
    /// </summary>  
    public class PackageManifest
    {
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("version")]
        public string Version { get; set; } = string.Empty;

        [JsonProperty("displayName")]
        public string DisplayName { get; set; } = string.Empty;

        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;

        [JsonProperty("unity")]
        public string Unity { get; set; } = string.Empty;

        [JsonProperty("unityRelease")]
        public string UnityRelease { get; set; } = string.Empty;

        [JsonProperty("repository")]
        public string Repository { get; set; } = string.Empty;

        [JsonProperty("license")]
        public string License { get; set; } = string.Empty;

        [JsonProperty("dependencies")]
        public Dependencies Dependencies { get; set; }

        [JsonProperty("keywords")]
        public string[] Keywords { get; set; }

        [JsonProperty("author")]
        public Author Author { get; set; }

        [JsonProperty("samples")]
        public Sample[] Samples { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; } = string.Empty;
    }

    public partial class Author
    {
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [JsonProperty("email")]
        public string Email { get; set; } = string.Empty;

        [JsonProperty("url")]
        public string Url { get; set; } = string.Empty;
    }

    public partial class Dependencies
    {
        [JsonProperty("com.andtech.core")]
        public string ComAndtechCore { get; set; } = string.Empty;
    }

    public partial class Sample
    {
        [JsonProperty("displayName")]
        public string DisplayName { get; set; } = string.Empty;

        [JsonProperty("Description")]
        public string Description { get; set; } = string.Empty;

        [JsonProperty("path")]
        public string Path { get; set; } = string.Empty;
    }
}
