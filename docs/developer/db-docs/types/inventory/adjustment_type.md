# inventory.adjustment_type type

| Schema | [inventory](../../schemas/inventory.md) |
| ------ | ----------------------------------------------- |
| Type Name | adjustment_type |
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
CREATE TYPE inventory.adjustment_type AS
(
&nbsp;&nbsp;&nbsp;&nbsp;tran_type character varying(6),
&nbsp;&nbsp;&nbsp;&nbsp;item_code character varying(28),
&nbsp;&nbsp;&nbsp;&nbsp;unit_name character varying(504),
&nbsp;&nbsp;&nbsp;&nbsp;quantity decimal_strict
);
```


### Related Contents
* [Schema List](../../schemas.md)
* [Type List](../../types.md)
* [Table of Contents](../../README.md)

