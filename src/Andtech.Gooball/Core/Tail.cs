namespace Andtech.Gooball
{

	internal class Tail
	{
		private readonly string path;
		private StreamReader reader;
		private int pollingInterval = 20;

		public Tail(string path)
		{
			this.path = path;
		}

		public async Task RunAsync(CancellationToken cancellationToken = default)
		{
			while (!cancellationToken.IsCancellationRequested)
			{
				Dump();
				await Task.Delay(pollingInterval, cancellationToken: cancellationToken);
			}

			Dump();
		}

		void Dump()
		{
			if (reader is null && File.Exists(path))
			{
				reader = new StreamReader(path);
			}

			if (!(reader?.EndOfStream) ?? false)
			{
				Console.Write(reader.ReadToEnd());
			}
		}
	}
}
