# inventory.convert_unit function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.convert_unit(from_unit integer, to_unit integer)
RETURNS numeric
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : convert_unit
* Arguments : from_unit integer, to_unit integer
* Owner : frapid_db_user
* Result Type : numeric
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.convert_unit(from_unit integer, to_unit integer)
 RETURNS numeric
 LANGUAGE plpgsql
 STABLE
AS $function$
    DECLARE _factor decimal(30, 6);
BEGIN
    IF(inventory.get_root_unit_id($1) != inventory.get_root_unit_id($2)) THEN
        RETURN 0;
    END IF;

    IF($1 = $2) THEN
        RETURN 1.00;
    END IF;
    
    IF(inventory.is_parent_unit($1, $2)) THEN
            WITH RECURSIVE unit_cte(unit_id, value) AS 
            (
                SELECT tn.compare_unit_id, tn.value
                FROM inventory.compound_units AS tn 
				WHERE tn.base_unit_id = $1
				AND NOT tn.deleted

                UNION ALL

                SELECT 
                c.compare_unit_id, c.value * p.value
                FROM unit_cte AS p, 
                inventory.compound_units AS c 
                WHERE base_unit_id = p.unit_id
            )
        SELECT 1.00/value INTO _factor
        FROM unit_cte
        WHERE unit_id=$2;
    ELSE
            WITH RECURSIVE unit_cte(unit_id, value) AS 
            (
             SELECT tn.compare_unit_id, tn.value
                FROM inventory.compound_units AS tn 
				WHERE tn.base_unit_id = $2
				AND NOT tn.deleted
            UNION ALL
             SELECT 
                c.compare_unit_id, c.value * p.value
                FROM unit_cte AS p, 
              inventory.compound_units AS c 
                WHERE base_unit_id = p.unit_id
            )

        SELECT value INTO _factor
        FROM unit_cte
        WHERE unit_id=$1;
    END IF;

    RETURN _factor;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

