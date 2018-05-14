# finance.auto_verify function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.auto_verify(_tran_id bigint, _office_id integer)
RETURNS void
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : auto_verify
* Arguments : _tran_id bigint, _office_id integer
* Owner : frapid_db_user
* Result Type : void
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.auto_verify(_tran_id bigint, _office_id integer)
 RETURNS void
 LANGUAGE plpgsql
AS $function$
    DECLARE _transaction_master_id          bigint;
    DECLARE _transaction_posted_by          integer;
    DECLARE _verifier                       integer;
    DECLARE _status                         integer = 1;
    DECLARE _reason                         national character varying(128) = 'Automatically verified';
    DECLARE _rejected                       smallint=-3;
    DECLARE _closed                         smallint=-2;
    DECLARE _withdrawn                      smallint=-1;
    DECLARE _unapproved                     smallint = 0;
    DECLARE _auto_approved                  smallint = 1;
    DECLARE _approved                       smallint=2;
    DECLARE _book                           text;
    DECLARE _verification_limit             public.money_strict2;
    DECLARE _posted_amount                  public.money_strict2;
    DECLARE _has_policy                     boolean=false;
    DECLARE _voucher_date                   date;
BEGIN
    _transaction_master_id := $1;

    SELECT
        finance.transaction_master.book,
        finance.transaction_master.value_date,
        finance.transaction_master.user_id
    INTO
        _book,
        _voucher_date,
        _transaction_posted_by  
    FROM finance.transaction_master
    WHERE finance.transaction_master.transaction_master_id=_transaction_master_id
	AND NOT finance.transaction_master.deleted;
    
    SELECT
        SUM(amount_in_local_currency)
    INTO
        _posted_amount
    FROM
        finance.transaction_details
    WHERE finance.transaction_details.transaction_master_id = _transaction_master_id
    AND finance.transaction_details.tran_type='Cr';


    SELECT
        true,
        verification_limit
    INTO
        _has_policy,
        _verification_limit
    FROM finance.auto_verification_policy
    WHERE finance.auto_verification_policy.user_id=_transaction_posted_by
    AND finance.auto_verification_policy.office_id = _office_id
    AND finance.auto_verification_policy.is_active=true
    AND now() >= effective_from
    AND now() <= ends_on
	AND NOT finance.auto_verification_policy.deleted;

    IF(_has_policy=true) THEN
        UPDATE finance.transaction_master
        SET 
            last_verified_on = now(),
            verified_by_user_id=_verifier,
            verification_status_id=_status,
            verification_reason=_reason
        WHERE
            finance.transaction_master.transaction_master_id=_transaction_master_id
        OR
            finance.transaction_master.cascading_tran_id=_transaction_master_id
        OR
        finance.transaction_master.transaction_master_id = 
        (
            SELECT cascading_tran_id
            FROM finance.transaction_master
            WHERE finance.transaction_master.transaction_master_id=_transaction_master_id 
        );
    ELSE
        RAISE NOTICE 'No auto verification policy found for this user.';
    END IF;
    RETURN;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

