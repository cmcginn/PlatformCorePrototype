using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using MongoDB.Bson;
using Newtonsoft.Json.Linq;

namespace PlatformCorePrototype.Web.Api
{
    public class DocumentController : ApiController
    {

        public void Post(JObject value)
        {
            var x = BsonDocument.Parse(value.ToString());
            var q = "V";
        }
    }
}
