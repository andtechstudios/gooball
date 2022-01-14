using System.Threading.Tasks;

namespace Andtech.Gooball
{

	internal class Program
	{

		private static async Task Main(string[] args) => await Interpreter.Instance.RunAsync(args);
	}
}
