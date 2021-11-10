using System.Collections.Generic;

namespace Andtech.Gooball
{

	internal class UnityStartInfo
	{
		public string PreferredEditorVersion { get; set; }
		public bool DryRun { get; set; }
		public bool Follow { get; set; }
		public List<string> Args => args;

		private readonly List<string> args;

		public UnityStartInfo(params string[] collection)
		{
			args = new List<string>(collection);
		}

		public static UnityStartInfo Open(UnityProjectOptions options, params string[] args)
		{
			var startInfo = new UnityStartInfo(args);
			if (!ArgumentUtility.HasFlag(args, "projectPath"))
			{
				startInfo.Args.Add("-projectPath");
				startInfo.Args.Add(options.ProjectPath);
			}
			startInfo.DryRun = options.DryRun;
			startInfo.Follow = options.Follow;

			return startInfo;
		}

		public static UnityStartInfo Build(UnityProjectOptions options, params string[] args)
		{
			var startInfo = new UnityStartInfo(args);
			if (!ArgumentUtility.HasFlag(args, "batchMode"))
			{
				startInfo.Args.Add("-batchMode");
			}
			if (!ArgumentUtility.HasFlag(args, "quit"))
			{
				startInfo.Args.Add("-quit");
			}
			if (!ArgumentUtility.HasFlag(args, "projectPath"))
			{
				startInfo.Args.Add("-projectPath");
				startInfo.Args.Add(options.ProjectPath);
			}
			startInfo.DryRun = options.DryRun;
			startInfo.Follow = options.Follow;

			return startInfo;
		}

		public static UnityStartInfo Test(UnityProjectOptions options, params string[] args)
		{
			var startInfo = new UnityStartInfo(args);
			if (!ArgumentUtility.HasFlag(args, "runTests"))
			{
				startInfo.args.Add($"-runTests");
			}
			if (!ArgumentUtility.HasFlag(args, "batchMode"))
			{
				startInfo.Args.Add("-batchMode");
			}
			if (!ArgumentUtility.HasFlag(args, "projectPath"))
			{
				startInfo.Args.Add("-projectPath");
				startInfo.Args.Add(options.ProjectPath);
			}
			startInfo.DryRun = options.DryRun;
			startInfo.Follow = options.Follow;

			return startInfo;
		}
	}
}
