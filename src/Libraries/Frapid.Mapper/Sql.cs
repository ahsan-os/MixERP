using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Frapid.Mapper.Database;

namespace Frapid.Mapper
{
    public class Sql
    {
        private static readonly Regex ParameterPattern = new Regex(@"(?<!@)@\w+");
        private readonly StringBuilder _builder;
        private readonly List<int> _parameterTokens;
        private List<object> _parameters;

        public Sql()
        {
            this._builder = new StringBuilder();
            this._parameters = new List<object>();
            this._parameterTokens = new List<int>();
        }

        public Sql(string token, params object[] parameters) : this()
        {
            token = this.ProcessToken(token);
            this._builder.Append(token + Environment.NewLine);
            this._parameters.AddRange(parameters);
        }

        public void AppendParameters(List<object> parameters)
        {
            this._parameters = parameters;
        }

        public Sql Append(Sql sql)
        {
            this._parameters.AddRange(sql._parameters);
            this._builder.Append(sql._builder);

            return this;
        }

        public IEnumerable<int> GetParameters()
        {
            var matches = ParameterPattern.Matches(this.GetQuery());
            var parameters = (from Match match in matches select int.Parse(match.Value.Replace("@", ""))).ToList();
            return parameters.Distinct();
        }

        private string ProcessToken(string token)
        {
            token = token.Replace(",@", ", @");
            var matches = ParameterPattern.Matches(token);
            int offset = this._parameterTokens.DefaultIfEmpty().Max();
            int count = this._parameterTokens.Count;

            if (count > 0)
            {
                offset++;
            }


            var matchList = new List<int>();
            foreach (Match match in matches)
            {
                string matchValue = match.Value.Replace("@", "");
                int value;

                if (int.TryParse(matchValue, out value))
                {
                    matchList.Add(value);
                }
            }

            foreach (int index in matchList.Distinct().OrderByDescending(x => x))
            {
                int newIndex = offset + index;
                token = ReplaceWord(token, "@" + index, "@" + newIndex);

                this._parameterTokens.Add(newIndex);
            }

            return token;
        }

        private static string ReplaceWord(string token, string find, string replace)
        {
            string pattern = $@"\B{find}\b";
            return Regex.Replace(token, pattern, replace);
        }

        public Sql Append(string token, params object[] args)
        {
            token = this.ProcessToken(token);
            return this.Append(new Sql(token, args));
        }

        public Sql Where(string token, params object[] args)
        {
            token = this.ProcessToken(token);
            return this.Append(new Sql("WHERE (" + token + ")", args));
        }

        public Sql In(string token, params object[] args)
        {
            var parameters = args;

            if (args.Length == 1)
            {
                var candidate = args[0];
                bool isArray = candidate.GetType().IsArray;

                if (isArray)
                {
                    var enumerable = candidate as IEnumerable;
                    var innerParameter = new List<object>();

                    if (enumerable != null)
                    {
                        innerParameter.AddRange(enumerable.Cast<object>());
                    }

                    parameters = innerParameter.ToArray();
                }
            }


            int count = parameters.Length;
            string argTokens = string.Join(", ", Enumerable.Range(0, count).Select(x => "@" + x));

            token = token.Replace("@0", argTokens);

            token = this.ProcessToken(token);
            return this.Append(new Sql(token, parameters));
        }

        public Sql And(string token, params object[] args)
        {
            token = this.ProcessToken(token);
            return this.Append(new Sql("AND (" + token + ")", args));
        }

        public Sql Limit(DatabaseType type, int limit, int offset, string orderBy)
        {
            string token = "ORDER BY " + orderBy;

            if (type == DatabaseType.SqlServer)
            {
                var builder = new StringBuilder();
                builder.Append("ORDER BY " + orderBy);
                builder.Append(" OFFSET @0 ROWS");
                builder.Append(" FETCH NEXT @1 ROWS ONLY");
                token = this.ProcessToken(builder.ToString());
                return this.Append(new Sql(token, offset, limit));
            }


            token += " LIMIT @0 OFFSET @1";
            token = this.ProcessToken(token);
            return this.Append(new Sql(token, limit, offset));
        }

        public Sql OrderBy(params object[] columns)
        {
            return this.Append(new Sql("ORDER BY " + string.Join(", ", (from x in columns select x.ToString()).ToArray())));
        }

        public Sql Select(params object[] columns)
        {
            return this.Append(new Sql("SELECT " + string.Join(", ", (from x in columns select x.ToString()).ToArray())));
        }

        public Sql From(params object[] tables)
        {
            return this.Append(new Sql("FROM " + string.Join(", ", (from x in tables select x.ToString()).ToArray())));
        }

        public Sql GroupBy(params object[] columns)
        {
            return this.Append(new Sql("GROUP BY " + string.Join(", ", (from x in columns select x.ToString()).ToArray())));
        }


        public string GetQuery()
        {
            return this._builder.ToString();
        }

        public List<object> GetParameterValues()
        {
            var parameters = new List<object>();

            foreach (var parameter in this._parameters)
            {
                var type = parameter?.GetType();

                if (type != null && type.IsEnum)
                {
                    parameters.Add((int)parameter);
                }
                else
                {
                    parameters.Add(parameter);
                }
            }

            return parameters;
        }
    }
}