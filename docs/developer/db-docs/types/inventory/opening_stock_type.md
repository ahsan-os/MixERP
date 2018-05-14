# inventory.opening_stock_type type

| Schema | [inventory](../../schemas/inventory.md) |
| ------ | ----------------------------------------------- |
| Type Name | opening_stock_type |
| Base Type | - |
| Owner | frapid_db_user |
| Collation |  |
| Default |  |
| Type | Composite Type |
| Store Type | Compressed Inline/Seconary |
| Not Null | False |
| Description |  |

Type Definition:

```plpgsql
CREATE TYPE inventory.opening_stock_type AS
(
&nbsp;&nbsp;&nbsp;&nbsp;store_id integer,
&nbsp;&nbsp;&nbsp;&nbsp;item_id integer,
&nbsp;&nbsp;&nbsp;&nbsp;quantity decimal_strict,
&nbsp;&nbsp;&nbsp;&nbsp;unit_id integer,
&nbsp;&nbsp;&nbsp;&nbsp;price money_strict
);
```


### Related Contents
* [Schema List](../../schemas.md)
* [Type List](../../types.md)
* [Table of Contents](../../README.md)

