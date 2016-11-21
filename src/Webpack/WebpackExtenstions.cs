using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Webpack {
	/// <summary>
	/// Extension methods for adding webpack functionality to an asp.net 5 application
	/// </summary>
	public static class WebpackExtensions {

		/// <summary>
		/// Adds webpack functionality to the application. It should be used only in development environment.
		/// This method does not require an external configuration file and covers the most usual cases for webpack.
		/// Based on the provided <paramref name="options"/> the underlying middleware will add the required script tags
		/// so there is not need to add them manually.
		/// </summary>
		public static IApplicationBuilder UseWebpack(this IApplicationBuilder app, WebpackOptions options) {
			var webpack = app.ApplicationServices.GetService<IWebpack>();
			var middleWareOptions = webpack.Execute(options);
			app.UseMiddleware<WebpackMiddleware>(middleWareOptions);
			return app;
		}

	    ///  <summary>
	    ///  Adds webpack functionality to the application. It should be used only in development environment.
	    ///  This method is used when hot loading is not necessary and we need from webpack just to create the bundles.
	    ///  The <paramref name="outputFileName"/> is required for the underlying middleware to add the appropriate script tag
	    ///  </summary>
	    /// <param name="app"></param>
	    /// <param name="configFile">The path to the external configuration file e.g. webpack/webpack.development.js</param>
	    ///  <param name="outputFileName">
	    ///  The file name of the output bundle
	    ///  This value must be the same as the one in external configuration file output section
	    ///  output: {
	    /// 	  filename: 'bundle.js',
	    /// 	},
	    ///  </param>
	    public static IApplicationBuilder UseWebpack(this IApplicationBuilder app, string configFile, string outputFileName) {
			var webpack = app.ApplicationServices.GetService<IWebpack>();
			var middleWareOptions = webpack.Execute(configFile, outputFileName, null);
			app.UseMiddleware<WebpackMiddleware>(middleWareOptions);
			return app;
		}

	    ///  <summary>
	    ///  Adds webpack functionality to the application. It should be used only in development environment.
	    ///  This method is used when we need to enable hot loading.
	    ///  The <paramref name="outputFileName"/> and <paramref name="devServerOptions"/> are required for the underlying middleware to add the appropriate script tag
	    ///  </summary>
	    /// <param name="app"></param>
	    /// <param name="configFile">The path to the external configuration file e.g. webpack/webpack.development.js</param>
	    ///  <param name="outputFileName">
	    ///  The file name of the output bundle
	    ///  This value must be the same as the one in external configuration file output section
	    ///  output: {
	    /// 	  filename: 'bundle.js',
	    /// 	},
	    ///  </param>
	    ///  <param name="devServerOptions">
	    ///  The development server configuration options
	    ///  These values should be the same as the ones in external configuration file devserver section
	    ///  devServer: {
	    ///    host: "0.0.0.0",
	    ///    port: "3000",
	    ///  },
	    ///  </param>
	    public static IApplicationBuilder UseWebpack(
			this IApplicationBuilder app,
			string configFile,
			string outputFileName,
			WebpackDevServerOptions devServerOptions) {
			var webpack = app.ApplicationServices.GetService<IWebpack>();
			var options = webpack.Execute(configFile, outputFileName, devServerOptions);
			app.UseMiddleware<WebpackMiddleware>(options);
			return app;
		}

        /// <summary>
        /// Adds webpack functionality to the application. It should be used only in development environment.
		/// This method requires an external configuration file and covers specific configurations based on an external config file.
		/// Based on the provided <paramref name="webpackOptions"/> the underlying middleware will add the required script tags
		/// so there is not need to add them manually.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="configFile">The path to the external configuration file e.g. webpack/webpack.development.js</param>
        /// <param name="webpackOptions">Configuration options which sets flags on the webpack command and adds the required script tags.</param>
        /// <returns></returns>
        public static IApplicationBuilder UseWebpack(this IApplicationBuilder app, string configFile, WebpackOptions webpackOptions)
        {
            var webpack = app.ApplicationServices.GetService<IWebpack>();
            var options = webpack.Execute(configFile, webpackOptions);
            app.UseMiddleware<WebpackMiddleware>(options);
            return app;
        }
    }
}
