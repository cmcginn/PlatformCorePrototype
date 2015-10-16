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
        private static bool _Initialized;
        public static void ConfigureMappings()
        {
            if (_Initialized)
                return;
            Mapper.Initialize(cfg =>
            {
                cfg.AddProfile<ViewDefinitionMetadataToViewDefinitionModelProfile>();
                cfg.AddProfile<LinkedListViewDefinitionMetadataToLinkedListViewDefinitionModelProfile>();
                //cfg.AddProfile<IViewDefinitionMetadataToViewDefinitionMetadataProfile>();
                //cfg.AddProfile<ViewDefinitionMetadataToViewDefinitionModelProfile>();

            });
            Mapper.AssertConfigurationIsValid();
            _Initialized = true;
           
        }
    }
}