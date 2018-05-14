using System;
using System.Collections.Generic;
using System.Linq;
using Frapid.Framework.Extensions;

namespace Frapid.Framework
{
    public static class TypeExtensions
    {
        private static readonly Dictionary<Type, string> Aliases = new Dictionary<Type, string>
        {
            {typeof(byte), "byte"},
            {typeof(byte?), "byte?"},
            {typeof(sbyte), "sbyte"},
            {typeof(sbyte?), "sbyte?"},
            {typeof(short), "short"},
            {typeof(short?), "short?"},
            {typeof(ushort), "ushort"},
            {typeof(ushort?), "ushort?"},
            {typeof(int), "int"},
            {typeof(int?), "int?"},
            {typeof(uint), "uint"},
            {typeof(uint?), "uint?"},
            {typeof(long), "long"},
            {typeof(long?), "long?"},
            {typeof(ulong), "ulong"},
            {typeof(ulong?), "ulong?"},
            {typeof(float), "float"},
            {typeof(float?), "float?"},
            {typeof(double), "double"},
            {typeof(double?), "double?"},
            {typeof(decimal), "decimal"},
            {typeof(decimal?), "decimal?"},
            {typeof(object), "object"},
            {typeof(bool), "bool"},
            {typeof(bool?), "bool?"},
            {typeof(char), "char"},
            {typeof(char?), "char?"},
            {typeof(string), "string"},
            {typeof(DateTime), "System.DateTime"},
            {typeof(DateTime?), "System.DateTime?"},
            {typeof(DateTimeOffset), "System.DateTimeOffset"},
            {typeof(DateTimeOffset?), "System.DateTimeOffset?"},
            {typeof(TimeSpan), "System.TimeSpan"},
            {typeof(TimeSpan?), "System.TimeSpan?"}
        };

        public static Type GetWellKnownType(this string value)
        {
            return Aliases.FirstOrDefault(x => x.Value.ToUpperInvariant().Equals(value.Or("").ToUpperInvariant())).Key ?? typeof(object);
        }
    }
}