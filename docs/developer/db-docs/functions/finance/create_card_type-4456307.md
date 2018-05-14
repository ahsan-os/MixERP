# finance.create_card_type function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.create_card_type(_card_type_id integer, _card_type_code character varying, _card_type_name character varying)
RETURNS void
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : create_card_type
* Arguments : _card_type_id integer, _card_type_code character varying, _card_type_name character varying
* Owner : frapid_db_user
* Result Type : void
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.create_card_type(_card_type_id integer, _card_type_code character varying, _card_type_name character varying)
 RETURNS void
 LANGUAGE plpgsql
AS $function$
BEGIN
    IF NOT EXISTS
    (
        SELECT * FROM finance.card_types
        WHERE card_type_id = _card_type_id
    ) THEN
        INSERT INTO finance.card_types(card_type_id, card_type_code, card_type_name)
        SELECT _card_type_id, _card_type_code, _card_type_name;
    ELSE
        UPDATE finance.card_types
        SET 
            card_type_id =      _card_type_id, 
            card_type_code =    _card_type_code, 
            card_type_name =    _card_type_name
        WHERE
            card_type_id =      _card_type_id;
    END IF;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

