# inventory.inventory_setup table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | office_id | [ ] | integer | 0 |  |
| 2 | inventory_system | [ ] | character varying | 50 |  |
| 3 | cogs_calculation_method | [ ] | character varying | 50 |  |
| 4 | allow_multiple_opening_inventory | [ ] | boolean | 0 |  |
| 5 | default_discount_account_id | [ ] | integer | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 1 | [office_id](../core/offices.md) | inventory_setup_office_id_fkey | core.offices.office_id |
| 5 | [default_discount_account_id](../finance/accounts.md) | inventory_setup_default_discount_account_id_fkey | finance.accounts.account_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| inventory_setup_pkey | frapid_db_user | btree | office_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |
| inventory_setup_inventory_system_check CHECK (inventory_system::text = ANY (ARRAY['Periodic'::character varying, 'Perpetual'::character varying]::text[])) |  |
| inventory_setup_cogs_calculation_method_check CHECK (cogs_calculation_method::text = ANY (ARRAY['FIFO'::character varying, 'LIFO'::character varying, 'MAVCO'::character varying]::text[])) |  |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 4 | allow_multiple_opening_inventory | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
