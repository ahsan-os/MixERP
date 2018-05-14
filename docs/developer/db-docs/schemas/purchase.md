# purchase schema

**Tables**

| # | Table Name | Owner | Tablespace | Description |
| --- | --- | --- | --- | --- |
| 1 | [item_cost_prices](../tables/purchase/item_cost_prices.md) | frapid_db_user | DEFAULT |  |
| 2 | [order_details](../tables/purchase/order_details.md) | frapid_db_user | DEFAULT |  |
| 3 | [orders](../tables/purchase/orders.md) | frapid_db_user | DEFAULT |  |
| 4 | [price_types](../tables/purchase/price_types.md) | frapid_db_user | DEFAULT |  |
| 5 | [purchase_returns](../tables/purchase/purchase_returns.md) | frapid_db_user | DEFAULT |  |
| 6 | [purchases](../tables/purchase/purchases.md) | frapid_db_user | DEFAULT |  |
| 7 | [quotation_details](../tables/purchase/quotation_details.md) | frapid_db_user | DEFAULT |  |
| 8 | [quotations](../tables/purchase/quotations.md) | frapid_db_user | DEFAULT |  |



**Sequences**

| # | Sequence Name | Owner | Data Type | Start Value | Increment | Description |
| --- | --- | --- | --- | --- | --- | --- |
| 1 | item_cost_prices_item_cost_price_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 2 | order_details_order_detail_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 3 | orders_order_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 4 | price_types_price_type_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 5 | purchase_returns_purchase_return_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 6 | purchases_purchase_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 7 | quotation_details_quotation_detail_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 8 | quotations_quotation_id_seq | frapid_db_user | bigint | 1 | 1 |  |


**Views**

| # | View Name | Owner | Tablespace | Description |
| --- | --- | --- | --- | --- |
| 1 | [item_cost_price_scrud_view](../views/purchase/item_cost_price_scrud_view.md) | frapid_db_user | DEFAULT |  |
| 2 | [item_view](../views/purchase/item_view.md) | frapid_db_user | DEFAULT |  |



**Materialized Views**

| # | Matview Name | Owner | Tablespace | Description |
| --- | --- | --- | --- | --- |



**Functions**

| # | Function | Owner | Description |
| --- | --- | --- | --- |
| 1 | [get_account_payables_report(_office_id integer, _from date)RETURNS TABLE(office_id integer, office_name character varying, account_id integer, account_number character varying, account_name character varying, previous_period numeric, current_period numeric, total_amount numeric)](../functions/purchase/get_account_payables_report-4462962.md) | frapid_db_user |  |
| 2 | [get_item_cost_price(_item_id integer, _supplier_id bigint, _unit_id integer)RETURNS money_strict2](../functions/purchase/get_item_cost_price-4462952.md) | frapid_db_user |  |
| 3 | [get_order_view(_user_id integer, _office_id integer, _supplier character varying, _from date, _to date, _expected_from date, _expected_to date, _id bigint, _reference_number character varying, _internal_memo character varying, _terms character varying, _posted_by character varying, _office character varying)RETURNS TABLE(id bigint, supplier character varying, value_date date, expected_date date, reference_number character varying, terms character varying, internal_memo character varying, posted_by character varying, office character varying, transaction_ts timestamp with time zone)](../functions/purchase/get_order_view-4462953.md) | frapid_db_user |  |
| 4 | [get_price_type_id_by_price_type_code(_price_type_code character varying)RETURNS integer](../functions/purchase/get_price_type_id_by_price_type_code-4462954.md) | frapid_db_user |  |
| 5 | [get_price_type_id_by_price_type_name(_price_type_name character varying)RETURNS integer](../functions/purchase/get_price_type_id_by_price_type_name-4462955.md) | frapid_db_user |  |
| 6 | [get_quotation_view(_user_id integer, _office_id integer, _supplier character varying, _from date, _to date, _expected_from date, _expected_to date, _id bigint, _reference_number character varying, _internal_memo character varying, _terms character varying, _posted_by character varying, _office character varying)RETURNS TABLE(id bigint, supplier character varying, value_date date, expected_date date, reference_number character varying, terms character varying, internal_memo character varying, posted_by character varying, office character varying, transaction_ts timestamp with time zone)](../functions/purchase/get_quotation_view-4462956.md) | frapid_db_user |  |
| 7 | [get_supplier_id_by_supplier_code(text)RETURNS bigint](../functions/purchase/get_supplier_id_by_supplier_code-4462957.md) | frapid_db_user |  |
| 8 | [post_purchase(_office_id integer, _user_id integer, _login_id bigint, _value_date date, _book_date date, _cost_center_id integer, _reference_number character varying, _statement_reference text, _supplier_id integer, _price_type_id integer, _shipper_id integer, _details purchase.purchase_detail_type[])RETURNS bigint](../functions/purchase/post_purchase-4462958.md) | frapid_db_user |  |
| 9 | [post_return(_transaction_master_id bigint, _office_id integer, _user_id integer, _login_id bigint, _value_date date, _book_date date, _cost_center_id integer, _supplier_id integer, _price_type_id integer, _shipper_id integer, _reference_number character varying, _statement_reference text, _details purchase.purchase_detail_type[])RETURNS bigint](../functions/purchase/post_return-4462960.md) | frapid_db_user |  |



**Triggers**

| # | Trigger | Owner | Description |
| --- | --- | --- | --- |



**Types**

| # | Type | Base Type | Owner | Collation | Default | Type | StoreType | NotNull | Description |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| 1 | [purchase_detail_type](../types/purchase/purchase_detail_type.md) | - | frapid_db_user |  |  | Composite Type | Compressed Inline/Seconary | False |  |


### Related Contents
* [Schema List](../schemas.md)
* [Table of Contents](../../README.md)