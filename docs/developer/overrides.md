# Overrides

Frapid searches for overridden views on the theme and instance directories and loads them if found:

For:

* **Domain Name**: fizbuzz.com
* **Area Name** : Frapid.Account
* **Controller Name** : SignUp
* **Action Name** : Index
* **Current Theme** : frapid

When you request the view:

```cs
var view = View(GetRazorView<AreaRegistration>("SignUp/Index.cshtml"));
```

it will be searched on the current theme directory (~/Tenants/fizzbuzz_com/Areas/Frapid.WebsiteBuilder/Themes/frapid/):

`~/Tenants/fizzbuzz_com/Areas/Frapid.WebsiteBuilder/Themes/frapid/Areas/Frapid.Account/Views/SignUp/Index.cshtml`

if not found then

`~/Tenants/fizzbuzz_com/Areas/Frapid.Account/Views/SignUp/Index.cshtml`

if not found then

`~/Areas/Frapid.Account/Views/SignUp/Index.cshtml`

[Back to Developer Documentation](README.md)
