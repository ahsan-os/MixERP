# finance.is_restricted_mode function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.is_restricted_mode()
RETURNS boolean
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : is_restricted_mode
* Arguments : 
* Owner : frapid_db_user
* Result Type : boolean
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.is_restricted_mode()
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
BEGIN
    IF EXISTS
    (
        SELECT 0 FROM finance.day_operation
        WHERE completed = false
        LIMIT 1
    ) THEN
        RETURN true;
    END IF;

    RETURN false;
END;
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

