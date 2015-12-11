using Newtonsoft.Json;
using System;

namespace Webpack {
	/// <summary>
	/// Custom json converter used to write a property as raw value (no quotes)
	/// </summary>
	public class PlainJsonStringConverter : JsonConverter {
		public override bool CanConvert(Type objectType) {
			return objectType == typeof(string);
		}
		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
			return reader.Value;
		}
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
			writer.WriteRawValue((string)value);
		}
	}
}
