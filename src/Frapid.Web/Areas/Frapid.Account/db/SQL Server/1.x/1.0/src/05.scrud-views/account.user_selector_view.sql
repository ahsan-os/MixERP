IF OBJECT_ID('account.user_selector_view') IS NOT NULL
DROP VIEW account.user_selector_view;

GO

CREATE VIEW account.user_selector_view
AS
SELECT
    account.users.user_id,
    account.users.name AS user_name
FROM account.users
WHERE account.users.deleted = 0;

GO