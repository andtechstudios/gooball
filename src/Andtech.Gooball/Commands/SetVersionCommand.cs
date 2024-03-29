﻿
/* Unmerged change from project 'Gooball (net6.0)'
Before:
using CommandLine;
using System;
After:
using System;
*/
using System.Text.RegularExpressions;
using CommandLine;

namespace Andtech.Gooball
{

	internal class SetVersionCommand
	{
		[Verb("set-version", HelpText = "Set the version number of a Unity project.")]
		public class Options : BaseOptions
		{
			[Value(1, HelpText = "The version.")]
			public string Value { get; set; }

			[Option("trim", HelpText = "Trim non-numeric characters from the front of string.")]
			public bool Trim { get; set; }
		}

		public async Task OnParseAsync(Options options)
		{
			var project = Project.Read(options.ProjectPath);

			var value = options.Value;
			if (options.Trim)
			{
				value = Regex.Replace(value, @"^[^\d]+", string.Empty);
			}
			project.SetVersion(value);
		}
	}
}
