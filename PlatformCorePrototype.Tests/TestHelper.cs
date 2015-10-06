using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PlatformCorePrototype.Core;
using PlatformCorePrototype.Core.DataStructures;
using PlatformCorePrototype.Services;
using PlatformCorePrototype.Tests.Services;

namespace PlatformCorePrototype.Tests
{
    public class TestHelper:ServiceTestBase
    {
        public static DataCollectionMetadata GetDataCollectionMetadata(string collectionName)
        {
            var svc = new DataService();
            var settings = new DataSourceSettings
            {
                CollectionName = collectionName,
                DataSourceLocation = Globals.MongoConnectionString
            };
            return svc.GetDataCollectionMetadata(settings).Result;
        }
    }
}
