using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using AutoMapper;
using PlatformCorePrototype.Core.DataStructures;
using PlatformCorePrototype.Core.Models;
using PlatformCorePrototype.Services;
using PlatformCorePrototype.Web.Services;

namespace PlatformCorePrototype.Web.Api
{
    public class QueryBuilderController : ApiController
    {
        private DataService service = new DataService();
        //POST:api/QueryBuilder
        public async Task<IQueryBuilder> Get(string id)
        {
            IDataService svc = new MongoDataService();

            var collectionMetadata = await svc.GetCollectionMetadataByViewId(id) as LinkedListDataCollectionMetadata;
            var viewDefinition =
                collectionMetadata.Views.Single(x => x.ViewId == id) as LinkedListViewDefinitionMetadata;
            var result = Mapper.Map<IQueryBuilder>(viewDefinition) as LinkedListQueryBuilder;
            var paths = await svc.GetLinkedListMaps(id);
            result.LinkedListMaps = paths.Select(x => Mapper.Map<ILinkedListMap>(x)).ToList();
            return result;
        }
    }
}