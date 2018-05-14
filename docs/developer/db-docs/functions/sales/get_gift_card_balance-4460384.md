# sales.get_gift_card_balance function:

```plpgsql
CREATE OR REPLACE FUNCTION sales.get_gift_card_balance(_gift_card_id integer, _value_date date)
RETURNS numeric
```
* Schema : [sales](../../schemas/sales.md)
* Function Name : get_gift_card_balance
* Arguments : _gift_card_id integer, _value_date date
* Owner : frapid_db_user
* Result Type : numeric
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION sales.get_gift_card_balance(_gift_card_id integer, _value_date date)
 RETURNS numeric
 LANGUAGE plpgsql
AS $function$
    DECLARE _debit          numeric(30, 6);
    DECLARE _credit         numeric(30, 6);
BEGIN
    SELECT SUM(COALESCE(sales.gift_card_transactions.amount, 0))
    INTO _debit
    FROM sales.gift_card_transactions
    INNER JOIN finance.transaction_master
    ON finance.transaction_master.transaction_master_id = sales.gift_card_transactions.transaction_master_id
    WHERE finance.transaction_master.verification_status_id > 0
    AND sales.gift_card_transactions.transaction_type = 'Dr'
    AND finance.transaction_master.value_date <= _value_date;

    SELECT SUM(COALESCE(sales.gift_card_transactions.amount, 0))
    INTO _credit
    FROM sales.gift_card_transactions
    INNER JOIN finance.transaction_master
    ON finance.transaction_master.transaction_master_id = sales.gift_card_transactions.transaction_master_id
    WHERE finance.transaction_master.verification_status_id > 0
    AND sales.gift_card_transactions.transaction_type = 'Cr'
    AND finance.transaction_master.value_date <= _value_date;

    --Gift cards are account payables
    RETURN COALESCE(_credit, 0) - COALESCE(_debit, 0);
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

