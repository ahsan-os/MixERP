# finance.get_default_currency_code_by_office_id function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.get_default_currency_code_by_office_id(office_id integer)
RETURNS character varying
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : get_default_currency_code_by_office_id
* Arguments : office_id integer
* Owner : frapid_db_user
* Result Type : character varying
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.get_default_currency_code_by_office_id(office_id integer)
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN
    (
        SELECT core.offices.currency_code 
        FROM core.offices
        WHERE core.offices.office_id = $1
		AND NOT core.offices.deleted	
    );
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

