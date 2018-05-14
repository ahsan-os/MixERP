# finance.frequency_setup_scrud_view view

| Schema | [finance](../../schemas/finance.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | frequency_setup_scrud_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW finance.frequency_setup_scrud_view
 AS
 SELECT frequency_setups.frequency_setup_id,
    frequency_setups.fiscal_year_code,
    frequency_setups.frequency_setup_code,
    frequency_setups.value_date,
    frequencies.frequency_code,
    core.get_office_name_by_office_id(frequency_setups.office_id) AS office
   FROM finance.frequency_setups
     JOIN finance.frequencies ON frequencies.frequency_id = frequency_setups.frequency_id
  WHERE NOT frequency_setups.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

