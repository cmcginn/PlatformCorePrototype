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

                cfg.AddProfile<BsonDocumentToFilterSpecification>();
                cfg.AddProfile<BsonDocumentToViewDefinitionMetadata>();
                cfg.AddProfile<BsonDocumentToDataColumnMetadataProfile>();
                cfg.AddProfile<BsonDocumentToDataCollectionMetadataProfile>();
                cfg.AddProfile<BsonDocumentToLinkedListDataCollectionMetadataProfile>();
                //Core.Configuration.MappingConfiguration.Configure(cfg);
                //cfg.AddProfile<LinkedListQueryBuilderToMongoLinkedListQueryStrategyProfile>();
            });
 
            Mapper.AssertConfigurationIsValid();
            _Initialized = true;
           
        }
    }
}