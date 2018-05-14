# Frapid Console

Frapid Console is a command line executable file which provides you tools to automate basic Frapid tasks. Frapid Console is a part of Frapid build process:

- creating tenant
- creating test application
- etc 



## Commands



#### Create App

Creates a new Frapid app or module with the given name.

**Syntax**

```shell
create app <AppName>
```

| Token      | Description                              |
| ---------- | :--------------------------------------- |
| create app | Name of the command, creates a new frapid app. |
| AppName    | The fully qualified assembly name of your application or module. |

**Example**

```shell
create app Frapid.Tasks.Todo
```

**Result:**

A new app is created on the following path:

`/Areas/Frapid.Tasks.Todo`

Do not forget to edit the file `AppInfo.json` on your newly created application directory.



#### Create Site

Creates a new tenant or web site.

**Syntax**

```shell
create site <DomainName> provider <ProviderName> [cleanup when done]
```

| Token             | Description                              |
| ----------------- | :--------------------------------------- |
| create site       | Name of the command, creates a new tenant. |
| DomainName        | The DNS domain name of the website that points to the web server on which Frapid site is hosted. Please make sure that you have an entry for this domain on DomainsApproved.json file. |
| provider          | The name of  database provider. Valid values are: `Npgsql` for PostgreSQL Server or `System.Data.SqlClient` for SQL Server. |
| cleanup when done | Optional parameter, deletes the tenant after creating it. Useful for testing purpose only. **Warning: use at your own risk!** |

**Example**

```shell
create site localhost provider Npgsql
```

**Result:**

A new tenant is created on the following path:

`/Tenants/localhost`

If you configured everything correctly, Frapid should now be accessible on:

http://localhost



#### Create Theme

Creates a new theme on the given tenant.

**Syntax**

```shell
create theme <ThemeName> on instance <Instance>
or
create theme <ThemeName> on domain <Domain>
```

| Token        | Description                              |
| ------------ | :--------------------------------------- |
| create theme | Name of the command, creates a new theme. |
| ThemeName    | The name of the theme created.           |
| on instance  | Signifies that the tenant should be located by the tenant name. |
| Instance     | Name of the tenant to search.            |
| on domain    | Signifies that the tenant should be located by the domain name. |
| Domain       | Name of the tenant's domain.             |

**Example**

```shell
create theme semantic3 on insance localhost
```

**Result:**

A new theme is created on the following path:

`/Tenants/localhost/Areas/Frapid.WebsiteBuilder/Themes/semantic3`



#### Create Resource

Creates strongly typed resource on the given application, or for all applications.

**Syntax**

```shell
create resource [on <AppName>]
or
create resource
```

| Token           | Description                              |
| --------------- | :--------------------------------------- |
| create resource | Name of the command, creates strongly-typed resource files. |
| on AppName      | The name of the app for which resource should be created. If you omit this token, Frapid will create resource for all apps. |

**Example**

```shell
create resource on Frapid.WebsiteBuilder
```

**Result:**

A new file is created on the following path:

`/Areas/Frapid.WebsiteBuilder/I18N.cs`

If this is the first time you've generated strongly-typed resource for this app, you will just need to include this file on Visual Studio. Thenceforth, the file will change every time you run this command.



#### Pack Resource

Creates a resource package (translation files) for localization for a given culture, or for all cultures.

**Syntax**

```shell
pack resource [for <Language>]
or
pack resource
```

| Token         | Description                              |
| ------------- | :--------------------------------------- |
| pack resource | Name of the command, creates translation files to submit for work. |
| for Language  | The name of the culture for which resource should be packaged. If you omit this token, Frapid will create resource for all supported cultures. |

**Example**

```shell
pack resource
```

**Result:**

Resource packages are generated and overwritten on the following path:

`/Packages/i18n`

The above path contains list of language files to send for translation job. Pickup all languages you require, and copy it somewhere else for transfer.

**Please Note:**

Please make sure that you do not accidentally run this command and overwrite if you placed finished translation here for unpacking.



[Back to Documentation](README.md)

