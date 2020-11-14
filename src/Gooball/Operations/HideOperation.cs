using CommandLine;
using System.IO;

namespace Gooball {

	[Verb("hide-folder", HelpText = "Hide a folder from Unity.")]
	internal class HideOptions {
		[Value(0, Required = true, MetaName = "folder", HelpText = "The folder to hide.")]
		public string Folder { get; set; }
	}

	internal static class HideOperation {

		[Operation(typeof(HideOptions))]
		public static void OnParse(HideOptions options) {
			HideProjectFolder(options.Folder);
		}

		private static void HideProjectFolder(string folderPath) {
			HideFolder();
			HideMetaFile();

			void HideFolder() {
				if (!Directory.Exists(folderPath))
					throw new DirectoryNotFoundException(folderPath);

				var name = Path.GetFileName(folderPath);
				var directory = Path.GetDirectoryName(folderPath);
				var destination = Path.Join(directory, $"{name}~");
				if (Directory.Exists(destination))
					Directory.Delete(destination, true);
				Directory.Move(folderPath, destination);
			}

			void HideMetaFile() {
				var source = Path.ChangeExtension(folderPath, ".meta");
				if (!File.Exists(source))
					throw new DirectoryNotFoundException(source);

				File.Delete(source);
			}
		}
	}
}
