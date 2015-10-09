using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PlatformCorePrototype.Core;
using PlatformCorePrototype.Core.DataStructures;
using PlatformCorePrototype.Services.DataStructures;
using PlatformCorePrototype.Services.Mapping;
using PlatformCorePrototype.Services.Models;
using PlatformCorePrototype.Web.Mapping;

namespace PlatformCorePrototype.Tests.Mapping
{
    [TestClass]
    public class MappingTests
    {
        [ClassInitialize]
        public static void Initialize(TestContext context)
        {
            MappingConfiguration.ConfigureMappings();
        }

        [TestMethod]
        public void ViewDefinitionMetadataToViewDefinitionProfileTest()
        {
            var source = new ViewDefinitionMetadata();
            var codeFilter = new FilterSpecification
            {
                Column = new DataColumnMetadata { ColumnName = "Code", DataType = Globals.StringDatatypeName },
                FilterType = FilterTypes.Value,
                DisplayOrder = 0
            };
            source.Id = "segments_view1";
            source.MetadataCollectionId = "segments";
            source.Filters.Add(codeFilter);
            var actual = Mapper.Map<ViewDefinitionModel>(source);
            Assert.IsTrue(actual.Filters.Any());
            Assert.IsNotNull(actual.QueryBuilder);
        }

        [TestMethod]
        public void QueryBuilderToMongoQueryDefinitionProfileTest()
        {
            var source = new QueryBuilder
            {
                SelectedFilters = new List<FilterSpecification>
                {
                    new FilterSpecification
                    {
                        Column=new DataColumnMetadata{ ColumnName="Code", DataType=Globals.StringDatatypeName},
                         DisplayOrder=0,
                          FilterType= FilterTypes.Value,
                           SelectionMode="multi",
                            FilterValues= new List<FilterValue>
                            {
                                new FilterValue{ Key="Test1", Value = "Test1"},
                                new FilterValue{ Key="Test2", Value = "Test2", Active=true},
                                new FilterValue{ Key="Test3", Value = "Test3",Active = true},
                            }
                    }
                }
            };
            var actual = Mapper.Map<MongoQueryDefinition>(source);
            Assert.IsTrue(actual.Filters.Any());

        }
    }
}
