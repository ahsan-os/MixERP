INSERT INTO calendar.categories(user_id, category_name, color_code, is_local, category_order)
SELECT user_id, 'Personal', 'color1 category color', true, 0 FROM account.users;

INSERT INTO calendar.categories(user_id, category_name, color_code, is_local, category_order)
SELECT user_id, 'Office', 'color2 category color', true, 0 FROM account.users;

