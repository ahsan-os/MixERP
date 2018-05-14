# finance.can_post_transaction function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.can_post_transaction(_login_id bigint, _user_id integer, _office_id integer, transaction_book text, _value_date timestamp without time zone)
RETURNS boolean
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : can_post_transaction
* Arguments : _login_id bigint, _user_id integer, _office_id integer, transaction_book text, _value_date timestamp without time zone
* Owner : frapid_db_user
* Result Type : boolean
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.can_post_transaction(_login_id bigint, _user_id integer, _office_id integer, transaction_book text, _value_date timestamp without time zone)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN finance.can_post_transaction(_login_id, _user_id, _office_id, transaction_book, _value_date::date);
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

