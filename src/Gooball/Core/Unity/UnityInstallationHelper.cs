using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Andtech.Gooball
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

		private readonly Queue<string> editorSearchPaths;

		public UnityInstallationHelper(string editorInstallationRoot = null)
		{
			if (string.IsNullOrEmpty(editorInstallationRoot))
			{
				editorSearchPaths = new Queue<string>(DEFAULT_PATHS);
			}
			else
			{
				editorSearchPaths = new Queue<string>(1);
				editorSearchPaths.Enqueue(editorInstallationRoot);
			}
		}

		public IEnumerable<UnityEditor> GetInstalledEditors()
		{
			var windowsDriveRegex = new Regex("^C:");

			var wsl = editorSearchPaths
				.Where(x => windowsDriveRegex.IsMatch(x))
				.Select(x => windowsDriveRegex.Replace(x, "/mnt/c"));
			var roots = editorSearchPaths
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

			return Enumerable.Empty<UnityEditor>();
		}

		public UnityEditor GetBestEditor(Version version)
		{
			var editors = GetInstalledEditors();
			var versionHelper = new VersionSelectionHelper(editors.Select(x => x.Version));
			var bestVersion = versionHelper.GetBestVersion(version);

			return editors.First(x => x.Version == bestVersion);
		}

		private IEnumerable<UnityEditor> GetInstalledEditors(string root)
		{
			var editors =
				from path in Directory.EnumerateDirectories(root, "*")
				select UnityEditor.Read(path);

			return editors
				.OrderBy(x => x.Version.Major)
				.ThenBy(x => x.Version.Minor)
				.ThenBy(x => x.Version.Build)
				.ThenBy(x => x.Version.Revision);
		}
	}
}
