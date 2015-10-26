using Microsoft.VisualStudio.TestTools.UnitTesting;
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