# inventory.get_cost_of_goods_sold function:

```plpgsql
CREATE OR REPLACE FUNCTION inventory.get_cost_of_goods_sold(_item_id integer, _unit_id integer, _store_id integer, _quantity numeric)
RETURNS money_strict
```
* Schema : [inventory](../../schemas/inventory.md)
* Function Name : get_cost_of_goods_sold
* Arguments : _item_id integer, _unit_id integer, _store_id integer, _quantity numeric
* Owner : frapid_db_user
* Result Type : money_strict
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION inventory.get_cost_of_goods_sold(_item_id integer, _unit_id integer, _store_id integer, _quantity numeric)
 RETURNS money_strict
 LANGUAGE plpgsql
AS $function$
    DECLARE _backup_quantity            decimal(30, 6);
    DECLARE _base_quantity              decimal(30, 6);
    DECLARE _base_unit_id               integer;
    DECLARE _base_unit_cost             money_strict;
    DECLARE _total_sold                 integer;
    DECLARE _office_id                  integer = inventory.get_office_id_by_store_id($3);
    DECLARE _method                     text = inventory.get_cost_of_good_method(_office_id);
BEGIN
    --backup base quantity in decimal(30, 6)
    _backup_quantity                := inventory.get_base_quantity_by_unit_id($2, $4);
    --convert base quantity to whole number
    _base_quantity                  := CEILING(_backup_quantity);
    _base_unit_id                   := inventory.get_root_unit_id($2);
        
    IF(_method = 'MAVCO') THEN
        --RAISE NOTICE '% % % %',_item_id, _store_id, _base_quantity, 1.00;
        RETURN transactions.get_mavcogs(_item_id, _store_id, _base_quantity, 1.00);
    END IF; 


    SELECT COALESCE(SUM(base_quantity), 0) INTO _total_sold
    FROM inventory.verified_checkout_details_view
    WHERE transaction_type='Cr'
    AND item_id = _item_id;

    DROP TABLE IF EXISTS temp_cost_of_goods_sold;
    CREATE TEMPORARY TABLE temp_cost_of_goods_sold
    (
        id                     BIGSERIAL,
        checkout_detail_id     bigint,
        audit_ts               TIMESTAMP WITH TIME ZONE,
        value_date             date,
        price                  money_strict,
        transaction_type       text
                    
    ) ON COMMIT DROP;


    /*TODO:
    ALTERNATIVE AND MUCH EFFICIENT APPROACH
        SELECT
            *,
            (
                SELECT SUM(base_quantity)
                FROM inventory.verified_checkout_details_view AS i
                WHERE i.checkout_detail_id <= v.checkout_detail_id
                AND item_id = 1
            ) AS total
        FROM inventory.verified_checkout_details_view AS v
        WHERE item_id = 1
        ORDER BY value_date, checkout_id;
    */
    WITH stock_cte AS
    (
        SELECT
            checkout_detail_id, 
            audit_ts,
            value_date,
            generate_series(1, base_quantity::integer) AS series,
            (price * quantity) / base_quantity AS price,
            transaction_type
        FROM inventory.verified_checkout_details_view
        WHERE item_id = $1
        AND store_id = $3
    )
        
    INSERT INTO temp_cost_of_goods_sold(checkout_detail_id, audit_ts, value_date, price, transaction_type)
    SELECT checkout_detail_id, audit_ts, value_date, price, transaction_type FROM stock_cte
    ORDER BY value_date, audit_ts, checkout_detail_id;


    IF(_method = 'LIFO') THEN
        SELECT SUM(price) INTO _base_unit_cost
        FROM 
        (
            SELECT price
            FROM temp_cost_of_goods_sold
            WHERE transaction_type ='Dr'
            ORDER BY id DESC
            OFFSET _total_sold
            LIMIT _base_quantity
        ) S;
    ELSIF (_method = 'FIFO') THEN
        SELECT SUM(price) INTO _base_unit_cost
        FROM 
        (
            SELECT price
            FROM temp_cost_of_goods_sold
            WHERE transaction_type ='Dr'
            ORDER BY id
            OFFSET _total_sold
            LIMIT _base_quantity
        ) S;
    ELSIF (_method != 'MAVCO') THEN
        RAISE EXCEPTION 'Invalid configuration: COGS method.'
        USING ERRCODE='P6010';
    END IF;

    --APPLY decimal(30, 6) QUANTITY PROVISON
    _base_unit_cost := _base_unit_cost * (_backup_quantity / _base_quantity);

    RETURN _base_unit_cost;
END
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

