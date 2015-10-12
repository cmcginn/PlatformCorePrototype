using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Web.Http;
using Newtonsoft.Json.Serialization;
using PlatformCorePrototype.Web.Infrastructure;


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
            
            //jsonFormatter.SerializerSettings.Converters.Add(new JsonNetBsonDocumentConverter());
           
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
