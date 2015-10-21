using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson.Serialization;
using PlatformCorePrototype.Core.DataStructures;

using PlatformCorePrototype.Web.Mapping;

namespace PlatformCorePrototype.Tests
{
    /// <summary>
    /// Summary description for ServiceTestBase
    /// </summary>
    [TestClass]
    public class TestBase
    {
        public TestBase()
        {
            MappingConfiguration.ConfigureMappings();
   
        }

    }
}
