﻿using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlatformCorePrototype.Services.Models;
using PlatformCorePrototype.Tests.Services;
using PlatformCorePrototype.Web.Mapping;
using PlatformCorePrototype.Web.Services;

namespace PlatformCorePrototype.Tests.Web.Services
{
    [TestClass]
    public class DataServiceTests : ServiceTestBase
    {
        DataService GetTarget()
        {
            return new DataService();
        }

        [TestMethod]
        public void GetViewDefinitionAsyncTest()
        {
            MappingConfiguration.ConfigureMappings();
            var target = GetTarget();
            var actual = target.GetViewDefinitionAsync("linkedlist_account_view1").Result;

            Assert.IsNotNull(actual);
            var vd = actual as LinkedListViewDefinitionModel;
            Assert.IsTrue(vd.Paths.Any());


        }
    }
}
