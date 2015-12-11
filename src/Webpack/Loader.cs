using Newtonsoft.Json;
using System.Collections.Generic;

namespace Webpack {
	/// <summary>
	///	Represents a webpack loader
	/// </summary>
	internal class Loader {
		[JsonConverter(typeof(PlainJsonStringConverter))]
		public string Test { get; set; }
		public IList<string> Loaders { get; set; }
		[JsonConverter(typeof(PlainJsonStringConverter))]
		public string Exclude { get; set; }

	}
}
