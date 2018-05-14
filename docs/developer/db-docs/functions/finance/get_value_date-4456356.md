# finance.get_value_date function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.get_value_date(_office_id integer)
RETURNS date
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : get_value_date
* Arguments : _office_id integer
* Owner : frapid_db_user
* Result Type : date
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.get_value_date(_office_id integer)
 RETURNS date
 LANGUAGE plpgsql
AS $function$
    DECLARE this            RECORD;
    DECLARE _value_date     date;
BEGIN
    SELECT * FROM finance.day_operation
    WHERE office_id = _office_id
    AND value_date =
    (
        SELECT MAX(value_date)
        FROM finance.day_operation
        WHERE office_id = _office_id
    ) INTO this;

    IF(this.day_id IS NOT NULL) THEN
        IF(this.completed) THEN
            _value_date  := this.value_date + interval '1' day;
        ELSE
            _value_date  := this.value_date;    
        END IF;
    END IF;

    IF(_value_date IS NULL) THEN
        _value_date := NOW() AT time zone config.get_server_timezone();
    END IF;
    
    RETURN _value_date;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

