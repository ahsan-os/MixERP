# hrm.get_employee_name_by_employee_id function:

```plpgsql
CREATE OR REPLACE FUNCTION hrm.get_employee_name_by_employee_id(_employee_id integer)
RETURNS text
```
* Schema : [hrm](../../schemas/hrm.md)
* Function Name : get_employee_name_by_employee_id
* Arguments : _employee_id integer
* Owner : frapid_db_user
* Result Type : text
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION hrm.get_employee_name_by_employee_id(_employee_id integer)
 RETURNS text
 LANGUAGE plpgsql
 STABLE
AS $function$
BEGIN
    RETURN
        employee_name
    FROM hrm.employees
    WHERE hrm.employees.employee_id = $1
    AND NOT hrm.employees.deleted;    
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

