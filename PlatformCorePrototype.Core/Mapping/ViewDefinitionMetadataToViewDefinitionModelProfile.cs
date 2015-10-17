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
            Mapper.CreateMap<ViewDefinitionMetadata, ViewDefinitionModel>()
                .ForMember(dest => dest.QueryBuilder, (s) => s.Ignore())
                .AfterMap((source, dest) =>
                {
                    dest.QueryBuilder = new QueryBuilder {ViewId = source.Id};
                });
        }
    }
}
