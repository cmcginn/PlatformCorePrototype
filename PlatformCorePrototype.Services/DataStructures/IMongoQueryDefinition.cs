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
    public interface IMongoQueryDefinition:IQueryDefinition
    {
        FilterDefinition<BsonDocument> GetMatchDocument();
        List<BsonDocument> GetPipeline();
        string ViewId { get; set; }
    }
}
