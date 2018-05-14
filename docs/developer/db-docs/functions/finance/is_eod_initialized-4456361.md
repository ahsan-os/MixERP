# finance.is_eod_initialized function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.is_eod_initialized(_office_id integer, _value_date date)
RETURNS boolean
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : is_eod_initialized
* Arguments : _office_id integer, _value_date date
* Owner : frapid_db_user
* Result Type : boolean
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.is_eod_initialized(_office_id integer, _value_date date)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
BEGIN
    IF EXISTS
    (
        SELECT * FROM finance.day_operation
        WHERE office_id = _office_id
        AND value_date = _value_date
        AND completed = false
    ) then
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

