IF OBJECT_ID('core.check_parent_office_trigger') IS NOT NULL
DROP TRIGGER core.check_parent_office_trigger

GO

CREATE TRIGGER core.check_parent_office_trigger
ON core.offices 
FOR INSERT, UPDATE
AS 
BEGIN
	IF((SELECT office_id FROM INSERTED) = (SELECT parent_office_id FROM INSERTED))
		RAISERROR('Same office cannot be a parent office', 16, 1);
	RETURN;
END;

GO