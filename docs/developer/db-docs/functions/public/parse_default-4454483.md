# public.parse_default function:

```plpgsql
CREATE OR REPLACE FUNCTION public.parse_default(text)
RETURNS text
```
* Schema : [public](../../schemas/public.md)
* Function Name : parse_default
* Arguments : text
* Owner : frapid_db_user
* Result Type : text
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION public.parse_default(text)
 RETURNS text
 LANGUAGE plpgsql
AS $function$
    DECLARE _sql text;
    DECLARE _val text;
BEGIN
    IF($1 NOT LIKE 'nextval%') THEN
        _sql := 'SELECT ' || $1;
        EXECUTE _sql INTO _val;
        RAISE NOTICE '%', _sql;
        RETURN _val;
    END IF;

    IF($1 = 'now()') THEN
        RETURN '';
    END IF;

    RETURN $1;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

