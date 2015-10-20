using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using MongoDB.Bson;
using PlatformCorePrototype.Core.DataStructures;
using PlatformCorePrototype.Core.Models;

namespace PlatformCorePrototype.Services.Mapping
{
    public class BsonDocumentToDataCollectionMetadataProfile:Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<BsonDocument, IDataCollectionMetadata>()
                .Include<BsonDocument,LinkedListDataCollectionMetadata>()
                .ForMember(dest => dest.Id, (src) => src.MapFrom(x => x["_id"]))
                .ForMember(dest => dest.DataSourceLocation, (src) => src.MapFrom(x => x["DataSourceLocation"]))
                .ForMember(dest => dest.DataSourceName, (src) => src.MapFrom(x => x["DataSourceName"]))
                .ForMember(dest => dest.DataStorageType,
                    (src) => src.MapFrom(x => (DataStorageStructureTypes) int.Parse(x["DataStorageType"].ToString())))
                .ForMember(dest => dest.Views, src => src.MapFrom<List<ViewDefinitionMetadata>>(x=>Mapper.Map<BsonDocument[],List<ViewDefinitionMetadata>>(x["Views"].AsBsonArray.Select(m=>m.AsBsonDocument).ToArray())))
                .ForMember(dest => dest.Columns, src => src.MapFrom<List<DataColumnMetadata>>(x=>Mapper.Map<BsonDocument[],List<DataColumnMetadata>>(x["Columns"].AsBsonArray.Select(m=>m.AsBsonDocument).ToArray())));

        }
    }

    public class BsonDocumentToLinkedListDataCollectionMetadataProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<BsonDocument, LinkedListDataCollectionMetadata>()
                .ForMember(dest => dest.MapCollectionName, src => src.MapFrom(x => x["MapCollectionName"]))
                .ForMember(dest => dest.KeyColumnName, src => src.MapFrom(x => x["KeyColumnName"]));
          

        }
    }
    public class BsonDocumentToViewDefinitionMetadata : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<BsonDocument, ViewDefinitionMetadata>()
                .ForMember(dest => dest.ViewId, src => src.MapFrom(x => x["ViewId"]))
                .ForMember(dest => dest.Filters,
                    src =>
                        src.MapFrom<List<FilterSpecification>>(
                            x =>
                                Mapper.Map<BsonDocument[], List<FilterSpecification>>(
                                    x["Filters"].AsBsonArray.Select(m => m.AsBsonDocument).ToArray())))
                .ForMember(dest => dest.Slicers,
                    src =>
                        src.MapFrom<List<SlicerSpecification>>(
                            x =>
                                Mapper.Map<BsonDocument[], List<SlicerSpecification>>(
                                    x["Slicers"].AsBsonArray.Select(m => m.AsBsonDocument).ToArray())))
                .ForMember(dest => dest.Measures,
                    src =>
                        src.MapFrom<List<MeasureSpecification>>(
                            x =>
                                Mapper.Map<BsonDocument[], List<MeasureSpecification>>(
                                    x["Measures"].AsBsonArray.Select(m => m.AsBsonDocument).ToArray())));
        }
    }

    public class BsonDocumentToSlicerSpecificationProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<BsonDocument, SlicerSpecification>()
                .ForMember(dest => dest.Column,
                    src =>
                        src.MapFrom<DataColumnMetadata>(
                            x => Mapper.Map<BsonDocument, DataColumnMetadata>(x["Column"].AsBsonDocument)))
                .ForMember(dest => dest.DisplayOrder, src => src.MapFrom(x => x["DisplayOrder"]));

        } 

    }

    public class BsonDocumentToMeasureSpecificationProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<BsonDocument, MeasureSpecification>()
                .ForMember(dest => dest.Column, src =>
                    src.MapFrom<DataColumnMetadata>(
                        x => Mapper.Map<BsonDocument, DataColumnMetadata>(x["Column"].AsBsonDocument)))
                .ForMember(dest => dest.DisplayOrder, src => src.MapFrom(x => x["DisplayOrder"]))
                .ForMember(dest => dest.AggregateOperationType,
                    src => src.MapFrom(x => (AggregateOperationTypes) int.Parse(x["AggregateOperationType"].ToString())));
        }
    }
    public class BsonDocumentToFilterSpecification : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<BsonDocument, FilterSpecification>()
                .ForMember(dest => dest.Column,
                    src =>
                        src.MapFrom<DataColumnMetadata>(
                            x => Mapper.Map<BsonDocument, DataColumnMetadata>(x["Column"].AsBsonDocument)))
                .ForMember(dest => dest.DisplayOrder, src => src.MapFrom(x => x["DisplayOrder"]))
                .ForMember(dest => dest.SelectionMode, src => src.MapFrom(x => x["SelectionMode"]))
                .ForMember(dest => dest.FilterType,
                    src => src.MapFrom(x => (FilterTypes) int.Parse(x["FilterType"].ToString())))
                .ForMember(dest => dest.Dependencies,
                    src =>
                        src.MapFrom<List<FilterSpecification>>(
                            x =>
                                Mapper.Map<BsonDocument[], List<FilterSpecification>>(
                                    x["Dependencies"].AsBsonArray.Select(m => m.AsBsonDocument).ToArray())))
                .ForMember(dest => dest.FilterValues, src => src.Ignore());
        }
    }
    public class BsonDocumentToDataColumnMetadataProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<BsonDocument, DataColumnMetadata>()
                .ForMember(dest => dest.ColumnName, src => src.MapFrom(x => x["ColumnName"]))
                .ForMember(dest => dest.DataType, src => src.MapFrom(x => x["DataType"]))
                .ForMember(dest => dest.Columns, src => src.MapFrom<List<DataColumnMetadata>>(x => Mapper.Map<BsonDocument[], List<DataColumnMetadata>>(x["Columns"].AsBsonArray.Select(m => m.AsBsonDocument).ToArray())));
        }

    }

    public class ViewDefinitionMetadataToIQueryBuilderProfile : Profile
    {
        protected override void Configure()
        {
            Mapper.CreateMap<IViewDefinitionMetadata, IQueryBuilder>()
                .ForMember(dest => dest.ViewId, src => src.MapFrom(x => x.ViewId))
                .ForMember(dest => dest.AvailableFilters, src => src.MapFrom(x => x.Filters))
                .ForMember(dest => dest.AvailableMeasures, src => src.MapFrom(x => x.Measures))
                .ForMember(dest => dest.AvailableSlicers, src => src.MapFrom(x => x.Slicers))
                .ForMember(dest => dest.SelectedFilters, src => src.Ignore())
                .ForMember(dest => dest.SelectedMeasures, src => src.Ignore())
                .ForMember(dest => dest.SelectedSlicers, src => src.Ignore())
                .Include<LinkedListViewDefinitionMetadata, LinkedListQueryBuilder>();

        }
    }
    public class LinkedListViewDefinitionMetadataToLinkedListQueryBuilder : Profile
    {

        protected override void Configure()
        {
            Mapper.CreateMap<LinkedListViewDefinitionMetadata,LinkedListQueryBuilder>()
                .ForMember(dest=>dest.AvailablePaths,src=>src.MapFrom(x=>x.Paths))
                .ForMember(dest => dest.SelectedPath, src => src.Ignore())
                .ForMember(dest => dest.SelectedKey, src => src.Ignore())
                .ForMember(dest => dest.ExcludeChildren, src => src.Ignore());

        }
    }


   

}
