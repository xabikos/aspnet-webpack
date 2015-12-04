using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Webpack.Commands {
	public class Program {

		public static void Main(string[] args) {
			Console.WriteLine("Trying to reach npm");
			Process process = new Process();
			foreach(DictionaryEntry entry in Environment.GetEnvironmentVariables()) {
				Console.WriteLine($"Entry key: {entry.Key} Entry value: {entry.Value}");
			}
			var osEnVariable = Environment.GetEnvironmentVariable("OS");
			var npm = "npm";
			if(!string.IsNullOrEmpty(osEnVariable) && string.Equals(osEnVariable, "Windows_NT", StringComparison.OrdinalIgnoreCase)) {
				var home = Environment.GetEnvironmentVariable("HOMEPATH");
				npm = Path.Combine(home, "AppData", "Roaming", "npm", "npm.cmd");
			}
			process.StartInfo = new ProcessStartInfo() {
				FileName = npm,
				Arguments = "start",
				//UseShellExecute = false,
				//WorkingDirectory = workingDir
			};
			process.Start();

			Console.WriteLine("Trying to launch Kestrel");
			var mergedArgs = new[] { "--server", "Microsoft.AspNet.Server.Kestrel" }.Concat(args).ToArray();
			Microsoft.AspNet.Hosting.Program.Main(mergedArgs);
		}
	}
}
