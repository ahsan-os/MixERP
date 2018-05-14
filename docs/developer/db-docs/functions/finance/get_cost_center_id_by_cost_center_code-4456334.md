# finance.get_cost_center_id_by_cost_center_code function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.get_cost_center_id_by_cost_center_code(text)
RETURNS integer
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : get_cost_center_id_by_cost_center_code
* Arguments : text
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.get_cost_center_id_by_cost_center_code(text)
 RETURNS integer
 LANGUAGE plpgsql
 STABLE
AS $function$
BEGIN
    RETURN cost_center_id
    FROM finance.cost_centers
    WHERE finance.cost_centers.cost_center_code=$1
	AND NOT finance.cost_centers.deleted;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

