# finance.get_cash_repository_balance function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.get_cash_repository_balance(_cash_repository_id integer, _currency_code character varying)
RETURNS money_strict2
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : get_cash_repository_balance
* Arguments : _cash_repository_id integer, _currency_code character varying
* Owner : frapid_db_user
* Result Type : money_strict2
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.get_cash_repository_balance(_cash_repository_id integer, _currency_code character varying)
 RETURNS money_strict2
 LANGUAGE plpgsql
AS $function$
    DECLARE _debit public.money_strict2;
    DECLARE _credit public.money_strict2;
BEGIN
    SELECT COALESCE(SUM(amount_in_currency), 0::public.money_strict2) INTO _debit
    FROM finance.verified_transaction_view
    WHERE cash_repository_id=$1
    AND currency_code=$2
    AND tran_type='Dr';

    SELECT COALESCE(SUM(amount_in_currency), 0::public.money_strict2) INTO _credit
    FROM finance.verified_transaction_view
    WHERE cash_repository_id=$1
    AND currency_code=$2
    AND tran_type='Cr';

    RETURN _debit - _credit;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

