# finance.perform_eod_operation function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.perform_eod_operation(_user_id integer, _login_id bigint, _office_id integer, _value_date date)
RETURNS boolean
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : perform_eod_operation
* Arguments : _user_id integer, _login_id bigint, _office_id integer, _value_date date
* Owner : frapid_db_user
* Result Type : boolean
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.perform_eod_operation(_user_id integer, _login_id bigint, _office_id integer, _value_date date)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$
    DECLARE _routine            regproc;
    DECLARE _routine_id         integer;
    DECLARE this                RECORD;
    DECLARE _sql                text;
    DECLARE _is_error           boolean=false;
    DECLARE _notice             text;
    DECLARE _office_code        text;
BEGIN
    IF(_value_date IS NULL) THEN
        RAISE EXCEPTION 'Invalid date.'
        USING ERRCODE='P3008';
    END IF;

    IF(NOT account.is_admin(_user_id)) THEN
        RAISE EXCEPTION 'Access is denied.'
        USING ERRCODE='P9001';
    END IF;

    IF(_value_date != finance.get_value_date(_office_id)) THEN
        RAISE EXCEPTION 'Invalid value date.'
        USING ERRCODE='P3007';
    END IF;

    SELECT * FROM finance.day_operation
    WHERE value_date=_value_date 
    AND office_id = _office_id INTO this;

    IF(this IS NULL) THEN
        RAISE EXCEPTION 'Invalid value date.'
        USING ERRCODE='P3007';
    ELSE    
        IF(this.completed OR this.completed_on IS NOT NULL) THEN
            RAISE EXCEPTION 'End of day operation was already performed.'
            USING ERRCODE='P5102';
            _is_error        := true;
        END IF;
    END IF;

    IF EXISTS
    (
        SELECT * FROM finance.transaction_master
        WHERE value_date < _value_date
        AND verification_status_id = 0
    ) THEN
        RAISE EXCEPTION 'Past dated transactions in verification queue.'
        USING ERRCODE='P5103';
        _is_error        := true;
    END IF;

    IF EXISTS
    (
        SELECT * FROM finance.transaction_master
        WHERE value_date = _value_date
        AND verification_status_id = 0
    ) THEN
        RAISE EXCEPTION 'Please verify transactions before performing end of day operation.'
        USING ERRCODE='P5104';
        _is_error        := true;
    END IF;
    
    IF(NOT _is_error) THEN
        _office_code        := core.get_office_code_by_office_id(_office_id);
        _notice             := 'EOD started.'::text;
        RAISE INFO  '%', _notice;

        FOR this IN
        SELECT routine_id, routine_name 
        FROM finance.routines 
        WHERE status 
        ORDER BY "order" ASC
        LOOP
            _routine_id             := this.routine_id;
            _routine                := this.routine_name;
            _sql                    := format('SELECT * FROM %1$s($1, $2, $3, $4);', _routine);

            RAISE NOTICE '%', _sql;

            _notice             := 'Performing ' || _routine::text || '.';
            RAISE INFO '%', _notice;

            PERFORM pg_sleep(5);
            EXECUTE _sql USING _user_id, _login_id, _office_id, _value_date;

            _notice             := 'Completed  ' || _routine::text || '.';
            RAISE INFO '%', _notice;
            
            PERFORM pg_sleep(5);            
        END LOOP;


        UPDATE finance.day_operation SET 
            completed_on = NOW(), 
            completed_by = _user_id,
            completed = true
        WHERE value_date=_value_date
        AND office_id = _office_id;

        _notice             := 'EOD of ' || _office_code || ' for ' || _value_date::text || ' completed without errors.'::text;
        RAISE INFO '%', _notice;

        _notice             := 'OK'::text;
        RAISE INFO '%', _notice;

        RETURN true;
    END IF;

    RETURN false;    
END;
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

