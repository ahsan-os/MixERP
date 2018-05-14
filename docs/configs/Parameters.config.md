# Application Parameters

This file contains standard application configuration data.

**~/Resources/Configs/Parameters.config**
```xml
<?xml version="1.0"?>
<configuration>
  <appSettings>
    <add key="Cultures" value="ar,de,en,es,fil,fr,id,ja,ms,nl,pt,ru,sv,zh" />
    <add key="ApplicationLogDirectory" value="C:\frapid-logs" />
    <add key="MinimumLogLevel" value="Information" />
  </appSettings>
</configuration>
```
## Configuration Explained

| Key                           | Configuration|
|-------------------------------|---------------------------------------------------------|
| Cultures                      | List of cultures that application users can select during log-in. |
| ApplicationLogDirectory       | The physical path of directory where you want to store application logs. |
| MinimumLogLevel               | [The minimum log level](https://github.com/serilog/serilog/wiki/Writing-Log-Events#log-event-levels). Valid values: Verbose, Debug, Information, Warning, Error, Fatal.  |



### Related Contents

* [Installing Frapid](../installation/README.md)
* [Developer Documentation](../developer/README.md)
* [Documentation Home](../../README.md)