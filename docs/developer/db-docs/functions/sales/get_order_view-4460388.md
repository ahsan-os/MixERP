# sales.get_order_view function:

```plpgsql
CREATE OR REPLACE FUNCTION sales.get_order_view(_user_id integer, _office_id integer, _customer character varying, _from date, _to date, _expected_from date, _expected_to date, _id bigint, _reference_number character varying, _internal_memo character varying, _terms character varying, _posted_by character varying, _office character varying)
RETURNS TABLE(id bigint, customer character varying, value_date date, expected_date date, reference_number character varying, terms character varying, internal_memo character varying, posted_by character varying, office character varying, transaction_ts timestamp with time zone)
```
* Schema : [sales](../../schemas/sales.md)
* Function Name : get_order_view
* Arguments : _user_id integer, _office_id integer, _customer character varying, _from date, _to date, _expected_from date, _expected_to date, _id bigint, _reference_number character varying, _internal_memo character varying, _terms character varying, _posted_by character varying, _office character varying
* Owner : frapid_db_user
* Result Type : TABLE(id bigint, customer character varying, value_date date, expected_date date, reference_number character varying, terms character varying, internal_memo character varying, posted_by character varying, office character varying, transaction_ts timestamp with time zone)
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION sales.get_order_view(_user_id integer, _office_id integer, _customer character varying, _from date, _to date, _expected_from date, _expected_to date, _id bigint, _reference_number character varying, _internal_memo character varying, _terms character varying, _posted_by character varying, _office character varying)
 RETURNS TABLE(id bigint, customer character varying, value_date date, expected_date date, reference_number character varying, terms character varying, internal_memo character varying, posted_by character varying, office character varying, transaction_ts timestamp with time zone)
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN QUERY
    WITH RECURSIVE office_cte(office_id) AS 
    (
        SELECT _office_id
        UNION ALL
        SELECT
            c.office_id
        FROM 
        office_cte AS p, 
        core.offices AS c 
        WHERE 
        parent_office_id = p.office_id
    )

    SELECT 
        sales.orders.order_id,
        inventory.get_customer_name_by_customer_id(sales.orders.customer_id),
        sales.orders.value_date,
        sales.orders.expected_delivery_date,
        sales.orders.reference_number,
        sales.orders.terms,
        sales.orders.internal_memo,
        account.get_name_by_user_id(sales.orders.user_id)::national character varying(500) AS posted_by,
        core.get_office_name_by_office_id(office_id)::national character varying(500) AS office,
        sales.orders.transaction_timestamp
    FROM sales.orders
    WHERE 1 = 1
    AND sales.orders.value_date BETWEEN _from AND _to
    AND sales.orders.expected_delivery_date BETWEEN _expected_from AND _expected_to
    AND sales.orders.office_id IN (SELECT office_id FROM office_cte)
    AND (COALESCE(_id, 0) = 0 OR _id = sales.orders.order_id)
    AND COALESCE(LOWER(sales.orders.reference_number), '') LIKE '%' || LOWER(_reference_number) || '%' 
    AND COALESCE(LOWER(sales.orders.internal_memo), '') LIKE '%' || LOWER(_internal_memo) || '%' 
    AND COALESCE(LOWER(sales.orders.terms), '') LIKE '%' || LOWER(_terms) || '%' 
    AND LOWER(inventory.get_customer_name_by_customer_id(sales.orders.customer_id)) LIKE '%' || LOWER(_customer) || '%' 
    AND LOWER(account.get_name_by_user_id(sales.orders.user_id)) LIKE '%' || LOWER(_posted_by) || '%' 
    AND LOWER(core.get_office_name_by_office_id(sales.orders.office_id)) LIKE '%' || LOWER(_office) || '%' 
    AND NOT sales.orders.deleted;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

