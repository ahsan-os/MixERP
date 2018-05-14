# finance.verify_transaction function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.verify_transaction(_transaction_master_id bigint, _office_id integer, _user_id integer, _login_id bigint, _verification_status_id smallint, _reason character varying)
RETURNS bigint
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : verify_transaction
* Arguments : _transaction_master_id bigint, _office_id integer, _user_id integer, _login_id bigint, _verification_status_id smallint, _reason character varying
* Owner : frapid_db_user
* Result Type : bigint
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.verify_transaction(_transaction_master_id bigint, _office_id integer, _user_id integer, _login_id bigint, _verification_status_id smallint, _reason character varying)
 RETURNS bigint
 LANGUAGE plpgsql
AS $function$
    DECLARE _transaction_posted_by          integer;
    DECLARE _book                           text;
    DECLARE _can_verify                     boolean;
    DECLARE _verification_limit             public.money_strict2;
    DECLARE _can_self_verify                boolean;
    DECLARE _self_verification_limit        public.money_strict2;
    DECLARE _posted_amount                  public.money_strict2;
    DECLARE _has_policy                     boolean=false;
    DECLARE _journal_date                   date;
    DECLARE _journal_office_id              integer;
    DECLARE _cascading_tran_id              bigint;
BEGIN

    SELECT
        finance.transaction_master.book,
        finance.transaction_master.value_date,
        finance.transaction_master.office_id,
        finance.transaction_master.user_id
    INTO
        _book,
        _journal_date,
        _journal_office_id,
        _transaction_posted_by  
    FROM
    finance.transaction_master
    WHERE finance.transaction_master.transaction_master_id=_transaction_master_id
	AND NOT finance.transaction_master.deleted;


    IF(_journal_office_id <> _office_id) THEN
        RAISE EXCEPTION 'Access is denied. You cannot verify a transaction of another office.'
        USING ERRCODE='P9014';
    END IF;
        
    SELECT
        SUM(amount_in_local_currency)
    INTO
        _posted_amount
    FROM finance.transaction_details
    WHERE finance.transaction_details.transaction_master_id = _transaction_master_id
    AND finance.transaction_details.tran_type='Cr';


    SELECT
        true,
        can_verify,
        verification_limit,
        can_self_verify,
        self_verification_limit
    INTO
        _has_policy,
        _can_verify,
        _verification_limit,
        _can_self_verify,
        _self_verification_limit
    FROM finance.journal_verification_policy
    WHERE finance.journal_verification_policy.user_id=_user_id
    AND finance.journal_verification_policy.office_id = _office_id
    AND finance.journal_verification_policy.is_active=true
    AND now() >= effective_from
    AND now() <= ends_on
	AND NOT finance.journal_verification_policy.deleted;

    IF(NOT _can_self_verify AND _user_id = _transaction_posted_by) THEN
        _can_verify := false;
    END IF;

    IF(_has_policy) THEN
        IF(_can_verify) THEN
        
            SELECT cascading_tran_id
            INTO _cascading_tran_id
            FROM finance.transaction_master
            WHERE finance.transaction_master.transaction_master_id=_transaction_master_id
			AND NOT finance.transaction_master.deleted;
            
            UPDATE finance.transaction_master
            SET 
                last_verified_on = now(),
                verified_by_user_id=_user_id,
                verification_status_id=_verification_status_id,
                verification_reason=_reason
            WHERE
                finance.transaction_master.transaction_master_id=_transaction_master_id
            OR 
                finance.transaction_master.cascading_tran_id =_transaction_master_id
            OR
            finance.transaction_master.transaction_master_id = _cascading_tran_id;

            RAISE NOTICE 'Done.';

            IF(COALESCE(_cascading_tran_id, 0) = 0) THEN
                SELECT transaction_master_id
                INTO _cascading_tran_id
                FROM finance.transaction_master
                WHERE finance.transaction_master.cascading_tran_id=_transaction_master_id
				AND NOT finance.transaction_master.deleted;
            END IF;
            
            RETURN COALESCE(_cascading_tran_id, 0);
        ELSE
            RAISE EXCEPTION 'Please ask someone else to verify your transaction.'
            USING ERRCODE='P4031';
        END IF;
    ELSE
        RAISE EXCEPTION 'No verification policy found for this user.'
        USING ERRCODE='P4030';
    END IF;

    RETURN 0;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

