# finance.get_exchange_rate function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.get_exchange_rate(office_id integer, currency_code character varying)
RETURNS decimal_strict2
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : get_exchange_rate
* Arguments : office_id integer, currency_code character varying
* Owner : frapid_db_user
* Result Type : decimal_strict2
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.get_exchange_rate(office_id integer, currency_code character varying)
 RETURNS decimal_strict2
 LANGUAGE plpgsql
AS $function$
    DECLARE _local_currency_code national character varying(12)= '';
    DECLARE _unit integer_strict2 = 0;
    DECLARE _exchange_rate decimal_strict2=0;
BEGIN
    SELECT core.offices.currency_code
    INTO _local_currency_code
    FROM core.offices
    WHERE core.offices.office_id=$1
	AND NOT core.offices.deleted;

    IF(_local_currency_code = $2) THEN
        RETURN 1;
    END IF;

    SELECT unit, exchange_rate
    INTO _unit, _exchange_rate
    FROM finance.exchange_rate_details
    INNER JOIN finance.exchange_rates
    ON finance.exchange_rate_details.exchange_rate_id = finance.exchange_rates.exchange_rate_id
    WHERE finance.exchange_rates.office_id=$1
    AND foreign_currency_code=$2;

    IF(_unit = 0) THEN
        RETURN 0;
    END IF;
    
    RETURN _exchange_rate/_unit;    
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

