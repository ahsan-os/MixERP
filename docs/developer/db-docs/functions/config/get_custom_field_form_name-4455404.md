# config.get_custom_field_form_name function:

```plpgsql
CREATE OR REPLACE FUNCTION config.get_custom_field_form_name(_table_name character varying)
RETURNS character varying
```
* Schema : [config](../../schemas/config.md)
* Function Name : get_custom_field_form_name
* Arguments : _table_name character varying
* Owner : frapid_db_user
* Result Type : character varying
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION config.get_custom_field_form_name(_table_name character varying)
 RETURNS character varying
 LANGUAGE plpgsql
 STABLE
AS $function$
BEGIN
    RETURN form_name 
    FROM config.custom_field_forms
    WHERE config.custom_field_forms.table_name = _table_name
	AND NOT config.custom_field_forms.deleted;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

