# inventory.get_customer_transaction_summary function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.get_customer_transaction_summary(office_id integer, customer_id integer)
RETURNS TABLE(currency_code character varying, currency_symbol character varying, total_due_amount numeric, office_due_amount numeric)
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : get_customer_transaction_summary
* Arguments : office_id integer, customer_id integer
* Owner : frapid_db_user
* Result Type : TABLE(currency_code character varying, currency_symbol character varying, total_due_amount numeric, office_due_amount numeric)
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.get_customer_transaction_summary(office_id integer, customer_id integer)
 RETURNS TABLE(currency_code character varying, currency_symbol character varying, total_due_amount numeric, office_due_amount numeric)
 LANGUAGE plpgsql
AS $function$
    DECLARE root_office_id      integer = 0;
    DECLARE _currency_code      national character varying(12); 
    DECLARE _currency_symbol    national character varying(12);
    DECLARE _total_due_amount   decimal(30, 6); 
    DECLARE _office_due_amount  decimal(30, 6); 
    DECLARE _last_receipt_date  date;
    DECLARE _transaction_value  decimal(30, 6);
BEGIN
    _currency_code := inventory.get_currency_code_by_customer_id(customer_id);

    SELECT core.currencies.currency_symbol 
    INTO _currency_symbol
    FROM core.currencies
    WHERE core.currencies.currency_code = _currency_code;

    SELECT core.offices.office_id INTO root_office_id
    FROM core.offices
    WHERE parent_office_id IS NULL;



    _total_due_amount := inventory.get_total_customer_due(root_office_id, customer_id);
    _office_due_amount := inventory.get_total_customer_due(office_id, customer_id);

    RETURN QUERY
    SELECT _currency_code, _currency_symbol, _total_due_amount, _office_due_amount;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

