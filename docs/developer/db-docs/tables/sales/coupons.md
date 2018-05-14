# sales.coupons table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | coupon_id | [ ] | integer | 0 |  |
| 1 | coupon_id | [ ] | integer | 0 |  |
| 2 | coupon_code | [ ] | character varying | 100 |  |
| 2 | coupon_name | [ ] | character varying | 100 |  |
| 3 | description | [ ] | text | 0 |  |
| 3 | coupon_code | [ ] | character varying | 100 |  |
| 4 | discount_amount | [x] | money_strict | 0 |  |
| 4 | discount_rate | [ ] | decimal_strict | 0 |  |
| 5 | is_percentage | [ ] | boolean | 0 |  |
| 5 | discount_percent | [x] | decimal_strict | 0 |  |
| 6 | maximum_discount_amount | [x] | decimal_strict | 0 |  |
| 6 | begins_from | [x] | date | 0 |  |
| 7 | associated_price_type_id | [x] | integer | 0 |  |
| 7 | ends_on | [x] | date | 0 |  |
| 8 | minimum_purchase_amount | [x] | decimal_strict2 | 0 |  |
| 8 | max_usage | [ ] | integer_strict | 0 |  |
| 9 | revoked | [ ] | boolean | 0 |  |
| 9 | maximum_purchase_amount | [x] | decimal_strict2 | 0 |  |
| 10 | audit_user_id | [x] | integer | 0 |  |
| 10 | begins_from | [x] | date | 0 |  |
| 11 | expires_on | [x] | date | 0 |  |
| 11 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 12 | maximum_usage | [x] | integer_strict | 0 |  |
| 12 | deleted | [x] | boolean | 0 |  |
| 13 | enable_ticket_printing | [x] | boolean | 0 |  |
| 14 | for_ticket_of_price_type_id | [x] | integer | 0 |  |
| 15 | for_ticket_having_minimum_amount | [x] | decimal_strict2 | 0 |  |
| 16 | for_ticket_having_maximum_amount | [x] | decimal_strict2 | 0 |  |
| 17 | for_ticket_of_unknown_customers_only | [x] | boolean | 0 |  |
| 18 | audit_user_id | [x] | integer | 0 |  |
| 19 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 20 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 7 | [associated_price_type_id](../sales/price_types.md) | coupons_associated_price_type_id_fkey | sales.price_types.price_type_id |
| 10 | [audit_user_id](../account/users.md) | coupons_audit_user_id_fkey | account.users.user_id |
| 14 | [for_ticket_of_price_type_id](../sales/price_types.md) | coupons_for_ticket_of_price_type_id_fkey | sales.price_types.price_type_id |
| 18 | [audit_user_id](../account/users.md) | coupons_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| coupons_pkey | frapid_db_user | btree | coupon_id |  |
| coupons_coupon_code_uix | frapid_db_user | btree | upper(coupon_code::text) |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | coupon_id | nextval('helpdesk.coupons_coupon_id_seq'::regclass) |
| 1 | coupon_id | nextval('sales.coupons_coupon_id_seq'::regclass) |
| 5 | is_percentage | false |
| 8 | max_usage | 0 |
| 9 | revoked | false |
| 11 | audit_ts | now() |
| 12 | deleted | false |
| 19 | audit_ts | now() |
| 20 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
