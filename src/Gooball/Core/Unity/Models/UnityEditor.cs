using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Andtech.Gooball
{

	public class UnityEditor
	{
		public string Path { get; set; }
		public string ExecutablePath => GetExecutablePath(Path);
		public Version Version { get; set; }
		public string VersionRaw { get; set; }

		public static UnityEditor Read(string path)
		{
			var fileName = System.IO.Path.GetFileName(path);
			var version = VersionUtility.Parse(fileName);

			return new UnityEditor()
			{
				Path = path,
				Version = version,
				VersionRaw = fileName
			};
		}

		public static string GetExecutablePath(string directory)
		{
			if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
			{
				return System.IO.Path.Join(directory, "Unity.app/Contents/MacOS/Unity");
			}
			else if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
			{
				return System.IO.Path.Join(directory, "Editor/Unity.exe");
			}

			return System.IO.Path.Join(directory, "Editor/Unity.exe");
		}
	}
}
