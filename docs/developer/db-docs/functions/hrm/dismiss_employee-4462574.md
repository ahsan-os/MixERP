# hrm.dismiss_employee function:

```plpgsql
CREATE OR REPLACE FUNCTION hrm.dismiss_employee()
RETURNS trigger
```
* Schema : [hrm](../../schemas/hrm.md)
* Function Name : dismiss_employee
* Arguments : 
* Owner : frapid_db_user
* Result Type : trigger
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION hrm.dismiss_employee()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
    DECLARE _service_end        date;
    DECLARE _new_status_id      integer;
BEGIN
    IF(hstore(NEW) ? 'change_status_to') THEN
        _new_status_id := NEW.change_status_to;
    END IF;

    IF(hstore(NEW) ? 'service_end_date') THEN
        _service_end := NEW.service_end_date;
    END IF;

    IF(_service_end = NULL) THEN
        IF(hstore(NEW) ? 'desired_resign_date') THEN
            _service_end := NEW.desired_resign_date;
        END IF;
    END IF;
    
    IF(NEW.verification_status_id > 0) THEN        
        UPDATE hrm.employees
        SET
            service_ended_on = _service_end
        WHERE employee_id = NEW.employee_id;

        IF(_new_status_id IS NOT NULL) THEN
            UPDATE hrm.employees
            SET
                current_employment_status_id = _new_status_id
            WHERE employee_id = NEW.employee_id;
        END IF;        
    END IF;

    RETURN NEW;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

