DROP FUNCTION IF EXISTS addressbook.get_tags(_user_id integer);

CREATE FUNCTION addressbook.get_tags(_user_id integer)
RETURNS text
AS
$$
BEGIN
    RETURN string_agg(tag, ',')
    FROM 
    (
        SELECT DISTINCT unnest(string_to_array(tags, ',')) AS tag
        FROM addressbook.contacts
        WHERE NOT addressbook.contacts.deleted
        AND (addressbook.contacts.is_private = false OR addressbook.contacts.created_by = _user_id)
    ) AS tags;
END
$$
LANGUAGE plpgsql;

SELECT * FROM addressbook.get_tags(1);