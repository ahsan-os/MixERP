# inventory.delete_variant_item function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.delete_variant_item(_item_id integer)
RETURNS boolean
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : delete_variant_item
* Arguments : _item_id integer
* Owner : frapid_db_user
* Result Type : boolean
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.delete_variant_item(_item_id integer)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
BEGIN
    DELETE FROM inventory.item_variants WHERE item_id = _item_id;
    DELETE FROM inventory.items WHERE item_id = _item_id;
    RETURN true;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

