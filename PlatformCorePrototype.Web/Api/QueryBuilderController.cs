using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using PlatformCorePrototype.Core.Models;
using PlatformCorePrototype.Web.Services;

namespace PlatformCorePrototype.Web.Api
{
    public class QueryBuilderController : ApiController
    {
        private DataService service = new DataService();
        //POST:api/QueryBuilder
        public async Task<List<dynamic>> Post(QueryBuilder value)
        {
            throw new NotImplementedException();
            // var result =  await service.GetDataAsync(value);
            //return result;
        }
    }
}