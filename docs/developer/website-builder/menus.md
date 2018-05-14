# Displaying Database Menu Items

The database menu items can be iterated through using the function `GetMenuItems` of `Menus` class under `Frapid.WebsiteBuilder.DAL` namespace.

**Import the Namespace**
```cshtml
@using Frapid.WebsiteBuilder.DAL
```

**Enumerate Menu Items**

```cs
@foreach (var item in Menus.GetMenuItems("Default"))
{
    <a href="@item.Url">@item.Title</a>
}
```


**Header.cshtml**

```html
@using Frapid.WebsiteBuilder.DAL
<section id="header" class="ui attached inverted blue stackable menu segment">
    <div class="ui container">
        <a class="ui inverted header" href="/">
            <img src="/my/templates/images/logo.png" />
        </a>
        <div class="right menu">
            @foreach (var item in Menus.GetMenuItems("Default"))
            {
                <a class="item" href="@item.Url">@item.Title</a>
            }
        </div>
    </div>
</section>
```

**Note**

Since we are using [Semantic UI](http://semantic-ui.com/collections/menu.html), 
we have added `stackable menu` class to the section.

[Back to Theme Development](theme.md)

