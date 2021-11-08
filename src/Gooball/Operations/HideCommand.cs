using CommandLine;
using System.IO;
using System.Text.RegularExpressions;

namespace Gooball
{

	internal class HideCommand
	{
		[Verb("hide", HelpText = "Hide assets from the Unity asset database.")]
		public class Options
		{
			[Value(0, HelpText = "Hides the path in a package manifest instead of the filesystem.")]
			public string TargetPath { get; set; }
			[Option("in-package", HelpText = "Hides the path in a package manifest instead of the filesystem.")]
			public string PackagePath { get; set; }
		}

		public static void OnParse(Options options)
		{
			if (string.IsNullOrEmpty(options.PackagePath))
			{
				var targetPath = options.TargetPath;
				HideFolder();
				HideMetaFile();

				void HideFolder()
				{
					var fileName = Path.GetFileName(targetPath);
					var directory = Path.GetDirectoryName(targetPath);
					var destinationPath = Path.Join(directory, $"{fileName}~");

					if (Directory.Exists(targetPath))
					{
						if (Directory.Exists(destinationPath))
						{
							Directory.Delete(destinationPath, true);
						}
						Directory.Move(targetPath, destinationPath);
					}
					else if (File.Exists(targetPath))
					{
						if (File.Exists(destinationPath))
						{
							File.Delete(destinationPath);
						}
						File.Move(targetPath, destinationPath);
					}
				}

				void HideMetaFile()
				{
					var metaFilePath = Path.ChangeExtension(targetPath, ".meta");
					if (File.Exists(metaFilePath))
					{
						File.Delete(metaFilePath);
					}
				}
			}
			else
			{
				var packagePath = options.PackagePath;
				var package = Package.Read(packagePath);
				foreach (var sample in package.Samples)
				{
					var destination = HideFolderInPath(options.TargetPath, sample.Path);
					sample.Path = destination;
				}
				Package.Write(package);

				string HideFolderInPath(string folderPath, string path)
				{
					var folderName = Path.GetFileName(folderPath);
					MatchEvaluator evaluator = m =>
					{
						var chunk = m.Value;
						var p = Regex.Replace(chunk, folderName, folderName + "~");

						return p;
					};

					return PathUtility.FolderRegex(folderPath).Replace(path, evaluator);
				}
			}
		}
	}
}
