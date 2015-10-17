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
    public class ViewDefinitionController : ApiController
    {
        private DataService service = new DataService();
        //Get:api/ApiController/{viewId}
        public async Task<ViewDefinitionModel> Get(string id)
        {
           
            var result = await service.GetViewDefinitionAsync(id);
            return result;
        }

    }
}
