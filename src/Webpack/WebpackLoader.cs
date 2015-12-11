using Newtonsoft.Json;
using System.Collections.Generic;

namespace Webpack {
	/// <summary>
	///	Represents a webpack loader
	/// </summary>
	internal class WebpackLoader {
		[JsonConverter(typeof(PlainJsonStringConverter))]
		public string Test { get; set; }
		public string Loader { get; set; }
		[JsonConverter(typeof(PlainJsonStringConverter))]
		public string Exclude { get; set; }
		public Query Query { get; set; }
	}

	internal class Query {
		public IList<string> Presets { get; set; }
	}
}
