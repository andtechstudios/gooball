namespace Andtech.Gooball
{

	internal class UnityStartInfo
	{
		public UnityEditor Editor { get; set; }
		public Project Project { get; set; }
		public bool DryRun { get; set; }
		public bool Follow { get; set; }
		public List<string> Args => args;

		private readonly List<string> args;

		public UnityStartInfo(params string[] collection)
		{
			args = new List<string>(collection);
		}

		public static UnityStartInfo Open(BaseOptions options) => Default(options, requireProject: true);

		public static UnityStartInfo Build(BaseOptions options)
		{
			var startInfo = Default(options, requireProject: true);
			if (!ArgumentUtility.HasFlag(Session.Instance.PassthroughArgs, "batchMode"))
			{
				startInfo.Args.Add("-batchMode");
			}
			if (!ArgumentUtility.HasFlag(Session.Instance.PassthroughArgs, "quit"))
			{
				startInfo.Args.Add("-quit");
			}

			return startInfo;
		}

		public static UnityStartInfo Test(BaseOptions options)
		{
			var startInfo = Default(options, requireProject: true);
			if (!ArgumentUtility.HasFlag(Session.Instance.PassthroughArgs, "runTests"))
			{
				startInfo.args.Add($"-runTests");
			}
			if (!ArgumentUtility.HasFlag(Session.Instance.PassthroughArgs, "batchMode"))
			{
				startInfo.Args.Add("-batchMode");
			}

			return startInfo;
		}

		public static UnityStartInfo Run(BaseOptions options) => Default(options);

		private static UnityStartInfo Default(BaseOptions options, bool requireProject = false)
		{
			var args = Session.Instance.PassthroughArgs;
			var startInfo = new UnityStartInfo(args)
			{
				DryRun = options.DryRun,
				Follow = options.Follow,
			};

			if (!ArgumentUtility.TryGetOption(args, "projectPath", out var projectPath))
			{
				projectPath = options.ProjectPath;
			}

			try
			{
				var project = Project.Read(projectPath);
				startInfo.Editor = Session.Instance.GetEditor(project.EditorVersion);
				startInfo.Project = project;

				startInfo.Args.Add("-projectPath");
				startInfo.Args.Add(projectPath);
			}
			catch (ProjectNotFoundException)
			{
				if (requireProject)
				{
					throw;
				}

				startInfo.Editor = Session.Instance.GetEditor();
			}

			return startInfo;
		}
	}
}
