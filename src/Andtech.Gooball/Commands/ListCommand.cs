
/* Unmerged change from project 'Gooball (net6.0)'
Before:
using CommandLine;
using System;
using System.Threading.Tasks;
After:
using System;
using System.Threading.Tasks;
using CommandLine;
*/
using CommandLine;

namespace Andtech.Gooball
{

	internal class ListCommand
	{
		[Verb("list", HelpText = "List installed editors.")]
		public class Options : BaseOptions { }

		public async Task OnParseAsync(Options options)
		{
			foreach (var editor in Session.Instance.InstallationHelper.Editors)
			{
				Console.WriteLine($"{editor.VersionRaw} [{editor.ExecutablePath}]");
			}
		}
	}
}
