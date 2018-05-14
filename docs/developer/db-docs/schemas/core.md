# core schema

**Tables**

| # | Table Name | Owner | Tablespace | Description |
| --- | --- | --- | --- | --- |
| 1 | [app_dependencies](../tables/core/app_dependencies.md) | frapid_db_user | DEFAULT |  |
| 2 | [apps](../tables/core/apps.md) | frapid_db_user | DEFAULT |  |
| 3 | [countries](../tables/core/countries.md) | frapid_db_user | DEFAULT |  |
| 4 | [currencies](../tables/core/currencies.md) | frapid_db_user | DEFAULT |  |
| 6 | [genders](../tables/core/genders.md) | frapid_db_user | DEFAULT |  |
| 7 | [marital_statuses](../tables/core/marital_statuses.md) | frapid_db_user | DEFAULT |  |
| 8 | [menus](../tables/core/menus.md) | frapid_db_user | DEFAULT |  |
| 9 | [notification_statuses](../tables/core/notification_statuses.md) | frapid_db_user | DEFAULT |  |
| 10 | [notifications](../tables/core/notifications.md) | frapid_db_user | DEFAULT |  |
| 11 | [offices](../tables/core/offices.md) | frapid_db_user | DEFAULT |  |
| 12 | [verification_statuses](../tables/core/verification_statuses.md) | frapid_db_user | DEFAULT | Verification statuses are integer values used to represent the state of a transaction.
For example, a verification status of value "0" would mean that the transaction has not yet been verified.
A negative value indicates that the transaction was rejected, whereas a positive value means approved.

Remember:
1. Only approved transactions appear on ledgers and final reports.
2. Cash repository balance is maintained on the basis of LIFO principle. 

   This means that cash balance is affected (reduced) on your repository as soon as a credit transaction is posted,
   without the transaction being approved on the first place. If you reject the transaction, the cash balance then increases.
   This also means that the cash balance is not affected (increased) on your repository as soon as a debit transaction is posted.
   You will need to approve the transaction.

   It should however be noted that the cash repository balance might be less than the total cash shown on your balance sheet,
   if you have pending transactions to verify. You cannot perform EOD operation if you have pending verifications.
 |
| 13 | [week_days](../tables/core/week_days.md) | frapid_db_user | DEFAULT |  |



**Sequences**

| # | Sequence Name | Owner | Data Type | Start Value | Increment | Description |
| --- | --- | --- | --- | --- | --- | --- |
| 1 | app_dependencies_app_dependency_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 2 | apps_app_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 3 | currencies_currency_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 4 | frequencies_frequency_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 5 | marital_statuses_marital_status_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 6 | menus_menu_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 7 | offices_office_id_seq | frapid_db_user | bigint | 1 | 1 |  |


**Views**

| # | View Name | Owner | Tablespace | Description |
| --- | --- | --- | --- | --- |
| 1 | [office_scrud_view](../views/core/office_scrud_view.md) | frapid_db_user | DEFAULT |  |



**Materialized Views**

| # | Matview Name | Owner | Tablespace | Description |
| --- | --- | --- | --- | --- |



**Functions**

| # | Function | Owner | Description |
| --- | --- | --- | --- |
| 1 | [create_app(_app_name text, _i18n_key character varying, _name text, _version_number text, _publisher text, _published_on date, _icon text, _landing_url text, _dependencies text[])RETURNS void](../functions/core/create_app-4454698.md) | frapid_db_user |  |
| 2 | [create_menu(_app_name text, _i18n_key character varying, _menu_name text, _url text, _icon text, _parent_menu_name text)RETURNS integer](../functions/core/create_menu-4454701.md) | frapid_db_user |  |
| 3 | [create_menu(_sort integer, _app_name text, _i18n_key character varying, _menu_name text, _url text, _icon text, _parent_menu_name text)RETURNS integer](../functions/core/create_menu-4454700.md) | frapid_db_user |  |
| 4 | [create_menu(_sort integer, _app_name text, _i18n_key character varying, _menu_name text, _url text, _icon text, _parent_menu_id integer)RETURNS integer](../functions/core/create_menu-4454699.md) | frapid_db_user |  |
| 5 | [get_currency_code_by_office_id(_office_id integer)RETURNS character varying](../functions/core/get_currency_code_by_office_id-4454702.md) | frapid_db_user |  |
| 6 | [get_hstore_field(_hstore hstore, _column_name text)RETURNS text](../functions/core/get_hstore_field-4454703.md) | frapid_db_user |  |
| 7 | [get_my_notifications(_login_id bigint)RETURNS TABLE(notification_id uuid, associated_app character varying, associated_menu_id integer, url character varying, formatted_text character varying, icon character varying, seen boolean, event_date timestamp with time zone)](../functions/core/get_my_notifications-4454704.md) | frapid_db_user |  |
| 8 | [get_office_code_by_office_id(_office_id integer)RETURNS character varying](../functions/core/get_office_code_by_office_id-4454705.md) | frapid_db_user |  |
| 9 | [get_office_id_by_office_name(_office_name text)RETURNS integer](../functions/core/get_office_id_by_office_name-4454706.md) | frapid_db_user |  |
| 10 | [get_office_ids(root_office_id integer)RETURNS SETOF integer](../functions/core/get_office_ids-4454707.md) | frapid_db_user |  |
| 11 | [get_office_name_by_office_id(_office_id integer)RETURNS text](../functions/core/get_office_name_by_office_id-4454708.md) | frapid_db_user |  |
| 12 | [is_valid_office_id(integer)RETURNS boolean](../functions/core/is_valid_office_id-4454709.md) | frapid_db_user |  |
| 13 | [mark_notification_as_seen(_notification_id uuid, _user_id integer)RETURNS void](../functions/core/mark_notification_as_seen-4454710.md) | frapid_db_user |  |



**Triggers**

| # | Trigger | Owner | Description |
| --- | --- | --- | --- |



**Types**

| # | Type | Base Type | Owner | Collation | Default | Type | StoreType | NotNull | Description |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../schemas.md)
* [Table of Contents](../../README.md)