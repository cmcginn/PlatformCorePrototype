using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson.Serialization;
using PlatformCorePrototype.Core.DataStructures;
using PlatformCorePrototype.Services.Configuration;
using PlatformCorePrototype.Web.Mapping;

namespace PlatformCorePrototype.Tests.Services
{
    /// <summary>
    /// Summary description for ServiceTestBase
    /// </summary>
    [TestClass]
    public class ServiceTestBase
    {
        [ClassInitialize]
        public static void Initialize(TestContext ctx)
        {
            MappingConfiguration.ConfigureMappings();
            MongoClassMapRegistration.RegisterClassMaps();
        }

    }
}
