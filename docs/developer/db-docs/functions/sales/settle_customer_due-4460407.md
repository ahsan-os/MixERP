# sales.settle_customer_due function:

```plpgsql
CREATE OR REPLACE FUNCTION sales.settle_customer_due(_customer_id integer, _office_id integer)
RETURNS void
```
* Schema : [sales](../../schemas/sales.md)
* Function Name : settle_customer_due
* Arguments : _customer_id integer, _office_id integer
* Owner : frapid_db_user
* Result Type : void
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION sales.settle_customer_due(_customer_id integer, _office_id integer)
 RETURNS void
 LANGUAGE plpgsql
 STRICT
AS $function$
    DECLARE _settled_transactions           bigint[];
    DECLARE _settling_amount                numeric(30, 6);
    DECLARE _closing_balance                numeric(30, 6);
    DECLARE _total_sales                    numeric(30, 6);
    DECLARE _customer_account_id            integer = inventory.get_account_id_by_customer_id(_customer_id);
BEGIN   
    --Closing balance of the customer
    SELECT
        SUM
        (
            CASE WHEN tran_type = 'Cr' 
            THEN amount_in_local_currency 
            ELSE amount_in_local_currency  * -1 
            END
        ) INTO _closing_balance
    FROM finance.transaction_details
    INNER JOIN finance.transaction_master
    ON finance.transaction_master.transaction_master_id = finance.transaction_details.transaction_master_id
    WHERE finance.transaction_master.verification_status_id > 0
    AND NOT finance.transaction_master.deleted
    AND finance.transaction_master.office_id = _office_id
    AND finance.transaction_details.account_id = _customer_account_id;


    --Since customer account is receivable, change the balance to debit
    _closing_balance := _closing_balance * -1;

    --Sum of total sales amount
    SELECT 
        SUM
        (
            (inventory.checkout_details.quantity * inventory.checkout_details.price) 
            - 
            inventory.checkout_details.discount 
            + 
            inventory.checkout_details.tax
            + 
            inventory.checkout_details.shipping_charge
        ) INTO _total_sales
    FROM inventory.checkouts
    INNER JOIN sales.sales
    ON sales.sales.checkout_id = inventory.checkouts.checkout_id
    INNER JOIN inventory.checkout_details
    ON inventory.checkouts.checkout_id = inventory.checkout_details.checkout_id
    INNER JOIN finance.transaction_master
    ON inventory.checkouts.transaction_master_id = finance.transaction_master.transaction_master_id
    WHERE finance.transaction_master.verification_status_id > 0
    AND finance.transaction_master.office_id = _office_id
    AND sales.sales.customer_id = _customer_id;


    _settling_amount := _total_sales - _closing_balance;

    WITH all_sales
    AS
    (
        SELECT 
            inventory.checkouts.transaction_master_id,
            SUM
            (
                (inventory.checkout_details.quantity * inventory.checkout_details.price) 
                - 
                inventory.checkout_details.discount 
                + 
                inventory.checkout_details.tax
                + 
                inventory.checkout_details.shipping_charge
            ) as due
        FROM inventory.checkouts
        INNER JOIN sales.sales
        ON sales.sales.checkout_id = inventory.checkouts.checkout_id
        INNER JOIN inventory.checkout_details
        ON inventory.checkouts.checkout_id = inventory.checkout_details.checkout_id
        INNER JOIN finance.transaction_master
        ON inventory.checkouts.transaction_master_id = finance.transaction_master.transaction_master_id
        WHERE finance.transaction_master.book = ANY(ARRAY['Sales.Direct', 'Sales.Delivery'])
        AND finance.transaction_master.office_id = _office_id
        AND finance.transaction_master.verification_status_id > 0      --Approved
        AND sales.sales.customer_id = _customer_id                     --of this customer
        GROUP BY inventory.checkouts.transaction_master_id
    ),
    sales_summary
    AS
    (
        SELECT 
            transaction_master_id, 
            due, 
            SUM(due) OVER(ORDER BY transaction_master_id) AS cumulative_due
        FROM all_sales
    )

    SELECT 
        ARRAY_AGG(transaction_master_id) INTO _settled_transactions
    FROM sales_summary
    WHERE cumulative_due <= _settling_amount;

    UPDATE sales.sales
    SET credit_settled = true
    WHERE transaction_master_id = ANY(_settled_transactions);
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

