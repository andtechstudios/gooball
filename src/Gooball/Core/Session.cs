using Andtech.Common;

namespace Andtech.Gooball
{

	internal class Session
	{
		public string[] ToolArgs { get; set; }
		public string[] PassthroughArgs { get; set; }
		public string PreferredEditorVersion { get; set; } = "latest";
		public UnityInstallationHelper InstallationHelper { get; set; }

		public static Session Instance { get; set; }

		public UnityEditor GetEditor() => GetEditor(PreferredEditorVersion);

		public UnityEditor GetEditor(string fallbackVersion)
		{
			var version = string.IsNullOrEmpty(PreferredEditorVersion) ? fallbackVersion : PreferredEditorVersion;
			Log.WriteLine($"Version strategy is: '{version}'", Verbosity.diagnostic);
			return InstallationHelper.GetEditor(version);
		}
	}
}
