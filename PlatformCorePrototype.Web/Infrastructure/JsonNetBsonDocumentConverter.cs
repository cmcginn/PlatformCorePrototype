using System;
using MongoDB.Bson;
using Newtonsoft.Json;

namespace PlatformCorePrototype.Web.Infrastructure
{
    public class JsonNetBsonDocumentConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return (objectType == typeof (BsonDocument));
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue,
            JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //handle object id serialization
            var doc = (BsonDocument) value;
            BsonElement idElement;
            if (doc.TryGetElement("_id", out idElement))
            {
                doc.SetElement(new BsonElement("_id", new BsonString(idElement.Value.ToString())));
            }

            serializer.Serialize(writer, doc.ToBsonDocument());
        }
    }
}