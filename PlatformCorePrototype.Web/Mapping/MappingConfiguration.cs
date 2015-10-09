using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using PlatformCorePrototype.Services.Mapping;

namespace PlatformCorePrototype.Web.Mapping
{
    public class MappingConfiguration
    {
        public static void ConfigureMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<QueryBuilderToMongoQueryDefinitionProfile>();
                cfg.AddProfile<ViewDefinitionMetadataToViewDefinitionProfile>();
            });
           
           
        }
    }
}