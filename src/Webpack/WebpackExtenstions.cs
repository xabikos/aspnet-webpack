using Microsoft.AspNet.Builder;
using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Diagnostics;
using System.IO;

namespace Webpack {
	public static class WebpackExtensions {

		private const string webpack = "webpack";
		private const string webpacDevServer = "webpack-dev-server";

		public static IApplicationBuilder UseWebpack(this IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory, WebpackOptions options) {
			var toolToExecute = options.EnableHotLoading ? webpacDevServer : webpack;
			var logger = loggerFactory.CreateLogger(toolToExecute);

			logger.LogInformation($"Verifying required tools are installed");
			EnsuereNodeModluesInstalled(options, logger);
			logger.LogInformation($"All node modules are properly installed");

			logger.LogInformation($"{toolToExecute} Execution started");
			var arguments = "";
			// If an external configuration file is provided then call webpack or webpack-dev-server with that configuration file
			if (!string.IsNullOrEmpty(options.ExternalConfigurationFile)) {
				arguments = $"--config {options.ExternalConfigurationFile}";
			}
			else {
				arguments = ArgumentsHelper.GetWebpackArguments(env.WebRootPath, options);
			}
			logger.LogInformation($"{toolToExecute} is called with these arguments: {arguments}");
			Process process = new Process();
			process.StartInfo = new ProcessStartInfo() {
				FileName = GetNodeExecutable(toolToExecute),
				Arguments = arguments,
				UseShellExecute = false
			};
			process.Start();
			logger.LogInformation($"{toolToExecute} started successfully");

			app.UseMiddleware<WebpackMiddleware>(options);

			return app;
		}

		private static void EnsuereNodeModluesInstalled(WebpackOptions options, ILogger logger) {
			if (!File.Exists(GetNodeExecutable(webpack))) {
				logger.LogError("webpack is not installed. Please install it by executing npm i webpack");
			}
			if (options.EnableHotLoading && !File.Exists(GetNodeExecutable(webpacDevServer))) {
				logger.LogError("webpack-dev-server is not installed. Please install it by executing npm i webpack-dev-server");
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
