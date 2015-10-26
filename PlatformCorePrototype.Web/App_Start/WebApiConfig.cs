using System.Linq;
using System.Net.Http.Formatting;
using System.Web.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace PlatformCorePrototype.Web
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            var bson = new BsonMediaTypeFormatter();

            config.Formatters.Add(bson);
            var jsonFormatter = config.Formatters.OfType<JsonMediaTypeFormatter>().First();
            jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            jsonFormatter.SerializerSettings.TypeNameHandling = TypeNameHandling.Auto;
            //jsonFormatter.SerializerSettings.Converters.Add(new JsonNetBsonDocumentConverter());

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute("DefaultApi", "api/{controller}/{id}", new {id = RouteParameter.Optional}
                );
        }
    }
}