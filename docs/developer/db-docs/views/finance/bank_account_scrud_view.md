# finance.bank_account_scrud_view view

| Schema | [finance](../../schemas/finance.md) |
| ------ | ----------------------------------------------- |
| Materialized View Name | bank_account_scrud_view |
| Owner | frapid_db_user |
| Tablespace | DEFAULT |
| Description |  |

**Source:**

```plpgsql
 CREATE OR REPLACE VIEW finance.bank_account_scrud_view
 AS
 SELECT bank_accounts.bank_account_id,
    bank_accounts.account_id,
    users.name AS maintained_by,
    ((offices.office_code::text || '('::text) || offices.office_name::text) || ')'::text AS office_name,
    bank_accounts.bank_name,
    bank_accounts.bank_branch,
    bank_accounts.bank_contact_number,
    bank_accounts.bank_account_number,
    bank_accounts.bank_account_type,
    bank_accounts.relationship_officer_name
   FROM finance.bank_accounts
     JOIN account.users ON bank_accounts.maintained_by_user_id = users.user_id
     JOIN core.offices ON bank_accounts.office_id = offices.office_id
  WHERE NOT bank_accounts.deleted;
```


### Related Contents
* [Schema List](../../schemas.md)
* [View List](../../views.md)
* [Materialized View List](../../materialized-views.md)
* [Table of Contents](../../README.md)

