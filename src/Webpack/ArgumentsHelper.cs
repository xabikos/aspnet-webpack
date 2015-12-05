using System.Text;

namespace Webpack {
	internal class ArgumentsHelper {

		private const string JsFiles = "--module-bind js=babel ";
		private const string JsxFiles = "--module-bind jsx=babel ";
		private const string CssFiles = "--module-bind css=style!css ";
		private const string LessFiles = "--module-bind less=style!css!less ";
		private const string SassFiles = "--module-bind scss=style!css!sass ";


		/// <summary>
		/// Creates and returns the appropriate arguments list for the webpack based on the provided options
		/// </summary>
		public static string GetWebpackArguments(WebpackOptions options) {
			var result = new StringBuilder(JsFiles);
			if (options.HandleJsxFiles) {
				result.Append(JsxFiles);
			}
			if (options.HandleStyles) {
				switch (options.StylesType) {
					case StylesType.Css:
						result.Append(CssFiles);
						break;
					case StylesType.Sass:
						result.Append(SassFiles);
						break;
					case StylesType.Less:
						result.Append(LessFiles);
						break;
				}
			}
			result.Append($"--entry ./{options.EntryPoint} ");
			result.Append($"--output-path {options.OutputPath} ");
			result.Append($"--output-filename {options.OutputFileName}");

			return result.ToString();
		}
	}
}
