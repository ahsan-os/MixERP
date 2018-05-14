# inventory.get_mavcogs function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.get_mavcogs(_item_id integer, _store_id integer, _base_quantity numeric, _factor numeric)
RETURNS numeric
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : get_mavcogs
* Arguments : _item_id integer, _store_id integer, _base_quantity numeric, _factor numeric
* Owner : frapid_db_user
* Result Type : numeric
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.get_mavcogs(_item_id integer, _store_id integer, _base_quantity numeric, _factor numeric)
 RETURNS numeric
 LANGUAGE plpgsql
AS $function$
    DECLARE _base_unit_cost money_strict;
BEGIN
    CREATE TEMPORARY TABLE temp_staging
    (
            id              SERIAL NOT NULL,
            value_date      date,
            audit_ts        TIMESTAMP WITH TIME ZONE,
            base_quantity   decimal(30, 6),
            price           decimal(30, 6)
            
    ) ON COMMIT DROP;


    INSERT INTO temp_staging(value_date, audit_ts, base_quantity, price)
    SELECT value_date, audit_ts, 
    CASE WHEN tran_type = 'Dr' THEN
    base_quantity ELSE base_quantity  * -1 END, 
    CASE WHEN tran_type = 'Dr' THEN
    (price * quantity/base_quantity)
    ELSE
    0
    END
    FROM inventory.verified_checkout_details_view
    WHERE item_id = $1
    AND store_id=$2
    order by value_date, audit_ts, checkout_detail_id;




    WITH RECURSIVE stock_transaction(id, base_quantity, price, sum_m, sum_base_quantity, last_id) AS 
    (
      SELECT id, base_quantity, price, base_quantity * price, base_quantity, id
      FROM temp_staging WHERE id = 1
      UNION ALL
      SELECT child.id, child.base_quantity, 
             CASE WHEN child.base_quantity < 0 then parent.sum_m / parent.sum_base_quantity ELSE child.price END, 
             parent.sum_m + CASE WHEN child.base_quantity < 0 then parent.sum_m / parent.sum_base_quantity ELSE child.price END * child.base_quantity,
             parent.sum_base_quantity + child.base_quantity,
             child.id 
      FROM temp_staging child JOIN stock_transaction parent on child.id = parent.last_id + 1
    )

    SELECT 
            --base_quantity,                                                        --left for debuging purpose
            --price,                                                                --left for debuging purpose
            --base_quantity * price AS amount,                                      --left for debuging purpose
            --SUM(base_quantity * price) OVER(ORDER BY id) AS cv_amount,            --left for debuging purpose
            --SUM(base_quantity) OVER(ORDER BY id) AS cv_quantity,                  --left for debuging purpose
            SUM(base_quantity * price) OVER(ORDER BY id)  / SUM(base_quantity) OVER(ORDER BY id) INTO _base_unit_cost
    FROM stock_transaction
    ORDER BY id DESC
    LIMIT 1;

    RETURN _base_unit_cost * _factor * _base_quantity;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

