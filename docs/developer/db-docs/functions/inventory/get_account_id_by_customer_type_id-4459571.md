# inventory.get_account_id_by_customer_type_id function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.get_account_id_by_customer_type_id(_customer_type_id integer)
RETURNS integer
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : get_account_id_by_customer_type_id
* Arguments : _customer_type_id integer
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.get_account_id_by_customer_type_id(_customer_type_id integer)
 RETURNS integer
 LANGUAGE plpgsql
 STABLE
AS $function$
BEGIN
    RETURN account_id
    FROM inventory.customer_types
    WHERE customer_type_id=_customer_type_id;
END;
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

