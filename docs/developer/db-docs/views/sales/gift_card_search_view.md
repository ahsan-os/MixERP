# sales.gift_card_search_view view

| Schema | [sales](../../schemas/sales.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | gift_card_search_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW sales.gift_card_search_view
 AS
 SELECT gift_cards.gift_card_id,
    gift_cards.gift_card_number,
    replace((COALESCE(gift_cards.first_name::text || ' '::text, ''::text) || COALESCE(gift_cards.middle_name::text || ' '::text, ''::text)) || COALESCE(gift_cards.last_name, ''::character varying)::text, '  '::text, ' '::text) AS name,
    replace((COALESCE(gift_cards.address_line_1::text || ' '::text, ''::text) || COALESCE(gift_cards.address_line_2::text || ' '::text, ''::text)) || COALESCE(gift_cards.street, ''::character varying)::text, '  '::text, ' '::text) AS address,
    gift_cards.city,
    gift_cards.state,
    gift_cards.country,
    gift_cards.po_box,
    gift_cards.zipcode,
    gift_cards.phone_numbers,
    gift_cards.fax
   FROM sales.gift_cards
  WHERE NOT gift_cards.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

