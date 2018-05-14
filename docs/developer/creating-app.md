# Creating a New Frapid Module

Developing an application in Frapid is no different than developing plain old ASP.net MVC applications. These are the checklist you need to remember:

**Creating an App**

Locate the [Frapid Console](frapid-console.md) utility under:

```shell
/bin/frapid.exe
```

and enter the command to create your app:

```shell
create app MyAwesomeApp
```

The application will be created on `/Areas/MyAwesomeApp`. Edit the file [AppInfo.json](AppInfo.json.md) and open the solution file `MyAwesomeApp.sln` in Visual Studio.



**Controllers**

- Inherit from `Frapid.Areas.BaseController` instead of `Controller` for public controllers. In other words, for pages which do not require user to be logged in.
- Inherit from `Frapid.Areas.FrapidController` for controllers which may or may not require the user to be logged in. This is useful in cases when you want to display additional information for logged in user, whilst keeping the remainder of the page public. Please note that `FrapidController` extends `BaseController`.
- Inherit from `Frapid.WebsiteBuilder.Controllers.WebsiteBuilderController` for frontend pages. For example, the [Sign Up Feature](https://github.com/frapid/frapid/blob/master/src/Frapid.Web/Areas/Frapid.Account/Controllers/Frontend/SignupController.cs) extends `WebsiteBuilderController`. Please note that `WebsiteBuilderController` extends `FrapidController`. 


- Inherit from `Frapid.Dashboard.Controllers.DashboardController` for protected pages, which require user to be logged in and additionally require to use the backend theme layout file. For example, the [User List Backend Page](https://github.com/frapid/frapid/blob/master/src/Frapid.Web/Areas/Frapid.Account/Controllers/Backend/UserController.cs) extends `DashboardController`. 
- Inherit from `Frapid.Dashboard.Controllers.BackendController` instead of `DashboardController` for protected pages which do not require backend theme layout file (master page). For example, the [Sales Ticket Report](https://github.com/mixerp/sales/blob/master/Controllers/Backend/Tasks/SalesTicketController.cs) extends `BackendController` instead of `DashboardController` because, even though the sales ticket is a protected page, it does not need any layout or master page.



**Uncategorized:**

- Decorate your controller with `[Antiforgery]` attribute if your controller contains any action except `GET, HEAD, OPTIONS`. In other words, if your controller allows data to be written or deleted, you must use the `[Antiforgery]` attribute. Remember that you cannot substitute this attribute with `System.Web.Mvc.ValidateAntiForgeryTokenAttribute`.
- Views can be [overridden on tenants and themes](docs/developer/overrides.md).
- Decorate your action method with `[RestrictAnonymous]` attribute if you are not inheriting from `DashboardController` or `BackendController` and still want to protect that action from anonymous access. 
- If you want to execute your custom code [during application start](i-startup-registration.md), implement the interface `IStartupRegistraion`. For example, the account app upserts installed domains [by implementing this interface](https://github.com/frapid/frapid/blob/master/src/Frapid.Web/Areas/Frapid.Account/StartupTask.cs).
- Ask questions in [MixERP Forums](https://mixerp.org/forums), we will try to help you as much as we can.



[Back to Developer Documentation](README.md)
