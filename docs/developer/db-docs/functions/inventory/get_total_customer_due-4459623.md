# inventory.get_total_customer_due function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.get_total_customer_due(office_id integer, customer_id integer)
RETURNS numeric
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : get_total_customer_due
* Arguments : office_id integer, customer_id integer
* Owner : frapid_db_user
* Result Type : numeric
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.get_total_customer_due(office_id integer, customer_id integer)
 RETURNS numeric
 LANGUAGE plpgsql
AS $function$
    DECLARE _account_id                     integer         = inventory.get_account_id_by_customer_id($2);
    DECLARE _debit                          decimal(30, 6)  = 0;
    DECLARE _credit                         decimal(30, 6)  = 0;
    DECLARE _local_currency_code            national character varying(12) = core.get_currency_code_by_office_id($1); 
    DECLARE _base_currency_code             national character varying(12) = inventory.get_currency_code_by_customer_id($2);
    DECLARE _amount_in_local_currency       decimal(30, 6)= 0;
    DECLARE _amount_in_base_currency        decimal(30, 6)= 0;
    DECLARE _er decimal_strict2 = 0;
BEGIN

    SELECT SUM(amount_in_local_currency)
    INTO _debit
    FROM finance.verified_transaction_view
    WHERE finance.verified_transaction_view.account_id IN (SELECT * FROM finance.get_account_ids(_account_id))
    AND finance.verified_transaction_view.office_id IN (SELECT * FROM core.get_office_ids($1))
    AND tran_type='Dr';

    SELECT SUM(amount_in_local_currency)
    INTO _credit
    FROM finance.verified_transaction_view
    WHERE finance.verified_transaction_view.account_id IN (SELECT * FROM finance.get_account_ids(_account_id))
    AND finance.verified_transaction_view.office_id IN (SELECT * FROM core.get_office_ids($1))
    AND tran_type='Cr';

    _er := COALESCE(finance.convert_exchange_rate($1, _local_currency_code, _base_currency_code), 0);


    IF(_er = 0) THEN
        RAISE EXCEPTION 'Exchange rate between % and % was not found.', _local_currency_code, _base_currency_code
        USING ERRCODE='P4010';
    END IF;


    _amount_in_local_currency = COALESCE(_debit, 0) - COALESCE(_credit, 0);


    _amount_in_base_currency = _amount_in_local_currency * _er; 

    RETURN _amount_in_base_currency;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

