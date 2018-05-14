# inventory.list_closing_stock function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.list_closing_stock(_store_id integer)
RETURNS TABLE(item_id integer, item_code text, item_name text, unit_id integer, unit_name text, quantity numeric)
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : list_closing_stock
* Arguments : _store_id integer
* Owner : frapid_db_user
* Result Type : TABLE(item_id integer, item_code text, item_name text, unit_id integer, unit_name text, quantity numeric)
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.list_closing_stock(_store_id integer)
 RETURNS TABLE(item_id integer, item_code text, item_name text, unit_id integer, unit_name text, quantity numeric)
 LANGUAGE plpgsql
AS $function$
BEGIN
    DROP TABLE IF EXISTS temp_closing_stock;

    CREATE TEMPORARY TABLE temp_closing_stock
    (
        item_id             integer,
        item_code           text,
        item_name           text,
        unit_id             integer,
        unit_name           text,
        quantity            decimal(30, 6),
        maintain_inventory  boolean
    ) ON COMMIT DROP;

    INSERT INTO temp_closing_stock(item_id, unit_id, quantity)
    SELECT 
        inventory.verified_checkout_details_view.item_id, 
        inventory.verified_checkout_details_view.base_unit_id,
        SUM(CASE WHEN inventory.verified_checkout_details_view.transaction_type='Dr' THEN inventory.verified_checkout_details_view.base_quantity ELSE inventory.verified_checkout_details_view.base_quantity * -1 END)
    FROM inventory.verified_checkout_details_view
    WHERE inventory.verified_checkout_details_view.store_id = _store_id
    GROUP BY inventory.verified_checkout_details_view.item_id, inventory.verified_checkout_details_view.store_id, inventory.verified_checkout_details_view.base_unit_id;

    UPDATE temp_closing_stock SET 
        item_code = inventory.items.item_code,
        item_name = inventory.items.item_name,
        maintain_inventory = inventory.items.maintain_inventory
    FROM inventory.items
    WHERE temp_closing_stock.item_id = inventory.items.item_id;

    DELETE FROM temp_closing_stock WHERE NOT temp_closing_stock.maintain_inventory;

    UPDATE temp_closing_stock SET 
        unit_name = inventory.units.unit_name
    FROM inventory.units
    WHERE temp_closing_stock.unit_id = inventory.units.unit_id;

    RETURN QUERY
    SELECT 
        temp_closing_stock.item_id, 
        temp_closing_stock.item_code, 
        temp_closing_stock.item_name, 
        temp_closing_stock.unit_id, 
        temp_closing_stock.unit_name, 
        temp_closing_stock.quantity
    FROM temp_closing_stock
    ORDER BY item_id;
END;
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

