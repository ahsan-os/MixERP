DROP VIEW IF EXISTS account.sign_in_view;

CREATE VIEW account.sign_in_view
AS
SELECT
    account.logins.login_id,
    account.users.name,
    account.users.email,
    account.logins.user_id,
    account.roles.role_id,
    account.roles.role_name,
    account.roles.is_administrator,
    account.logins.browser,
    account.logins.ip_address,
    account.logins.login_timestamp,
    account.logins.culture,
    account.logins.is_active,
    account.logins.office_id,
    core.offices.office_code,
    core.offices.office_name,
    core.offices.office_code || ' (' || core.offices.office_name || ')' AS office,
    core.offices.logo,
    core.offices.registration_date,
    core.offices.po_box,
    core.offices.address_line_1,
    core.offices.address_line_2,
    core.offices.street,
    core.offices.city,
    core.offices.state,
    core.offices.zip_code,
    core.offices.country,
    core.offices.email AS office_email,
    core.offices.phone,
    core.offices.fax,
    core.offices.url,
    core.offices.currency_code,
    core.currencies.currency_name,
    core.currencies.currency_symbol,
    core.currencies.hundredth_name,
    core.offices.pan_number,
    account.users.last_seen_on
FROM account.logins
INNER JOIN account.users
ON account.users.user_id = account.logins.user_id
INNER JOIN account.roles
ON account.roles.role_id = account.users.role_id
INNER JOIN core.offices
ON core.offices.office_id = account.logins.office_id
LEFT JOIN core.currencies
ON core.currencies.currency_code = core.offices.currency_code
WHERE NOT account.logins.deleted;

