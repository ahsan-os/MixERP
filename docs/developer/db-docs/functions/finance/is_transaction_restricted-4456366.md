# finance.is_transaction_restricted function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.is_transaction_restricted(_office_id integer)
RETURNS boolean
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : is_transaction_restricted
* Arguments : _office_id integer
* Owner : frapid_db_user
* Result Type : boolean
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.is_transaction_restricted(_office_id integer)
 RETURNS boolean
 LANGUAGE plpgsql
 STABLE
AS $function$
BEGIN
    RETURN NOT allow_transaction_posting
    FROM core.offices
    WHERE office_id=$1;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

