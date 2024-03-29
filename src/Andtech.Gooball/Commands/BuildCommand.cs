﻿using Andtech.Common;
using CommandLine;

namespace Andtech.Gooball
{

	internal class BuildCommand
	{
		[Verb("build", HelpText = "Build a Unity project.")]
		internal class Options : BaseOptions { }

		public async Task OnParseAsync(Options options)
		{
			var startInfo = UnityStartInfo.Build(options);
			var process = new UnityProcess(startInfo)
			{
				EnableWSLMode = !options.DisablePowershellShim,
			};
			try
			{
				await process.RunAsync();
				Environment.ExitCode = process.ExitCode;
			}
			catch (ProjectNotFoundException)
			{
				Log.Error.WriteLine($"The directory at '{options.ProjectPath}' is not a Unity project.", ConsoleColor.Red, Verbosity.minimal);
				Environment.Exit(1);
			}
			catch
			{
				Environment.ExitCode = process.ExitCode;
				throw;
			}
		}
	}
}
