# finance.get_retained_earnings function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.get_retained_earnings(_date_to date, _office_id integer, _factor integer)
RETURNS numeric
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : get_retained_earnings
* Arguments : _date_to date, _office_id integer, _factor integer
* Owner : frapid_db_user
* Result Type : numeric
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.get_retained_earnings(_date_to date, _office_id integer, _factor integer)
 RETURNS numeric
 LANGUAGE plpgsql
AS $function$
    DECLARE     _date_from              date;
    DECLARE     _net_profit             numeric(30, 6);
    DECLARE     _paid_dividends         numeric(30, 6);
BEGIN
    IF(COALESCE(_factor, 0) = 0) THEN
        _factor := 1;
    END IF;
    _date_from              := finance.get_fiscal_year_start_date(_office_id);    
    _net_profit             := finance.get_net_profit(_date_from, _date_to, _office_id, _factor, true);

    SELECT 
        COALESCE(SUM(CASE tran_type WHEN 'Dr' THEN amount_in_local_currency ELSE amount_in_local_currency * -1 END) / _factor, 0)
    INTO 
        _paid_dividends
    FROM finance.verified_transaction_mat_view
    WHERE value_date <=_date_to
    AND account_master_id BETWEEN 15300 AND 15400
    AND office_id IN (SELECT * FROM core.get_office_ids(_office_id));
    
    RETURN _net_profit - _paid_dividends;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

