# Preventing Anonymous Access to Controller Actions

If you are familiar with ASP.net ```Authorize``` attribute, ```RestrictAnonymous``` is an extension of that.

```cs
[RestrictAnonymous]
public ActionResult Index()
{
  return View();
}
```

When you decorate a controller or action with ```RestrictAnonymous``` attribute, access to that controller or action will be forbidden to anonymous users.


[Back to Developer Documentation](../README.md)
