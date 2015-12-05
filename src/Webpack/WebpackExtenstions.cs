using Microsoft.AspNet.Builder;
using System;
using System.Diagnostics;
using System.IO;

namespace Webpack {
	public static class WebpackExtensions {
		public static IApplicationBuilder UseWebpack(this IApplicationBuilder app, WebpackOptions options) {
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
				UseShellExecute = false
			};
			process.Start();

			return app;
		}

		private static string CreateWebpackArguments(WebpackOptions options) {
			var result = "--module-bind js=babel  --module-bind scss=style!css!sass ";
			var entryPoint = $"--entry ./{options.EntryPoint} ";
			result += entryPoint;
			var outputPath = $"--output-path {options.OutputPath} ";
			result += outputPath;
			result += "--output-filename bundle.js";
			return result;
		}
	}
}
