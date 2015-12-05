using Microsoft.AspNet.Builder;
using System;
using System.Diagnostics;
using System.IO;

namespace Webpack {
	public static class WebpackExtensions {

		private const string webpack = "webpack";
		private const string webpacDevServer = "webpack-dev-server";

		public static IApplicationBuilder UseWebpack(this IApplicationBuilder app, WebpackOptions options) {
			EnsuereNodeModluesInstalled(options);

			WebpackOptions.CurrentOptions = options;

			Process process = new Process();
			process.StartInfo = new ProcessStartInfo() {
				FileName = GetNodeExecutable(webpack),
				Arguments = ArgumentsHelper.GetWebpackArguments(options),
				UseShellExecute = false
			};
			process.Start();

			return app;
		}

		private static void EnsuereNodeModluesInstalled(WebpackOptions options) {
			if (!File.Exists(GetNodeExecutable(webpack))) {
				throw new InvalidOperationException("webpack is not installed");
			}
			if (options.UseDevelopmentServer && !File.Exists(GetNodeExecutable(webpacDevServer))) {
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
