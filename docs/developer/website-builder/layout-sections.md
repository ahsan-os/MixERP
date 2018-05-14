# Layout Sections

There's more than one way to skin a cat. You can create multiple sections for a website theme.
Let's take a look on our [basic theme](theme.md):

```html
<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Basic Frapid Theme</title>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/semantic-ui/2.1.7/semantic.min.css" rel="stylesheet">
  </head>
  <body>
    <section id="header" class="ui attached inverted blue segment">
        <div class="ui container">
            <a class="ui inverted header" href="/">Logo</a>
        </div>
    </section>
    <section id="body" style="height:calc(100% - 92px);">
        @RenderBody()
    </section>
    <section id="footer" class="ui inverted attached segment">
        <div class="ui container">
            Footer
        </div>
    </section>
    @Html.AntiForgeryToken()
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/semantic-ui/2.1.7/semantic.min.js"></script>
  </body>
</html>
```

This layout can be broken down into three sections:

* header
* footer
* master layout

**Header.cshtml**

```html
<section id="header" class="ui attached inverted blue segment">
    <div class="ui container">
        <a class="ui inverted header" href="/">Logo</a>
    </div>
</section>
```

**Footer.cshtml**

```html
<section id="footer" class="ui inverted attached segment">
    <div class="ui container">
        Footer
    </div>
</section>
```

**Master Layout**

```html
<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Basic Frapid Theme</title>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/semantic-ui/2.1.7/semantic.min.css" rel="stylesheet">
  </head>
  <body>
    <!-- Header -->
    <section id="body" style="height:calc(100% - 92px);">
        @RenderBody()
    </section>
    <!-- Footer -->
    @Html.AntiForgeryToken()
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/semantic-ui/2.1.7/semantic.min.js"></script>
  </body>
</html>
```

### Remember

* You should use ASP.net MVC `@Html.Partial` helper to include sections in your master layout.
* The layout path of your theme is available in `ViewBag.LayoutPath` property.
* Resources (style sheets, images, and scripts) of your theme are resolved on `/my/template/` path.
For example, if your template contains `images/logo.png`, using the path `/my/template/images/logo.png` will correctly load it.
* You must keep the `@Html.AntiForgeryToken()` method in the layout.

Now that you have understood the key elements, the master layout would be something like this.

```html
@using Frapid.WebsiteBuilder.Controllers
<!DOCTYPE html>
<html lang="en">
  <head>
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <meta name="viewport" content="width=device-width, initial-scale=1">
    <title>Basic Frapid Theme</title>
    <link href="https://cdnjs.cloudflare.com/ajax/libs/semantic-ui/2.1.7/semantic.min.css" rel="stylesheet">
  </head>
  <body>
    @{
        string header = ViewBag.LayoutPath + "Header.cshtml";
        string footer = ViewBag.LayoutPath + "Footer.cshtml";
    }
    @Html.Partial(header)
    <section id="body" style="height:calc(100% - 92px);">
        @RenderBody()
    </section>
    @Html.Partial(footer)
    @Html.AntiForgeryToken()
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.11.3/jquery.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/semantic-ui/2.1.7/semantic.min.js"></script>
  </body>
</html>
```

**Footer.cshtml**

```html
<section id="footer" class="ui inverted attached segment">
    <div class="ui container">
        Copyright &copy; 2016. All rights and lefts reserved &reg;.
    </div>
</section>
```

**Header.cshtml**

```html
<section id="header" class="ui attached inverted blue segment">
    <div class="ui container">
        <a class="ui inverted header" href="/">
            <img src="/my/templates/images/logo.png" />
        </a>
    </div>
</section>
```

Follow the [displaying menu items from database](menus.md) documentation to dynamically load menu items on the header file.

[Back to Theme Development](theme.md)
