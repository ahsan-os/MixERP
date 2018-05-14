# sales.sales_detail_type type

| Schema | [sales](../../schemas/sales.md) |
| ------ | ----------------------------------------------- |
| Type Name | sales_detail_type |
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
CREATE TYPE sales.sales_detail_type AS
(
&nbsp;&nbsp;&nbsp;&nbsp;store_id integer,
&nbsp;&nbsp;&nbsp;&nbsp;transaction_type character varying(6),
&nbsp;&nbsp;&nbsp;&nbsp;item_id integer,
&nbsp;&nbsp;&nbsp;&nbsp;quantity decimal_strict,
&nbsp;&nbsp;&nbsp;&nbsp;unit_id integer,
&nbsp;&nbsp;&nbsp;&nbsp;price money_strict,
&nbsp;&nbsp;&nbsp;&nbsp;discount_rate money_strict2,
&nbsp;&nbsp;&nbsp;&nbsp;tax money_strict2,
&nbsp;&nbsp;&nbsp;&nbsp;shipping_charge money_strict2
);
```


### Related Contents
* [Schema List](../../schemas.md)
* [Type List](../../types.md)
* [Table of Contents](../../README.md)

