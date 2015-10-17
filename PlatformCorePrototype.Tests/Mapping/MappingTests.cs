using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlatformCorePrototype.Core;
using PlatformCorePrototype.Core.DataStructures;
using PlatformCorePrototype.Core.Models;
using PlatformCorePrototype.Services.DataStructures;


using PlatformCorePrototype.Tests.Services;
using PlatformCorePrototype.Web.Mapping;

namespace PlatformCorePrototype.Tests.Mapping
{
    [TestClass]
    public class MappingTests:TestBase
    {
        [TestMethod]
        public void IViewDefinitionMetadataToViewDefinitionMetadataTest()
        {
           // MappingConfiguration.ConfigureMappings();
            ViewDefinitionMetadata source = new ViewDefinitionMetadata();
            ViewDefinitionMetadata actual = Mapper.Map<ViewDefinitionMetadata>(source);
        }
        [TestMethod]
        public void ViewDefinitionMetadataToViewDefinitionModelTest()
        {

           
             ViewDefinitionMetadata source = new ViewDefinitionMetadata();
             ViewDefinition actual = Mapper.Map<ViewDefinition>(source);
            
        }

        [TestMethod]
        public void ViewDefinitionMetadataToLinkedListViewDefinitionMetadataTest()
        {
            ViewDefinitionMetadata source = new LinkedListViewDefinitionMetadata
            {
                Paths = new List<LinkedListPathSpecification>
                {
                    new LinkedListPathSpecification {DisplayName = "Test", Name = "Test", Level = 0}
                }
            };
            LinkedListViewDefinitionMetadata actual = Mapper.Map<LinkedListViewDefinitionMetadata>(source);
            Assert.IsTrue(actual.Paths.Any());
        }

        [TestMethod]
        public void LinkedListViewDefinitionMetadataToLinkedListViewDefinitionModelTest()
        {
          
            LinkedListViewDefinitionMetadata source = new LinkedListViewDefinitionMetadata();
            source.Paths = new List<LinkedListPathSpecification>
            {
                new LinkedListPathSpecification {DisplayName = "Account", Level = 0, Name = "Account"}
            };
            LinkedListViewDefinition actual = Mapper.Map<LinkedListViewDefinition>(source);
            Assert.IsTrue(actual.Paths.Any());
        }

        [TestMethod]
        public void IQueryBuilderToMongoLinkedListQueryStrategyTest()
        {
            IQueryBuilder source = new LinkedListQueryBuilder();
            
        }
       
    }
}
