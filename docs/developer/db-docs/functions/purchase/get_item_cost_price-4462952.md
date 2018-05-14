# purchase.get_item_cost_price function:

```plpgsql
CREATE OR REPLACE FUNCTION purchase.get_item_cost_price(_item_id integer, _supplier_id bigint, _unit_id integer)
RETURNS money_strict2
```
* Schema : [purchase](../../schemas/purchase.md)
* Function Name : get_item_cost_price
* Arguments : _item_id integer, _supplier_id bigint, _unit_id integer
* Owner : frapid_db_user
* Result Type : money_strict2
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION purchase.get_item_cost_price(_item_id integer, _supplier_id bigint, _unit_id integer)
 RETURNS money_strict2
 LANGUAGE plpgsql
 STABLE
AS $function$
    DECLARE _price              public.money_strict2;
    DECLARE _costing_unit_id    integer;
    DECLARE _factor             decimal(30, 6);
  
BEGIN
    --Fist pick the catalog price which matches all these fields:
    --Item, Customer Type, Price Type, and Unit.
    --This is the most effective price.
    SELECT 
        purchase.item_cost_prices.price, 
        purchase.item_cost_prices.unit_id
    INTO 
        _price,
        _costing_unit_id
    FROM purchase.item_cost_prices
    WHERE purchase.item_cost_prices.item_id=_item_id
    AND purchase.item_cost_prices.supplier_id =_supplier_id
    AND purchase.item_cost_prices.unit_id = _unit_id
    AND NOT purchase.item_cost_prices.deleted;


    IF(_costing_unit_id IS NULL) THEN
        --We do not have a cost price of this item for the unit supplied.
        --Let's see if this item has a price for other units.
        SELECT 
            purchase.item_cost_prices.price, 
            purchase.item_cost_prices.unit_id
        INTO 
            _price, 
            _costing_unit_id
        FROM purchase.item_cost_prices
        WHERE purchase.item_cost_prices.item_id=_item_id
        AND purchase.item_cost_prices.supplier_id =_supplier_id
	AND NOT purchase.item_cost_prices.deleted;
    END IF;

    
    IF(_price IS NULL) THEN
        --This item does not have cost price defined in the catalog.
        --Therefore, getting the default cost price from the item definition.
        SELECT 
            cost_price, 
            unit_id
        INTO 
            _price, 
            _costing_unit_id
        FROM inventory.items
        WHERE inventory.items.item_id = _item_id
		AND NOT inventory.items.deleted;
    END IF;

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

