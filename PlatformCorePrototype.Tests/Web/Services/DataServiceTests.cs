using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlatformCorePrototype.Core.DataStructures;
using PlatformCorePrototype.Services;
using PlatformCorePrototype.Tests.Services;
using PlatformCorePrototype.Web.Mapping;
using PlatformCorePrototype.Web.Services;

namespace PlatformCorePrototype.Tests.Web.Services
{
    [TestClass]
    public class DataServiceTests : TestBase
    {
        DataService GetTarget()
        {
            return new DataService();
        }

        [TestMethod]
        public void  GetDataAsyncTest()
        {
            var mds = new MongoDataService();
            var vd = mds.GetViewDefinitionAsync("linkedlist_account_view1").Result;

          var target = GetTarget();
          var actual = target.GetDataAsync(vd.QueryBuilder).Result.ToList();
            //Assert.IsTrue(actual.Any());
        }
    }
}
