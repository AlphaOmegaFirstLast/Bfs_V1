using System.Text;
using Dapper;

namespace Bfs.Core.Data;

public static class QueryExtensions
{
    public static DynamicParameters ApplyPagination(this StringBuilder sql, QueryBaseRequest request,
        DynamicParameters parameters)
    {
        if (request == null) return parameters;

        sql.AppendLine(" OFFSET @Offset ROWS");
        sql.AppendLine(" FETCH NEXT @PageSize ROWS ONLY");

        var offset = (request.PageIndex - 1) * request.PageSize;
        parameters.Add("Offset", offset);
        parameters.Add("PageSize", request.PageSize);

        return parameters;
    }

    public static void ApplySort(this StringBuilder sql, QueryBaseRequest request, List<string> allowedSortFields,
        SortOption? defaultSort = null)
    {
        // Default sorting, sort is a must to get pagination work correctly!!!
        // sort fields must include at least one id field to set a unique value for pagination. 
        var sort = request.SortOption;
        if (sort == null || string.IsNullOrEmpty(sort.SortBy))
        {
            if (defaultSort != null) //(allowedSortAggregates.Count > 0)
            {
                sort = defaultSort;
            }
            else
            {
                sort = new SortOption();
                sort.SortBy = "Id";
                sort.Direction = "ASC";
            }
        }

        // Sanitize sort field and direction
        var sortFields = sort.SortBy.Split(',');
        if (!allowedSortFields.Select(x => x.Trim().ToLower())
                .Intersect(sortFields.Select(x => x.Trim().ToLower()))
                .Any())
        {
            sort.SortBy = "id";
            sort.Direction = "ASC";
        }

        sort.Direction = sort.Direction?.ToLower() == "desc" ? "DESC" : "ASC";
        sortFields = sort.SortBy.Split(',');
        sortFields[0] = $"{sortFields[0]} {sort.Direction}"; //apply direction to the first sort field
        var sortClause = string.Join(',', sortFields); // in case more than one sort field. example an aggrigation, id

        sql.AppendLine($" ORDER BY {sortClause}");
    }
}