# finance.get_fiscal_half_end_date function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.get_fiscal_half_end_date(_office_id integer)
RETURNS date
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : get_fiscal_half_end_date
* Arguments : _office_id integer
* Owner : frapid_db_user
* Result Type : date
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.get_fiscal_half_end_date(_office_id integer)
 RETURNS date
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN MIN(value_date) 
    FROM finance.frequency_setups
    WHERE value_date >= finance.get_value_date(_office_id)
    AND frequency_id > 3
    AND finance.frequency_setups.office_id = _office_id;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

