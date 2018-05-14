# core.get_office_id_by_office_name function:

```plpgsql
CREATE OR REPLACE FUNCTION core.get_office_id_by_office_name(_office_name text)
RETURNS integer
```
* Schema : [core](../../schemas/core.md)
* Function Name : get_office_id_by_office_name
* Arguments : _office_name text
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION core.get_office_id_by_office_name(_office_name text)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN core.offices.office_id
    FROM core.offices
    WHERE core.offices.office_name = _office_name
	AND NOT core.offices.deleted;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

