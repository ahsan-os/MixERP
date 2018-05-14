# public.text_to_int_array function:

```plpgsql
CREATE OR REPLACE FUNCTION public.text_to_int_array(_input text, _remove_nulls boolean DEFAULT true)
RETURNS integer[]
```
* Schema : [public](../../schemas/public.md)
* Function Name : text_to_int_array
* Arguments : _input text, _remove_nulls boolean DEFAULT true
* Owner : frapid_db_user
* Result Type : integer[]
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION public.text_to_int_array(_input text, _remove_nulls boolean DEFAULT true)
 RETURNS integer[]
 LANGUAGE plpgsql
 STABLE
AS $function$
    DECLARE _int_array                  integer[];
BEGIN
    WITH items
    AS
    (
        SELECT
            item, 
            item ~ '^[0-9]+$' AS is_number
        FROM 
            unnest(regexp_split_to_array(_input, ',')) AS item
    )
    SELECT
        array_agg(item)
    INTO
        _int_array
    FROM items
    WHERE is_number;

    RETURN _int_array;   
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

