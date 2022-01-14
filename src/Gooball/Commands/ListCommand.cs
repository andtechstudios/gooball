using CommandLine;
using System;
using System.Threading.Tasks;

namespace Andtech.Gooball
{

	internal class ListCommand
	{
		[Verb("list", HelpText = "List installed editors.")]
		public class Options : GooballOptions { }

		public async Task OnParseAsync(Options options)
		{
			foreach (var editor in Session.Instance.InstallationHelper.Editors)
			{
				Console.WriteLine($"{editor.VersionRaw} [{editor.ExecutablePath}]");
			}
		}
	}
}
