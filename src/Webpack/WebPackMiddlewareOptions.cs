using System.Collections.Generic;

namespace Webpack {

	/// <summary>
	/// The required options for the middleware in order to add the appropriate script tags
	/// </summary>
	internal class WebPackMiddlewareOptions {

		/// <summary>
		/// Indicates if auto sync should be enabled by using webpack's development server
		/// </summary>
		public bool EnableHotLoading { get; set; }

		/// <summary>
		/// The collection with output file names. For each name a separate script tag should be injected in the page
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
