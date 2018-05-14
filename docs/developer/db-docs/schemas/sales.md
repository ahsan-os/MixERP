# sales schema

**Tables**

| # | Table Name | Owner | Tablespace | Description |
| --- | --- | --- | --- | --- |
| 1 | [cashier_login_info](../tables/sales/cashier_login_info.md) | frapid_db_user | DEFAULT |  |
| 2 | [cashiers](../tables/sales/cashiers.md) | frapid_db_user | DEFAULT |  |
| 3 | [closing_cash](../tables/sales/closing_cash.md) | frapid_db_user | DEFAULT |  |
| 4 | [coupons](../tables/sales/coupons.md) | frapid_db_user | DEFAULT |  |
| 5 | [customer_receipts](../tables/sales/customer_receipts.md) | frapid_db_user | DEFAULT |  |
| 6 | [gift_card_transactions](../tables/sales/gift_card_transactions.md) | frapid_db_user | DEFAULT |  |
| 7 | [gift_cards](../tables/sales/gift_cards.md) | frapid_db_user | DEFAULT |  |
| 8 | [item_selling_prices](../tables/sales/item_selling_prices.md) | frapid_db_user | DEFAULT |  |
| 9 | [late_fee](../tables/sales/late_fee.md) | frapid_db_user | DEFAULT |  |
| 10 | [late_fee_postings](../tables/sales/late_fee_postings.md) | frapid_db_user | DEFAULT |  |
| 11 | [opening_cash](../tables/sales/opening_cash.md) | frapid_db_user | DEFAULT |  |
| 12 | [order_details](../tables/sales/order_details.md) | frapid_db_user | DEFAULT |  |
| 13 | [orders](../tables/sales/orders.md) | frapid_db_user | DEFAULT |  |
| 14 | [payment_terms](../tables/sales/payment_terms.md) | frapid_db_user | DEFAULT |  |
| 15 | [price_types](../tables/sales/price_types.md) | frapid_db_user | DEFAULT |  |
| 16 | [quotation_details](../tables/sales/quotation_details.md) | frapid_db_user | DEFAULT |  |
| 17 | [quotations](../tables/sales/quotations.md) | frapid_db_user | DEFAULT |  |
| 18 | [returns](../tables/sales/returns.md) | frapid_db_user | DEFAULT |  |
| 19 | [sales](../tables/sales/sales.md) | frapid_db_user | DEFAULT |  |



**Sequences**

| # | Sequence Name | Owner | Data Type | Start Value | Increment | Description |
| --- | --- | --- | --- | --- | --- | --- |
| 1 | cashiers_cashier_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 2 | closing_cash_closing_cash_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 3 | coupons_coupon_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 4 | customer_receipts_receipt_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 5 | gift_card_transactions_transaction_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 6 | gift_cards_gift_card_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 7 | item_selling_prices_item_selling_price_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 8 | late_fee_late_fee_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 9 | opening_cash_opening_cash_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 10 | order_details_order_detail_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 11 | orders_order_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 12 | payment_terms_payment_term_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 13 | price_types_price_type_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 14 | quotation_details_quotation_detail_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 15 | quotations_quotation_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 16 | returns_return_id_seq | frapid_db_user | bigint | 1 | 1 |  |
| 17 | sales_sales_id_seq | frapid_db_user | bigint | 1 | 1 |  |


**Views**

| # | View Name | Owner | Tablespace | Description |
| --- | --- | --- | --- | --- |
| 1 | [cashier_scrud_view](../views/sales/cashier_scrud_view.md) | frapid_db_user | DEFAULT |  |
| 2 | [coupon_view](../views/sales/coupon_view.md) | frapid_db_user | DEFAULT |  |
| 3 | [gift_card_search_view](../views/sales/gift_card_search_view.md) | frapid_db_user | DEFAULT |  |
| 4 | [gift_card_transaction_view](../views/sales/gift_card_transaction_view.md) | frapid_db_user | DEFAULT |  |
| 5 | [item_selling_price_scrud_view](../views/sales/item_selling_price_scrud_view.md) | frapid_db_user | DEFAULT |  |
| 6 | [item_view](../views/sales/item_view.md) | frapid_db_user | DEFAULT |  |
| 7 | [payment_term_scrud_view](../views/sales/payment_term_scrud_view.md) | frapid_db_user | DEFAULT |  |
| 8 | [sales_view](../views/sales/sales_view.md) | frapid_db_user | DEFAULT |  |



**Materialized Views**

| # | Matview Name | Owner | Tablespace | Description |
| --- | --- | --- | --- | --- |



**Functions**

