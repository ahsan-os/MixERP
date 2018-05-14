# finance.get_month_start_date function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.get_month_start_date(_office_id integer)
RETURNS date
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : get_month_start_date
* Arguments : _office_id integer
* Owner : frapid_db_user
* Result Type : date
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.get_month_start_date(_office_id integer)
 RETURNS date
 LANGUAGE plpgsql
AS $function$
    DECLARE _date               date;
BEGIN
    SELECT MAX(value_date) + 1
    INTO _date
    FROM finance.frequency_setups
    WHERE value_date < 
    (
        SELECT MIN(value_date)
        FROM finance.frequency_setups
        WHERE value_date >= finance.get_value_date(_office_id)
        AND finance.frequency_setups.office_id = _office_id
    );

    IF(_date IS NULL) THEN
        SELECT starts_from 
        INTO _date
        FROM finance.fiscal_year
        WHERE finance.fiscal_year.office_id = _office_id;
    END IF;

    RETURN _date;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

