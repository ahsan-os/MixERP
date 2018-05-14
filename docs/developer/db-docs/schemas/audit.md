# audit schema

**Tables**

| # | Table Name | Owner | Tablespace | Description |
| --- | --- | --- | --- | --- |
| 1 | [logged_actions](../tables/audit/logged_actions.md) | frapid_db_user | DEFAULT |  |
| 2 | [transaction_log](../tables/audit/transaction_log.md) | frapid_db_user | DEFAULT |  |



**Sequences**

| # | Sequence Name | Owner | Data Type | Start Value | Increment | Description |
| --- | --- | --- | --- | --- | --- | --- |
| 1 | transaction_log_log_id_seq | frapid_db_user | bigint | 1 | 1 |  |


**Views**

| # | View Name | Owner | Tablespace | Description |
| --- | --- | --- | --- | --- |
| 1 | [transaction_log_view](../views/audit/transaction_log_view.md) | frapid_db_user | DEFAULT |  |



**Materialized Views**

| # | Matview Name | Owner | Tablespace | Description |
| --- | --- | --- | --- | --- |



**Functions**

| # | Function | Owner | Description |
| --- | --- | --- | --- |



**Triggers**

| # | Trigger | Owner | Description |
| --- | --- | --- | --- |
| 1 | [disable_update_transaction_log()RETURNS TRIGGER](../functions/audit/disable_update_transaction_log-4458111.md) | frapid_db_user |  |



**Types**

| # | Type | Base Type | Owner | Collation | Default | Type | StoreType | NotNull | Description |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../schemas.md)
* [Table of Contents](../../README.md)