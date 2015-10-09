﻿using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Bson.Serialization;
using PlatformCorePrototype.Core.DataStructures;
using PlatformCorePrototype.Services.Configuration;

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
            MongoClassMapRegistration.RegisterClassMaps();
        }
    }
}
