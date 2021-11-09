using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Andtech.Gooball
{
	internal class LogDumper
	{
		private readonly string path;

		public LogDumper(string path)
		{
			this.path = path;
		}

		public async Task Listen(CancellationToken cancellationToken = default)
		{
			using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			using (var sr = new StreamReader(fs))
			{
				if (sr.BaseStream.Length > 1024)
				{
					sr.BaseStream.Seek(-512, SeekOrigin.End);
				}

				while (!cancellationToken.IsCancellationRequested)
				{
					string line = await sr.ReadLineAsync();

					if (line != null)
					{
						Console.WriteLine(line);
					}
				}

				Console.WriteLine("Is at end of stream: " + sr.EndOfStream);
			}
		}
	}
}
