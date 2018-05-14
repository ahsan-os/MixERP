# finance.initialize_eod_operation function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.initialize_eod_operation(_user_id integer, _office_id integer, _value_date date)
RETURNS void
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : initialize_eod_operation
* Arguments : _user_id integer, _office_id integer, _value_date date
* Owner : frapid_db_user
* Result Type : void
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.initialize_eod_operation(_user_id integer, _office_id integer, _value_date date)
 RETURNS void
 LANGUAGE plpgsql
AS $function$
    DECLARE this            RECORD;    
BEGIN
    IF(_value_date IS NULL) THEN
        RAISE EXCEPTION 'Invalid date.'
        USING ERRCODE='P3008';        
    END IF;

    IF(NOT account.is_admin(_user_id)) THEN
        RAISE EXCEPTION 'Access is denied.'
        USING ERRCODE='P9010';
    END IF;

    IF(_value_date != finance.get_value_date(_office_id)) THEN
        RAISE EXCEPTION 'Invalid value date.'
        USING ERRCODE='P3007';
    END IF;

    SELECT * FROM finance.day_operation
    WHERE value_date=_value_date 
    AND office_id = _office_id INTO this;

    IF(this IS NULL) THEN
        INSERT INTO finance.day_operation(office_id, value_date, started_on, started_by)
        SELECT _office_id, _value_date, NOW(), _user_id;
    ELSE    
        RAISE EXCEPTION 'EOD operation was already initialized.'
        USING ERRCODE='P8101';
    END IF;

    RETURN;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

