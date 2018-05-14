# inventory.get_account_id_by_customer_id function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.get_account_id_by_customer_id(_customer_id integer)
RETURNS integer
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : get_account_id_by_customer_id
* Arguments : _customer_id integer
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.get_account_id_by_customer_id(_customer_id integer)
 RETURNS integer
 LANGUAGE plpgsql
 STABLE
AS $function$
BEGIN
    RETURN inventory.customers.account_id
    FROM inventory.customers
    WHERE inventory.customers.customer_id=_customer_id
    AND NOT inventory.customers.deleted;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

