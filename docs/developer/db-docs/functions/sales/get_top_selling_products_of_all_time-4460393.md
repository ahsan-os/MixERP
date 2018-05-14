# sales.get_top_selling_products_of_all_time function:

```plpgsql
CREATE OR REPLACE FUNCTION sales.get_top_selling_products_of_all_time(_office_id integer)
RETURNS TABLE(id integer, item_id integer, item_code text, item_name text, total_sales numeric)
```
* Schema : [sales](../../schemas/sales.md)
* Function Name : get_top_selling_products_of_all_time
* Arguments : _office_id integer
* Owner : frapid_db_user
* Result Type : TABLE(id integer, item_id integer, item_code text, item_name text, total_sales numeric)
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION sales.get_top_selling_products_of_all_time(_office_id integer)
 RETURNS TABLE(id integer, item_id integer, item_code text, item_name text, total_sales numeric)
 LANGUAGE plpgsql
AS $function$
BEGIN
    CREATE TEMPORARY TABLE IF NOT EXISTS top_selling_products_of_all_time
    (
        id              integer,
        item_id         integer,
        item_code       text,
        item_name       text,
        total_sales     numeric
    ) ON COMMIT DROP;

    INSERT INTO top_selling_products_of_all_time(id, item_id, total_sales)
    SELECT ROW_NUMBER() OVER(), *
    FROM
    (
        SELECT         
                inventory.verified_checkout_view.item_id, 
                SUM((price * quantity) - discount + tax) AS sales_amount
        FROM inventory.verified_checkout_view
        WHERE inventory.verified_checkout_view.office_id = _office_id
        AND inventory.verified_checkout_view.book ILIKE 'sales%'
        GROUP BY inventory.verified_checkout_view.item_id
        ORDER BY 2 DESC
        LIMIT 10
    ) t;

    UPDATE top_selling_products_of_all_time AS t
    SET 
        item_code = inventory.items.item_code,
        item_name = inventory.items.item_name
    FROM inventory.items
    WHERE t.item_id = inventory.items.item_id;
    
    RETURN QUERY
    SELECT * FROM top_selling_products_of_all_time;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

