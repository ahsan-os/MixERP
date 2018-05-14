IF OBJECT_ID('website.contact_scrud_view') IS NOT NULL
DROP VIEW website.contact_scrud_view;

GO

CREATE VIEW website.contact_scrud_view
AS
SELECT
	website.contacts.contact_id,
	website.contacts.title,
	website.contacts.name,
	website.contacts.position,
	website.contacts.email,
	website.contacts.display_contact_form,
	website.contacts.display_email
FROM website.contacts
WHERE website.contacts.deleted = 0;

GO