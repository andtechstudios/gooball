using System.Text.RegularExpressions;

namespace Andtech.Gooball
{

	public class UnityInstallationHelper
	{
		public IEnumerable<UnityEditor> Editors => editors;

		private readonly List<UnityEditor> editors;
		private const string PATH_DEFAULT_WINDOWS = @"C:/Program Files/Unity/Hub/Editor";
		private const string PATH_DEFAULT_OSX = @"/Applications/Unity/Hub/Editor";
		private const string PATH_DEFAULT_LINUX = @"~/Unity/Hub/Editor";
		private static IEnumerable<string> DEFAULT_PATHS
		{
			get
			{
				yield return PATH_DEFAULT_WINDOWS;
				yield return PATH_DEFAULT_OSX;
				yield return PATH_DEFAULT_LINUX;
			}
		}

		public UnityInstallationHelper(string installRoot = null)
		{
			editors = GetEditors(installRoot);
		}

		private List<UnityEditor> GetEditors(string installRoot)
		{
			var searchPaths = new Queue<string>();
			if (string.IsNullOrEmpty(installRoot))
			{
				searchPaths = new Queue<string>(DEFAULT_PATHS);
			}
			else
			{
				searchPaths = new Queue<string>(1);
				searchPaths.Enqueue(installRoot);
			}

			var windowsDriveRegex = new Regex("^C:");

			var wsl = searchPaths
				.Where(x => windowsDriveRegex.IsMatch(x))
				.Select(x => windowsDriveRegex.Replace(x, "/mnt/c"));
			var roots = searchPaths
				.Concat(wsl);

			foreach (var editorRoot in roots)
			{
				try
				{
					var editors = EnumerateUnityEditors(editorRoot);
					if (editors.Any())
					{
						return editors.ToList();
					}
				}
				catch (IOException) { }
			}

			return new List<UnityEditor>(0);
		}

		public UnityEditor GetEditor(string version = null)
		{
			// Latest
			if (version == "latest")
			{
				return Editors.Last();
			}

			// Exact match
			var matches = Editors.Where(x => x.VersionRaw == version);
			if (matches.Any())
			{
				return matches.First();
			}

			// Next-highest
			if (Version.TryParse(version, out var v))
			{
				var versionHelper = new VersionSelectionHelper(Editors.Select(x => x.Version));
				var bestVersion = versionHelper.GetBestVersion(v);

				return editors.FirstOrDefault(x => x.Version == bestVersion);
			}

			return null;
		}

		private IEnumerable<UnityEditor> EnumerateUnityEditors(string root)
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
