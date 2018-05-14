DROP VIEW IF EXISTS website.contact_scrud_view;

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
WHERE NOT website.contacts.deleted;