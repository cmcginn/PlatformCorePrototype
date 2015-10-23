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
        public async Task<ViewDefinition> Get(string id)
        {
            var result = await service.GetViewDefinitionAsync(id);
            return result;
        }
    }
}