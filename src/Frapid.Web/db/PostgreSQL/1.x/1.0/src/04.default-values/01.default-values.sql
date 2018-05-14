INSERT INTO core.offices(office_code, office_name, currency_code, nick_name, po_box, address_line_1, address_line_2, street, city, state, country, phone, fax, email, url)
SELECT 'DEF', 'Default', 'USD', 'MixERP', '3415', 'Lobortis. Avenue', '', '', 'Rocky Mount', 'WA', 'United States', '(213) 3640-6139', '', 'info@mixerp.com', 'http://mixerp.com';

INSERT INTO core.genders(gender_code, gender_name)
SELECT 'M', 'Male' UNION ALL
SELECT 'F', 'Female' UNION ALL
SELECT 'U', 'Unspecified';

INSERT INTO core.marital_statuses(marital_status_code, marital_status_name, is_legally_recognized_marriage)
SELECT 'NEM', 'Never Married',          false UNION ALL
SELECT 'SEP', 'Separated',              false UNION ALL
SELECT 'MAR', 'Married',                true UNION ALL
SELECT 'LIV', 'Living Relationship',    false UNION ALL
SELECT 'DIV', 'Divorced',               false UNION ALL
SELECT 'WID', 'Widower',                false UNION ALL
SELECT 'CIV', 'Civil Union',            true;

INSERT INTO core.currencies(currency_code, currency_symbol, currency_name, hundredth_name)
SELECT 'NPR', 'Rs ',       'Nepali Rupees',        'paisa'     UNION ALL
SELECT 'USD', '$',      'United States Dollar', 'cents'     UNION ALL
SELECT 'GBP', '£',      'Pound Sterling',       'penny'     UNION ALL
SELECT 'EUR', '€',      'Euro',                 'cents'     UNION ALL
SELECT 'JPY', '¥',      'Japanese Yen',         'sen'       UNION ALL
SELECT 'CHF', 'CHF',    'Swiss Franc',          'centime'   UNION ALL
SELECT 'CAD', '¢',      'Canadian Dollar',      'cent'      UNION ALL
SELECT 'AUD', 'AU$',    'Australian Dollar',    'cent'      UNION ALL
SELECT 'HKD', 'HK$',    'Hong Kong Dollar',     'cent'      UNION ALL
SELECT 'INR', '₹',      'Indian Rupees',        'paise'     UNION ALL
SELECT 'SEK', 'kr',     'Swedish Krona',        'öre'       UNION ALL
SELECT 'NZD', 'NZ$',    'New Zealand Dollar',   'cent';


INSERT INTO core.verification_statuses
SELECT -3,  'Rejected'                              UNION ALL
SELECT -2,  'Closed'                                UNION ALL
SELECT -1,  'Withdrawn'                             UNION ALL
SELECT 0,   'Unverified'                            UNION ALL
SELECT 1,   'Automatically Approved by Workflow'    UNION ALL
SELECT 2,   'Approved';
