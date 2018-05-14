# finance.get_journal_view function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.get_journal_view(_user_id integer, _office_id integer, _from date, _to date, _tran_id bigint, _tran_code character varying, _book character varying, _reference_number character varying, _statement_reference character varying, _posted_by character varying, _office character varying, _status character varying, _verified_by character varying, _reason character varying)
RETURNS TABLE(transaction_master_id bigint, transaction_code text, book text, value_date date, book_date date, reference_number text, statement_reference text, posted_by text, office text, status text, verified_by text, verified_on timestamp with time zone, reason text, transaction_ts timestamp with time zone)
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : get_journal_view
* Arguments : _user_id integer, _office_id integer, _from date, _to date, _tran_id bigint, _tran_code character varying, _book character varying, _reference_number character varying, _statement_reference character varying, _posted_by character varying, _office character varying, _status character varying, _verified_by character varying, _reason character varying
* Owner : frapid_db_user
* Result Type : TABLE(transaction_master_id bigint, transaction_code text, book text, value_date date, book_date date, reference_number text, statement_reference text, posted_by text, office text, status text, verified_by text, verified_on timestamp with time zone, reason text, transaction_ts timestamp with time zone)
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.get_journal_view(_user_id integer, _office_id integer, _from date, _to date, _tran_id bigint, _tran_code character varying, _book character varying, _reference_number character varying, _statement_reference character varying, _posted_by character varying, _office character varying, _status character varying, _verified_by character varying, _reason character varying)
 RETURNS TABLE(transaction_master_id bigint, transaction_code text, book text, value_date date, book_date date, reference_number text, statement_reference text, posted_by text, office text, status text, verified_by text, verified_on timestamp with time zone, reason text, transaction_ts timestamp with time zone)
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
        finance.transaction_master.transaction_master_id, 
        finance.transaction_master.transaction_code::text,
        finance.transaction_master.book::text,
        finance.transaction_master.value_date,
        finance.transaction_master.book_date,
        finance.transaction_master.reference_number::text,
        finance.transaction_master.statement_reference::text,
        account.get_name_by_user_id(finance.transaction_master.user_id)::text as posted_by,
        core.get_office_name_by_office_id(finance.transaction_master.office_id)::text as office,
        finance.get_verification_status_name_by_verification_status_id(finance.transaction_master.verification_status_id)::text as status,
        account.get_name_by_user_id(finance.transaction_master.verified_by_user_id)::text as verified_by,
        finance.transaction_master.last_verified_on AS verified_on,
        finance.transaction_master.verification_reason::text AS reason,    
        finance.transaction_master.transaction_ts
    FROM finance.transaction_master
    WHERE 1 = 1
    AND finance.transaction_master.value_date BETWEEN _from AND _to
    AND office_id IN (SELECT office_id FROM office_cte)
    AND (_tran_id = 0 OR _tran_id  = finance.transaction_master.transaction_master_id)
    AND LOWER(finance.transaction_master.transaction_code) LIKE '%' || LOWER(_tran_code) || '%' 
    AND LOWER(finance.transaction_master.book) LIKE '%' || LOWER(_book) || '%' 
    AND COALESCE(LOWER(finance.transaction_master.reference_number), '') LIKE '%' || LOWER(_reference_number) || '%' 
    AND COALESCE(LOWER(finance.transaction_master.statement_reference), '') LIKE '%' || LOWER(_statement_reference) || '%' 
    AND COALESCE(LOWER(finance.transaction_master.verification_reason), '') LIKE '%' || LOWER(_reason) || '%' 
    AND LOWER(account.get_name_by_user_id(finance.transaction_master.user_id)) LIKE '%' || LOWER(_posted_by) || '%' 
    AND LOWER(core.get_office_name_by_office_id(finance.transaction_master.office_id)) LIKE '%' || LOWER(_office) || '%' 
    AND COALESCE(LOWER(finance.get_verification_status_name_by_verification_status_id(finance.transaction_master.verification_status_id)), '') LIKE '%' || LOWER(_status) || '%' 
    AND COALESCE(LOWER(account.get_name_by_user_id(finance.transaction_master.verified_by_user_id)), '') LIKE '%' || LOWER(_verified_by) || '%'    
    AND NOT finance.transaction_master.deleted
	ORDER BY value_date ASC, verification_status_id DESC;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

