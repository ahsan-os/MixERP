# inventory schema

**Tables**

| # | Table Name | Owner | Tablespace | Description |
| --- | --- | --- | --- | --- |
| 1 | [attributes](../tables/inventory/attributes.md) | frapid_db_user | DEFAULT |  |
| 2 | [brands](../tables/inventory/brands.md) | frapid_db_user | DEFAULT |  |
| 3 | [checkout_details](../tables/inventory/checkout_details.md) | frapid_db_user | DEFAULT |  |
| 4 | [checkouts](../tables/inventory/checkouts.md) | frapid_db_user | DEFAULT |  |
| 5 | [compound_units](../tables/inventory/compound_units.md) | frapid_db_user | DEFAULT |  |
| 6 | [counters](../tables/inventory/counters.md) | frapid_db_user | DEFAULT |  |
| 7 | [customer_types](../tables/inventory/customer_types.md) | frapid_db_user | DEFAULT |  |
| 8 | [customers](../tables/inventory/customers.md) | frapid_db_user | DEFAULT |  |
| 9 | [inventory_setup](../tables/inventory/inventory_setup.md) | frapid_db_user | DEFAULT |  |
| 10 | [inventory_transfer_deliveries](../tables/inventory/inventory_transfer_deliveries.md) | frapid_db_user | DEFAULT |  |
| 11 | [inventory_transfer_delivery_details](../tables/inventory/inventory_transfer_delivery_details.md) | frapid_db_user | DEFAULT |  |
| 12 | [inventory_transfer_request_details](../tables/inventory/inventory_transfer_request_details.md) | frapid_db_user | DEFAULT |  |
| 13 | [inventory_transfer_requests](../tables/inventory/inventory_transfer_requests.md) | frapid_db_user | DEFAULT |  |
| 14 | [item_groups](../tables/inventory/item_groups.md) | frapid_db_user | DEFAULT |  |
| 15 | [item_types](../tables/inventory/item_types.md) | frapid_db_user | DEFAULT |  |
| 16 | [item_variants](../tables/inventory/item_variants.md) | frapid_db_user | DEFAULT |  |
| 17 | [items](../tables/inventory/items.md) | frapid_db_user | DEFAULT |  |
| 18 | [shippers](../tables/inventory/shippers.md) | frapid_db_user | DEFAULT |  |
| 19 | [store_types](../tables/inventory/store_types.md) | frapid_db_user | DEFAULT |  |
| 20 | [stores](../tables/inventory/stores.md) | frapid_db_user | DEFAULT |  |
| 21 | [supplier_types](../tables/inventory/supplier_types.md) | frapid_db_user | DEFAULT |  |
| 22 | [suppliers](../tables/inventory/suppliers.md) | frapid_db_user | DEFAULT |  |
| 23 | [units](../tables/inventory/units.md) | frapid_db_user | DEFAULT |  |
| 24 | [variants](../tables/inventory/variants.md) | frapid_db_user | DEFAULT |  |



**Sequences**

| # | Sequence Name | Owner | Data Type | Start Value | Increment | Description |
| --- | --- | --- | --- | --- | --- | --- |
| 1 | attributes_attribute_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 2 | brands_brand_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 3 | checkout_details_checkout_detail_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 4 | checkouts_checkout_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 5 | compound_units_compound_unit_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 6 | counters_counter_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 7 | customer_types_customer_type_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 8 | customers_customer_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 9 | inventory_transfer_deliveries_inventory_transfer_delivery_i_seq | frapid_db_user | bigint | 1 | 1 |  |
| 10 | inventory_transfer_delivery_d_inventory_transfer_delivery_d_seq | frapid_db_user | bigint | 1 | 1 |  |
| 11 | inventory_transfer_request_de_inventory_transfer_request_de_seq | frapid_db_user | bigint | 1 | 1 |  |
| 12 | inventory_transfer_requests_inventory_transfer_request_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 13 | item_groups_item_group_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 14 | item_types_item_type_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 15 | item_variants_item_variant_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 16 | items_item_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 17 | shippers_shipper_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 18 | store_types_store_type_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 19 | stores_store_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 20 | supplier_types_supplier_type_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 21 | suppliers_supplier_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 22 | units_unit_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 23 | variants_variant_id_seq | frapid_db_user | bigint | 1 | 1 |  |


**Views**

