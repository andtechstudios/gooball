using CommandLine;
using System;
using System.IO;
using System.Text.RegularExpressions;

namespace Gooball
{


	[Verb("package", HelpText = "Commands for working with custom Unity packages.")]
	internal class PackageOptions
	{
		[Value(0, Required = true, MetaName = "command", HelpText = "The package action to perform.")]
		public string Command { get; set; }
		[Value(1, Required = false, Default = "./", MetaName = "package-path", HelpText = "The path to the package.")]
		public string PackagePath { get; set; }

		// Bump options
		[Option("major", HelpText = "Increment the major version code.")]
		public bool IncrementMajor { get; set; }
		[Option("minor", HelpText = "Increment the minor version code.")]
		public bool IncrementMinor { get; set; }
		[Option("patch", HelpText = "Increment the patch version code.")]
		public bool IncrementPatch { get; set; }

		// Hide options
		[Value(2, MetaName = "folder", HelpText = "The folder hide in the manifest. (Relative to the package root)")]
		public string FolderPath { get; set; }
	}

	internal static class PackageOperation
	{

		[Operation(typeof(PackageOptions))]
		public static void OnParse(PackageOptions options)
		{
			var packagePath = options.PackagePath;
			var package = Package.Read(packagePath);

			switch (options.Command)
			{
				case "get-version":
					GetVersion();
					break;
				case "ignore-folder":
					IgnoreFolder();
					break;
				case "bump":
					Bump();
					break;
			}

			void GetVersion()
			{
				Console.WriteLine(package.Version);
			}

			void IgnoreFolder()
			{
				Ignore(package, options.FolderPath);
				Package.Write(package);
			}

			void Bump()
			{
				package.Increment(GetVersionMask());
				Package.Write(package);

				VersionFlag GetVersionMask()
				{
					if (options.IncrementMajor)
						return VersionFlag.Major;

					if (options.IncrementMinor)
						return VersionFlag.Minor;

					if (options.IncrementPatch)
						return VersionFlag.Build;

					return VersionFlag.None;
				}
			}
		}

		private static void Ignore(Package package, string folderPath)
		{
			foreach (var sample in package.Samples)
			{
				var destination = HideFolderInPath(folderPath, sample.Path);
				sample.Path = destination;
			}

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
