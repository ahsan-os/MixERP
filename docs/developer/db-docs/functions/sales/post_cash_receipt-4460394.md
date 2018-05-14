# sales.post_cash_receipt function:

```plpgsql
CREATE OR REPLACE FUNCTION sales.post_cash_receipt(_user_id integer, _office_id integer, _login_id bigint, _customer_id integer, _customer_account_id integer, _currency_code character varying, _local_currency_code character varying, _base_currency_code character varying, _exchange_rate_debit decimal_strict, _exchange_rate_credit decimal_strict, _reference_number character varying, _statement_reference character varying, _cost_center_id integer, _cash_account_id integer, _cash_repository_id integer, _value_date date, _book_date date, _receivable money_strict2, _tender money_strict2, _change money_strict2, _cascading_tran_id bigint)
RETURNS bigint
```
* Schema : [sales](../../schemas/sales.md)
* Function Name : post_cash_receipt
* Arguments : _user_id integer, _office_id integer, _login_id bigint, _customer_id integer, _customer_account_id integer, _currency_code character varying, _local_currency_code character varying, _base_currency_code character varying, _exchange_rate_debit decimal_strict, _exchange_rate_credit decimal_strict, _reference_number character varying, _statement_reference character varying, _cost_center_id integer, _cash_account_id integer, _cash_repository_id integer, _value_date date, _book_date date, _receivable money_strict2, _tender money_strict2, _change money_strict2, _cascading_tran_id bigint
* Owner : frapid_db_user
* Result Type : bigint
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION sales.post_cash_receipt(_user_id integer, _office_id integer, _login_id bigint, _customer_id integer, _customer_account_id integer, _currency_code character varying, _local_currency_code character varying, _base_currency_code character varying, _exchange_rate_debit decimal_strict, _exchange_rate_credit decimal_strict, _reference_number character varying, _statement_reference character varying, _cost_center_id integer, _cash_account_id integer, _cash_repository_id integer, _value_date date, _book_date date, _receivable money_strict2, _tender money_strict2, _change money_strict2, _cascading_tran_id bigint)
 RETURNS bigint
 LANGUAGE plpgsql
AS $function$
    DECLARE _book                               text = 'Sales Receipt';
    DECLARE _transaction_master_id              bigint;
    DECLARE _debit                              public.money_strict2;
    DECLARE _credit                             public.money_strict2;
    DECLARE _lc_debit                           public.money_strict2;
    DECLARE _lc_credit                          public.money_strict2;
BEGIN
    IF NOT finance.can_post_transaction(_login_id, _user_id, _office_id, _book, _value_date) THEN
        RETURN 0;
    END IF;

    IF(_tender < _receivable) THEN
        RAISE EXCEPTION 'The tendered amount must be greater than or equal to sales amount';
    END IF;
    
    _debit                                  := _receivable;
    _lc_debit                               := _receivable * _exchange_rate_debit;

    _credit                                 := _receivable * (_exchange_rate_debit/ _exchange_rate_credit);
    _lc_credit                              := _receivable * _exchange_rate_debit;
    
    INSERT INTO finance.transaction_master
    (
        transaction_master_id, 
        transaction_counter, 
        transaction_code, 
        book, 
        value_date, 
        book_date,
        user_id, 
        login_id, 
        office_id, 
        cost_center_id, 
        reference_number, 
        statement_reference,
        audit_user_id,
        cascading_tran_id
    )
    SELECT 
        nextval(pg_get_serial_sequence('finance.transaction_master', 'transaction_master_id')), 
        finance.get_new_transaction_counter(_value_date), 
        finance.get_transaction_code(_value_date, _office_id, _user_id, _login_id),
        _book,
        _value_date,
        _book_date,
        _user_id,
        _login_id,
        _office_id,
        _cost_center_id,
        _reference_number,
        _statement_reference,
        _user_id,
        _cascading_tran_id;


    _transaction_master_id := currval(pg_get_serial_sequence('finance.transaction_master', 'transaction_master_id'));

    --Debit
    INSERT INTO finance.transaction_details(transaction_master_id, office_id, value_date, book_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id)
    SELECT _transaction_master_id, _office_id, _value_date, _book_date, 'Dr', _cash_account_id, _statement_reference, _cash_repository_id, _currency_code, _debit, _local_currency_code, _exchange_rate_debit, _lc_debit, _user_id;

    --Credit
    INSERT INTO finance.transaction_details(transaction_master_id, office_id, value_date,  book_date, tran_type, account_id, statement_reference, cash_repository_id, currency_code, amount_in_currency, local_currency_code, er, amount_in_local_currency, audit_user_id)
    SELECT _transaction_master_id, _office_id, _value_date, _book_date, 'Cr', _customer_account_id, _statement_reference, NULL, _base_currency_code, _credit, _local_currency_code, _exchange_rate_credit, _lc_credit, _user_id;
    
    
    INSERT INTO sales.customer_receipts(transaction_master_id, customer_id, currency_code, er_debit, er_credit, cash_repository_id, posted_date, tender, change)
    SELECT _transaction_master_id, _customer_id, _currency_code, _exchange_rate_debit, _exchange_rate_credit, _cash_repository_id, _value_date, _tender, _change;

    RETURN _transaction_master_id;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

