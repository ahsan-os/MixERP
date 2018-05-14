# core.is_valid_office_id function:

```plpgsql
CREATE OR REPLACE FUNCTION core.is_valid_office_id(integer)
RETURNS boolean
```
* Schema : [core](../../schemas/core.md)
* Function Name : is_valid_office_id
* Arguments : integer
* Owner : frapid_db_user
* Result Type : boolean
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION core.is_valid_office_id(integer)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
BEGIN
    IF EXISTS(SELECT 1 FROM core.offices WHERE office_id=$1) THEN
        RETURN true;
    END IF;

    RETURN false;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

