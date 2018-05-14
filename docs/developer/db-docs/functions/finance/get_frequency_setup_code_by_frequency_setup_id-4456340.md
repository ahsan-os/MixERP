# finance.get_frequency_setup_code_by_frequency_setup_id function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.get_frequency_setup_code_by_frequency_setup_id(_frequency_setup_id integer)
RETURNS text
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : get_frequency_setup_code_by_frequency_setup_id
* Arguments : _frequency_setup_id integer
* Owner : frapid_db_user
* Result Type : text
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.get_frequency_setup_code_by_frequency_setup_id(_frequency_setup_id integer)
 RETURNS text
 LANGUAGE plpgsql
 STABLE
AS $function$
BEGIN
    RETURN frequency_setup_code
    FROM finance.frequency_setups
    WHERE finance.frequency_setups.frequency_setup_id = $1
	AND NOT finance.frequency_setups.deleted;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

