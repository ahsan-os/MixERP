using System;
using System.Collections.Generic;
using Frapid.Configuration.Db;
using Frapid.DataAccess.Models;
using Frapid.Framework.Extensions;
using Frapid.i18n;
using Frapid.Mapper;
using Frapid.Mapper.Extensions;
using Frapid.Mapper.Helpers;

namespace Frapid.DataAccess
{
    public class FilterManager
    {
        public static void AddFilters(ref Sql sql, List<Filter> filters)
        {
            if (filters == null ||
                filters.Count.Equals(0))
            {
                return;
            }

            foreach (var filter in filters)
            {
                string column = Sanitizer.SanitizeIdentifierName(filter.ColumnName).ToUnderscoreLowerCase();

                if (string.IsNullOrWhiteSpace(column))
                {
                    continue;
                }

                string statement = filter.FilterStatement;

                if (statement == null ||
                    statement.ToUpperInvariant() != "OR")
                {
                    statement = "AND";
                }

                statement += " ";

                switch ((FilterCondition) filter.FilterCondition)
                {
                    case FilterCondition.IsEqualTo:
                        sql.Append(statement + Sanitizer.SanitizeIdentifierName(column) + " = @0", GetValue(filter.Type, filter.FilterValue));
                        break;
                    case FilterCondition.IsNotEqualTo:
                        sql.Append(statement + Sanitizer.SanitizeIdentifierName(column) + " != @0", GetValue(filter.Type, filter.FilterValue));
                        break;
                    case FilterCondition.IsLessThan:
                        sql.Append(statement + Sanitizer.SanitizeIdentifierName(column) + " < @0", GetValue(filter.Type, filter.FilterValue));
                        break;
                    case FilterCondition.IsLessThanEqualTo:
                        sql.Append(statement + Sanitizer.SanitizeIdentifierName(column) + " <= @0", GetValue(filter.Type, filter.FilterValue));
                        break;
                    case FilterCondition.IsGreaterThan:
                        sql.Append(statement + Sanitizer.SanitizeIdentifierName(column) + " > @0", GetValue(filter.Type, filter.FilterValue));
                        break;
                    case FilterCondition.IsGreaterThanEqualTo:
                        sql.Append(statement + Sanitizer.SanitizeIdentifierName(column) + " >= @0", GetValue(filter.Type, filter.FilterValue));
                        break;
                    case FilterCondition.IsBetween:
                        sql.Append(statement + Sanitizer.SanitizeIdentifierName(column) + " BETWEEN @0 AND @1", GetValue(filter.Type, filter.FilterValue), GetValue(filter.Type, filter.FilterValue));
                        break;
                    case FilterCondition.IsNotBetween:
                        sql.Append(statement + Sanitizer.SanitizeIdentifierName(column) + " NOT BETWEEN @0 AND @1", GetValue(filter.Type, filter.FilterValue), GetValue(filter.Type, filter.FilterValue));
                        break;
                    case FilterCondition.IsLike:
                        sql.Append(statement + " LOWER(COALESCE(" + Sanitizer.SanitizeIdentifierName(column) + ", '')) LIKE @0",
                            "%" + filter.FilterValue.Or("").ToLower(CultureManager.GetCurrent()) + "%");
                        break;
                    case FilterCondition.IsNotLike:
                        sql.Append(statement + " LOWER(COALESCE(" + Sanitizer.SanitizeIdentifierName(column) + ", '')) NOT LIKE @0",
                            "%" + filter.FilterValue.Or("").ToLower(CultureManager.GetCurrent()) + "%");
                        break;
                }
            }
        }

        private static object GetValue(Type type, string value)
        {
            var converted = TypeConverter.Convert(value, type);
            return converted;
        }

        public static void AddFilters<T>(ref Sql sql, T poco, List<Filter> filters)
        {
            if (filters == null ||
                filters.Count.Equals(0))
            {
                return;
            }

            foreach (var filter in filters)
            {
                if (string.IsNullOrWhiteSpace(filter.ColumnName))
                {
                    if (!string.IsNullOrWhiteSpace(filter.PropertyName))
                    {
                        filter.ColumnName = filter.PropertyName.ToUnderscoreCase();
                    }
                }

                string column = Sanitizer.SanitizeIdentifierName(filter.ColumnName);

                if (string.IsNullOrWhiteSpace(column))
                {
                    continue;
                }

                string statement = filter.FilterStatement;

                if (statement == null ||
                    statement.ToUpperInvariant() != "OR")
                {
                    statement = "AND";
                }

                statement += " ";

                switch ((FilterCondition) filter.FilterCondition)
                {
                    case FilterCondition.IsEqualTo:
                        sql.Append(statement + Sanitizer.SanitizeIdentifierName(column) + " = @0", filter.FilterValue);
                        break;
                    case FilterCondition.IsNotEqualTo:
                        sql.Append(statement + Sanitizer.SanitizeIdentifierName(column) + " != @0", filter.FilterValue);
                        break;
                    case FilterCondition.IsLessThan:
                        sql.Append(statement + Sanitizer.SanitizeIdentifierName(column) + " < @0", filter.FilterValue);
                        break;
                    case FilterCondition.IsLessThanEqualTo:
                        sql.Append(statement + Sanitizer.SanitizeIdentifierName(column) + " <= @0", filter.FilterValue);
                        break;
                    case FilterCondition.IsGreaterThan:
                        sql.Append(statement + Sanitizer.SanitizeIdentifierName(column) + " > @0", filter.FilterValue);
                        break;
                    case FilterCondition.IsGreaterThanEqualTo:
                        sql.Append(statement + Sanitizer.SanitizeIdentifierName(column) + " >= @0", filter.FilterValue);
                        break;
                    case FilterCondition.IsBetween:
                        sql.Append(statement + Sanitizer.SanitizeIdentifierName(column) + " BETWEEN @0 AND @1", filter.FilterValue, filter.FilterAndValue);
                        break;
                    case FilterCondition.IsNotBetween:
                        sql.Append(statement + Sanitizer.SanitizeIdentifierName(column) + " NOT BETWEEN @0 AND @1", filter.FilterValue, filter.FilterAndValue);
                        break;
                    case FilterCondition.IsLike:
                        sql.Append(statement + " lower(" + Sanitizer.SanitizeIdentifierName(column) + ") LIKE @0", "%" + filter.FilterValue.ToLower(CultureManager.GetCurrent()) + "%");
                        break;
                    case FilterCondition.IsNotLike:
                        sql.Append(statement + " lower(" + Sanitizer.SanitizeIdentifierName(column) + ") NOT LIKE @0", "%" + filter.FilterValue.ToLower(CultureManager.GetCurrent()) + "%");
                        break;
                }
            }
        }
    }
}