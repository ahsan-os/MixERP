# inventory.count_sales function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.count_sales(_item_id integer, _unit_id integer, _store_id integer)
RETURNS numeric
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : count_sales
* Arguments : _item_id integer, _unit_id integer, _store_id integer
* Owner : frapid_db_user
* Result Type : numeric
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.count_sales(_item_id integer, _unit_id integer, _store_id integer)
 RETURNS numeric
 LANGUAGE plpgsql
 STABLE
AS $function$
    DECLARE _base_unit_id integer;
    DECLARE _credit decimal(30, 6);
    DECLARE _factor decimal(30, 6);
BEGIN
    --Get the base item unit
    SELECT 
        inventory.get_root_unit_id(inventory.items.unit_id) 
    INTO _base_unit_id
    FROM inventory.items
    WHERE inventory.items.item_id=$1
	AND NOT inventory.items.deleted;

    SELECT 
        COALESCE(SUM(base_quantity), 0)
    INTO _credit
    FROM inventory.checkout_details
    INNER JOIN inventory.checkouts
    ON inventory.checkouts.checkout_id = inventory.checkout_details.checkout_id
    INNER JOIN finance.transaction_master
    ON inventory.checkouts.transaction_master_id = finance.transaction_master.transaction_master_id
    WHERE finance.transaction_master.verification_status_id > 0
    AND inventory.checkout_details.item_id=$1
    AND inventory.checkout_details.store_id=$3
    AND inventory.checkout_details.transaction_type='Cr';

    _factor = inventory.convert_unit(_base_unit_id, $2);
    RETURN _credit * _factor;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

