using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace JsonDotNet.Extras.CustomConverters
{
	public class ResolutionFullConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var res = (Resolution)value;
			writer.WriteStartObject();
			writer.WritePropertyName("height");
			writer.WriteValue(res.height);
			writer.WritePropertyName("width");
			writer.WriteValue(res.width);
			writer.WritePropertyName("refreshRate");
			writer.WriteValue(res.refreshRate);
			writer.WriteEndObject();
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(Resolution);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var obj = JObject.Load(reader);
			var props = obj.Properties().ToList();

			var result = new Resolution();
			if (props.Any(p => p.Name == "height"))
				result.height = (int)obj["height"];

			if (props.Any(p => p.Name == "width"))
				result.width = (int)obj["width"];

			if (props.Any(p => p.Name == "refreshRate"))
				result.refreshRate = (int)obj["refreshRate"];

			return result;
		}

		public override bool CanRead
		{
			get { return true; }
		}
	}
}
