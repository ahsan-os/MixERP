# sales.get_account_receivables_report function:

```plpgsql
CREATE OR REPLACE FUNCTION sales.get_account_receivables_report(_office_id integer, _from date)
RETURNS TABLE(office_id integer, office_name character varying, account_id integer, account_number character varying, account_name character varying, previous_period numeric, current_period numeric, total_amount numeric)
```
* Schema : [sales](../../schemas/sales.md)
* Function Name : get_account_receivables_report
* Arguments : _office_id integer, _from date
* Owner : frapid_db_user
* Result Type : TABLE(office_id integer, office_name character varying, account_id integer, account_number character varying, account_name character varying, previous_period numeric, current_period numeric, total_amount numeric)
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION sales.get_account_receivables_report(_office_id integer, _from date)
 RETURNS TABLE(office_id integer, office_name character varying, account_id integer, account_number character varying, account_name character varying, previous_period numeric, current_period numeric, total_amount numeric)
 LANGUAGE plpgsql
AS $function$
BEGIN
    DROP TABLE IF EXISTS _results;
    
    CREATE TEMPORARY TABLE _results
    (
        office_id                   integer,
        office_name                 national character varying(500),
        account_id                  integer,
        account_number              national character varying(24),
        account_name                national character varying(500),
        previous_period             numeric(30, 6),
        current_period              numeric(30, 6),
        total_amount                numeric(30, 6)
    ) ON COMMIT DROP;

    INSERT INTO _results(office_id, office_name, account_id)
    SELECT DISTINCT inventory.customers.account_id, core.get_office_name_by_office_id(_office_id), _office_id FROM inventory.customers;

    UPDATE _results
    SET
        account_number  = finance.accounts.account_number,
        account_name    = finance.accounts.account_name
    FROM finance.accounts
    WHERE finance.accounts.account_id = _results.account_id;


    UPDATE _results AS results
    SET previous_period = 
    (        
        SELECT 
            SUM
            (
                CASE WHEN finance.verified_transaction_view.tran_type = 'Dr' THEN
                finance.verified_transaction_view.amount_in_local_currency
                ELSE
                finance.verified_transaction_view.amount_in_local_currency * -1
                END                
            ) AS amount
        FROM finance.verified_transaction_view
        WHERE finance.verified_transaction_view.value_date < _from
        AND finance.verified_transaction_view.office_id IN (SELECT * FROM core.get_office_ids(_office_id))
        AND finance.verified_transaction_view.account_id IN
        (
            SELECT * FROM finance.get_account_ids(results.account_id)
        )
    );

    UPDATE _results AS results
    SET current_period = 
    (        
        SELECT 
            SUM
            (
                CASE WHEN finance.verified_transaction_view.tran_type = 'Dr' THEN
                finance.verified_transaction_view.amount_in_local_currency
                ELSE
                finance.verified_transaction_view.amount_in_local_currency * -1
                END                
            ) AS amount
        FROM finance.verified_transaction_view
        WHERE finance.verified_transaction_view.value_date >= _from
        AND finance.verified_transaction_view.office_id IN (SELECT * FROM core.get_office_ids(_office_id))
        AND finance.verified_transaction_view.account_id IN
        (
            SELECT * FROM finance.get_account_ids(results.account_id)
        )
    );

    UPDATE _results
    SET total_amount = COALESCE(_results.previous_period, 0) + COALESCE(_results.current_period, 0);
    
    RETURN QUERY
    SELECT * FROM _results;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

