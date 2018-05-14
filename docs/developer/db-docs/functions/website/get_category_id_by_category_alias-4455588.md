# website.get_category_id_by_category_alias function:

```plpgsql
CREATE OR REPLACE FUNCTION website.get_category_id_by_category_alias(_alias text)
RETURNS integer
```
* Schema : [website](../../schemas/website.md)
* Function Name : get_category_id_by_category_alias
* Arguments : _alias text
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION website.get_category_id_by_category_alias(_alias text)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN category_id
    FROM website.categories
    WHERE website.categories.alias = _alias
	AND NOT website.categories.deleted;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

