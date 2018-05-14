# inventory.count_item_in_stock function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.count_item_in_stock(_item_id integer, _unit_id integer, _store_id integer)
RETURNS numeric
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : count_item_in_stock
* Arguments : _item_id integer, _unit_id integer, _store_id integer
* Owner : frapid_db_user
* Result Type : numeric
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.count_item_in_stock(_item_id integer, _unit_id integer, _store_id integer)
 RETURNS numeric
 LANGUAGE plpgsql
 STABLE
AS $function$
    DECLARE _debit decimal(30, 6);
    DECLARE _credit decimal(30, 6);
    DECLARE _balance decimal(30, 6);
BEGIN

    _debit := inventory.count_purchases($1, $2, $3);
    _credit := inventory.count_sales($1, $2, $3);

    _balance:= _debit - _credit;    
    return _balance;  
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

