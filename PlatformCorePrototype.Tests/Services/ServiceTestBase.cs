using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson.Serialization;
using PlatformCorePrototype.Core.DataStructures;

namespace PlatformCorePrototype.Tests.Services
{
    /// <summary>
    /// Summary description for ServiceTestBase
    /// </summary>
    [TestClass]
    public class ServiceTestBase
    {
        public ServiceTestBase()
        {
            BsonClassMap.RegisterClassMap<DataColumnMetadata>(dcm => dcm.AutoMap());
            BsonClassMap.RegisterClassMap<DataCollectionMetadata>(dcm =>
            {
                dcm.AutoMap();
                dcm.MapIdMember(c => c.CollectionName);
            });
        }
    }
}
