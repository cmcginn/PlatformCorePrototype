using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using PlatformCorePrototype.Core.DataStructures;
using PlatformCorePrototype.Web.Services;

namespace PlatformCorePrototype.Web.Api
{
    public class FilterValuesController : ApiController
    {
        private DataService service = new DataService();
        //POST:api/FilterValuesController
        public async Task<List<FilterSpecification>> Post(ViewDefinition value)
        {
            throw new System.NotImplementedException();
            //return await service.GetFilterValuesAsync(value);
        }
    }
}
