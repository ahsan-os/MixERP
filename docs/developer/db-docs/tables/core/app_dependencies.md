# core.app_dependencies table



| # | Column Name | Nullable | Data Type | Max Length | Description |
| --- | --- | --- | --- | --- | --- |
| 1 | app_dependency_id | [ ] | integer | 0 |  |
| 2 | app_name | [x] | character varying | 100 |  |
| 3 | depends_on | [x] | character varying | 100 |  |
| 4 | audit_user_id | [x] | integer | 0 |  |
| 5 | audit_ts | [x] | timestamp with time zone | 0 |  |
| 6 | deleted | [x] | boolean | 0 |  |



**Foreign Keys**

| # | Column Name | Key Name | References |
| --- | --- | --- | --- |
| 2 | [app_name](../core/apps.md) | app_dependencies_app_name_fkey | core.apps.app_name |
| 3 | [depends_on](../core/apps.md) | app_dependencies_depends_on_fkey | core.apps.app_name |



**Indices**

| Index Name | Owner | Access Method | Definition | Description |
| --- | --- | --- | --- | --- |
| app_dependencies_pkey | frapid_db_user | btree | app_dependency_id |  |



**Check Constraints**

| Constraint Name | Description |
| --- | --- |



**Default Values**

| # | Column Name | Default |
| --- | --- | --- |
| 1 | app_dependency_id | nextval('core.app_dependencies_app_dependency_id_seq'::regclass) |
| 5 | audit_ts | now() |
| 6 | deleted | false |


**Triggers**

| Trigger Name | Targets | On Event | Timing | Condition | Order | Orientation | Description |
| --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../../schemas.md)
* [Table List](../../tables.md)
* [Sequence List](../../sequences.md)
* [Table of Contents](../../README.md)
