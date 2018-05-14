# finance.get_verification_status_name_by_verification_status_id function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.get_verification_status_name_by_verification_status_id(_verification_status_id integer)
RETURNS text
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : get_verification_status_name_by_verification_status_id
* Arguments : _verification_status_id integer
* Owner : frapid_db_user
* Result Type : text
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.get_verification_status_name_by_verification_status_id(_verification_status_id integer)
 RETURNS text
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN
        verification_status_name
    FROM core.verification_statuses
    WHERE core.verification_statuses.verification_status_id = _verification_status_id
	AND NOT core.verification_statuses.deleted;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

