using System.Collections.Generic;
using Frapid.Authorization.DTO;

namespace Frapid.Authorization.ViewModels
{
    public class GroupMenuPolicy
    {
        public IEnumerable<Office> Offices { get; set; }
        public IEnumerable<Role> Roles { get; set; }
        public IEnumerable<Menu> Menus { get; set; }
    }
}