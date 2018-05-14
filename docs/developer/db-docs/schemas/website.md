# website schema

**Tables**

| # | Table Name | Owner | Tablespace | Description |
| --- | --- | --- | --- | --- |
| 1 | [categories](../tables/website/categories.md) | frapid_db_user | DEFAULT |  |
| 2 | [configurations](../tables/website/configurations.md) | frapid_db_user | DEFAULT |  |
| 3 | [contacts](../tables/website/contacts.md) | frapid_db_user | DEFAULT |  |
| 4 | [contents](../tables/website/contents.md) | frapid_db_user | DEFAULT |  |
| 5 | [email_subscriptions](../tables/website/email_subscriptions.md) | frapid_db_user | DEFAULT |  |
| 6 | [menu_items](../tables/website/menu_items.md) | frapid_db_user | DEFAULT |  |
| 7 | [menus](../tables/website/menus.md) | frapid_db_user | DEFAULT |  |



**Sequences**

| # | Sequence Name | Owner | Data Type | Start Value | Increment | Description |
| --- | --- | --- | --- | --- | --- | --- |
| 1 | categories_category_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 2 | configurations_configuration_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 3 | contacts_contact_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 4 | contents_content_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 5 | menu_items_menu_item_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 6 | menus_menu_id_seq | frapid_db_user | bigint | 1 | 1 |  |


**Views**

| # | View Name | Owner | Tablespace | Description |
| --- | --- | --- | --- | --- |
| 1 | [contact_scrud_view](../views/website/contact_scrud_view.md) | frapid_db_user | DEFAULT |  |
| 2 | [content_scrud_view](../views/website/content_scrud_view.md) | frapid_db_user | DEFAULT |  |
| 3 | [email_subscription_insert_view](../views/website/email_subscription_insert_view.md) | frapid_db_user | DEFAULT |  |
| 4 | [email_subscription_scrud_view](../views/website/email_subscription_scrud_view.md) | frapid_db_user | DEFAULT |  |
| 5 | [menu_item_view](../views/website/menu_item_view.md) | frapid_db_user | DEFAULT |  |
| 6 | [published_content_view](../views/website/published_content_view.md) | frapid_db_user | DEFAULT |  |
| 7 | [tag_view](../views/website/tag_view.md) | frapid_db_user | DEFAULT |  |
| 8 | [yesterdays_email_subscriptions](../views/website/yesterdays_email_subscriptions.md) | frapid_db_user | DEFAULT |  |



**Materialized Views**

| # | Matview Name | Owner | Tablespace | Description |
| --- | --- | --- | --- | --- |



**Functions**

| # | Function | Owner | Description |
| --- | --- | --- | --- |
| 1 | [add_email_subscription(_email text)RETURNS boolean](../functions/website/add_email_subscription-4455585.md) | frapid_db_user |  |
| 2 | [add_hit(_category_alias character varying, _alias character varying)RETURNS void](../functions/website/add_hit-4455586.md) | frapid_db_user |  |
| 3 | [get_category_id_by_category_alias(_alias text)RETURNS integer](../functions/website/get_category_id_by_category_alias-4455588.md) | frapid_db_user |  |
| 4 | [get_category_id_by_category_name(_category_name text)RETURNS integer](../functions/website/get_category_id_by_category_name-4455587.md) | frapid_db_user |  |
| 5 | [get_menu_id_by_menu_name(_menu_name character varying)RETURNS integer](../functions/website/get_menu_id_by_menu_name-4455589.md) | frapid_db_user |  |
| 6 | [remove_email_subscription(_email text)RETURNS boolean](../functions/website/remove_email_subscription-4455590.md) | frapid_db_user |  |



**Triggers**

| # | Trigger | Owner | Description |
| --- | --- | --- | --- |
| 1 | [email_subscription_confirmation_trigger()RETURNS TRIGGER](../functions/website/email_subscription_confirmation_trigger-4455603.md) | frapid_db_user |  |



**Types**

| # | Type | Base Type | Owner | Collation | Default | Type | StoreType | NotNull | Description |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |


### Related Contents
* [Schema List](../schemas.md)
* [Table of Contents](../../README.md)