# inventory.item_groups table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | item_group_id | [ ] | integer | 0 |  |
| 1 | item_group_id | [ ] | integer | 0 |  |
| 2 | item_group_code | [ ] | character varying | 24 |  |
| 2 | item_group_code | [ ] | character varying | 12 |  |
| 3 | item_group_name | [ ] | character varying | 100 |  |
| 3 | item_group_name | [ ] | character varying | 500 |  |
| 4 | exclude_from_purchase | [ ] | boolean | 0 |  |
| 4 | audit_user_id | [x] | integer | 0 |  |
| 5 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 5 | exclude_from_sales | [ ] | boolean | 0 |  |
| 6 | sales_account_id | [ ] | integer | 0 |  |
| 6 | deleted | [x] | boolean | 0 |  |
| 7 | sales_discount_account_id | [ ] | integer | 0 |  |
| 8 | sales_return_account_id | [ ] | integer | 0 |  |
| 9 | purchase_account_id | [ ] | integer | 0 |  |
| 10 | purchase_discount_account_id | [ ] | integer | 0 |  |
| 11 | inventory_account_id | [ ] | integer | 0 |  |
| 12 | cost_of_goods_sold_account_id | [ ] | integer | 0 |  |
| 13 | parent_item_group_id | [x] | integer | 0 |  |
| 14 | audit_user_id | [x] | integer | 0 |  |
| 15 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 16 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 4 | [audit_user_id](../account/users.md) | item_groups_audit_user_id_fkey | account.users.user_id |
| 6 | [sales_account_id](../finance/accounts.md) | item_groups_sales_account_id_fkey | finance.accounts.account_id |
| 7 | [sales_discount_account_id](../finance/accounts.md) | item_groups_sales_discount_account_id_fkey | finance.accounts.account_id |
| 8 | [sales_return_account_id](../finance/accounts.md) | item_groups_sales_return_account_id_fkey | finance.accounts.account_id |
| 9 | [purchase_account_id](../finance/accounts.md) | item_groups_purchase_account_id_fkey | finance.accounts.account_id |
| 10 | [purchase_discount_account_id](../finance/accounts.md) | item_groups_purchase_discount_account_id_fkey | finance.accounts.account_id |
| 11 | [inventory_account_id](../finance/accounts.md) | item_groups_inventory_account_id_fkey | finance.accounts.account_id |
| 12 | [cost_of_goods_sold_account_id](../finance/accounts.md) | item_groups_cost_of_goods_sold_account_id_fkey | finance.accounts.account_id |
| 13 | [parent_item_group_id](../inventory/item_groups.md) | item_groups_parent_item_group_id_fkey | inventory.item_groups.item_group_id |
| 14 | [audit_user_id](../account/users.md) | item_groups_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| item_groups_pkey | frapid_db_user | btree | item_group_id |  |
| item_groups_item_group_code_uix | frapid_db_user | btree | upper(item_group_code::text) |  |
| item_groups_item_group_name_uix | frapid_db_user | btree | upper(item_group_name::text) |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | item_group_id | nextval('inventory.item_groups_item_group_id_seq'::regclass) |
| 1 | item_group_id | nextval('foodcourt.item_groups_item_group_id_seq'::regclass) |
| 4 | exclude_from_purchase | false |
| 5 | audit_ts | now() |
| 5 | exclude_from_sales | false |
| 6 | deleted | false |
| 15 | audit_ts | now() |
| 16 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
