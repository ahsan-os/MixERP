# sales.coupon_view view

| Schema | [sales](../../schemas/sales.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | coupon_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW sales.coupon_view
 AS
 SELECT coupons.coupon_id,
    coupons.coupon_code,
    coupons.coupon_name,
    coupons.discount_rate,
    coupons.is_percentage,
    coupons.maximum_discount_amount,
    coupons.associated_price_type_id,
    associated_price_type.price_type_code AS associated_price_type_code,
    associated_price_type.price_type_name AS associated_price_type_name,
    coupons.minimum_purchase_amount,
    coupons.maximum_purchase_amount,
    coupons.begins_from,
    coupons.expires_on,
    coupons.maximum_usage,
    coupons.enable_ticket_printing,
    coupons.for_ticket_of_price_type_id,
    for_ticket_of_price_type.price_type_code AS for_ticket_of_price_type_code,
    for_ticket_of_price_type.price_type_name AS for_ticket_of_price_type_name,
    coupons.for_ticket_having_minimum_amount,
    coupons.for_ticket_having_maximum_amount,
    coupons.for_ticket_of_unknown_customers_only
   FROM sales.coupons
     LEFT JOIN sales.price_types associated_price_type ON associated_price_type.price_type_id = coupons.associated_price_type_id
     LEFT JOIN sales.price_types for_ticket_of_price_type ON for_ticket_of_price_type.price_type_id = coupons.for_ticket_of_price_type_id;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

