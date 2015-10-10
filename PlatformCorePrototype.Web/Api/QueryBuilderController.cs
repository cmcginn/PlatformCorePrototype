using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using MongoDB.Bson;
using PlatformCorePrototype.Services.DataStructures;
using PlatformCorePrototype.Web.Services;

namespace PlatformCorePrototype.Web.Api
{
    public class QueryBuilderController : ApiController
    {
        private DataService service = new DataService();
        //POST:api/FilterValuesController
        public async Task<List<dynamic>> Post(QueryBuilder value)
        {
            var result =  await service.GetDataAsync(value);
            return result;
        }
    }
}
