# config.create_custom_field function:

```plpgsql
CREATE OR REPLACE FUNCTION config.create_custom_field(_form_name character varying, _before_field character varying, _field_order integer, _after_field character varying, _field_name character varying, _field_label character varying, _data_type character varying, _description character varying)
RETURNS void
```
* Schema : [config](../../schemas/config.md)
* Function Name : create_custom_field
* Arguments : _form_name character varying, _before_field character varying, _field_order integer, _after_field character varying, _field_name character varying, _field_label character varying, _data_type character varying, _description character varying
* Owner : frapid_db_user
* Result Type : void
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION config.create_custom_field(_form_name character varying, _before_field character varying, _field_order integer, _after_field character varying, _field_name character varying, _field_label character varying, _data_type character varying, _description character varying)
 RETURNS void
 LANGUAGE plpgsql
AS $function$
    DECLARE _table_name         national character varying(500);
    DECLARE _key_name           national character varying(500);
    DECLARE _sql                text;
    DECLARE _key_data_type      national character varying(500);
    DECLARE _cf_data_type       national character varying(500);
BEGIN
    SELECT
        config.custom_field_forms.table_name,
        config.custom_field_forms.key_name
    INTO
        _table_name,
        _key_name
    FROM config.custom_field_forms
    WHERE config.custom_field_forms.form_name = _form_name
	AND NOT config.custom_field_forms.deleted;

    SELECT 
        format_type(a.atttypid, a.atttypmod)
    INTO
        _key_data_type
    FROM   pg_index i
    JOIN   pg_attribute a ON a.attrelid = i.indrelid
                         AND a.attnum = ANY(i.indkey)
    WHERE  i.indrelid = _table_name::regclass
    AND    i.indisprimary;

    SELECT
        underlying_type
    INTO
        _cf_data_type
    FROM config.custom_field_data_types
    WHERE config.custom_field_data_types.data_type = _data_type
	AND NOT config.custom_field_data_types.deleted;

    
    _sql := 'CREATE TABLE IF NOT EXISTS %s_cf
            (
                %s %s PRIMARY KEY REFERENCES %1$s
            );';

    EXECUTE format(_sql, _table_name, _key_name, _key_data_type);

    _sql := 'DO
            $ALTER$
            BEGIN
                IF NOT EXISTS
                (
                    SELECT 1
                    FROM   pg_attribute 
                    WHERE  attrelid = ''%s_cf''::regclass
                    AND    attname = ''%s''
                    AND    NOT attisdropped
                ) THEN
                    ALTER TABLE %1$s_cf
                    ADD COLUMN %2$s	%s;
                END IF;
            END
            $ALTER$
            LANGUAGE plpgsql;';
                
   EXECUTE format(_sql, _table_name, lower(_field_name), _cf_data_type);

   IF NOT EXISTS
   (
        SELECT 1
        FROM config.custom_field_setup
        WHERE config.custom_field_setup.form_name = _form_name
        AND config.custom_field_setup.field_name = _field_name
		AND NOT config.custom_field_setup.deleted
   ) THEN
       INSERT INTO config.custom_field_setup(form_name, before_field, field_order, after_field, field_name, field_label, data_type, description)
       SELECT _form_name, _before_field, _field_order, _after_field, _field_name, _field_label, _data_type, _description;
   END IF;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

