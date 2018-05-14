﻿using System.Web.Mvc;
using Frapid.Areas;

namespace Frapid.AddressBook
{
    public class AreaRegistration : FrapidAreaRegistration
    {
        public override string AreaName => "Frapid.AddressBook";

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.Routes.LowercaseUrls = true;
            context.Routes.MapMvcAttributeRoutes();
        }
    }
}