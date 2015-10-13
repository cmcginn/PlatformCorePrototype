using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using PlatformCorePrototype.Core.DataStructures;

namespace PlatformCorePrototype.Services.DataStructures
{
    public interface IMongoQueryStrategy:IQueryStrategy
    {
        FilterDefinition<dynamic> GetFilterDefinition();
        List<BsonDocument> GetPipeline();
        Task<List<dynamic>> RunQueryAsync();
    }
}
