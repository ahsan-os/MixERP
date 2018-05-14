# inventory.get_item_cost_price function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.get_item_cost_price(_item_id integer, _unit_id integer)
RETURNS money_strict2
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : get_item_cost_price
* Arguments : _item_id integer, _unit_id integer
* Owner : frapid_db_user
* Result Type : money_strict2
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.get_item_cost_price(_item_id integer, _unit_id integer)
 RETURNS money_strict2
 LANGUAGE plpgsql
 STABLE
AS $function$
    DECLARE _price              public.money_strict2;
    DECLARE _costing_unit_id    integer;
    DECLARE _factor             decimal(30, 6);
  
BEGIN    
    SELECT 
        cost_price, 
        unit_id
    INTO 
        _price, 
        _costing_unit_id
    FROM inventory.items
    WHERE inventory.items.item_id = _item_id
	AND NOT inventory.items.deleted;

    --Get the unitary conversion factor if the requested unit does not match with the price defition.
    _factor := inventory.convert_unit(_unit_id, _costing_unit_id);
    RETURN _price * _factor;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

