ALTER TABLE core.offices
ADD FOREIGN KEY(audit_user_id) REFERENCES account.users;