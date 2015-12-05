using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNet.Builder;
using Webpack.Commands;
using System.IO;
using Microsoft.AspNet.Hosting;

namespace Aspnet.Webpack.Extensions {
	public static class WebpackExtensions {
		public static IApplicationBuilder UseWebpack(this IApplicationBuilder app, IHostingEnvironment env, WebpackOptions options) {
			var args = CreateWebpackArguments(options);

			var webpack = Path.Combine(Directory.GetCurrentDirectory(), "node_modules", ".bin", "webpack");
			var osEnVariable = Environment.GetEnvironmentVariable("OS");
			if (!string.IsNullOrEmpty(osEnVariable) && string.Equals(osEnVariable, "Windows_NT", StringComparison.OrdinalIgnoreCase)) {
				webpack += ".cmd";
			}

			Process process = new Process();
			process.StartInfo = new ProcessStartInfo() {
				FileName = webpack,
				Arguments = args,
				UseShellExecute = false,
				WorkingDirectory = Directory.GetCurrentDirectory()
			};
			process.Start();

			return app;
		}

		private static string CreateWebpackArguments(WebpackOptions options) {
			var result = "--module-bind 'js=babel-loader'  --module-bind 'scss=style!css!sass' ";
			var entryPoint = $"--entry ./{options.EntryPoint} ";
			result += entryPoint;
			var outputPath = $"--output-path {options.OutputPath} ";
			result += outputPath;
			result += "--output-filename bundle.js";
			return result;
		}
	}

	public class WebpackOptions {

		public WebpackOptions(string entryPoint = "app/index.js", string outputPath = "wwwroot") {
			EntryPoint = entryPoint;
			OutputPath = outputPath;
		}

		/// <summary>
		/// The relative path to applications root
		/// </summary>
		public string EntryPoint { get; set; }

		/// <summary>
		/// The relative path of the output
		/// </summary>
		public string OutputPath { get; set; }

	}
}
