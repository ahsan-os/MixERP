# sales.post_late_fee function:

```plpgsql
CREATE OR REPLACE FUNCTION sales.post_late_fee(_user_id integer, _login_id bigint, _office_id integer, _value_date date)
RETURNS void
```
* Schema : [sales](../../schemas/sales.md)
* Function Name : post_late_fee
* Arguments : _user_id integer, _login_id bigint, _office_id integer, _value_date date
* Owner : frapid_db_user
* Result Type : void
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION sales.post_late_fee(_user_id integer, _login_id bigint, _office_id integer, _value_date date)
 RETURNS void
 LANGUAGE plpgsql
AS $function$
    DECLARE this                        RECORD;
    DECLARE _transaction_master_id      bigint;
    DECLARE _tran_counter               integer;
    DECLARE _transaction_code           text;
    DECLARE _default_currency_code      national character varying(12);
    DECLARE _book_name                  national character varying(100) = 'Late Fee';
BEGIN
    DROP TABLE IF EXISTS temp_late_fee;

    CREATE TEMPORARY TABLE temp_late_fee
    (
        transaction_master_id           bigint,
        value_date                      date,
        payment_term_id                 integer,
        payment_term_code               text,
        payment_term_name               text,        
        due_on_date                     boolean,
        due_days                        integer,
        due_frequency_id                integer,
        grace_period                    integer,
        late_fee_id                     integer,
        late_fee_posting_frequency_id   integer,
        late_fee_code                   text,
        late_fee_name                   text,
        is_flat_amount                  boolean,
        rate                            numeric(30, 6),
        due_amount                      public.money_strict2,
        late_fee                        public.money_strict2,
        customer_id                     integer,
        customer_account_id             integer,
        late_fee_account_id             integer,
        due_date                        date
    ) ON COMMIT DROP;

    WITH unpaid_invoices
    AS
    (
        SELECT 
             finance.transaction_master.transaction_master_id, 
             finance.transaction_master.value_date,
             sales.sales.payment_term_id,
             sales.payment_terms.payment_term_code,
             sales.payment_terms.payment_term_name,
             sales.payment_terms.due_on_date,
             sales.payment_terms.due_days,
             sales.payment_terms.due_frequency_id,
             sales.payment_terms.grace_period,
             sales.payment_terms.late_fee_id,
             sales.payment_terms.late_fee_posting_frequency_id,
             sales.late_fee.late_fee_code,
             sales.late_fee.late_fee_name,
             sales.late_fee.is_flat_amount,
             sales.late_fee.rate,
            0.00 as due_amount,
            0.00 as late_fee,
             sales.sales.customer_id,
            inventory.get_account_id_by_customer_id(sales.sales.customer_id) AS customer_account_id,
             sales.late_fee.account_id AS late_fee_account_id
        FROM  inventory.checkouts
        INNER JOIN sales.sales
        ON sales.sales.checkout_id = inventory.checkouts.checkout_id
        INNER JOIN  finance.transaction_master
        ON  finance.transaction_master.transaction_master_id =  inventory.checkouts.transaction_master_id
        INNER JOIN  sales.payment_terms
        ON  sales.payment_terms.payment_term_id =  sales.sales.payment_term_id
        INNER JOIN  sales.late_fee
        ON  sales.payment_terms.late_fee_id =  sales.late_fee.late_fee_id
        WHERE  finance.transaction_master.verification_status_id > 0
        AND  finance.transaction_master.book = ANY(ARRAY['Sales.Delivery', 'Sales.Direct'])
        AND  sales.sales.is_credit AND NOT  sales.sales.credit_settled
        AND  sales.sales.payment_term_id IS NOT NULL
        AND  sales.payment_terms.late_fee_id IS NOT NULL
        AND  finance.transaction_master.transaction_master_id NOT IN
        (
            SELECT  sales.late_fee_postings.transaction_master_id        --We have already posted the late fee before.
            FROM  sales.late_fee_postings
        )
    ), 
    unpaid_invoices_details
    AS
    (
        SELECT *, 
        CASE WHEN unpaid_invoices.due_on_date
        THEN unpaid_invoices.value_date + unpaid_invoices.due_days + unpaid_invoices.grace_period
        ELSE finance.get_frequency_end_date(unpaid_invoices.due_frequency_id, unpaid_invoices.value_date) +  unpaid_invoices.grace_period END as due_date
        FROM unpaid_invoices
    )


    INSERT INTO temp_late_fee
    SELECT * FROM unpaid_invoices_details
    WHERE unpaid_invoices_details.due_date <= _value_date;


    UPDATE temp_late_fee
    SET due_amount = 
    (
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
            )
        FROM inventory.checkout_details
        INNER JOIN  inventory.checkouts
        ON  inventory.checkouts. checkout_id = inventory.checkout_details. checkout_id
        WHERE  inventory.checkouts.transaction_master_id = temp_late_fee.transaction_master_id
    ) WHERE NOT temp_late_fee.is_flat_amount;

    UPDATE temp_late_fee
    SET late_fee = temp_late_fee.rate
    WHERE temp_late_fee.is_flat_amount;

    UPDATE temp_late_fee
    SET late_fee = temp_late_fee.due_amount * temp_late_fee.rate / 100
    WHERE NOT temp_late_fee.is_flat_amount;

    _default_currency_code                  :=  core.get_currency_code_by_office_id(_office_id);

    FOR this IN
    SELECT * FROM temp_late_fee
    WHERE temp_late_fee.late_fee > 0
    AND temp_late_fee.customer_account_id IS NOT NULL
    AND temp_late_fee.late_fee_account_id IS NOT NULL
    LOOP
        _transaction_master_id  := nextval(pg_get_serial_sequence(' finance.transaction_master', 'transaction_master_id'));
        _tran_counter           :=  finance.get_new_transaction_counter(_value_date);
        _transaction_code       :=  finance.get_transaction_code(_value_date, _office_id, _user_id, _login_id);

        INSERT INTO  finance.transaction_master
        (
            transaction_master_id, 
            transaction_counter, 
            transaction_code, 
            book, 
            value_date, 
            user_id, 
            office_id, 
            reference_number,
            statement_reference,
            verification_status_id,
            verified_by_user_id,
            verification_reason
        ) 
        SELECT            
            _transaction_master_id, 
            _tran_counter, 
            _transaction_code, 
            _book_name, 
            _value_date, 
            _user_id, 
            _office_id,             
            this.transaction_master_id::text AS reference_number,
            this.late_fee_name AS statement_reference,
            1,
            _user_id,
            'Automatically verified by workflow.';

        INSERT INTO  finance.transaction_details
        (
            transaction_master_id,
            value_date,
            tran_type, 
            account_id, 
            statement_reference, 
            currency_code, 
            amount_in_currency, 
            er, 
            local_currency_code, 
            amount_in_local_currency
        )
        SELECT
            _transaction_master_id,
            _value_date,
            'Cr',
            this.late_fee_account_id,
            this.late_fee_name || ' (' || core.get_customer_code_by_customer_id(this.customer_id) || ')',
            _default_currency_code, 
            this.late_fee, 
            1 AS exchange_rate,
            _default_currency_code,
            this.late_fee
        UNION ALL
        SELECT
            _transaction_master_id,
            _value_date,
            'Dr',
            this.customer_account_id,
            this.late_fee_name,
            _default_currency_code, 
            this.late_fee, 
            1 AS exchange_rate,
            _default_currency_code,
            this.late_fee;

        INSERT INTO  sales.late_fee_postings(transaction_master_id, customer_id, value_date, late_fee_tran_id, amount)
        SELECT this.transaction_master_id, this.customer_id, _value_date, _transaction_master_id, this.late_fee;
    END LOOP;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

