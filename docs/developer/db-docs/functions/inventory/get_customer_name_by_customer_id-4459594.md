# inventory.get_customer_name_by_customer_id function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.get_customer_name_by_customer_id(_customer_id integer)
RETURNS character varying
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : get_customer_name_by_customer_id
* Arguments : _customer_id integer
* Owner : frapid_db_user
* Result Type : character varying
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.get_customer_name_by_customer_id(_customer_id integer)
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN inventory.customers.customer_name
    FROM inventory.customers
    WHERE inventory.customers.customer_id = _customer_id;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

