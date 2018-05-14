using System;
using Frapid.DataAccess;
using Frapid.Mapper.Decorators;

namespace Frapid.Account.DTO
{
    [TableName("account.users")]
    [PrimaryKey("user_id", AutoIncrement = true)]
    public sealed class User : IPoco
    {
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int OfficeId { get; set; }
        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }
        public bool? Status { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public DateTimeOffset? LastSeenOn { get; set; }
        public string LastIp { get; set; }
        public string LastBrowser { get; set; }
        public int? AuditUserId { get; set; }
        public DateTimeOffset? AuditTs { get; set; }
    }
}