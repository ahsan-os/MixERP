using System;

namespace Frapid.Mapper.Decorators
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class TableNameAttribute : Attribute
    {
        public TableNameAttribute(string tableName)
        {
            this.TableName = tableName;
        }

        public string TableName { get; private set; }
    }
}