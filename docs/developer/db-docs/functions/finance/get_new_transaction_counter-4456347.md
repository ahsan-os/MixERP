# finance.get_new_transaction_counter function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.get_new_transaction_counter(date)
RETURNS integer
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : get_new_transaction_counter
* Arguments : date
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.get_new_transaction_counter(date)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
    DECLARE _ret_val integer;
BEGIN
    SELECT INTO _ret_val
        COALESCE(MAX(transaction_counter),0)
    FROM finance.transaction_master
    WHERE finance.transaction_master.value_date=$1
	AND NOT finance.transaction_master.deleted;

    IF _ret_val IS NULL THEN
        RETURN 1::integer;
    ELSE
        RETURN (_ret_val + 1)::integer;
    END IF;
END;
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

