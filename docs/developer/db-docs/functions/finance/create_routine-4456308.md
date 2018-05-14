# finance.create_routine function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.create_routine(_routine_code character varying, _routine regproc, _order integer)
RETURNS void
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : create_routine
* Arguments : _routine_code character varying, _routine regproc, _order integer
* Owner : frapid_db_user
* Result Type : void
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.create_routine(_routine_code character varying, _routine regproc, _order integer)
 RETURNS void
 LANGUAGE plpgsql
AS $function$
BEGIN
    IF NOT EXISTS(SELECT * FROM finance.routines WHERE routine_code=_routine_code) THEN
        INSERT INTO finance.routines(routine_code, routine_name, "order")
        SELECT $1, $2, $3;
        RETURN;
    END IF;

    UPDATE finance.routines
    SET
        routine_name = _routine,
        "order" = _order
    WHERE routine_code=_routine_code;
    RETURN;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

