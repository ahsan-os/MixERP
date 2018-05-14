-->-->-- src/Frapid.Web/Areas/Frapid.Calendar/db/PostgreSQL/2.x/2.0/src/99.sample-data/calendar.sample.sql --<--<--
INSERT INTO calendar.categories(user_id, category_name, color_code, is_local, category_order)
SELECT user_id, 'Personal', 'color1 category color', true, 0 FROM account.users;

INSERT INTO calendar.categories(user_id, category_name, color_code, is_local, category_order)
SELECT user_id, 'Office', 'color2 category color', true, 0 FROM account.users;


