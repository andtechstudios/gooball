using CommandLine;
using System.IO;

namespace Gooball
{

	[Verb("transform", HelpText = "Hide a folder from Unity.")]
	internal class TransformationOptions
	{
		[Value(0, Required = true, MetaName = "command", HelpText = "The transformation action to perform.")]
		public string Command { get; set; }

		// Hide-Folder options
		[Value(1, Required = true, MetaName = "target", HelpText = "The target file/folder.")]
		public string Target { get; set; }

		// Inject options
		[Option('h', "headerfile", HelpText = "A textfile containing the header text.")]
		public string HeaderFilePath { get; set; }
	}

	internal static class TransformationOperation
	{

		[Operation(typeof(TransformationOptions))]
		public static void OnParse(TransformationOptions options)
		{
			switch (options.Command)
			{
				case "inject":
					Inject();
					break;
				case "hide-folder":
					HideUnityFolder();
					break;
			}

			void Inject()
			{
				new Injector() { HeaderFilePath = options.HeaderFilePath }.Prepend(options.Target);
			}

			void HideUnityFolder()
			{
				var folderPath = options.Target;
				HideFolder();
				HideMetaFile();

				void HideFolder()
				{
					if (!Directory.Exists(folderPath))
						throw new DirectoryNotFoundException(folderPath);

					var name = Path.GetFileName(folderPath);
					var directory = Path.GetDirectoryName(folderPath);
					var destination = Path.Join(directory, $"{name}~");
					if (Directory.Exists(destination))
						Directory.Delete(destination, true);
					Directory.Move(folderPath, destination);
				}

				void HideMetaFile()
				{
					var source = Path.ChangeExtension(folderPath, ".meta");
					if (File.Exists(source))
						File.Delete(source);
				}
			}
		}
	}
}
