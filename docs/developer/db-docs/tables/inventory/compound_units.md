# inventory.compound_units table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | compound_unit_id | [ ] | integer | 0 |  |
| 2 | base_unit_id | [ ] | integer | 0 |  |
| 3 | value | [ ] | smallint | 0 |  |
| 4 | compare_unit_id | [ ] | integer | 0 |  |
| 5 | audit_user_id | [x] | integer | 0 |  |
| 6 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 7 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [base_unit_id](../inventory/units.md) | compound_units_base_unit_id_fkey | inventory.units.unit_id |
| 4 | [compare_unit_id](../inventory/units.md) | compound_units_compare_unit_id_fkey | inventory.units.unit_id |
| 5 | [audit_user_id](../account/users.md) | compound_units_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| compound_units_pkey | frapid_db_user | btree | compound_unit_id |  |
| compound_units_base_unit_id_value_uix | frapid_db_user | btree | base_unit_id, value |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |
| compound_units_value_check CHECK (value > 0) |  |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | compound_unit_id | nextval('inventory.compound_units_compound_unit_id_seq'::regclass) |
| 3 | value | 0 |
| 6 | audit_ts | now() |
| 7 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
