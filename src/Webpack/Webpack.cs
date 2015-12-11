using Microsoft.AspNet.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;

namespace Webpack {
	internal class Webpack : IWebpack {

		private const string webpack = "webpack";
		private const string webpacDevServer = "webpack-dev-server";

		private readonly ILoggerFactory _loggerFactory;
		private readonly string _webRootPath;

		public Webpack(IHostingEnvironment env, ILoggerFactory loggerFactory) {
			_webRootPath = env.WebRootPath;
			_loggerFactory = loggerFactory;
		}

		public void Execute(WebpackOptions options) {
			var toolToExecute = options.EnableHotLoading ? webpacDevServer : webpack;
			var logger = _loggerFactory.CreateLogger(toolToExecute);

			logger.LogInformation($"Verifying required tools are installed");
			EnsuereNodeModluesInstalled(options, logger);
			logger.LogInformation($"All node modules are properly installed");

			CreateWebpackConfigurationFile(options);

			logger.LogInformation($"{toolToExecute} Execution started");
			var arguments = "";
			// If an external configuration file is provided then call webpack or webpack-dev-server with that configuration file
			if (!string.IsNullOrEmpty(options.ExternalConfigurationFile)) {
				arguments = $"--config {options.ExternalConfigurationFile}";
			}
			else {
				arguments = ArgumentsHelper.GetWebpackArguments(_webRootPath, options);
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

		private static void CreateWebpackConfigurationFile(WebpackOptions options) {
			if (!Directory.Exists("webpack")) {
				Directory.CreateDirectory("webpack");
			}
			var loaders = new List<Loader>();
			loaders.Add(new Loader {
				Test = "/\\.js/",
				Loaders = new[] { "babel" },
				Exclude = "/node_modules/"
			});
			if (options.HandleJsxFiles) {
				loaders.Add(new Loader {
					Test = "/\\.jsx/",
					Loaders = new[] { "babel" },
					Exclude = "/node_modules/"
				});
			}
			var exports = new {
				module = new {
					loaders
				}
			};
			var jsonResult = JsonConvert.SerializeObject(exports,
				new JsonSerializerSettings {
					Formatting = Formatting.Indented,
					ContractResolver = new CamelCasePropertyNamesContractResolver()
				});
			var fileContent = $"module.exports = {jsonResult}";
			using (var fs = File.Create(Path.Combine("webpack", "webpack.dev.js"))) {
				using (var streamWriter = new StreamWriter(fs)) {
					streamWriter.WriteLine(fileContent);
				}
			}
		}

	}
}
