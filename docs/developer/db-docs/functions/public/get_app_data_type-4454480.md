# public.get_app_data_type function:

```plpgsql
CREATE OR REPLACE FUNCTION public.get_app_data_type(_nullable text, _db_data_type text)
RETURNS text
```
* Schema : [public](../../schemas/public.md)
* Function Name : get_app_data_type
* Arguments : _nullable text, _db_data_type text
* Owner : frapid_db_user
* Result Type : text
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION public.get_app_data_type(_nullable text, _db_data_type text)
 RETURNS text
 LANGUAGE plpgsql
 STABLE
AS $function$
    DECLARE _type text;
BEGIN
    IF(_db_data_type IN('int2', 'smallint', 'smallint_strict', 'smallint_strict2', 'cardinal_number')) THEN
        _type := 'short';
    END IF;

    IF(_db_data_type IN('int4', 'oid', 'int', 'integer', 'integer_strict', 'integer_strict2')) THEN
        _type := 'int';
    END IF;

    IF(_db_data_type IN('int8', 'bigint')) THEN
        _type := 'long';
    END IF;

    IF(_db_data_type IN('varchar', 'character varying', 'text', 'bpchar', 'photo', 'color', 'regproc', 'character_data', 'yes_or_no', 'name')) THEN
        RETURN 'string';
    END IF;


    IF(_db_data_type IN('character', 'char')) THEN
        RETURN 'char';
    END IF;

    IF(_db_data_type IN('money_strict', 'money_strict2', 'money', 'decimal_strict', 'decimal_strict2', 'decimal', 'numeric')) THEN
        RETURN 'decimal';
    END IF;

    IF(_db_data_type IN('uuid')) THEN
        RETURN 'System.Guid';
    END IF;
    
    IF(_db_data_type IN('date')) THEN
        _type := 'System.DateTime';
    END IF;
    	
    IF(_db_data_type IN('timestamp', 'timestamptz')) THEN
        _type := 'System.DateTimeOffset';
    END IF;

    IF(_db_data_type IN('time')) THEN
        _type := 'System.TimeSpan';
    END IF;
    
    IF(_db_data_type IN('bool', 'boolean')) THEN
        _type := 'bool';
    END IF;

    IF(_nullable) THEN
        _type := _type || '?';
    END IF;
    
    RETURN _type;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

