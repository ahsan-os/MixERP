using System;
using Frapid.Mapper.Decorators;

namespace Frapid.Calendar.DTO
{
    [TableName("calendar.categories")]
    [PrimaryKey("category_id")]
    public sealed class Category
    {
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public string CategoryName { get; set; }
        public string ColorCode { get; set; }
        public bool IsLocal { get; set; }
        public short? CategoryOrder { get; set; }
        public int AuditUserId { get; set; }
        public DateTimeOffset AuditTs { get; set; }
        public bool Deleted { get; set; }
    }
}