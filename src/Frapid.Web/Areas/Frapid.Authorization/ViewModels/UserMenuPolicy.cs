using System.Collections.Generic;
using Frapid.Authorization.DTO;

namespace Frapid.Authorization.ViewModels
{
    public class UserMenuPolicy
    {
        public IEnumerable<Office> Offices { get; set; }
        public IEnumerable<User> Users { get; set; }
        public IEnumerable<Menu> Menus { get; set; }
    }
}