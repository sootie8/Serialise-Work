using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace JsonDotNet.Extras.CustomConverters
{
    public class EnumerableVector3Converter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            if(value == null)
                writer.WriteNull();

            Vector3[] src = (value as IEnumerable<Vector3>).ToArray();

            if (src == null)
                writer.WriteNull();

            writer.WriteStartArray();
            for (var i = 0; i < src.Length; i++)
            {
                writer.WriteStartObject();
                writer.WritePropertyName("x");
                writer.WriteValue(src[i].x);
                writer.WritePropertyName("y");
                writer.WriteValue(src[i].y);
                writer.WritePropertyName("z");
                writer.WriteValue(src[i].z);
                writer.WriteEndObject();
            }
            writer.WriteEndArray();
        }

        public override bool CanConvert(Type objectType)
        {
            return typeof(IEnumerable<Vector3>).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var obj = JObject.Load(reader);

            var result = new Vector3();

            for (var i = 0; i < obj.Count; i++)
            {
                result.x = (float) obj["x"];
                result.y = (float) obj["y"];
                result.z = (float) obj["z"];
            }

            return result;
        }

        public override bool CanRead
        {
            get { return true; }
        }
    }
}
