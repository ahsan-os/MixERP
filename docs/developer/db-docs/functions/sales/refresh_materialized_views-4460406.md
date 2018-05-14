# sales.refresh_materialized_views function:

```plpgsql
CREATE OR REPLACE FUNCTION sales.refresh_materialized_views(_user_id integer, _login_id bigint, _office_id integer, _value_date date)
RETURNS void
```
* Schema : [sales](../../schemas/sales.md)
* Function Name : refresh_materialized_views
* Arguments : _user_id integer, _login_id bigint, _office_id integer, _value_date date
* Owner : frapid_db_user
* Result Type : void
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION sales.refresh_materialized_views(_user_id integer, _login_id bigint, _office_id integer, _value_date date)
 RETURNS void
 LANGUAGE plpgsql
AS $function$
BEGIN
    REFRESH MATERIALIZED VIEW finance.trial_balance_view;
    REFRESH MATERIALIZED VIEW inventory.verified_checkout_view;
    REFRESH MATERIALIZED VIEW finance.verified_transaction_mat_view;
    REFRESH MATERIALIZED VIEW finance.verified_cash_transaction_mat_view;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

