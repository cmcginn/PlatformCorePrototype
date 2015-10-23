using System;
using System.Threading.Tasks;
using PlatformCorePrototype.Core.DataStructures;

namespace PlatformCorePrototype.Web.Services
{
    public class DataService
    {
        public async Task<ViewDefinition> GetViewDefinitionAsync(string viewId)
        {
            throw new NotImplementedException();

            //var service = new MongoDataService();
            //var result = service.GetViewDefinitionAsync(viewId);
            //return await result;
        }

        //public async Task<List<dynamic>> GetDataAsync(IQueryBuilder queryBuilder)
        //{
        //    Task<List<dynamic>> myT = null;
        //    var strategy = Mapper.Map<MongoLinkedListQueryStrategy<dynamic>>(queryBuilder);
        //    var service = new MongoDataService();
        //    var t = Task.Run(() =>
        //    {
        //        var s = service.GetDataAsync(strategy);
        //        myT = s;
        //    });
        //    Task.WaitAll(t);
        //    return await myT;
        //    //var task = new Task<Task<List<dynamic>>>((t) =>
        //    //{
        //    //    return service.GetDataAsync(strategy);
        //    //});
        //    //  var result = 
        //    //  return await result;
        //}
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