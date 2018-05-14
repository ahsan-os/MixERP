# finance.get_net_profit function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.get_net_profit(_date_from date, _date_to date, _office_id integer, _factor integer, _no_provison boolean DEFAULT false)
RETURNS numeric
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : get_net_profit
* Arguments : _date_from date, _date_to date, _office_id integer, _factor integer, _no_provison boolean DEFAULT false
* Owner : frapid_db_user
* Result Type : numeric
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.get_net_profit(_date_from date, _date_to date, _office_id integer, _factor integer, _no_provison boolean DEFAULT false)
 RETURNS numeric
 LANGUAGE plpgsql
AS $function$
    DECLARE _incomes                numeric(30, 6) = 0;
    DECLARE _expenses               numeric(30, 6) = 0;
    DECLARE _profit_before_tax      numeric(30, 6) = 0;
    DECLARE _tax_paid               numeric(30, 6) = 0;
    DECLARE _tax_provison           numeric(30, 6) = 0;
BEGIN
    SELECT SUM(CASE tran_type WHEN 'Cr' THEN amount_in_local_currency ELSE amount_in_local_currency * -1 END)
    INTO _incomes
    FROM finance.verified_transaction_mat_view
    WHERE value_date >= _date_from AND value_date <= _date_to
    AND office_id IN (SELECT * FROM core.get_office_ids(_office_id))
    AND account_master_id >=20100
    AND account_master_id <= 20300;
    
    SELECT SUM(CASE tran_type WHEN 'Dr' THEN amount_in_local_currency ELSE amount_in_local_currency * -1 END)
    INTO _expenses
    FROM finance.verified_transaction_mat_view
    WHERE value_date >= _date_from AND value_date <= _date_to
    AND office_id IN (SELECT * FROM core.get_office_ids(_office_id))
    AND account_master_id >=20400
    AND account_master_id <= 20701;
    
    SELECT SUM(CASE tran_type WHEN 'Dr' THEN amount_in_local_currency ELSE amount_in_local_currency * -1 END)
    INTO _tax_paid
    FROM finance.verified_transaction_mat_view
    WHERE value_date >= _date_from AND value_date <= _date_to
    AND office_id IN (SELECT * FROM core.get_office_ids(_office_id))
    AND account_master_id =20800;
    
    _profit_before_tax := COALESCE(_incomes, 0) - COALESCE(_expenses, 0);

    IF(_no_provison) THEN
        RETURN (_profit_before_tax - COALESCE(_tax_paid, 0)) / _factor;
    END IF;
    
    _tax_provison      := finance.get_income_tax_provison_amount(_office_id, _profit_before_tax, COALESCE(_tax_paid, 0));
    
    RETURN (_profit_before_tax - (COALESCE(_tax_provison, 0) + COALESCE(_tax_paid, 0))) / _factor;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

