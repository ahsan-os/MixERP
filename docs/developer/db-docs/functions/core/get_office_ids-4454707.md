# core.get_office_ids function:

```plpgsql
CREATE OR REPLACE FUNCTION core.get_office_ids(root_office_id integer)
RETURNS SETOF integer
```
* Schema : [core](../../schemas/core.md)
* Function Name : get_office_ids
* Arguments : root_office_id integer
* Owner : frapid_db_user
* Result Type : SETOF integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION core.get_office_ids(root_office_id integer)
 RETURNS SETOF integer
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN QUERY 
    (
        WITH RECURSIVE office_cte(office_id, path) AS (
         SELECT
            tn.office_id,  tn.office_id::TEXT AS path
            FROM core.offices AS tn 
			WHERE tn.office_id =$1
			AND NOT tn.deleted
        UNION ALL
         SELECT
            c.office_id, (p.path || '->' || c.office_id::TEXT)
            FROM office_cte AS p, core.offices AS c 
			WHERE parent_office_id = p.office_id
        )

        SELECT office_id 
		FROM office_cte
    );
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

