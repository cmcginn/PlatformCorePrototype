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
    public class TestHelper:TestBase
    {
        public static IDataCollectionMetadata GetDataCollectionMetadata(string collectionName)
        {
            var svc = new MongoDataService();
            return svc.GetDataCollectionMetadata(collectionName).Result;
        }
    }
}
