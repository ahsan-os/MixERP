using System;
using System.Collections.Generic;
using System.Linq;
using Frapid.DataAccess.Models;

namespace Frapid.Authorization.ViewModels
{
    public class AccessType
    {
        public int AccessTypeId { get; set; }
        public string AccessTypeName { get; set; }

        public static List<AccessType> GetAccessTypes()
        {
            var accessTypes = Enum.GetNames(typeof(AccessTypeEnum)).Where(x => !x.Equals("None")).Select(item => new AccessType
            {
                AccessTypeName = item,
                AccessTypeId = (int) Enum.Parse(typeof(AccessTypeEnum), item)
            }).ToList();

            return accessTypes;
        }
    }
}