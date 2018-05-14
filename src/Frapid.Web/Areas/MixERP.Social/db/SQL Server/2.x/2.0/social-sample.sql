-->-->-- src/Frapid.Web/Areas/MixERP.Social/db/Sql Server/2.x/2.0/src/99.sample-data/social.sample.sql --<--<--
DELETE FROM social.liked_by;
DELETE FROM social.feeds;

INSERT INTO social.feeds(audit_ts, event_timestamp, formatted_text, created_by, scope, is_public)
SELECT created_on, created_on, name + ' joined us as ' + account.get_role_name_by_role_id(account.users.role_id) + '.', account.users.user_id, 'Notifications', 1
FROM account.users
ORDER BY user_id DESC;

--SELECT * FROM social.feeds;

