# Approved Domains

Since frapid allows you to host multiple tenants under a single IIS Application, each tenant is [identified by its domain name](../concepts/database-naming-convention.md). 
When you create IIS website (for frapid application) and enable it to listen on any DNS domain name, each domain automatically becomes a tenant.

**~/Resources/Configs/DomainsApproved.json**
```json
[
  {
    "DbProvider": "Npgsql",
    "DomainName": "postgres.localhost",
    "BackupDirectory": "/backups/mixerp",
    "BackupDirectoryIsFixedPath": false,
    "EnforceSsl": false,
    "CdnDomain": "cdn2.mixerp.com",
    "AdminEmail": "demo@mixerp.com",
    "BcryptedAdminPassword": "$2a$10$PdCGxBu65cEmBy.suBS2H..KYEd/1bMTx3hk1nUSD/k6s6948weZK",
    "Synonyms": [
      "init01.salesific.com",
      "localhost"
    ]
  },
  {
    "DbProvider": "System.Data.SqlClient",
    "DomainName": "frapid.localhost",
    "BackupDirectory": "/backups/frapid",
    "BackupDirectoryIsFixedPath": false,
    "EnforceSsl": false,
    "CdnDomain": "",
    "AdminEmail": "demo@mixerp.net",
    "BcryptedAdminPassword": "$2a$10$PdCGxBu65cEmBy.suBS2H..YKDe/1bMTx3hk1nUSD/k6s6948weZK",
    "Synonyms": [
      "sqlserver.frapid.com"
    ]
  }
]
```

## How Are Domains Served in Frapid?

If the requested DNS domain name (say `example.com`) is resolved back and served by frapid IIS Application, an HTTP 404 error will be thrown if the entry for that DNS domain
name (`example.com`) is not present in the list of approved domains.

If the requested DNS domain has an entry in the list of approved domains, it will be served automatically and installed (if not already).

## How does Frapid use CDN?

Frapid automatically converts the HTML output so that the static resources are served from the `CdnDomain`. For example,

http://postgres.localhost/static.png

will be converted to

http://cdn2.mixerp.com/static.png

Remember that, in order for this to work, both DNS names `CdnDomain` and `DomainName` should resolve to the same web server.


## Why Cannot I Login to Admin Area?

Despite of the configuration file above clearly indicating that the administrator for `docs.frapid.com` is `admin@frapid.com`,
no user account is actually created when `docs.frapid.com` is installed or instantiated.

Once a user having the email address `admin@frapid.com` signs up or (signs in using social login), an account for that user is created with administrative rights.


### Related Contents

* [Installing Frapid](../installation/README.md)
* [Developer Documentation](../developer/README.md)
* [Documentation Home](../../README.md)
