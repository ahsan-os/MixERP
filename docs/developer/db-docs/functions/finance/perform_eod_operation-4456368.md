# finance.perform_eod_operation function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.perform_eod_operation(_login_id bigint)
RETURNS boolean
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : perform_eod_operation
* Arguments : _login_id bigint
* Owner : frapid_db_user
* Result Type : boolean
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.perform_eod_operation(_login_id bigint)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
    DECLARE _user_id    integer;
    DECLARE _office_id integer;
    DECLARE _value_date date;
BEGIN
    SELECT 
        user_id,
        office_id,
        finance.get_value_date(office_id)
    INTO
        _user_id,
        _office_id,
        _value_date
    FROM account.logins
    WHERE login_id=_login_id;

    RETURN finance.perform_eod_operation(_user_id,_login_id, _office_id, _value_date);
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

