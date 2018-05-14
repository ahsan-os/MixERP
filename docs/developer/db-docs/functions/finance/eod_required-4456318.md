# finance.eod_required function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.eod_required(_office_id integer)
RETURNS boolean
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : eod_required
* Arguments : _office_id integer
* Owner : frapid_db_user
* Result Type : boolean
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.eod_required(_office_id integer)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
    DECLARE _value_date     date = finance.get_value_date(_office_id);
BEGIN
    RETURN finance.fiscal_year.eod_required
    FROM finance.fiscal_year
    WHERE finance.fiscal_year.office_id = _office_id
    AND NOT finance.fiscal_year.deleted
    AND finance.fiscal_year.starts_from >= _value_date
    AND finance.fiscal_year.ends_on <= _value_date;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

