
namespace Webpack {
	/// <summary>
	/// Basic contract interface for Webpack execution environment
	/// </summary>
	public interface IWebpack {

		/// <summary>
		/// Executes the webpack or webpack-dev-server based on the provided <paramref name="options"/>
		/// </summary>
		/// <param name="options">The webpack configuration object</param>
		WebPackMiddlewareOptions Execute(WebpackOptions options);

		/// <summary>
		/// Executes the webpack or webpack-dev-server based on an external configuration file
		/// </summary>
		/// <param name="configFile">The webpack configuration object</param>
		/// <param name="outputFileName">The name of the output file</param>
		/// <param name="devServerOptions">The dev server options in case we need hot loading</param>
		WebPackMiddlewareOptions Execute(string configFile, string outputFileName, WebpackDevServerOptions devServerOptions);
	}
}
