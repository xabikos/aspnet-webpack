using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webpack {
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
