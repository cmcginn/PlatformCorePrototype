using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using MongoDB.Bson;
using PlatformCorePrototype.Core;
using PlatformCorePrototype.Core.DataStructures;
using PlatformCorePrototype.Core.Models;
using PlatformCorePrototype.Services;
using PlatformCorePrototype.Services.DataStructures;


namespace PlatformCorePrototype.Web.Services
{
    public class DataService
    {
        public async Task<ViewDefinition> GetViewDefinitionAsync(string viewId)
        {
    
            var service = new MongoDataService();
            var result = service.GetViewDefinitionAsync(viewId);
            return await result;
        }

        public async Task<List<dynamic>> GetDataAsync(IQueryBuilder queryBuilder)
        {
            Task<List<dynamic>> myT = null;
            var strategy = Mapper.Map<MongoLinkedListQueryStrategy<dynamic, int>>(queryBuilder);
            var service = new MongoDataService();
            var t = Task.Run(() =>
            {
                var s = service.GetDataAsync(strategy);
                myT = s;
            });
            Task.WaitAll(t);
            return await myT;
            //var task = new Task<Task<List<dynamic>>>((t) =>
            //{
            //    return service.GetDataAsync(strategy);
            //});
            //  var result = 
            //  return await result;
        }
        //public async Task<List<FilterSpecification>> GetFilterValuesAsync(ViewDefinitionModel view)
        //{
        //    var result = new Task<List<FilterSpecification>>(() =>
        //    {
               
        //        var taskResult = new List<FilterSpecification>();
        //        var codeFilter = new FilterSpecification();
        //        codeFilter.Column = new DataColumnMetadata
        //        {
        //            ColumnName = "Code",
        //            DataType = Globals.StringDataTypeName
        //        };
        //        codeFilter.DisplayOrder = 0;
        //        codeFilter.SelectionMode = "multi";
        //        var filterValues = new List<FilterValue>
        //        {
        //            new FilterValue{ Key="Account Main",Value="Account Main"},
        //            new FilterValue{ Key="Statistical",Value="Statistical"},
        //            new FilterValue{ Key="ACH Failed Fee Billed",Value="ACH Failed Fee Billed"},
        //            new FilterValue{ Key="ACH Failed Fee Collected",Value="ACH Failed Fee Collected"},
        //            new FilterValue{ Key="ACH Failed Fee Reversed",Value="ACH Failed Fee Reversed"},
        //            new FilterValue{ Key="ACH Failed Fee Voided",Value="ACH Failed Fee Voided"},
        //            new FilterValue{ Key="Active families",Value="Active families"},
        //            new FilterValue{ Key="Active families that made at least one successful payment",Value="Active families that made at least one successful payment"},
        //            new FilterValue{ Key="Amount",Value="Amount"},
        //            new FilterValue{ Key="Average Admin Fee",Value="Average Admin Fee"}

        //        };
        //        codeFilter.FilterValues = filterValues;
        //        taskResult.Add(codeFilter);
        //        return taskResult;
        //    });
        //    result.Start();
        //    return await result;
        //}

        //public async Task<List<dynamic>> GetDataAsync(IQueryBuilder queryBuilder)
        //{
        //    //var qd = Mapper.Map<MongoQueryDefinition>(queryBuilder);
        //    //var service = new MongoDataService();
        //    //var result = service.GetDataAsync(qd);
        //    //return await result;
        //}
    }
}