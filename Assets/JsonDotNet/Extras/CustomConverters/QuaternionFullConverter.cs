using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace JsonDotNet.Extras.CustomConverters
{
	public class QuaternionFullConverter : JsonConverter
	{
		public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
		{
			var qt = (Quaternion)value;
			writer.WriteStartObject();
			writer.WritePropertyName("w");
			writer.WriteValue(qt.w);
			writer.WritePropertyName("x");
			writer.WriteValue(qt.x);
			writer.WritePropertyName("y");
			writer.WriteValue(qt.y);
			writer.WritePropertyName("z");
			writer.WriteValue(qt.z);

			writer.WritePropertyName("eulerAngles");
			writer.WriteStartObject();
			writer.WritePropertyName("x");
			writer.WriteValue(qt.eulerAngles.x);
			writer.WritePropertyName("y");
			writer.WriteValue(qt.eulerAngles.y);
			writer.WritePropertyName("z");
			writer.WriteValue(qt.eulerAngles.z);
			writer.WriteEndObject();

			writer.WriteEndObject();
		}

		public override bool CanConvert(Type objectType)
		{
			return objectType == typeof(Quaternion);
		}

		public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
		{
			var obj = JObject.Load(reader);
			var props = obj.Properties().ToList();

			var result = new Quaternion();
			if (props.Any(p => p.Name == "w"))
				result.x = (float)obj["w"];

			if (props.Any(p => p.Name == "x"))
				result.x = (float)obj["x"];

			if (props.Any(p => p.Name == "y"))
				result.x = (float)obj["y"];

			if (props.Any(p => p.Name == "z"))
				result.x = (float)obj["z"];

			if (props.Any(p => p.Name == "eulerAngles"))
			{
				var eulerVecObj = obj["eulerAngles"];

				var eulerVec = new Vector3();
				if (props.Any(p => p.Name == "x"))
					eulerVec.x = (float)eulerVecObj["x"];

				if (props.Any(p => p.Name == "y"))
					eulerVec.x = (float)eulerVecObj["y"];

				if (props.Any(p => p.Name == "z"))
					eulerVec.x = (float)eulerVecObj["z"];

				result.eulerAngles = eulerVec;
			}

			return result;
		}

		public override bool CanRead
		{
			get { return true; }
		}
	}
}
