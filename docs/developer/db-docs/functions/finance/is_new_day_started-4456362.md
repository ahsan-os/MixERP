# finance.is_new_day_started function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.is_new_day_started(_office_id integer)
RETURNS boolean
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : is_new_day_started
* Arguments : _office_id integer
* Owner : frapid_db_user
* Result Type : boolean
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.is_new_day_started(_office_id integer)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
BEGIN
    IF EXISTS
    (
        SELECT 0 FROM finance.day_operation
        WHERE finance.day_operation.office_id = _office_id
        AND finance.day_operation.completed = false
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

