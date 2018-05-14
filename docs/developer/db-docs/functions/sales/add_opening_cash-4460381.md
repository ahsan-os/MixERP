# sales.add_opening_cash function:

```plpgsql
CREATE OR REPLACE FUNCTION sales.add_opening_cash(_user_id integer, _transaction_date timestamp without time zone, _amount numeric, _provided_by character varying, _memo character varying)
RETURNS void
```
* Schema : [sales](../../schemas/sales.md)
* Function Name : add_opening_cash
* Arguments : _user_id integer, _transaction_date timestamp without time zone, _amount numeric, _provided_by character varying, _memo character varying
* Owner : frapid_db_user
* Result Type : void
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION sales.add_opening_cash(_user_id integer, _transaction_date timestamp without time zone, _amount numeric, _provided_by character varying, _memo character varying)
 RETURNS void
 LANGUAGE plpgsql
AS $function$
BEGIN
	IF NOT EXISTS
	(
		SELECT 1
		FROM sales.opening_cash
		WHERE user_id = _user_id
		AND transaction_date = _transaction_date
	) THEN
		INSERT INTO sales.opening_cash(user_id, transaction_date, amount, provided_by, memo, audit_user_id, audit_ts, deleted)
		SELECT _user_id, _transaction_date, _amount, _provided_by, _memo, _user_id, NOW(), false;
	ELSE
		UPDATE sales.opening_cash
		SET
			amount = _amount,
			provided_by = _provided_by,
			memo = _memo,
			user_id = _user_id,
			audit_user_id = _user_id,
			audit_ts = NOW(),
			deleted = false
		WHERE user_id = _user_id
		AND transaction_date = _transaction_date;
	END IF;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

