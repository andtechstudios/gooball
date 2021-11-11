using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Andtech.Gooball
{

	internal class Tail
	{
		private readonly string path;

		public Tail(string path)
		{
			this.path = path;
		}

		public async Task Listen(CancellationToken cancellationToken = default)
		{
			using (var fs = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
			{
				using (var sr = new StreamReader(fs))
				{
					cancellationToken.Register(ReadToEnd);

					while (true)
					{
						string line = await sr.ReadLineAsync();

						if (line != null)
						{
							Console.WriteLine(line);
						}

						await Task.Delay(5, cancellationToken: cancellationToken);
					}

					void ReadToEnd()
					{
						string line = sr.ReadToEnd();

						if (line != null)
						{
							Console.WriteLine(line);
						}
					}
				}
			}
		}
	}
}
