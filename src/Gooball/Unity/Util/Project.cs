using System.IO;
using System.Text.RegularExpressions;

namespace Gooball {

	/// <summary>
	/// A Unity project.
	/// </summary>
	public class Project {
		public string Version { get; set; }
		public string EditorVersion { get; set; }

		public readonly string Path;

		private Project(string projectPath) {
			Path = projectPath;
		}

		public static Project Read(string path) {
			var project = new Project(path) {
				Version = GetValue(System.IO.Path.Join(path, "ProjectSettings", "ProjectSettings.asset"), "bundleVersion"),
				EditorVersion = GetValue(System.IO.Path.Join(path, "ProjectSettings", "ProjectVersion.txt"), "m_EditorVersion")
			};

			return project;
		}

		private static string GetValue(string file, string key) {
			string content = File.ReadAllText(file);
			var pattern = $@"{key}:\s*([a-f0-9\.]+)";
			var match = Regex.Match(content, pattern);

			return match.Success ? match.Groups[1].Value : null;
		}
	}
}
