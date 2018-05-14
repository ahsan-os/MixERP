# SQL Server Configuration

The database server parameters are configured on `~/Resources/Configs/SQLServer.config` file.
Please be advised that once you correctly configure this file, frapid will automatically create database(s) as and when required.

**~/Resources/Configs/DBServer.config**
```xml
<?xml version="1.0"?>
<configuration>
  <appSettings>
	<add key="Server" value="localhost" />
	<add key="Port" value="" />
	<add key="MetaDatabase" value="master" />
	<add key="EnablePooling" value="true" />
	<add key="MinPoolSize" value="0" />
	<add key="MaxPoolSize" value="100" />
	<add key="SuperUserId" value="sa" /><!-- Super user account is needed only to create database(s). -->
	<add key="SuperUserPassword" value="???" />
	<add key="TrustedSuperUserConnection" value="true" />
	<add key="UserId" value="frapid_db_user" /><!-- If not found, Frapid automatically creates the login: frapid_db_user/change-on-deployment@123. -->
	<add key="Password" value="change-on-deployment@123" />
	<add key="ReportUserId" value="report_user" /><!-- If not found, Frapid automatically creates the login: report_user/change-on-deployment@123. Make sure that you do not allow write permission to this user.-->
	<add key="ReportUserPassword" value="change-on-deployment@123" />
	<add key="DatabaseBackupDirectory" value="/Backups/" />
  </appSettings>
</configuration>
```

## Configuration Explained

| Key                        | Configuration                            |
| -------------------------- | ---------------------------------------- |
| Server                     | The hostname or IP address of your SQL Server Server instance. Usually "localhost". |
| Port                       | The port on which the SQL Server server is listening. Usually empty or "1433". |
| SuperUserId                | The user name of SQL Server superuser account. This account is used only when frapid needs to create a new database. |
| Password                   | Password for the above user.             |
| TrustedSuperUserConnection | Creates a trusted windows authentication connection with SQL Server. You can omit the fields `SuperUserId` and `SuperUserPassword` if you set this field to **true**. |
| UserId                     | If not found, frapid will create a user called "frapid_db_user". Frapid uses this account to communicate with the database server. This user has (or should have) minimum permission sets to perform database manipulation. Leave it as it is if you want frapid to take care of this for you. |
| Password                   | Password for the above user. The default password is "change-on-deployment". If you changed password for this account on SQL Server Server, change it here as well. |
| MetaDatabase               | The **master database** which contains multi-instance meta information. |
| ReportUserId               | If not found, frapid will create a user called "report_user". This user **must have a read-only access to the database**. Leave it as it is if you want frapid to take care of this for you. |
| ReportUserPassword         | Password for the above user. The default password is "change-on-deployment". If you changed password for this account on SQL Server Server, change it here as well. |

### Related Contents

* [Installing Frapid](../installation/README.md)
* [Developer Documentation](../developer/README.md)
* [Documentation Home](../../README.md)
