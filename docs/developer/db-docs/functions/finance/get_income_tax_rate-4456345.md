# finance.get_income_tax_rate function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.get_income_tax_rate(_office_id integer)
RETURNS decimal_strict
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : get_income_tax_rate
* Arguments : _office_id integer
* Owner : frapid_db_user
* Result Type : decimal_strict
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.get_income_tax_rate(_office_id integer)
 RETURNS decimal_strict
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN income_tax_rate
    FROM finance.tax_setups
    WHERE finance.tax_setups.office_id = _office_id
    AND NOT finance.tax_setups.deleted;
        
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

