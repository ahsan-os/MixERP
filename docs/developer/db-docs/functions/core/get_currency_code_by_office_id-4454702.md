# core.get_currency_code_by_office_id function:

```plpgsql
CREATE OR REPLACE FUNCTION core.get_currency_code_by_office_id(_office_id integer)
RETURNS character varying
```
* Schema : [core](../../schemas/core.md)
* Function Name : get_currency_code_by_office_id
* Arguments : _office_id integer
* Owner : frapid_db_user
* Result Type : character varying
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION core.get_currency_code_by_office_id(_office_id integer)
 RETURNS character varying
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN currency_code 
    FROM core.offices
    WHERE core.offices.office_id = _office_id
	AND NOT core.offices.deleted;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

