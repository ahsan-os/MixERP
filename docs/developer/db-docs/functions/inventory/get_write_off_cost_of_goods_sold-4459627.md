# inventory.get_write_off_cost_of_goods_sold function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.get_write_off_cost_of_goods_sold(_checkout_id bigint, _item_id integer, _unit_id integer, _quantity integer)
RETURNS money_strict2
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : get_write_off_cost_of_goods_sold
* Arguments : _checkout_id bigint, _item_id integer, _unit_id integer, _quantity integer
* Owner : frapid_db_user
* Result Type : money_strict2
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.get_write_off_cost_of_goods_sold(_checkout_id bigint, _item_id integer, _unit_id integer, _quantity integer)
 RETURNS money_strict2
 LANGUAGE plpgsql
AS $function$
    DECLARE _base_unit_id integer;
    DECLARE _factor decimal(30, 6);
BEGIN
    _base_unit_id    = inventory.get_root_unit_id(_unit_id);
    _factor          = inventory.convert_unit(_unit_id, _base_unit_id);


    RETURN
        SUM((cost_of_goods_sold / base_quantity) * _factor * _quantity)     
         FROM inventory.checkout_details
    WHERE checkout_id = _checkout_id
    AND item_id = _item_id;    
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

