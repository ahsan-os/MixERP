# public.poco_get_table_function_annotation function:

```plpgsql
CREATE OR REPLACE FUNCTION public.poco_get_table_function_annotation(_schema_name text, _table_name text)
RETURNS TABLE(id integer, column_name text, nullable text, db_data_type text, value text, max_length integer, primary_key text)
```
* Schema : [public](../../schemas/public.md)
* Function Name : poco_get_table_function_annotation
* Arguments : _schema_name text, _table_name text
* Owner : frapid_db_user
* Result Type : TABLE(id integer, column_name text, nullable text, db_data_type text, value text, max_length integer, primary_key text)
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION public.poco_get_table_function_annotation(_schema_name text, _table_name text)
 RETURNS TABLE(id integer, column_name text, nullable text, db_data_type text, value text, max_length integer, primary_key text)
 LANGUAGE plpgsql
AS $function$
    DECLARE _args           text;
BEGIN
    DROP TABLE IF EXISTS temp_annonation;

    CREATE TEMPORARY TABLE temp_annonation
    (
        id                      SERIAL,
        column_name             text,
        is_nullable             text DEFAULT('NO'),
        db_data_type            text,
        value                   text,
        max_length              integer DEFAULT(0),
        is_primary_key          text DEFAULT('NO')
    ) ON COMMIT DROP;


    SELECT
        pg_catalog.pg_get_function_arguments(pg_proc.oid) AS arguments
    INTO
        _args
    FROM pg_proc
    INNER JOIN pg_namespace
    ON pg_proc.pronamespace = pg_namespace.oid
    INNER JOIN pg_type
    ON pg_proc.prorettype = pg_type.oid
    INNER JOIN pg_namespace type_namespace
    ON pg_type.typnamespace = type_namespace.oid
    WHERE typname != ANY(ARRAY['trigger'])
    AND pg_namespace.nspname = _schema_name
    AND proname::text = _table_name;

    INSERT INTO temp_annonation(column_name, db_data_type)
    SELECT split_part(trim(unnest(regexp_split_to_array(_args, ','))), ' ', 1), trim(unnest(regexp_split_to_array(_args, ',')));

    UPDATE temp_annonation
    SET db_data_type = TRIM(REPLACE(temp_annonation.db_data_type, temp_annonation.column_name, ''));

    
    RETURN QUERY
    SELECT * FROM temp_annonation;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

