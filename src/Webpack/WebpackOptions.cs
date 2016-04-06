using System.Collections.Generic;

namespace Webpack {

	/// <summary>
	/// Options for webpack configuration.
	/// All the files are created based on the path that is returned from IHostingEnvironment.WebRootPath
	/// </summary>
	public class WebpackOptions {

		public WebpackOptions(
			string entryPoint = "app/index.js",
			string outputFileName = "bundle.js",
			bool handleStyles = true) {
			EntryPoint = entryPoint;
			OutputFileName = outputFileName;
			DevToolType = DevToolType.SourceMap;
			EnableES2015 = true;
			HandleStyles = handleStyles;
			EntryPoints = new List<EntryPoint>();
			DevServerOptions = new WebpackDevServerOptions();
			StylesTypes = new List<StylesType>();
			StaticFileTypes = new List<StaticFileType>();
		}

		/// <summary>
		/// The relative path to applications root
		/// </summary>
		public string EntryPoint { get; set; }

		/// <summary>
		/// The list for the individual entry points of the application
		/// For each entry point an output bundle will be created with the shape of EntryPointName.js
		/// If multiple entry points are specified all of them will be added as scripts in the page
		/// If this collections contains at least one element then the above property EntryPoint is ignored
		/// </summary>
		public IList<EntryPoint> EntryPoints { get; set; }

		/// <summary>
		/// The file name of the output bundle
		/// In order to put the file in a different folder use a relative path e.g. js/webpackBundle.js
		/// If not specified is "bundle.js"
		/// </summary>
		public string OutputFileName { get; set; }

		/// <summary>
		/// Indicates the type of the development tool will be used by webpack. See http://webpack.github.io/docs/configuration.html#devtool
		/// Default to source-map
		/// </summary>
		public DevToolType DevToolType { get; set; }

		/// <summary>
		/// Flag that enables ES2015 features (requires babel-loader to be installed)
		/// It's enabled <c>true</c> by default
		/// </summary>
		public bool EnableES2015 { get; set; }

		/// <summary>
		/// Indicates if webpack should handle the styles
		/// </summary>
		public bool HandleStyles { get; set; }

		/// <summary>
		/// The type of the styles
		/// </summary>
		public IList<StylesType> StylesTypes { get; set; }

		/// <summary>
		/// Indicates if webpack should handle jsx files (Reactjs)
		/// In case the react components live inside js files this could remain false
		/// </summary>
		public bool HandleJsxFiles { get; set; }

		/// <summary>
		/// Indicates if webpack should handle the templates for an Angular application
		/// It should be true to use <code>require('template.html')</code> inside directives
		/// </summary>
		public bool HandleAngularTemplates { get; set; }

		/// <summary>
		/// Indicates if webpack should handle the static file types through URL and file loader
		/// </summary>
		public bool HandleStaticFiles { get; set; }

		/// <summary>
		/// Indicates the limit of the file size in bytes to use with URL loader
		/// When a file exceeds that limit it will be handled by file loader
		/// https://github.com/webpack/url-loader
		/// </summary>
		public int StaticFileTypesLimit { get; set; }

		/// <summary>
		/// The type of the static files to handle
		/// </summary>
		public IList<StaticFileType> StaticFileTypes { get; set; }

		/// <summary>
		/// Indicates if auto sync should be enabled by using webpack's development server
		/// </summary>
		public bool EnableHotLoading { get; set; }

		/// <summary>
		/// The development server configuration options
		/// </summary>
		public WebpackDevServerOptions DevServerOptions { get; set; }
	}

	public enum StylesType {
		Css,
		Less,
		Sass
	}

	public enum DevToolType {
		Eval,
		CheapEvalSourceMap,
		CheapSourceMap,
		CheapModuleEvalSourceMap,
		CheapModuleSourceMap,
		EvalSourceMap,
		SourceMap
	}

	public enum StaticFileType {
		Png,
		Jpg,
		Svg,
		Woff,
		Woff2,
		Eot,
		Ttf
	}

	public class EntryPoint {
		/// <summary>
		/// The name of the entry point
		/// The file name will contain this name
		/// </summary>
		public string Name { get; set; }

		/// <summary>
		/// The relative path to the file
		/// </summary>
		public string FilePath { get; set; }
	}
}