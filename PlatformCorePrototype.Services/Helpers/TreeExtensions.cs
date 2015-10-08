using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using PlatformCorePrototype.Services.DataStructures;

namespace PlatformCorePrototype.Services.Helpers
{
    public static class TreeExtensions
    {
        public static FilterDefinition<BsonDocument> GetTreeNodeFilterPathDefinition(this List<string> source)
        {
            var builder = new FilterDefinitionBuilder<BsonDocument>();
            var path = String.Join(".", source);
            FilterDefinition<BsonDocument> result = builder.Eq("Path", path);
            return result;
        }
        public static FilterDefinition<BsonDocument> GetTreeNodeFilterDefinition(this BsonDocument source)
        {
            
            var builder = new FilterDefinitionBuilder<BsonDocument>();
            FilterDefinition<BsonDocument> result = null;
            var ancestors = source["Ancestors"] as BsonArray;

            var ids = new List<ObjectId> {ObjectId.Parse(source["_id"]["_id"].ToString())};
            if (ancestors != null)
            {
                var ancestorIds = ancestors.Select(x => ObjectId.Parse(x["_id"].ToString())).ToList();
                ids.AddRange(ancestorIds);
            }
            result = builder.In("_id._id", ids);
            return result;
        }


    }
}
