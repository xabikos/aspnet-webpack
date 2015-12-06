using System;

namespace Webpack {
	/// <summary>
	/// Contains the configuration options for the webpack development server when used
	/// </summary>
	public class WebpackDevServerOptions {

		public WebpackDevServerOptions(string host = "localhost", int port = 4000) {
			Host = host;
			Port = port;
		}

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
