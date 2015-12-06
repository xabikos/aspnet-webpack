using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using System;
using System.Diagnostics;
using System.IO;

namespace Webpack {
	public static class WebpackExtensions {

		private const string webpack = "webpack";
		private const string webpacDevServer = "webpack-dev-server";

		public static IApplicationBuilder UseWebpack(this IApplicationBuilder app, IHostingEnvironment env, WebpackOptions options) {
			EnsuereNodeModluesInstalled(options);
			var arguments = "";
			// If an external configuration file is provided then call webpack or webpack-dev-server with that configuration file
			if (!string.IsNullOrEmpty(options.ExternalConfigurationFile)) {
				arguments = $"--config {options.ExternalConfigurationFile}";
			}
			else {
				arguments = ArgumentsHelper.GetWebpackArguments(env.WebRootPath, options);
			}
			Process process = new Process();
			process.StartInfo = new ProcessStartInfo() {
				FileName = options.EnableHotLoading ? GetNodeExecutable(webpacDevServer) : GetNodeExecutable(webpack),
				Arguments = arguments,
				UseShellExecute = false
			};
			process.Start();

			app.UseMiddleware<WebpackMiddleware>(options);

			return app;
		}

		private static void EnsuereNodeModluesInstalled(WebpackOptions options) {
			if (!File.Exists(GetNodeExecutable(webpack))) {
				throw new InvalidOperationException("webpack is not installed");
			}
			if (options.EnableHotLoading && !File.Exists(GetNodeExecutable(webpacDevServer))) {
				throw new InvalidOperationException("webpack dev server is not installed");
			}
		}

		private static string GetNodeExecutable(string module) {
			var executable = Path.Combine(Directory.GetCurrentDirectory(), "node_modules", ".bin", module);
			var osEnVariable = Environment.GetEnvironmentVariable("OS");
			if (!string.IsNullOrEmpty(osEnVariable) && string.Equals(osEnVariable, "Windows_NT", StringComparison.OrdinalIgnoreCase)) {
				executable += ".cmd";
			}
			return executable;
		}

	}
}
