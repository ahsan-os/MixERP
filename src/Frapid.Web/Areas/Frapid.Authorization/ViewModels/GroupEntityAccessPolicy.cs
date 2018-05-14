using System.Collections.Generic;
using Frapid.Authorization.DTO;

namespace Frapid.Authorization.ViewModels
{
    public class GroupEntityAccessPolicy
    {
        public IEnumerable<Office> Offices { get; set; }
        public IEnumerable<Role> Roles { get; set; }
        public IEnumerable<Entity> Entities { get; set; }
        public IEnumerable<AccessType> AccessTypes { get; set; }
    }
}