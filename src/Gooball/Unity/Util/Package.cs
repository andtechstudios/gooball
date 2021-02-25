using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using static Gooball.PackageManifest;

namespace Gooball {

	/// <summary>
	/// A custom Unity package.
	/// </summary>
	public class Package {
		public readonly string Path;
		public string Version {
			get => Manifest.Version;
			set => Manifest.Version = value;
		}
		public Sample[] Samples => Manifest.Samples;

		private string ManifestPath => System.IO.Path.Join(Path, "package.json");
		private readonly PackageManifest Manifest;

		private Package(string path, PackageManifest manifest) {
			Path = path;
			Manifest = manifest;
		}

		public static Package Read(string packagePath) {
			var manifestPath = System.IO.Path.Join(packagePath, "package.json");

			var json = File.ReadAllText(manifestPath);
			var package = new Package(packagePath, JsonConvert.DeserializeObject<PackageManifest>(json));

			return package;
		}

		public static void Write(Package package) {
			var json = JsonConvert.SerializeObject(package.Manifest, Formatting.Indented);
			File.WriteAllText(package.ManifestPath, json);
		}
	}
}
