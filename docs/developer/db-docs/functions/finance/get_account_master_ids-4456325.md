# finance.get_account_master_ids function:

```plpgsql
CREATE OR REPLACE FUNCTION finance.get_account_master_ids(root_account_master_id integer)
RETURNS SETOF smallint
```
* Schema : [finance](../../schemas/finance.md)
* Function Name : get_account_master_ids
* Arguments : root_account_master_id integer
* Owner : frapid_db_user
* Result Type : SETOF smallint
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION finance.get_account_master_ids(root_account_master_id integer)
 RETURNS SETOF smallint
 LANGUAGE plpgsql
 STABLE
AS $function$
BEGIN
    RETURN QUERY 
    (
        WITH RECURSIVE account_cte(account_master_id, path) AS (
         SELECT
            tn.account_master_id,  tn.account_master_id::TEXT AS path
            FROM finance.account_masters AS tn 
			WHERE tn.account_master_id =$1
			AND NOT tn.deleted
        UNION ALL
         SELECT
            c.account_master_id, (p.path || '->' || c.account_master_id::TEXT)
            FROM account_cte AS p, finance.account_masters AS c WHERE parent_account_master_id = p.account_master_id
        )

        SELECT account_master_id FROM account_cte
    );
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

