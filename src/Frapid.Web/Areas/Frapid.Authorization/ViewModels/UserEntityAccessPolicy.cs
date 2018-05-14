using System.Collections.Generic;
using Frapid.Authorization.DTO;

namespace Frapid.Authorization.ViewModels
{
    public class UserEntityAccessPolicy
    {
        public IEnumerable<Office> Offices { get; set; }
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Entity> Entities { get; set; }
        public IEnumerable<AccessType> AccessTypes { get; set; }
    }
}