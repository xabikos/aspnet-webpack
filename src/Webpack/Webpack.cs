using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;

namespace Webpack {
	internal class Webpack : IWebpack {

		private const string webpack = "webpack";
		private const string webpackDevServer = "webpack-dev-server";

		private readonly ILoggerFactory _loggerFactory;
		private readonly string _webRootPath;

		public Webpack(IHostingEnvironment env, ILoggerFactory loggerFactory) {
			_webRootPath = env.WebRootPath;
			_loggerFactory = loggerFactory;
		}

		public WebPackMiddlewareOptions Execute(WebpackOptions options) {
			var toolToExecute = options.EnableHotLoading ? webpackDevServer : webpack;
			var logger = _loggerFactory.CreateLogger(toolToExecute);

			logger.LogInformation($"Verifying required tools are installed");
			EnsuereNodeModluesInstalled(options.EnableHotLoading, logger);
			logger.LogInformation($"All node modules are properly installed");

			var includeDefaultConfigFile = CreateWebpackConfigurationFile(options);

			logger.LogInformation($"{toolToExecute} Execution started");
			try {
				var arguments = ArgumentsHelper.GetWebpackArguments(_webRootPath, options, includeDefaultConfigFile);
				logger.LogInformation($"{toolToExecute} is called with these arguments: {arguments}");
				Process process = new Process();
				process.StartInfo = new ProcessStartInfo() {
					FileName = GetNodeExecutable(toolToExecute),
					Arguments = arguments,
					UseShellExecute = false
				};
				process.Start();
				logger.LogInformation($"{toolToExecute} started successfully");

				return new WebPackMiddlewareOptions {
					EnableHotLoading = options.EnableHotLoading,
					OutputFileName = options.OutputFileName,
					Host = options.DevServerOptions.Host,
					Port = options.DevServerOptions.Port
				};
			}
			catch (Win32Exception) {
				throw new InvalidProgramException("IIS Express is not supported by Asp.net Webpack. Please use Kestrel instead");
			}
		}

		public WebPackMiddlewareOptions Execute(string configFile, string outputFileName, WebpackDevServerOptions devServerOptions) {
			var enableHotLoading = devServerOptions != null;
			var toolToExecute = enableHotLoading ? webpackDevServer : webpack;
			var logger = _loggerFactory.CreateLogger(toolToExecute);

			logger.LogInformation($"Verifying required tools are installed");
			EnsuereNodeModluesInstalled(enableHotLoading, logger);
			logger.LogInformation($"All node modules are properly installed");

			logger.LogInformation($"{toolToExecute} Execution started");
			var arguments = $"--config {configFile}";

			logger.LogInformation($"{toolToExecute} is called with these arguments: {arguments}");
			Process process = new Process();
			process.StartInfo = new ProcessStartInfo() {
				FileName = GetNodeExecutable(toolToExecute),
				Arguments = arguments,
				UseShellExecute = false
			};
			process.Start();
			logger.LogInformation($"{toolToExecute} started successfully");

			var middleWareOptions = new WebPackMiddlewareOptions {
				OutputFileName = outputFileName,
				EnableHotLoading = enableHotLoading
			};
			if (enableHotLoading) {
				middleWareOptions.Host = devServerOptions.Host;
				middleWareOptions.Port = devServerOptions.Port;
			}

			return middleWareOptions;
		}

		private static void EnsuereNodeModluesInstalled(bool enableHotLoading, ILogger logger) {
			if (!File.Exists(GetNodeExecutable(webpack))) {
				logger.LogError("webpack is not installed. Please install it by executing npm i webpack");
			}
			if (enableHotLoading && !File.Exists(GetNodeExecutable(webpackDevServer))) {
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

		private static bool CreateWebpackConfigurationFile(WebpackOptions options) {
			var presets = new List<string>() {
				"es2015"
			};
			if (options.HandleJsxFiles) {
				presets.Add("react");
			}
			var query = new Query {
				Presets = presets
			};
			var loaders = new List<WebpackLoader>();
			if (options.EnableES2015) {
				loaders.Add(new WebpackLoader {
					Test = "/\\.js/",
					Loader = "babel-loader",
					Exclude = "/node_modules/",
					Query = query
				});
			}
			if (options.HandleJsxFiles) {
				loaders.Add(new WebpackLoader {
					Test = "/\\.jsx/",
					Loader = "babel-loader",
					Exclude = "/node_modules/",
					Query = query
				});
			}
			var exports = new {
				module = new {
					loaders
				}
			};
			// Create the external configuration file only if we need to use babel-loader
			if (loaders.Count > 0) {
				if (!Directory.Exists("webpack")) {
					Directory.CreateDirectory("webpack");
				}
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
				return true;
			}
			return false;
		}

	}
}
