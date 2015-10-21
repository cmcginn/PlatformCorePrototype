using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
using PlatformCorePrototype.Core.DataStructures;

namespace PlatformCorePrototype.Services.Configuration
{
    public class MongoClassMapRegistration
    {
        public static bool _Initialized;
        public static void RegisterClassMaps()
        {

            if (_Initialized)
                return;
            //BsonClassMap.RegisterClassMap<LinkedListMap<object>>(dss =>
            //{
            //    dss.AutoMap();
            //});
            //BsonClassMap.RegisterClassMap<LinkedListSettings>(ll =>
            //{
            //    ll.AutoMap();
            //});
            //BsonClassMap.RegisterClassMap<DataSourceSettings>(dss =>
            //{
            //    dss.AutoMap();
            //    dss.MapIdMember(c => c.CollectionName);
            //});
            //BsonClassMap.RegisterClassMap<DataColumnMetadata>(dcm => dcm.AutoMap());
            //BsonClassMap.RegisterClassMap<DataCollectionMetadata>(dcm =>
            //{
            //    dcm.AutoMap();
            //    dcm.MapIdMember(c => c.Id);
  
            //});
            //BsonClassMap.RegisterClassMap<LinkedListDataCollectionMetadata>(lldcm =>
            //{
            //    lldcm.AutoMap();
              
            //});
            //BsonClassMap.RegisterClassMap<ViewDefinitionMetadata>(vd => vd.AutoMap());

            //BsonClassMap.RegisterClassMap<LinkedListViewDefinitionMetadata>(llvd => llvd.AutoMap());

            _Initialized = true;
        }
    }
}
