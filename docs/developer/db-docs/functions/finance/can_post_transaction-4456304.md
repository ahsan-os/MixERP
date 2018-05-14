# finance.can_post_transaction function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.can_post_transaction(_login_id bigint, _user_id integer, _office_id integer, transaction_book text, _value_date date)
RETURNS boolean
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : can_post_transaction
* Arguments : _login_id bigint, _user_id integer, _office_id integer, transaction_book text, _value_date date
* Owner : frapid_db_user
* Result Type : boolean
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.can_post_transaction(_login_id bigint, _user_id integer, _office_id integer, transaction_book text, _value_date date)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
    DECLARE _eod_required                       boolean := finance.eod_required(_office_id);
    DECLARE _fiscal_year_start_date             date    := finance.get_fiscal_year_start_date(_office_id);
    DECLARE _fiscal_year_end_date               date    := finance.get_fiscal_year_end_date(_office_id);
BEGIN
    IF(account.is_valid_login_id(_login_id) = false) THEN
        RAISE EXCEPTION 'Invalid LoginId.'
        USING ERRCODE='P3101';
    END IF; 

    IF(core.is_valid_office_id(_office_id) = false) THEN
        RAISE EXCEPTION 'Invalid OfficeId.'
        USING ERRCODE='P3010';
    END IF;

    IF(finance.is_transaction_restricted(_office_id)) THEN
        RAISE EXCEPTION 'This establishment does not allow transaction posting.'
        USING ERRCODE='P5100';
    END IF;
    
    IF(_eod_required) THEN
        IF(finance.is_restricted_mode()) THEN
            RAISE EXCEPTION 'Cannot post transaction during restricted transaction mode.'
            USING ERRCODE='P5101';
        END IF;

        IF(_value_date < finance.get_value_date(_office_id)) THEN
            RAISE EXCEPTION 'Past dated transactions are not allowed.'
            USING ERRCODE='P5010';
        END IF;
    END IF;

    IF(_value_date < _fiscal_year_start_date) THEN
        RAISE EXCEPTION 'You cannot post transactions before the current fiscal year start date.'
        USING ERRCODE='P5010';
    END IF;

    IF(_value_date > _fiscal_year_end_date) THEN
        RAISE EXCEPTION 'You cannot post transactions after the current fiscal year end date.'
        USING ERRCODE='P5010';
    END IF;
    
    IF NOT EXISTS 
    (
        SELECT *
        FROM account.users
        INNER JOIN account.roles
        ON account.users.role_id = account.roles.role_id
        AND user_id = _user_id
    ) THEN
        RAISE EXCEPTION 'Access is denied. You are not authorized to post this transaction.'
        USING ERRCODE='P9010';        
    END IF;

    RETURN true;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

