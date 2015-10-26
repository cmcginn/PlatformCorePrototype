using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using PlatformCorePrototype.Core.DataStructures;

namespace PlatformCorePrototype.Services.DataStructures
{
    public interface IMongoDataService:IDataService
    {
        //Task<List<BsonDocument>> GetBsonDocumentsAsync(string database)
    }
}
