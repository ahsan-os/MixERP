# config.get_server_timezone function:

```plpgsql
CREATE OR REPLACE FUNCTION config.get_server_timezone()
RETURNS character varying
```
* Schema : [config](../../schemas/config.md)
* Function Name : get_server_timezone
* Arguments : 
* Owner : frapid_db_user
* Result Type : character varying
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION config.get_server_timezone()
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN
        pg_catalog.pg_settings.setting
    FROM pg_catalog.pg_settings
    WHERE name = 'log_timezone';
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

