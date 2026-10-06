using Bfs.Core.ObjectFields;
using Bfs.StockEx.Contracts;
using Bfs.StockEx.Data.Models;

namespace Bfs.StockEx.Domain.Mapper
{
    public static class CustomReportsMapper
    {
        public static CustomReports ToContract(this CustomReportsEntity entity)
        {
            var contract = new CustomReports()
            {
               Id= entity.Id,
Name= entity.Name,
Request= entity.Request,
BaseReport= entity.BaseReport,
IsPrivate= entity.IsPrivate,
IsDeleted= entity.IsDeleted,
CreatedBy= entity.CreatedBy,
Url= entity.Url,

            };

            return contract;
        }

        public static List<CustomReports> ToContract(this IEnumerable<CustomReportsEntity> CustomReportss)
        {
            return CustomReportss.Select(x => x.ToContract()).ToList();
        }

        public static List<CustomReportsEntity> ToEntity(this IEnumerable<CustomReports> CustomReportss)
        {
            return CustomReportss.Select(x => x.ToEntity()).ToList();
        }

        public static CustomReportsEntity ToEntity(this CustomReports contract, CustomReportsEntity entity = null)
        {
            var CustomReportsEntity = entity ?? new();

            CustomReportsEntity.Id= contract.Id;
CustomReportsEntity.Name= contract.Name;
CustomReportsEntity.Request= contract.Request;
CustomReportsEntity.BaseReport= contract.BaseReport;
CustomReportsEntity.IsPrivate= contract.IsPrivate;
CustomReportsEntity.IsDeleted= contract.IsDeleted;
CustomReportsEntity.CreatedBy= contract.CreatedBy;
CustomReportsEntity.Url= contract.Url;

            return CustomReportsEntity;
        }     
    }
}

