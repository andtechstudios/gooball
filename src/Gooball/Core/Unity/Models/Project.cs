using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Andtech.Gooball
{

	public class ProjectNotFoundException : Exception
	{
		public string Path { get; set; }

		public ProjectNotFoundException(string message) : base(message) { }
	}

	/// <summary>
	/// A Unity project.
	/// </summary>
	public class Project
	{
		public string Version { get; set; }
		public string EditorVersion { get; set; }

		public readonly string Path;

		private Project(string projectPath)
		{
			Path = projectPath;
		}

		public static Project Read(string path)
		{
			try
			{
				var project = new Project(path)
				{
					Version = GetValue(System.IO.Path.Join(path, "ProjectSettings", "ProjectSettings.asset"), "bundleVersion"),
					EditorVersion = GetValue(System.IO.Path.Join(path, "ProjectSettings", "ProjectVersion.txt"), "m_EditorVersion")
				};

				return project;
			}
			catch (IOException)
			{
				throw new ProjectNotFoundException("Unable to find the specified Unity Project.") { Path = path };
			}
		}

		public void SetVersion(string value) => SetValue(
			System.IO.Path.Join(Path, "ProjectSettings", "ProjectSettings.asset"),
			"bundleVersion",
			value
		);

		private static string GetValue(string file, string key)
		{
			string content = File.ReadAllText(file);
			var pattern = $@"{key}:\s*([^\n]+)";
			var match = Regex.Match(content, pattern);

			return match.Success ? match.Groups[1].Value : null;
		}

		private static void SetValue(string file, string key, string value)
		{
			string content = File.ReadAllText(file);
			var pattern = $@"{key}:\s*([^\n]+)";
			content = Regex.Replace(content, pattern, $"{key}: {value}");
			File.WriteAllText(file, content);
		}
	}
}
