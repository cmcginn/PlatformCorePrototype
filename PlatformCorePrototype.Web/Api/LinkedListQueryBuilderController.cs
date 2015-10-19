using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using PlatformCorePrototype.Core.Models;
using PlatformCorePrototype.Web.Services;

namespace PlatformCorePrototype.Web.Api
{

    public class LinkedListQueryBuilderController : ApiController
    {
        private DataService service = new DataService();
        //POST:api/QueryBuilder
        public async Task<List<dynamic>> Post(LinkedListQueryBuilder value)
        {
           // throw new System.NotImplementedException();
             var result =  await service.GetDataAsync(value);
            return result;
        }
    }
}
