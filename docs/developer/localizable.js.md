# localizable.js

The localizable.js utility automatically translates localization literals depending on the current language of the application user.

## Usage

Include the localizable.js utility on your View.

```html
<script src="/scripts/frapid/utilities/localizable.js"></script>
```

## Example

The localizable.js investigates html controls having the following attributes:

* data-localize
* data-localized-placeholder
* data-localized-title

### data-localize
**This**

```html
<div class="ui message">
    <span data-localize="CompanyName"></span>
</div>
```

**will be converted to (English)**

```html
<div class="ui message">
    <span>Company Name</span>
</div>
```

**or (German)**


```html
<div class="ui message">
    <span>Firmenname</span>
</div>
```

### data-localized-placeholder
**This**

```html
<input data-localized-placeholder="Customer" />
```

**will be converted to (English)**

```html
<input placeholder="Customer" />
```

**or (German)**


```html
<input placeholder="Kunde" />
```


### data-localized-title
**This**

```html
<input data-localized-title="Customer" />
```

**will be converted to (English)**

```html
<input title="Customer" />
```

**or (German)**


```html
<input title="Kunde" />
```



## Translate a Localization Key Using Javascript

To translate a given key to the current language, you can use a window function called `translate`. Example:

```js
window.translate('Customer')//Kunde in German
```



[Back to Internationalization](i18n.md)

