using System;
using Frapid.Framework;
using Frapid.Mapper.Decorators;

namespace Frapid.DataAccess.Models
{
    [TableName("config.filters")]
    [PrimaryKey("filter_id", AutoIncrement = true)]
    public sealed class Filter : IPoco
    {
        private string _dataType;
        public long FilterId { get; set; }
        public string ObjectName { get; set; }
        public string FilterName { get; set; }
        public bool IsDefault { get; set; }
        public bool IsDefaultAdmin { get; set; }
        public string FilterStatement { get; set; }
        public string ColumnName { get; set; }

        [Ignore]
        public string PropertyName { get; set; }

        public int FilterCondition { get; set; }
        public string FilterValue { get; set; }
        public string FilterAndValue { get; set; }

        #region Type

        public string DataType
        {
            get { return this._dataType; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    return;
                }

                this._dataType = value;

                try
                {
                    var type = value.GetWellKnownType();
                    this.Type = type ?? Type.GetType(value);
                }
                catch
                {
                    //Swallow                
                }
            }
        }

        [Ignore]
        public Type Type { get; set; }


        public int? AuditUserId { get; set; }
        public DateTimeOffset? AuditTs { get; set; }

        #endregion
    }
}