using System.Collections.Generic;

namespace Gooball
{

	internal class CommentHelper
	{

		readonly Dictionary<CommentStyle, ITextCommenter> Commenters;

		public CommentHelper(string newLine)
		{
			Commenters = new Dictionary<CommentStyle, ITextCommenter>() {
			{ CommentStyle.Singleline, new SinglelineTextCommenter(newLine) },
			{ CommentStyle.Multiline, new MultilineTextCommenter(newLine) }
		};
		}

		public string Process(IEnumerable<string> lines, CommentStyle style) => Commenters[style].Process(lines);
	}
}
