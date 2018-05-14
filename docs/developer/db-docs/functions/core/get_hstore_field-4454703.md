# core.get_hstore_field function:

```plpgsql
CREATE OR REPLACE FUNCTION core.get_hstore_field(_hstore hstore, _column_name text)
RETURNS text
```
* Schema : [core](../../schemas/core.md)
* Function Name : get_hstore_field
* Arguments : _hstore hstore, _column_name text
* Owner : frapid_db_user
* Result Type : text
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION core.get_hstore_field(_hstore hstore, _column_name text)
 RETURNS text
 LANGUAGE plpgsql
AS $function$
   DECLARE _field_value text;
BEGIN
    _field_value := _hstore->_column_name;
    RETURN _field_value;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

