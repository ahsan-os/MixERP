# inventory.is_parent_unit function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.is_parent_unit(parent integer, child integer)
RETURNS boolean
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : is_parent_unit
* Arguments : parent integer, child integer
* Owner : frapid_db_user
* Result Type : boolean
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.is_parent_unit(parent integer, child integer)
 RETURNS boolean
 LANGUAGE plpgsql
AS $function$      
BEGIN
    IF $1!=$2 THEN
        IF EXISTS
        (
            WITH RECURSIVE unit_cte(unit_id) AS 
            (
             SELECT tn.compare_unit_id
                FROM inventory.compound_units AS tn 
				WHERE tn.base_unit_id = $1
				AND NOT tn.deleted
            UNION ALL
             SELECT
                c.compare_unit_id
                FROM unit_cte AS p, 
              inventory.compound_units AS c 
                WHERE base_unit_id = p.unit_id
            )

            SELECT * FROM unit_cte
            WHERE unit_id=$2
        ) THEN
            RETURN TRUE;
        END IF;
    END IF;
    RETURN false;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

