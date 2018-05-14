# finance.create_payment_card function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.create_payment_card(_payment_card_code character varying, _payment_card_name character varying, _card_type_id integer)
RETURNS void
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : create_payment_card
* Arguments : _payment_card_code character varying, _payment_card_name character varying, _card_type_id integer
* Owner : frapid_db_user
* Result Type : void
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.create_payment_card(_payment_card_code character varying, _payment_card_name character varying, _card_type_id integer)
 RETURNS void
 LANGUAGE plpgsql
AS $function$
BEGIN
    IF NOT EXISTS
    (
        SELECT * FROM finance.payment_cards
        WHERE payment_card_code = _payment_card_code
    ) THEN
        INSERT INTO finance.payment_cards(payment_card_code, payment_card_name, card_type_id)
        SELECT _payment_card_code, _payment_card_name, _card_type_id;
    ELSE
        UPDATE finance.payment_cards
        SET 
            payment_card_code =     _payment_card_code, 
            payment_card_name =     _payment_card_name,
            card_type_id =          _card_type_id
        WHERE
            payment_card_code =     _payment_card_code;
    END IF;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

