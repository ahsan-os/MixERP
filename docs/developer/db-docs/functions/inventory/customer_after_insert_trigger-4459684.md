# inventory.customer_after_insert_trigger function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.customer_after_insert_trigger()
RETURNS trigger
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : customer_after_insert_trigger
* Arguments : 
* Owner : frapid_db_user
* Result Type : trigger
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.customer_after_insert_trigger()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
    DECLARE _parent_account_id          integer;
    DECLARE _customer_code              text;
    DECLARE _account_id                 integer;
BEGIN    
    _parent_account_id      := inventory.get_account_id_by_customer_type_id(NEW.customer_type_id);

    IF(COALESCE(NEW.customer_name, '') = '') THEN
		RAISE EXCEPTION 'The customer name cannot be left empty.';
    END IF;

    --Create a new account
    IF(NEW.account_id IS NULL) THEN
        INSERT INTO finance.accounts(account_master_id, account_number, currency_code, account_name, parent_account_id)
        SELECT finance.get_account_master_id_by_account_id(_parent_account_id), '15010-' || NEW.customer_id, NEW.currency_code, NEW.customer_name, _parent_account_id
        RETURNING account_id INTO _account_id;
    
        UPDATE inventory.customers
        SET 
            account_id=_account_id
        WHERE inventory.customers.customer_id = NEW.customer_id;

        RETURN NEW;
    END IF;

    RETURN NEW;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

