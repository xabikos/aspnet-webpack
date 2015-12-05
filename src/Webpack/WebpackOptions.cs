using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webpack {
	public class WebpackOptions {
		public static WebpackOptions CurrentOptions { get; set; }

		public WebpackOptions(
			string entryPoint = "app/index.js", string outputPath = "wwwroot",
			string outputFileName = "bundle.js", bool handleStyles = true) {
			EntryPoint = entryPoint;
			OutputPath = outputPath;
			OutputFileName = outputFileName;
			HandleStyles = handleStyles;
		}

		/// <summary>
		/// The relative path to applications root
		/// </summary>
		public string EntryPoint { get; set; }

		/// <summary>
		/// The relative path of the output
		/// </summary>
		public string OutputPath { get; set; }

		/// <summary>
		/// The file name of the output bundle
		/// If not specified is "bundle.js"
		/// </summary>
		public string OutputFileName { get; set; }

		/// <summary>
		/// Indicates if webpack should handle the styles
		/// </summary>
		public bool HandleStyles { get; set; }

		/// <summary>
		/// The type of the styles
		/// </summary>
		public StylesType StylesType { get; set; }

		/// <summary>
		/// Indicates if webpack should handle jsx files (Reactjs)
		/// In case the react components live inside js files this could remain false
		/// </summary>
		public bool HandleJsxFiles { get; set; }

		/// <summary>
		/// Indicates if webpack's development server should be launched
		/// </summary>
		public bool UseDevelopmentServer { get; set; }
	}

	public enum StylesType {
		Css,
		Less,
		Sass
	}
}
