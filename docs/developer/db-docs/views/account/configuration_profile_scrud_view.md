# account.configuration_profile_scrud_view view

| Schema | [account](../../schemas/account.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | configuration_profile_scrud_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW account.configuration_profile_scrud_view
 AS
 SELECT configuration_profiles.configuration_profile_id,
    configuration_profiles.profile_name,
    configuration_profiles.is_active,
    configuration_profiles.allow_registration,
    roles.role_name AS defult_role,
    ((offices.office_code::text || ' ('::text) || offices.office_name::text) || ')'::text AS default_office
   FROM account.configuration_profiles
     LEFT JOIN account.roles ON roles.role_id = configuration_profiles.registration_role_id
     LEFT JOIN core.offices ON offices.office_id = configuration_profiles.registration_office_id
  WHERE NOT configuration_profiles.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

