﻿using System.Collections.Generic;

namespace Webpack {

	/// <summary>
	/// The required options for the middleware in order to add the appropriate script tags
	/// </summary>
	public class WebPackMiddlewareOptions {

		/// <summary>
		/// Indicates if auto sync should be enabled by using webpack's development server
		/// </summary>
		public bool EnableHotLoading { get; set; }

        /// <summary>
        /// The file names of the output bundles
        /// In order to put the file in a different folder use a relative path e.g. js/webpackBundle.js
        /// If not specified, a single bundle is used "bundle.js"
        /// </summary>
        public IEnumerable<string> OutputFileNames { get; set; }

        /// <summary>
        /// The ip address of the server
        /// Default value if not specified is localhost
        /// </summary>
        public string Host { get; set; }

		/// <summary>
		/// The port of the server
		/// Default value if not specified is 4000
		/// </summary>
		public int Port { get; set; }
	}
}