| # | View Name | Owner | Tablespace | Description |
| --- | --- | --- | --- | --- |
| 1 | [checkout_detail_view](../views/inventory/checkout_detail_view.md) | frapid_db_user | DEFAULT |  |
| 2 | [checkout_view](../views/inventory/checkout_view.md) | frapid_db_user | DEFAULT |  |
| 3 | [compound_unit_scrud_view](../views/inventory/compound_unit_scrud_view.md) | frapid_db_user | DEFAULT |  |
| 4 | [customer_scrud_view](../views/inventory/customer_scrud_view.md) | frapid_db_user | DEFAULT |  |
| 5 | [item_scrud_view](../views/inventory/item_scrud_view.md) | frapid_db_user | DEFAULT |  |
| 6 | [item_view](../views/inventory/item_view.md) | frapid_db_user | DEFAULT |  |
| 7 | [transaction_view](../views/inventory/transaction_view.md) | frapid_db_user | DEFAULT |  |
| 8 | [verified_checkout_details_view](../views/inventory/verified_checkout_details_view.md) | frapid_db_user | DEFAULT |  |



**Materialized Views**

| # | Matview Name | Owner | Tablespace | Description |
| --- | --- | --- | --- | --- |
| 1 | [verified_checkout_view](../materialized-views/inventory/verified_checkout_view.md) | frapid_db_user | DEFAULT |  |


**Functions**

