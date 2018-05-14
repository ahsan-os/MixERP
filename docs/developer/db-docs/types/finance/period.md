# finance.period type

| Schema | [finance](../../schemas/finance.md) |
| ------ | ----------------------------------------------- |
| Type Name | period |
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
CREATE TYPE finance.period AS
(
&nbsp;&nbsp;&nbsp;&nbsp;period_name text,
&nbsp;&nbsp;&nbsp;&nbsp;date_from date,
&nbsp;&nbsp;&nbsp;&nbsp;date_to date
);
```


### Related Contents
* [Schema List](../../schemas.md)
* [Type List](../../types.md)
* [Table of Contents](../../README.md)

