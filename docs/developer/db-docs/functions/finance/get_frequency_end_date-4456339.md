# finance.get_frequency_end_date function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.get_frequency_end_date(_frequency_id integer, _value_date date)
RETURNS date
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : get_frequency_end_date
* Arguments : _frequency_id integer, _value_date date
* Owner : frapid_db_user
* Result Type : date
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.get_frequency_end_date(_frequency_id integer, _value_date date)
 RETURNS date
 LANGUAGE plpgsql
 STABLE
AS $function$
    DECLARE _end_date date;
BEGIN
    SELECT MIN(value_date)
    INTO _end_date
    FROM finance.frequency_setups
    WHERE value_date > $2
    AND frequency_id = ANY( finance.get_frequencies($1));

    RETURN _end_date;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

