using System.Text.RegularExpressions;

namespace Gooball {

	public class PathUtility {

		/// <summary>
		/// Returns a regex which correctly identifies a folder.
		/// </summary>
		/// <param name="folderPath">The path to the folder.</param>
		/// <returns>The regular expression.</returns>
		/// <remarks>Example: If "/Samples" is provided for <paramref name="folderPath"/>, the regex will accept "/Samples" and reject "/Costco/Samples".</remarks>
		public static Regex FolderRegex(string folderPath) {
			var pattern = @$"^{folderPath}(/|\\|$)";
			return new Regex(pattern);
		}
	}
}
