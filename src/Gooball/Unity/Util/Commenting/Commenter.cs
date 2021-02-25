using System.Collections.Generic;
using System.Linq;

namespace Gooball
{

	internal interface ITextCommenter
	{

		string Process(IEnumerable<string> lines);
	}

	internal abstract class BaseTextCommenter : ITextCommenter
	{
		protected string BeginLine;
		protected string MidPrefix;
		protected string EndLine;

		protected readonly string NewLine;

		public BaseTextCommenter(string newLine)
		{
			NewLine = newLine;
		}

		public virtual string Process(IEnumerable<string> lines)
		{
			var commentedLines =
				from line in lines
				select $"{MidPrefix}{line}";

			var commentedSection = string.Join(NewLine, commentedLines);

			return $"{BeginLine}{commentedSection}{NewLine}{EndLine}";
		}
	}

	internal class SinglelineTextCommenter : BaseTextCommenter
	{

		public SinglelineTextCommenter(string newLine) : base(newLine)
		{
			BeginLine = string.Empty;
			MidPrefix = "// ";
			EndLine = string.Empty;
		}
	}

	internal class MultilineTextCommenter : BaseTextCommenter
	{

		public MultilineTextCommenter(string newLine) : base(newLine)
		{
			BeginLine = "/*" + NewLine;
			MidPrefix = " *	";
			EndLine = " */" + NewLine;
		}
	}
}
