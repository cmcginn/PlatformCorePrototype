//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using AutoMapper;
//using PlatformCorePrototype.Core.DataStructures;
//using PlatformCorePrototype.Core.Models;


//namespace PlatformCorePrototype.Core.Mapping
//{
//    public class LinkedListViewDefinitionMetadataToLinkedListViewDefinitionModelProfile : Profile
//    {
//        protected override void Configure()
//        {
//            Mapper.CreateMap<LinkedListViewDefinitionMetadata, ViewDefinition>()
//                .ForMember(dest => dest.QueryBuilder, dest=>dest.Ignore())
             
//                .ForMember(dest => dest.DataDefinition, (s) => s.UseValue(new DataDefinition { DataStorageType = DataStorageStructureTypes.LinkedList}))
//                .AfterMap((source, dest) =>
//                {
//                    var linkedListQueryBuilder = new LinkedListQueryBuilder();
//                    linkedListQueryBuilder.AvailableFilters = source.Filters;
//                    linkedListQueryBuilder.AvailablePaths = source.Paths;
//                    linkedListQueryBuilder.SelectedPath =
//                        linkedListQueryBuilder.AvailablePaths.OrderBy(x => x.DisplayOrder).FirstOrDefault();
//                    dest.QueryBuilder = linkedListQueryBuilder;
//                    //dest.DataDefinition.CollectionName = source.MetadataCollectionId;
//                });

//        }
//    }
//}
