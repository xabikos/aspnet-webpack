﻿using System.Collections.Generic;
using System.Linq;

namespace Webpack {

	/// <summary>
	/// Options for webpack configuration.
	/// All the files are created based on the path that is returned from IHostingEnvironment.WebRootPath
	/// </summary>
	public class WebpackOptions {

		public WebpackOptions(string entryPoint = "app/index.js", string outputFileName = "bundle.js", bool handleStyles = true) {
			EntryPoint = entryPoint;
			DevToolType = DevToolType.SourceMap;
			EnableES2015 = true;
			HandleStyles = handleStyles;
			DevServerOptions = new WebpackDevServerOptions();
			StylesTypes = new List<StylesType>();
			StaticFileTypes = new List<StaticFileType>();
            OutputFileName = outputFileName;
        }

        /// <summary>
        /// The relative path to applications root
        /// </summary>
        public string EntryPoint { get; set; }

        /// <summary>
        /// The file name of a single output bundle
        /// In order to put the file in a different folder use a relative path e.g. js/webpackBundle.js
        /// If not specified is "bundle.js"
        /// </summary>
        public string OutputFileName { get; set; }

        /// <summary>
        /// The file names of the output bundles
        /// In order to put the files in a different folder use a relative path(s) e.g. js/webpackBundle.js
        /// If not specified, as single bundle, "bundle.js", is used
        /// </summary>
        public IEnumerable<string> OutputFileNames { get; set; }

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
        /// https://webpack.github.io/docs/hot-module-replacement.html
        /// </summary>
        public bool HandleStaticFiles { get; set; }

        /// <summary>
        /// Indicates hot module replacement should be enabled
        /// </summary>
	    public bool EnableHotModuleReplacement { get; set; }

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

        /// <summary>
        /// Indicates if there are multiple bundles set
        /// </summary>
	    public bool HasMultipleBundles()
        {
            return OutputFileNames != null && OutputFileNames.Any();
        }

	    public List<string> GetBundlesList()
	    {
            List<string> outputNames;
            if (!HasMultipleBundles())
            {
                outputNames = new List<string> { OutputFileName };
            }
            else
            {
                outputNames = OutputFileNames as List<string>;
            }
	        return outputNames;
	    }
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
}