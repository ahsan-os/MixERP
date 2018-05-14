DROP VIEW IF EXISTS account.user_selector_view;

CREATE VIEW account.user_selector_view
AS
SELECT
    account.users.user_id,
    account.users.name AS user_name
FROM account.users
WHERE NOT account.users.deleted;