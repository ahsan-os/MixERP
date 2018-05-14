# public.explode_array function:

```plpgsql
CREATE OR REPLACE FUNCTION public.explode_array(in_array anyarray)
RETURNS SETOF anyelement
```
* Schema : [public](../../schemas/public.md)
* Function Name : explode_array
* Arguments : in_array anyarray
* Owner : frapid_db_user
* Result Type : SETOF anyelement
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION public.explode_array(in_array anyarray)
 RETURNS SETOF anyelement
 LANGUAGE sql
 IMMUTABLE
AS $function$
    SELECT ($1)[s] FROM generate_series(1,array_upper($1, 1)) AS s;
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

