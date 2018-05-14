# inventory.get_customer_id_by_customer_code function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.get_customer_id_by_customer_code(text)
RETURNS integer
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : get_customer_id_by_customer_code
* Arguments : text
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.get_customer_id_by_customer_code(text)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
BEGIN
        RETURN customer_id
        FROM inventory.customers
        WHERE inventory.customers.customer_code=$1
		AND NOT inventory.customers.deleted;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

