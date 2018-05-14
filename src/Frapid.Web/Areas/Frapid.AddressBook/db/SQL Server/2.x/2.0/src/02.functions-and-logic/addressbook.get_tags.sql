IF OBJECT_ID('addressbook.get_tags') IS NOT NULL
DROP FUNCTION addressbook.get_tags;

GO

CREATE FUNCTION addressbook.get_tags(@user_id integer)
RETURNS national character varying(MAX)
AS
BEGIN
	DECLARE @tags national character varying(MAX);
	SELECT @tags = COALESCE(@tags + ',', '') +  member 
	FROM
	(
			SELECT DISTINCT member
			FROM addressbook.contacts
			CROSS APPLY core.split(tags)
			WHERE addressbook.contacts.deleted = 0
			AND (addressbook.contacts.is_private = 0 OR addressbook.contacts.created_by = @user_id)
	) AS tags;

	RETURN @tags;
END;

GO

--SELECT addressbook.get_tags(1);

GO