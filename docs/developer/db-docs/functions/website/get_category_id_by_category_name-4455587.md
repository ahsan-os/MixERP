# website.get_category_id_by_category_name function:

```plpgsql
CREATE OR REPLACE FUNCTION website.get_category_id_by_category_name(_category_name text)
RETURNS integer
```
* Schema : [website](../../schemas/website.md)
* Function Name : get_category_id_by_category_name
* Arguments : _category_name text
* Owner : frapid_db_user
* Result Type : integer
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION website.get_category_id_by_category_name(_category_name text)
 RETURNS integer
 LANGUAGE plpgsql
AS $function$
BEGIN
    RETURN category_id
    FROM website.categories
    WHERE website.categories.category_name = _category_name
	AND NOT website.categories.deleted;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

