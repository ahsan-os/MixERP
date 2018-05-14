using System;
using Frapid.Account.ViewModels;
using Frapid.DataAccess;

namespace Frapid.Account.DTO
{
    public class Registration : IPoco, IUserInfo
    {
        public Guid RegistrationId { get; set; }
        public string Phone { get; set; }
        public string Password { get; set; }
        public string Browser { get; set; }
        public string IpAddress { get; set; }
        public DateTimeOffset? RegisteredOn { get; set; }
        public bool Confirmed { get; set; }
        public DateTimeOffset? ConfirmedOn { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }
}