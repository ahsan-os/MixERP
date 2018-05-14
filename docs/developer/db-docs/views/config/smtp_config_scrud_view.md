# config.smtp_config_scrud_view view

| Schema | [config](../../schemas/config.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | smtp_config_scrud_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW config.smtp_config_scrud_view
 AS
 SELECT smtp_configs.smtp_config_id,
    smtp_configs.configuration_name,
    smtp_configs.enabled,
    smtp_configs.is_default,
    smtp_configs.from_display_name,
    smtp_configs.from_email_address
   FROM config.smtp_configs
  WHERE NOT smtp_configs.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

