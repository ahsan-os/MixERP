# finance.reconcile_account function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.reconcile_account(_transaction_detail_id bigint, _user_id integer, _new_book_date date, _reconciliation_memo text)
RETURNS void
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : reconcile_account
* Arguments : _transaction_detail_id bigint, _user_id integer, _new_book_date date, _reconciliation_memo text
* Owner : frapid_db_user
* Result Type : void
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.reconcile_account(_transaction_detail_id bigint, _user_id integer, _new_book_date date, _reconciliation_memo text)
 RETURNS void
 LANGUAGE plpgsql
AS $function$
    DECLARE _transaction_master_id      bigint;
BEGIN
    SELECT finance.transaction_details.transaction_master_id 
    INTO _transaction_master_id
    FROM finance.transaction_details
    WHERE finance.transaction_details.transaction_detail_id = _transaction_detail_id;

    UPDATE finance.transaction_master
    SET 
        book_date               = _new_book_date,
        audit_user_id           = _user_id,
        audit_ts                = NOW()
    WHERE finance.transaction_master.transaction_master_id = _transaction_master_id;


    UPDATE finance.transaction_details
    SET
        book_date               = _new_book_date,
        reconciliation_memo     = _reconciliation_memo,
        audit_user_id           = _user_id,
        audit_ts                = NOW()
    WHERE finance.transaction_details.transaction_master_id = _transaction_master_id;    
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

