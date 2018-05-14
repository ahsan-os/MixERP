# finance.get_cash_flow_heading_id_by_cash_flow_heading_code function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.get_cash_flow_heading_id_by_cash_flow_heading_code(_cash_flow_heading_code character varying)
RETURNS integer
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : get_cash_flow_heading_id_by_cash_flow_heading_code
* Arguments : _cash_flow_heading_code character varying
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.get_cash_flow_heading_id_by_cash_flow_heading_code(_cash_flow_heading_code character varying)
 RETURNS integer
 LANGUAGE plpgsql
 STABLE
AS $function$
BEGIN
    RETURN
        cash_flow_heading_id
    FROM
        finance.cash_flow_headings
    WHERE
        finance.cash_flow_headings.cash_flow_heading_code = $1
	AND NOT finance.cash_flow_headings.deleted;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

