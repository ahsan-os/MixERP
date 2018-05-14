# hrm.employee_social_network_details table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | employee_social_network_detail_id | [ ] | bigint | 0 |  |
| 2 | employee_id | [ ] | integer | 0 |  |
| 3 | social_network_id | [ ] | integer | 0 |  |
| 4 | profile_link | [ ] | character varying | 1000 |  |
| 5 | audit_user_id | [x] | integer | 0 |  |
| 6 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 7 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [employee_id](../hrm/employees.md) | employee_social_network_details_employee_id_fkey | hrm.employees.employee_id |
| 3 | [social_network_id](../hrm/social_networks.md) | employee_social_network_details_social_network_id_fkey | hrm.social_networks.social_network_id |
| 5 | [audit_user_id](../account/users.md) | employee_social_network_details_audit_user_id_fkey | account.users.user_id |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| employee_social_network_details_pkey | frapid_db_user | btree | employee_social_network_detail_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | employee_social_network_detail_id | nextval('hrm.employee_social_network_detai_employee_social_network_detai_seq'::regclass) |
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
