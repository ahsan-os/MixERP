# Running Background Tasks on Application Startup

You can run background task during application startup by implementing the `IStartupRegistration` inteface.

```cs
namespace Frapid.Framework
{
    public interface IStartupRegistration
    {
        string Description { get; set; }
        void Register();
    }
}
```

| Item  | Item Type | Details |
| --- | --- | --- |
| Description | Property | Your background task description. |
| Register | Method | The implementation details of your background task. |

Here is an example implementation of this interface for you:

[https://github.com/frapid/frapid/blob/master/src/Frapid.Web/Areas/Frapid.Account/StartupTask.cs](https://github.com/frapid/frapid/blob/master/src/Frapid.Web/Areas/Frapid.Account/StartupTask.cs)

[Back to Developer Documentation](README.md)