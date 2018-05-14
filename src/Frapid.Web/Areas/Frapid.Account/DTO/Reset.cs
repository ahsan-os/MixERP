using System;
using Frapid.Account.ViewModels;
using Frapid.DataAccess;

namespace Frapid.Account.DTO
{
    public class Reset : IUserInfo, IPoco
    {
        public Guid RequestId { get; set; }
        public int UserId { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTimeOffset RequestedOn { get; set; }
        public DateTimeOffset ExpiresOn { get; set; }
        public string Browser { get; set; }
        public string IpAddress { get; set; }
    }
}