# sales.get_late_fee_id_by_late_fee_code function:

```plpgsql
CREATE OR REPLACE FUNCTION sales.get_late_fee_id_by_late_fee_code(_late_fee_code character varying)
RETURNS integer
```
* Schema : [sales](../../schemas/sales.md)
* Function Name : get_late_fee_id_by_late_fee_code
* Arguments : _late_fee_code character varying
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION sales.get_late_fee_id_by_late_fee_code(_late_fee_code character varying)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN sales.late_fee.late_fee_id
    FROM sales.late_fee
    WHERE sales.late_fee.late_fee_code = _late_fee_code
    AND NOT sales.late_fee.deleted;    
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

