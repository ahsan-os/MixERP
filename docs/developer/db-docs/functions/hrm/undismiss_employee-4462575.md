# hrm.undismiss_employee function:

```plpgsql
CREATE OR REPLACE FUNCTION hrm.undismiss_employee()
RETURNS trigger
```
* Schema : [hrm](../../schemas/hrm.md)
* Function Name : undismiss_employee
* Arguments : 
* Owner : frapid_db_user
* Result Type : trigger
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION hrm.undismiss_employee()
 RETURNS trigger
 LANGUAGE plpgsql
AS $function$
BEGIN
    UPDATE hrm.employees
    SET
        service_ended_on = NULL
    WHERE employee_id = OLD.employee_id;

    RETURN OLD;    
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