| # | Function | Owner | Description |
| --- | --- | --- | --- |
| 1 | [convert_unit(from_unit integer, to_unit integer)RETURNS numeric](../functions/inventory/convert_unit-4459564.md) | frapid_db_user |  |
| 2 | [count_item_in_stock(_item_id integer, _unit_id integer, _store_id integer)RETURNS numeric](../functions/inventory/count_item_in_stock-4459565.md) | frapid_db_user |  |
| 3 | [count_purchases(_item_id integer, _unit_id integer, _store_id integer)RETURNS numeric](../functions/inventory/count_purchases-4459566.md) | frapid_db_user |  |
| 4 | [count_sales(_item_id integer, _unit_id integer, _store_id integer)RETURNS numeric](../functions/inventory/count_sales-4459567.md) | frapid_db_user |  |
| 5 | [create_item_variant(_variant_of integer, _item_id integer, _item_code character varying, _item_name character varying, _variants text, _user_id integer)RETURNS integer](../functions/inventory/create_item_variant-4459568.md) | frapid_db_user |  |
| 6 | [delete_variant_item(_item_id integer)RETURNS boolean](../functions/inventory/delete_variant_item-4459569.md) | frapid_db_user |  |
| 7 | [get_account_id_by_customer_id(_customer_id integer)RETURNS integer](../functions/inventory/get_account_id_by_customer_id-4459570.md) | frapid_db_user |  |
| 8 | [get_account_id_by_customer_type_id(_customer_type_id integer)RETURNS integer](../functions/inventory/get_account_id_by_customer_type_id-4459571.md) | frapid_db_user |  |
| 9 | [get_account_id_by_shipper_id(_shipper_id integer)RETURNS integer](../functions/inventory/get_account_id_by_shipper_id-4459572.md) | frapid_db_user |  |
| 10 | [get_account_id_by_supplier_id(_supplier_id integer)RETURNS integer](../functions/inventory/get_account_id_by_supplier_id-4459573.md) | frapid_db_user |  |
| 11 | [get_account_id_by_supplier_type_id(_supplier_type_id integer)RETURNS integer](../functions/inventory/get_account_id_by_supplier_type_id-4459574.md) | frapid_db_user |  |
| 12 | [get_account_statement(_value_date_from date, _value_date_to date, _user_id integer, _item_id integer, _store_id integer)RETURNS TABLE(id integer, value_date date, book_date date, store_name text, tran_code text, statement_reference text, debit numeric, credit numeric, balance numeric, book text, item_id integer, item_code text, item_name text, posted_on timestamp with time zone, posted_by text, approved_by text, verification_status integer)](../functions/inventory/get_account_statement-4459575.md) | frapid_db_user |  |
| 13 | [get_associated_unit_list(_any_unit_id integer)RETURNS integer[]](../functions/inventory/get_associated_unit_list-4459576.md) | frapid_db_user |  |
| 14 | [get_associated_units(_any_unit_id integer)RETURNS TABLE(unit_id integer, unit_code text, unit_name text)](../functions/inventory/get_associated_units-4459577.md) | frapid_db_user |  |
| 15 | [get_associated_units_by_item_code(_item_code text)RETURNS TABLE(unit_id integer, unit_code text, unit_name text)](../functions/inventory/get_associated_units_by_item_code-4459579.md) | frapid_db_user |  |
| 16 | [get_associated_units_by_item_id(_item_id integer)RETURNS TABLE(unit_id integer, unit_code text, unit_name text)](../functions/inventory/get_associated_units_by_item_id-4459578.md) | frapid_db_user |  |
| 17 | [get_base_quantity_by_unit_id(integer, numeric)RETURNS numeric](../functions/inventory/get_base_quantity_by_unit_id-4459580.md) | frapid_db_user |  |
| 18 | [get_base_quantity_by_unit_name(text, numeric)RETURNS numeric](../functions/inventory/get_base_quantity_by_unit_name-4459581.md) | frapid_db_user |  |
| 19 | [get_base_unit_id_by_unit_name(text)RETURNS integer](../functions/inventory/get_base_unit_id_by_unit_name-4459582.md) | frapid_db_user |  |
| 20 | [get_brand_id_by_brand_code(text)RETURNS integer](../functions/inventory/get_brand_id_by_brand_code-4459583.md) | frapid_db_user |  |
| 21 | [get_cash_account_id_by_store_id(_store_id integer)RETURNS bigint](../functions/inventory/get_cash_account_id_by_store_id-4459584.md) | frapid_db_user |  |
| 22 | [get_cash_repository_id_by_store_id(_store_id integer)RETURNS bigint](../functions/inventory/get_cash_repository_id_by_store_id-4459585.md) | frapid_db_user |  |
| 23 | [get_checkout_id_by_transaction_master_id(_checkout_id bigint)RETURNS bigint](../functions/inventory/get_checkout_id_by_transaction_master_id-4459586.md) | frapid_db_user |  |
| 24 | [get_cost_of_good_method(_office_id integer)RETURNS text](../functions/inventory/get_cost_of_good_method-4459587.md) | frapid_db_user |  |
| 25 | [get_cost_of_goods_sold(_item_id integer, _unit_id integer, _store_id integer, _quantity numeric)RETURNS money_strict](../functions/inventory/get_cost_of_goods_sold-4459588.md) | frapid_db_user |  |
| 26 | [get_cost_of_goods_sold_account_id(_item_id integer)RETURNS integer](../functions/inventory/get_cost_of_goods_sold_account_id-4459589.md) | frapid_db_user |  |
| 27 | [get_currency_code_by_customer_id(_customer_id integer)RETURNS character varying](../functions/inventory/get_currency_code_by_customer_id-4459590.md) | frapid_db_user |  |
| 28 | [get_currency_code_by_supplier_id(_supplier_id integer)RETURNS character varying](../functions/inventory/get_currency_code_by_supplier_id-4459591.md) | frapid_db_user |  |
| 29 | [get_customer_code_by_customer_id(_customer_id integer)RETURNS character varying](../functions/inventory/get_customer_code_by_customer_id-4459592.md) | frapid_db_user |  |
| 30 | [get_customer_id_by_customer_code(text)RETURNS integer](../functions/inventory/get_customer_id_by_customer_code-4459593.md) | frapid_db_user |  |
| 31 | [get_customer_name_by_customer_id(_customer_id integer)RETURNS character varying](../functions/inventory/get_customer_name_by_customer_id-4459594.md) | frapid_db_user |  |
| 32 | [get_customer_transaction_summary(office_id integer, customer_id integer)RETURNS TABLE(currency_code character varying, currency_symbol character varying, total_due_amount numeric, office_due_amount numeric)](../functions/inventory/get_customer_transaction_summary-4459595.md) | frapid_db_user |  |
| 33 | [get_customer_type_id_by_customer_id(_customer_id integer)RETURNS integer](../functions/inventory/get_customer_type_id_by_customer_id-4459596.md) | frapid_db_user |  |
| 34 | [get_customer_type_id_by_customer_type_code(text)RETURNS integer](../functions/inventory/get_customer_type_id_by_customer_type_code-4459597.md) | frapid_db_user |  |
| 35 | [get_inventory_account_id(_item_id integer)RETURNS integer](../functions/inventory/get_inventory_account_id-4459598.md) | frapid_db_user |  |
| 36 | [get_item_code_by_item_id(item_id_ integer)RETURNS character varying](../functions/inventory/get_item_code_by_item_id-4459599.md) | frapid_db_user |  |
| 37 | [get_item_cost_price(_item_id integer, _unit_id integer)RETURNS money_strict2](../functions/inventory/get_item_cost_price-4459600.md) | frapid_db_user |  |
| 38 | [get_item_group_id_by_item_group_code(text)RETURNS integer](../functions/inventory/get_item_group_id_by_item_group_code-4459601.md) | frapid_db_user |  |
| 39 | [get_item_id_by_item_code(_item_code text)RETURNS integer](../functions/inventory/get_item_id_by_item_code-4459602.md) | frapid_db_user |  |
| 40 | [get_item_name_by_item_id(item_id_ integer)RETURNS character varying](../functions/inventory/get_item_name_by_item_id-4459603.md) | frapid_db_user |  |
| 41 | [get_item_type_id_by_item_type_code(text)RETURNS integer](../functions/inventory/get_item_type_id_by_item_type_code-4459604.md) | frapid_db_user |  |
| 42 | [get_mavcogs(_item_id integer, _store_id integer, _base_quantity numeric, _factor numeric)RETURNS numeric](../functions/inventory/get_mavcogs-4459605.md) | frapid_db_user |  |
| 43 | [get_office_id_by_store_id(integer)RETURNS integer](../functions/inventory/get_office_id_by_store_id-4459606.md) | frapid_db_user |  |
| 44 | [get_opening_inventory_status(_office_id integer)RETURNS TABLE(office_id integer, multiple_inventory_allowed boolean, has_opening_inventory boolean)](../functions/inventory/get_opening_inventory_status-4459607.md) | frapid_db_user |  |
| 45 | [get_purchase_account_id(_item_id integer)RETURNS integer](../functions/inventory/get_purchase_account_id-4459608.md) | frapid_db_user |  |
| 46 | [get_purchase_discount_account_id(_item_id integer)RETURNS integer](../functions/inventory/get_purchase_discount_account_id-4459609.md) | frapid_db_user |  |
| 47 | [get_root_unit_id(_any_unit_id integer)RETURNS integer](../functions/inventory/get_root_unit_id-4459610.md) | frapid_db_user |  |
| 48 | [get_sales_account_id(_item_id integer)RETURNS integer](../functions/inventory/get_sales_account_id-4459611.md) | frapid_db_user |  |
| 49 | [get_sales_discount_account_id(_item_id integer)RETURNS integer](../functions/inventory/get_sales_discount_account_id-4459612.md) | frapid_db_user |  |
| 50 | [get_sales_return_account_id(_item_id integer)RETURNS integer](../functions/inventory/get_sales_return_account_id-4459613.md) | frapid_db_user |  |
| 51 | [get_shipper_id_by_shipper_code(_shipper_code character varying)RETURNS integer](../functions/inventory/get_shipper_id_by_shipper_code-4459614.md) | frapid_db_user |  |
| 52 | [get_shipper_id_by_shipper_name(_shipper_name character varying)RETURNS integer](../functions/inventory/get_shipper_id_by_shipper_name-4459615.md) | frapid_db_user |  |
| 53 | [get_store_id_by_store_code(_store_code text)RETURNS integer](../functions/inventory/get_store_id_by_store_code-4459616.md) | frapid_db_user |  |
| 54 | [get_store_id_by_store_name(_store_name text)RETURNS integer](../functions/inventory/get_store_id_by_store_name-4459617.md) | frapid_db_user |  |
| 55 | [get_store_name_by_store_id(integer)RETURNS text](../functions/inventory/get_store_name_by_store_id-4459618.md) | frapid_db_user |  |
| 56 | [get_store_type_id_by_store_type_code(text)RETURNS integer](../functions/inventory/get_store_type_id_by_store_type_code-4459619.md) | frapid_db_user |  |
| 57 | [get_supplier_id_by_supplier_code(text)RETURNS integer](../functions/inventory/get_supplier_id_by_supplier_code-4459620.md) | frapid_db_user |  |
| 58 | [get_supplier_name_by_supplier_id(_supplier_id integer)RETURNS character varying](../functions/inventory/get_supplier_name_by_supplier_id-4459621.md) | frapid_db_user |  |
| 59 | [get_supplier_type_id_by_supplier_type_code(text)RETURNS integer](../functions/inventory/get_supplier_type_id_by_supplier_type_code-4459622.md) | frapid_db_user |  |
| 60 | [get_total_customer_due(office_id integer, customer_id integer)RETURNS numeric](../functions/inventory/get_total_customer_due-4459623.md) | frapid_db_user |  |
| 61 | [get_unit_id_by_unit_code(text)RETURNS integer](../functions/inventory/get_unit_id_by_unit_code-4459624.md) | frapid_db_user |  |
| 62 | [get_unit_id_by_unit_name(_unit_name text)RETURNS integer](../functions/inventory/get_unit_id_by_unit_name-4459625.md) | frapid_db_user |  |
| 63 | [get_unit_name_by_unit_id(_unit_id integer)RETURNS character varying](../functions/inventory/get_unit_name_by_unit_id-4459626.md) | frapid_db_user |  |
| 64 | [get_write_off_cost_of_goods_sold(_checkout_id bigint, _item_id integer, _unit_id integer, _quantity integer)RETURNS money_strict2](../functions/inventory/get_write_off_cost_of_goods_sold-4459627.md) | frapid_db_user |  |
| 65 | [is_parent_unit(parent integer, child integer)RETURNS boolean](../functions/inventory/is_parent_unit-4459628.md) | frapid_db_user |  |
| 66 | [is_periodic_inventory(_office_id integer)RETURNS boolean](../functions/inventory/is_periodic_inventory-4459629.md) | frapid_db_user |  |
| 67 | [is_purchase(_transaction_master_id bigint)RETURNS boolean](../functions/inventory/is_purchase-4459630.md) | frapid_db_user |  |
| 68 | [is_valid_unit_id(_unit_id integer, _item_id integer)RETURNS boolean](../functions/inventory/is_valid_unit_id-4459631.md) | frapid_db_user |  |
| 69 | [list_closing_stock(_store_id integer)RETURNS TABLE(item_id integer, item_code text, item_name text, unit_id integer, unit_name text, quantity numeric)](../functions/inventory/list_closing_stock-4459632.md) | frapid_db_user |  |
| 70 | [post_adjustment(_office_id integer, _user_id integer, _login_id bigint, _store_id integer, _value_date date, _book_date date, _reference_number character varying, _statement_reference text, _details inventory.adjustment_type[])RETURNS bigint](../functions/inventory/post_adjustment-4459633.md) | frapid_db_user |  |
| 71 | [post_opening_inventory(_office_id integer, _user_id integer, _login_id bigint, _value_date date, _book_date date, _reference_number character varying, _statement_reference text, _details inventory.opening_stock_type[])RETURNS bigint](../functions/inventory/post_opening_inventory-4459635.md) | frapid_db_user |  |
| 72 | [post_transfer(_office_id integer, _user_id integer, _login_id bigint, _value_date date, _book_date date, _reference_number character varying, _statement_reference text, _details inventory.transfer_type[])RETURNS bigint](../functions/inventory/post_transfer-4459636.md) | frapid_db_user |  |



