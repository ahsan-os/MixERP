# Data Transfer Objects

Data transfer objects represent database objects instead of Entity Models. The whole and sole purpose of DTO is to facilitate data transfer without having any behavior at all.

## DTO Convention

DTO classes are representation of database objects, which have ```underscore_separated_lowercase_identifiers```, as shown below

```plpgsql
CREATE TABLE core.apps
(
    app_name                national character varying(100) PRIMARY KEY,
    name                    national character varying(100),
    version_number          national character varying(100),
    publisher               national character varying(100),
    published_on            date,
    icon                    national character varying(100),
    landing_url             text
)
```

whereas, the DTO classes

* have ```PascalCaseIdentifiers```.
* should implement ```IPoco interface```.
* are decorated with ```"TableName" Attribute```.
* may be decorated with ```"PrimaryKey" Attribute``` (if the table or view has a primary key defined in it).

```cs
    [TableName("core.apps")]
    [PrimaryKey("app_name", AutoIncrement = false)]
    public sealed class App : IPoco
    {
        public string AppName { get; set; }
        public string Name { get; set; }
        public string VersionNumber { get; set; }
        public string Publisher { get; set; }
        public DateTime? PublishedOn { get; set; }
        public string Icon { get; set; }
        public string LandingUrl { get; set; }
    }
```

[Back to Developer Documentation](../README.md)
