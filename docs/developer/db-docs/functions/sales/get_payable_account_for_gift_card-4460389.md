# sales.get_payable_account_for_gift_card function:

```plpgsql
CREATE OR REPLACE FUNCTION sales.get_payable_account_for_gift_card(_gift_card_id integer)
RETURNS integer
```
* Schema : [sales](../../schemas/sales.md)
* Function Name : get_payable_account_for_gift_card
* Arguments : _gift_card_id integer
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION sales.get_payable_account_for_gift_card(_gift_card_id integer)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN sales.gift_cards.payable_account_id
    FROM sales.gift_cards
    WHERE sales.gift_cards.gift_card_id= _gift_card_id
    AND NOT sales.gift_cards.deleted;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

