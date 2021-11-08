using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace Gooball
{

	internal class UnityArgs : IEnumerable<string>
	{
		public string ProjectPath
		{
			get
			{
				var index = FindArg("projectPath");
				if (index < 0)
					return null;

				return Args[index + 1];
			}
		}
		public string ExecuteMethod
		{
			get
			{
				var index = FindArg("executeMethod");
				if (index < 0)
					return null;

				return Args[index + 1];
			}
		}
		public string TestResults
		{
			get
			{
				var index = FindArg("testResults");
				if (index < 0)
					return null;

				return Args[index + 1];
			}
		}
		public bool BatchMode => FindArg("batchMode") >= 0;
		public bool Quit => FindArg("quit") >= 0;
		public bool RunTests => FindArg("runTests") >= 0;

		private readonly IList<string> Args;

		public UnityArgs(IEnumerable<string> collection)
		{
			Args = new List<string>(collection);
		}

		int FindArg(string switchText)
		{
			var regex = new Regex($"(-|--)?{switchText}", RegexOptions.IgnoreCase);
			for (int i = 0; i < Args.Count; i++)
			{
				if (regex.IsMatch(Args[i]))
					return i;
			}

			return -1;
		}

		IEnumerator<string> IEnumerable<string>.GetEnumerator() => Args.GetEnumerator();

		IEnumerator IEnumerable.GetEnumerator() => (Args as IEnumerable)?.GetEnumerator();
	}
}
