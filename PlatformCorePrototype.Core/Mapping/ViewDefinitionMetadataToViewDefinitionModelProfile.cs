using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using PlatformCorePrototype.Core.DataStructures;
using PlatformCorePrototype.Core.Models;


namespace PlatformCorePrototype.Core.Mapping
{
    public class ViewDefinitionMetadataToViewDefinitionModelProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<ViewDefinitionMetadata, ViewDefinition>()
                .ForMember(dest => dest.QueryBuilder, (s) => s.Ignore())
                .ForMember(dest=>dest.DataDefinition,(s)=>s.UseValue(new DataDefinition{ DataStorageType = DataStorageStructureTypes.Default}))
                .AfterMap((source, dest) =>
                {
                    dest.QueryBuilder = new QueryBuilder {ViewId = source.ViewId, AvailableFilters=source.Filters};
                    //dest.DataDefinition.CollectionName = source.MetadataCollectionId;
                });
        }
    }
}
