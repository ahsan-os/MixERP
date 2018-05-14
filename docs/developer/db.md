# Database of Frapid

Follow the [database documentation](db-docs/README.md) for more info. The database documentation also contains additional MixERP modules listed on individual [MixERP v2 Repositories](https://github.com/mixerp/mixerp) as an extended example. MixERP is free and open source software distributed under [AGPLv3](https://github.com/mixerp/mixerp/blob/master/LICENSE) license.



## Conventions

* A frapid module should create objects under its own single schema.
* A blank database script should be located on `~/src/Frapid.Web/Areas/<module>/db`.
* Optionally, sample database script can also be placed on `~/src/Frapid.Web/Areas/<module>/db`.

## Sql Bundler

It is advised to use [MixERP Sql Bundler Utility](http://github.com/mixerp/sqlbundler).

**Conventions**

* do not create a single SQL script and keep everything on it. It is difficult to manage that way.
* create your own directory structure [similar to frapid](https://github.com/frapid/frapid/tree/master/src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/PostgreSQL/1.x/1.0/src) and store individual sql files there.
* use SqlBundler.exe to bundle everything together to generate a single SQL file.

**How Sql Bundler Works?**

Create a `.sqlbundle` [file](https://github.com/frapid/frapid/blob/master/src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/PostgreSQL/1.x/1.0/website.sqlbundle) (`yaml` format) on your [db directory](https://github.com/frapid/frapid/tree/master/src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/PostgreSQL/1.x/1.0).

```yaml
- script-directory : db/1.x/1.0/src
- output-directory:db/1.x/1.0
```


* **script-directory**: The path containing your SQL files. Every single file having `.sql` (but not `.sample.sql`) extension is merged together to create a bundle.
* **output-directory**: The directory where the bundled file will be created. The bundled file will have the filename of `.sqlbundle` file.

**Syntax**

```
path-to\SqlBundler.exe root-path sqlbundle-directory include_sample
```

**Example**

- https://github.com/frapid/frapid/blob/master/src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/PostgreSQL/1.x/1.0/rebundle-db-with-sample.bat
- https://github.com/frapid/frapid/blob/master/src/Frapid.Web/Areas/Frapid.WebsiteBuilder/db/PostgreSQL/1.x/1.0/rebundle-db-without-sample.bat

[Back to Developer Documentation](README.md)
