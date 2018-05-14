# sales.get_gift_card_id_by_gift_card_number function:

```plpgsql
CREATE OR REPLACE FUNCTION sales.get_gift_card_id_by_gift_card_number(_gift_card_number character varying)
RETURNS integer
```
* Schema : [sales](../../schemas/sales.md)
* Function Name : get_gift_card_id_by_gift_card_number
* Arguments : _gift_card_number character varying
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION sales.get_gift_card_id_by_gift_card_number(_gift_card_number character varying)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN sales.gift_cards.gift_card_id
    FROM sales.gift_cards
    WHERE sales.gift_cards.gift_card_number = _gift_card_number
    AND NOT sales.gift_cards.deleted;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

