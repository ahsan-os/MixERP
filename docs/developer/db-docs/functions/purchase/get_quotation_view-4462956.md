# purchase.get_quotation_view function:

```plpgsql
CREATE OR REPLACE FUNCTION purchase.get_quotation_view(_user_id integer, _office_id integer, _supplier character varying, _from date, _to date, _expected_from date, _expected_to date, _id bigint, _reference_number character varying, _internal_memo character varying, _terms character varying, _posted_by character varying, _office character varying)
RETURNS TABLE(id bigint, supplier character varying, value_date date, expected_date date, reference_number character varying, terms character varying, internal_memo character varying, posted_by character varying, office character varying, transaction_ts timestamp with time zone)
```
* Schema : [purchase](../../schemas/purchase.md)
* Function Name : get_quotation_view
* Arguments : _user_id integer, _office_id integer, _supplier character varying, _from date, _to date, _expected_from date, _expected_to date, _id bigint, _reference_number character varying, _internal_memo character varying, _terms character varying, _posted_by character varying, _office character varying
* Owner : frapid_db_user
* Result Type : TABLE(id bigint, supplier character varying, value_date date, expected_date date, reference_number character varying, terms character varying, internal_memo character varying, posted_by character varying, office character varying, transaction_ts timestamp with time zone)
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION purchase.get_quotation_view(_user_id integer, _office_id integer, _supplier character varying, _from date, _to date, _expected_from date, _expected_to date, _id bigint, _reference_number character varying, _internal_memo character varying, _terms character varying, _posted_by character varying, _office character varying)
 RETURNS TABLE(id bigint, supplier character varying, value_date date, expected_date date, reference_number character varying, terms character varying, internal_memo character varying, posted_by character varying, office character varying, transaction_ts timestamp with time zone)
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
        purchase.quotations.quotation_id,
        inventory.get_supplier_name_by_supplier_id(purchase.quotations.supplier_id),
        purchase.quotations.value_date,
        purchase.quotations.expected_delivery_date,
        purchase.quotations.reference_number,
        purchase.quotations.terms,
        purchase.quotations.internal_memo,
        account.get_name_by_user_id(purchase.quotations.user_id)::national character varying(500) AS posted_by,
        core.get_office_name_by_office_id(office_id)::national character varying(500) AS office,
        purchase.quotations.transaction_timestamp
    FROM purchase.quotations
    WHERE 1 = 1
    AND purchase.quotations.value_date BETWEEN _from AND _to
    AND purchase.quotations.expected_delivery_date BETWEEN _expected_from AND _expected_to
    AND purchase.quotations.office_id IN (SELECT office_id FROM office_cte)
    AND (COALESCE(_id, 0) = 0 OR _id = purchase.quotations.quotation_id)
    AND COALESCE(LOWER(purchase.quotations.reference_number), '') LIKE '%' || LOWER(_reference_number) || '%' 
    AND COALESCE(LOWER(purchase.quotations.internal_memo), '') LIKE '%' || LOWER(_internal_memo) || '%' 
    AND COALESCE(LOWER(purchase.quotations.terms), '') LIKE '%' || LOWER(_terms) || '%' 
    AND LOWER(inventory.get_customer_name_by_customer_id(purchase.quotations.supplier_id)) LIKE '%' || LOWER(_supplier) || '%' 
    AND LOWER(account.get_name_by_user_id(purchase.quotations.user_id)) LIKE '%' || LOWER(_posted_by) || '%' 
    AND LOWER(core.get_office_name_by_office_id(purchase.quotations.office_id)) LIKE '%' || LOWER(_office) || '%' 
    AND NOT purchase.quotations.deleted;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

