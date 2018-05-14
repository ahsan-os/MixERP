# core.get_office_name_by_office_id function:

```plpgsql
CREATE OR REPLACE FUNCTION core.get_office_name_by_office_id(_office_id integer)
RETURNS text
```
* Schema : [core](../../schemas/core.md)
* Function Name : get_office_name_by_office_id
* Arguments : _office_id integer
* Owner : frapid_db_user
* Result Type : text
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION core.get_office_name_by_office_id(_office_id integer)
 RETURNS text
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN core.offices.office_name
    FROM core.offices
    WHERE core.offices.office_id = _office_id
	AND NOT core.offices.deleted;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

