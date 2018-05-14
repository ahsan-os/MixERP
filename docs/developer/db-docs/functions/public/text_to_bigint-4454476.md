# public.text_to_bigint function:

```plpgsql
CREATE OR REPLACE FUNCTION public.text_to_bigint(text)
RETURNS bigint
```
* Schema : [public](../../schemas/public.md)
* Function Name : text_to_bigint
* Arguments : text
* Owner : frapid_db_user
* Result Type : bigint
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION public.text_to_bigint(text)
 RETURNS bigint
 LANGUAGE sql
 IMMUTABLE STRICT
AS $function$SELECT int8in(textout($1));$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

