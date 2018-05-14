# config.get_custom_field_setup_id_by_table_name function:

```plpgsql
CREATE OR REPLACE FUNCTION config.get_custom_field_setup_id_by_table_name(_schema_name character varying, _table_name character varying, _field_name character varying)
RETURNS integer
```
* Schema : [config](../../schemas/config.md)
* Function Name : get_custom_field_setup_id_by_table_name
* Arguments : _schema_name character varying, _table_name character varying, _field_name character varying
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION config.get_custom_field_setup_id_by_table_name(_schema_name character varying, _table_name character varying, _field_name character varying)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN custom_field_setup_id
    FROM config.custom_field_setup
    WHERE config.custom_field_setup.form_name = config.get_custom_field_form_name(_schema_name, _table_name)
    AND config.custom_field_setup.field_name = _field_name
	AND NOT config.custom_field_setup.deleted;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

