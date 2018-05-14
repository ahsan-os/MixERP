# AppInfo.json File

Frapid uses the `AppInfo.json` file to discover your module (AKA application).

Here is an example AppInfo.json file of Account module.

```json
{
	"ApplicationName": "Frapid.Account",
	"AutoInstall": true,
	"Thumbnail": null,
	"Publisher": "MixERP Inc.",
	"Url": "http://frapid.com",
	"DocumentationUrl": "http://frapid.com",
	"AssemblyName": "Frapid.Account",
	"Version": "1.0",
	"RealeasedOn": null,
	"Description": "Frapid authentication module.",
	"Category": "Core Modules",
	"Bundle": "Frapid Framework",
    "IsMeta": false,
	"DbSchema": "account",
	"BlankDbPath": "~/Areas/Frapid.Account/db/1.x/1.0/account-blank.sql",
	"SampleDbPath": null,
	"PatchFilePath": null,
	"InstallSample": true,
	"My": "~/Areas/Frapid.Account/.my/db.sql",
    "OverrideTemplatePath":"~/Areas/Frapid.Account/Override",
    "OverrideDestination":"~/Tenants/{0}/Areas",
	"DependsOn": ["Frapid.Core"]
}
```

| Property  | Description |
| --- | --- |
| ApplicationName | Your application name. |
| AutoInstall | Set this true if you want this application to be automatically installed. Note: frapid will only auto install your module during new instance creation. Setting this to true **is not recommended** and can be overridden (ignored) on web.config. |
| Thumbnail | The path to this application's thumbnail or icon. Allowed format: png. |
| Publisher | The application publisher's name. |
| Url | Website Url of this application. |
| DocumentationUrl | Url to this application's documentation. |
| AssemblyName | Fully qualified assembly name of this application. |
| Version | This application's version. |
| RealeasedOn | The released date of this application. |
| Description | Enter a descriptive information about this application. Note that description text will be truncated on 500 characters on application directory listing. |
| Category | Category of this application. |
| Bundle | Multiple applications can be bundled together and installed in one go. Enter the name of this application's bundle or package. |
| IsMeta | When set to true, targets the [meta database](../configs/DbServer.config.md). This should be set to false on custom applications. |
| DbSchema | This application's database objects should reside in a single and unique schema. Enter the name of the schema here. |
| BlankDbPath | Path to this application's blank database script. |
| SampleDbPath | Path to this application's sample database script. |
| PatchFilePath | Path to this application's patch database script. A patch file is responsible to update the previous version of this application's database to the new version. For the first version of this application, set this to null. |
| InstallSample | Set this to true if you want the `[x] Install Sample?` checkbox shown to the user pre-checked. The backend user may still be able to un-check this option and install this application without installing the sample script. |
| My | The path to `my file` which will automatically run during installation. The `my file` is SQL file where you may store secrets like SMTP credentials which does not get committed to the git repository. This feature can be used on development environments only. Frapid running on production environment will ignore this setting. |
| OverrideTemplatePath | The path to your [override templates](overrides.md). During installation, Frapid will copy files under this directory to tenant-specific destination directory. |
| OverrideDestination | The destination where your override templates will be copied to during installation. The placeholder `{0}` will resolve to the tenant directory during installation. |
| DependsOn | Array of application names on which this application depends on. |

[Back to Developer Documentation](README.md)