**Triggers**

| # | Trigger | Owner | Description |
| --- | --- | --- | --- |
| 1 | [customer_after_insert_trigger()RETURNS TRIGGER](../functions/inventory/customer_after_insert_trigger-4459684.md) | frapid_db_user |  |
| 2 | [items_unit_check_trigger()RETURNS TRIGGER](../functions/inventory/items_unit_check_trigger-4459686.md) | frapid_db_user |  |
| 3 | [supplier_after_insert_trigger()RETURNS TRIGGER](../functions/inventory/supplier_after_insert_trigger-4459688.md) | frapid_db_user |  |



**Types**

| # | Type | Base Type | Owner | Collation | Default | Type | StoreType | NotNull | Description |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| 1 | [adjustment_type](../types/inventory/adjustment_type.md) | - | frapid_db_user |  |  | Composite Type | Compressed Inline/Seconary | False |  |
| 2 | [checkout_detail_type](../types/inventory/checkout_detail_type.md) | - | frapid_db_user |  |  | Composite Type | Compressed Inline/Seconary | False |  |
| 3 | [opening_stock_type](../types/inventory/opening_stock_type.md) | - | frapid_db_user |  |  | Composite Type | Compressed Inline/Seconary | False |  |
| 4 | [transfer_type](../types/inventory/transfer_type.md) | - | frapid_db_user |  |  | Composite Type | Compressed Inline/Seconary | False |  |


### Related Contents
* [Schema List](../schemas.md)
* [Table of Contents](../../README.md)