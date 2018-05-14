# hrm.employee_social_network_detail_scrud_view view

| Schema | [hrm](../../schemas/hrm.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | employee_social_network_detail_scrud_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW hrm.employee_social_network_detail_scrud_view
 AS
 SELECT employee_social_network_details.employee_social_network_detail_id,
    employee_social_network_details.employee_id,
    employees.employee_name,
    employee_social_network_details.social_network_id,
    social_networks.social_network_name,
    social_networks.icon_css_class,
    social_networks.base_url,
    employee_social_network_details.profile_link
   FROM hrm.employee_social_network_details
     JOIN hrm.employees ON employee_social_network_details.employee_id = employees.employee_id
     JOIN hrm.social_networks ON social_networks.social_network_id = employee_social_network_details.social_network_id
  WHERE NOT employee_social_network_details.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