| # | Function | Owner | Description |
| --- | --- | --- | --- |
| 1 | [add_gift_card_fund(_user_id integer, _office_id integer, _login_id bigint, _gift_card_id integer, _value_date date, _book_date date, _debit_account_id integer, _amount money_strict, _cost_center_id integer, _reference_number character varying, _statement_reference character varying)RETURNS bigint](../functions/sales/add_gift_card_fund-4460380.md) | frapid_db_user |  |
| 2 | [add_opening_cash(_user_id integer, _transaction_date timestamp without time zone, _amount numeric, _provided_by character varying, _memo character varying)RETURNS void](../functions/sales/add_opening_cash-4460381.md) | frapid_db_user |  |
| 3 | [get_account_receivables_report(_office_id integer, _from date)RETURNS TABLE(office_id integer, office_name character varying, account_id integer, account_number character varying, account_name character varying, previous_period numeric, current_period numeric, total_amount numeric)](../functions/sales/get_account_receivables_report-4460410.md) | frapid_db_user |  |
| 4 | [get_active_coupon_id_by_coupon_code(_coupon_code character varying)RETURNS integer](../functions/sales/get_active_coupon_id_by_coupon_code-4460382.md) | frapid_db_user |  |
| 5 | [get_avaiable_coupons_to_print(_tran_id bigint)RETURNS TABLE(coupon_id integer)](../functions/sales/get_avaiable_coupons_to_print-4460383.md) | frapid_db_user |  |
| 6 | [get_gift_card_balance(_gift_card_id integer, _value_date date)RETURNS numeric](../functions/sales/get_gift_card_balance-4460384.md) | frapid_db_user |  |
| 7 | [get_gift_card_id_by_gift_card_number(_gift_card_number character varying)RETURNS integer](../functions/sales/get_gift_card_id_by_gift_card_number-4460385.md) | frapid_db_user |  |
| 8 | [get_item_selling_price(_item_id integer, _customer_type_id integer, _price_type_id integer, _unit_id integer)RETURNS money_strict2](../functions/sales/get_item_selling_price-4460386.md) | frapid_db_user |  |
| 9 | [get_late_fee_id_by_late_fee_code(_late_fee_code character varying)RETURNS integer](../functions/sales/get_late_fee_id_by_late_fee_code-4460387.md) | frapid_db_user |  |
| 10 | [get_order_view(_user_id integer, _office_id integer, _customer character varying, _from date, _to date, _expected_from date, _expected_to date, _id bigint, _reference_number character varying, _internal_memo character varying, _terms character varying, _posted_by character varying, _office character varying)RETURNS TABLE(id bigint, customer character varying, value_date date, expected_date date, reference_number character varying, terms character varying, internal_memo character varying, posted_by character varying, office character varying, transaction_ts timestamp with time zone)](../functions/sales/get_order_view-4460388.md) | frapid_db_user |  |
| 11 | [get_payable_account_for_gift_card(_gift_card_id integer)RETURNS integer](../functions/sales/get_payable_account_for_gift_card-4460389.md) | frapid_db_user |  |
| 12 | [get_payable_account_id_by_gift_card_id(_gift_card_id integer)RETURNS integer](../functions/sales/get_payable_account_id_by_gift_card_id-4460390.md) | frapid_db_user |  |
| 13 | [get_quotation_view(_user_id integer, _office_id integer, _customer character varying, _from date, _to date, _expected_from date, _expected_to date, _id bigint, _reference_number character varying, _internal_memo character varying, _terms character varying, _posted_by character varying, _office character varying)RETURNS TABLE(id bigint, customer character varying, value_date date, expected_date date, reference_number character varying, terms character varying, internal_memo character varying, posted_by character varying, office character varying, transaction_ts timestamp with time zone)](../functions/sales/get_quotation_view-4460391.md) | frapid_db_user |  |
| 14 | [get_receivable_account_for_check_receipts(_store_id integer)RETURNS integer](../functions/sales/get_receivable_account_for_check_receipts-4460392.md) | frapid_db_user |  |
| 15 | [get_top_selling_products_of_all_time(_office_id integer)RETURNS TABLE(id integer, item_id integer, item_code text, item_name text, total_sales numeric)](../functions/sales/get_top_selling_products_of_all_time-4460393.md) | frapid_db_user |  |
| 16 | [post_cash_receipt(_user_id integer, _office_id integer, _login_id bigint, _customer_id integer, _customer_account_id integer, _currency_code character varying, _local_currency_code character varying, _base_currency_code character varying, _exchange_rate_debit decimal_strict, _exchange_rate_credit decimal_strict, _reference_number character varying, _statement_reference character varying, _cost_center_id integer, _cash_account_id integer, _cash_repository_id integer, _value_date date, _book_date date, _receivable money_strict2, _tender money_strict2, _change money_strict2, _cascading_tran_id bigint)RETURNS bigint](../functions/sales/post_cash_receipt-4460394.md) | frapid_db_user |  |
| 17 | [post_check_receipt(_user_id integer, _office_id integer, _login_id bigint, _customer_id integer, _customer_account_id integer, _receivable_account_id integer, _currency_code character varying, _local_currency_code character varying, _base_currency_code character varying, _exchange_rate_debit decimal_strict, _exchange_rate_credit decimal_strict, _reference_number character varying, _statement_reference character varying, _cost_center_id integer, _value_date date, _book_date date, _check_amount money_strict2, _check_bank_name character varying, _check_number character varying, _check_date date, _cascading_tran_id bigint)RETURNS bigint](../functions/sales/post_check_receipt-4460395.md) | frapid_db_user |  |
| 18 | [post_customer_receipt(_user_id integer, _office_id integer, _login_id bigint, _customer_id integer, _currency_code character varying, _cash_account_id integer, _amount money_strict, _exchange_rate_debit decimal_strict, _exchange_rate_credit decimal_strict, _reference_number character varying, _statement_reference character varying, _cost_center_id integer, _cash_repository_id integer, _posted_date date, _bank_account_id integer, _payment_card_id integer, _bank_instrument_code character varying, _bank_tran_code character varying)RETURNS bigint](../functions/sales/post_customer_receipt-4460396.md) | frapid_db_user |  |
| 19 | [post_late_fee(_user_id integer, _login_id bigint, _office_id integer, _value_date date)RETURNS void](../functions/sales/post_late_fee-4460398.md) | frapid_db_user |  |
| 20 | [post_receipt(_user_id integer, _office_id integer, _login_id bigint, _customer_id integer, _currency_code character varying, _exchange_rate_debit decimal_strict, _exchange_rate_credit decimal_strict, _reference_number character varying, _statement_reference text, _cost_center_id integer, _cash_account_id integer, _cash_repository_id integer, _value_date date, _book_date date, _receipt_amount money_strict, _tender money_strict2, _change money_strict2, _check_amount money_strict2, _check_bank_name character varying, _check_number character varying, _check_date date, _gift_card_number character varying, _store_id integer DEFAULT NULL::integer, _cascading_tran_id bigint DEFAULT NULL::bigint)RETURNS bigint](../functions/sales/post_receipt-4460400.md) | frapid_db_user |  |
| 21 | [post_receipt_by_gift_card(_user_id integer, _office_id integer, _login_id bigint, _customer_id integer, _customer_account_id integer, _currency_code character varying, _local_currency_code character varying, _base_currency_code character varying, _exchange_rate_debit decimal_strict, _exchange_rate_credit decimal_strict, _reference_number character varying, _statement_reference character varying, _cost_center_id integer, _value_date date, _book_date date, _gift_card_id integer, _gift_card_number character varying, _amount money_strict, _cascading_tran_id bigint)RETURNS bigint](../functions/sales/post_receipt_by_gift_card-4460401.md) | frapid_db_user |  |
| 22 | [post_return(_transaction_master_id bigint, _office_id integer, _user_id integer, _login_id bigint, _value_date date, _book_date date, _store_id integer, _counter_id integer, _customer_id integer, _price_type_id integer, _reference_number character varying, _statement_reference text, _details sales.sales_detail_type[])RETURNS bigint](../functions/sales/post_return-4460402.md) | frapid_db_user |  |
| 23 | [post_sales(_office_id integer, _user_id integer, _login_id bigint, _counter_id integer, _value_date date, _book_date date, _cost_center_id integer, _reference_number character varying, _statement_reference text, _tender money_strict2, _change money_strict2, _payment_term_id integer, _check_amount money_strict2, _check_bank_name character varying, _check_number character varying, _check_date date, _gift_card_number character varying, _customer_id integer, _price_type_id integer, _shipper_id integer, _store_id integer, _coupon_code character varying, _is_flat_discount boolean, _discount money_strict2, _details sales.sales_detail_type[], _sales_quotation_id bigint, _sales_order_id bigint)RETURNS bigint](../functions/sales/post_sales-4460404.md) | frapid_db_user |  |
| 24 | [refresh_materialized_views(_user_id integer, _login_id bigint, _office_id integer, _value_date date)RETURNS void](../functions/sales/refresh_materialized_views-4460406.md) | frapid_db_user |  |
| 25 | [settle_customer_due(_customer_id integer, _office_id integer)RETURNS void](../functions/sales/settle_customer_due-4460407.md) | frapid_db_user |  |
| 26 | [validate_items_for_return(_transaction_master_id bigint, _details sales.sales_detail_type[])RETURNS boolean](../functions/sales/validate_items_for_return-4460408.md) | frapid_db_user |  |



**Triggers**

| # | Trigger | Owner | Description |
| --- | --- | --- | --- |



**Types**

| # | Type | Base Type | Owner | Collation | Default | Type | StoreType | NotNull | Description |
| --- | --- | --- | --- | --- | --- | --- | --- | --- | --- |
| 1 | [sales_detail_type](../types/sales/sales_detail_type.md) | - | frapid_db_user |  |  | Composite Type | Compressed Inline/Seconary | False |  |


### Related Contents
* [Schema List](../schemas.md)
* [Table of Contents](../../README.md)