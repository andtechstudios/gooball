using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace Gooball {

	public class Injector {
		public string HeaderFilePath { get; set; }

		readonly string NewLine;
		readonly CommentHelper CommentHelper;

		private static readonly FileType[] FileTypes = new FileType[] {
			new FileType { Extension = ".cs", CommentStyle = CommentStyle.Multiline },
			new FileType { Extension = ".shader", CommentStyle = CommentStyle.Singleline }
		};

		public Injector() {
			NewLine = Environment.NewLine;
			CommentHelper = new CommentHelper(Environment.NewLine);
		}

		public void Prepend(string sourcePath) {
			var fileType = LookupFileType(sourcePath);
			var preamble = ReadHeader();
			var prefix = CommentHelper.Process(preamble, fileType.CommentStyle);

			Prepend(sourcePath, prefix);
		}

		private FileType LookupFileType(string filename) => FileTypes.FirstOrDefault(x => Regex.IsMatch(filename, $"{x.Extension}"));

		private IEnumerable<string> ReadHeader() {
			string header;
			if (HeaderFilePath is null) {
				header = Console.In.ReadToEnd();
			}
			else {
				header = File.ReadAllText(HeaderFilePath);
			}

			return header.Trim().Split(NewLine);
		}

		private void Prepend(string filename, string prefix) {
			var content = File.ReadAllText(filename);
			content = Regex.Replace(content, @"^\s+", string.Empty);
			content = $"{prefix}{NewLine}{content}";

			File.WriteAllText(filename, content);
		}
	}
}
