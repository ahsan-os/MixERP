# finance.get_income_tax_provison_amount function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.get_income_tax_provison_amount(_office_id integer, _profit numeric, _balance numeric)
RETURNS numeric
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : get_income_tax_provison_amount
* Arguments : _office_id integer, _profit numeric, _balance numeric
* Owner : frapid_db_user
* Result Type : numeric
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.get_income_tax_provison_amount(_office_id integer, _profit numeric, _balance numeric)
 RETURNS numeric
 LANGUAGE plpgsql
AS $function$
    DECLARE _rate real;
BEGIN
    _rate := finance.get_income_tax_rate(_office_id);

    RETURN
    (
        (_profit * _rate/100) - _balance
    );
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

