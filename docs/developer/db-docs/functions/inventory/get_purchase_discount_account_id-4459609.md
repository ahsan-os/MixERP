# inventory.get_purchase_discount_account_id function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.get_purchase_discount_account_id(_item_id integer)
RETURNS integer
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : get_purchase_discount_account_id
* Arguments : _item_id integer
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.get_purchase_discount_account_id(_item_id integer)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN
        purchase_discount_account_id
    FROM inventory.item_groups
    INNER JOIN inventory.items
    ON inventory.item_groups.item_group_id = inventory.items.item_group_id
    WHERE inventory.items.item_id = $1
	AND NOT inventory.item_groups.deleted;    
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

