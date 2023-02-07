namespace Andtech.Gooball
{

	internal class Tail
	{
		private readonly string path;
		private readonly FileSystemWatcher watcher;
		private StreamReader reader;

		public Tail(string path)
		{
			this.path = path;
			var filename = Path.GetFileName(path);
			var directory = Path.GetDirectoryName(path);
			directory = string.IsNullOrEmpty(directory) ? Environment.CurrentDirectory : directory;

			watcher = new FileSystemWatcher()
			{
				Path = directory,
				Filter = filename,
			};
			watcher.Changed += new FileSystemEventHandler(OnChanged);
		}

		public void Start()
		{
			watcher.EnableRaisingEvents = true;
		}

		public void Stop()
		{
			watcher.EnableRaisingEvents = false;
			Dump();
		}

		void OnChanged(object source, FileSystemEventArgs e)
		{
			Dump();
		}

		void Dump()
		{
			if (reader is null)
			{
				reader = new StreamReader(path);
			}

			Console.Write(reader.ReadToEnd());
		}
	}
}
