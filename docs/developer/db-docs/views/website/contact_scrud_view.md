# website.contact_scrud_view view

| Schema | [website](../../schemas/website.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | contact_scrud_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW website.contact_scrud_view
 AS
 SELECT contacts.contact_id,
    contacts.title,
    contacts.name,
    contacts."position",
    contacts.email,
    contacts.display_contact_form,
    contacts.display_email
   FROM website.contacts
  WHERE NOT contacts.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

