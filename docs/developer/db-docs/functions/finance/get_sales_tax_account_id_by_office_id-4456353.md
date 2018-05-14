# finance.get_sales_tax_account_id_by_office_id function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.get_sales_tax_account_id_by_office_id(_office_id integer)
RETURNS integer
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : get_sales_tax_account_id_by_office_id
* Arguments : _office_id integer
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.get_sales_tax_account_id_by_office_id(_office_id integer)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN finance.tax_setups.sales_tax_account_id
    FROM finance.tax_setups
    WHERE NOT finance.tax_setups.deleted
    AND finance.tax_setups.office_id = _office_id;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

