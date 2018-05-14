# Multiple Layout Files

[In previous topic](theme.md), you created a basic theme without getting into much detail.
But putting everything inside `Layout.cshtml` would be inefficient.

Splitting the layout into multiple files keeps you remain organized.

```
theme
├── css
│   ├── style.css
│   └── semantic.min.css
├── scripts
│   ├── theme.js
│   └── jquery.min.js
│   └── semantic.min.js
└── 404.cshtml
└── Footer.cshtml
└── Header.cshtml
└── Layout.cshtml
└── Layout-Home.cshtml
└── Preview.png
└── Theme.config
└── web.config
```

Follow the [layout sections](layout-sections.md) documentation for more information.

[Back to Theme Development](theme.md)