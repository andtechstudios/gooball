using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Gooball
{

	public class UnityInstallationHelper
	{
		private const string PATH_DEFAULT_WINDOWS = @"C:/Program Files/Unity/Hub/Editor";
		private const string PATH_DEFAULT_OSX = @"/Applications/Unity/Hub/Editor";
		private const string PATH_DEFAULT_LINUX = @"~/Unity/Hub/Editor";
		private IEnumerable<string> DEFAULT_PATHS
		{
			get
			{
				yield return PATH_DEFAULT_WINDOWS;
				yield return PATH_DEFAULT_OSX;
				yield return PATH_DEFAULT_LINUX;
			}
		}

		private readonly Queue<string> editorRoots;

		public UnityInstallationHelper()
		{
			editorRoots = new Queue<string>(DEFAULT_PATHS);
		}

		public UnityInstallationHelper(string editorInstallationRoot)
		{
			editorRoots = new Queue<string>(1);
			editorRoots.Enqueue(editorInstallationRoot);
		}

		public IEnumerable<string> GetInstalledEditors()
		{
			var windowsDriveRegex = new Regex("^C:");

			var wsl = editorRoots.Where(x => windowsDriveRegex.IsMatch(x)).Select(x => windowsDriveRegex.Replace(x, "/mnt/c"));
			var roots = editorRoots
				.Concat(wsl);

			foreach (var editorRoot in roots)
			{
				try
				{
					var editors = GetInstalledEditors(editorRoot);
					if (editors.Any())
					{
						return editors;
					}
				}
				catch (IOException) { }
			}

			return Enumerable.Empty<string>();
		}

		public string GetBestEditor(string projectVersion)
		{
			var installedEditors = GetInstalledEditors();

			string directory;
			if (installedEditors.Any(Match))
			{
				directory = installedEditors.First(Match);
			}
			else
			{
				directory = installedEditors.First();
			}

			return GetExecutablePath(directory);

			bool Match(string path) => Path.GetFileName(path) == projectVersion;
		}

		public static string GetExecutablePath(string directory)
		{
			return Path.Join(directory, "Editor/Unity.exe");
		}

		private IEnumerable<string> GetInstalledEditors(string root)
		{
			var editors =
				from path in Directory.EnumerateDirectories(root, "*")
				orderby path descending
				select path;

			return editors;
		}
	}
}
