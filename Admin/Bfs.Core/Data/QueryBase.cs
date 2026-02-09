using Dapper;
using Microsoft.AspNetCore.Http.HttpResults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bfs.Core.Data
{
    public class QueryBase<TFilter> where TFilter : new()
    {
        private StringBuilder sqlStatement = new StringBuilder();
        private DynamicParameters sqlParameters = new DynamicParameters();

        protected List<QueryField> _fieldList = new List<QueryField>();
        protected QueryRequest<TFilter> _request;

        public void SetUp(QueryRequest<TFilter> request)
        {
            _request = request;
            SetupFields();

            // Select statement
            var selectStatement = GetSelectStatement();
            sqlStatement.AppendLine(selectStatement);
            sqlStatement.AppendLine(GetRowNumberClause(_request));

            // From & Joins
            var fromJoinStatement = GetFromJoinStatement();
            sqlStatement.AppendLine(fromJoinStatement);

            // Where, applies filtering before Group on aggregate fields
            var whereParameters = new DynamicParameters();
            var whereConditions = GetWhereConditions(_request, whereParameters);
            sqlStatement.AppendLine($" Where {whereConditions}");
            sqlParameters.AddDynamicParams(whereParameters);

            // Group By, for aggregate fields
            var groupStatement = GetGroupStatement();
            if (!string.IsNullOrEmpty(groupStatement))
            {
                sqlStatement.AppendLine(groupStatement);
            }

            // Having, applies filtering after Group on aggregate fields
            var havingParameters = new DynamicParameters();
            var havingConditions = GetHavingConditions(_request, havingParameters);
            if (havingParameters.ParameterNames.ToList().Count > 0)
            {
                sqlStatement.AppendLine($" Having {havingConditions}");
                sqlParameters.AddDynamicParams(havingParameters);
            }
        }

        public QueryParams GetMainSqlStatement()
        {
            var mainSql = new StringBuilder();
            mainSql = mainSql.AppendLine(sqlStatement.ToString());
            mainSql.AppendLine($" Order By newOrder");  //Todo add the sort field before the newOrder

            var mainParameters = new DynamicParameters();
            mainParameters.AddDynamicParams(sqlParameters);

            var paginationParameters = new DynamicParameters();
            var paginationClause = mainSql.ApplyPagination(_request, paginationParameters);

            if (paginationParameters.ParameterNames.ToList().Count > 1)
            {
                mainParameters.AddDynamicParams(paginationParameters);
            }

            return new QueryParams() { sql = mainSql.ToString(), parameters = mainParameters };
        }

        public QueryParams GetCountSqlStatement()
        {
            // Build Count SQL Statement, before Pagination applied
            var countSql = new StringBuilder();
          //  countSql = countSql.AppendLine(sqlStatement.ToString());
            countSql.AppendLine($"Select Count(*) From ({sqlStatement} ) q ");

            var countParameters = new DynamicParameters();
            countParameters.AddDynamicParams(sqlParameters);

            return new QueryParams() { sql = countSql.ToString(), parameters = countParameters };
        }

        protected virtual void SetupFields()
        {
            _fieldList = new List<QueryField>();
        }

        protected string GetSelectStatement()
        {
            var selectFields = _fieldList.Select(f => $"{f.DbName} As {f.QueryName}");
            return $"Select {string.Join(", ", selectFields)} ";
        }

        private string GetGroupStatement()
        {
            var groupFields = _fieldList.Where(f => !f.IsAggregare).Select(f => f.DbName);
            return groupFields.Count() > 0 ? $" Group By {string.Join(", ", groupFields)} " : string.Empty;
        }

        private List<string> GetAllowedSortFields()
        {
            return _fieldList.Select(f => f.QueryName).ToList();
        }

        protected string GetRowNumberClause(QueryRequest<TFilter> request)
        {
            var queryNameSortby = request.SortOption?.SortBy ?? _fieldList.First().QueryName;
            var dbNameSortBy = GetAllowedSortFields().Contains(queryNameSortby) ?
                               _fieldList.First(f => f.QueryName == queryNameSortby).DbName
                             : _fieldList.First().DbName;
            var direction = request.SortOption?.Direction ?? "Asc";

            var sql = new StringBuilder();
            sql.AppendLine(" ,ROW_NUMBER() OVER ( ORDER BY ");
            sql.Append($@"{dbNameSortBy} {direction}");   //Row_Number works only with DB field names, can take more than 1 sort fields
            sql.AppendLine(" ) AS newOrder");
            return sql.ToString();
        }

        protected virtual string GetFromJoinStatement()
        {
            return string.Empty;
        }

        protected virtual string GetWhereConditions(QueryRequest<TFilter> request, DynamicParameters parameters)
        {
            return " 1=1 ";
        }

        protected virtual string GetHavingConditions(QueryRequest<TFilter> request, DynamicParameters havingParameters)
        {
            return string.Empty;
        }
    }

    public class  QueryParams
    {
        public string sql = string.Empty;

        public DynamicParameters parameters = new DynamicParameters();
    }
}
