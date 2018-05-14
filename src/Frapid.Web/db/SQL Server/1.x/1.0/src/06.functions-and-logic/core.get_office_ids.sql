IF OBJECT_ID('core.get_office_ids') IS NOT NULL
DROP FUNCTION core.get_office_ids;

GO

CREATE FUNCTION core.get_office_ids(@root_office_id integer)
RETURNS @results TABLE
(
	office_id		integer
)
AS
BEGIN
    WITH office_cte(office_id, path) AS (
     SELECT
        tn.office_id,  CAST(tn.office_id AS varchar(MAX)) AS path
        FROM core.offices AS tn WHERE tn.office_id = @root_office_id
    UNION ALL
     SELECT
        c.office_id, (p.path + '->' + CAST(c.office_id AS varchar(MAX)))
        FROM office_cte AS p, core.offices AS c WHERE parent_office_id = p.office_id
    )
	
	INSERT INTO @results
    SELECT office_id FROM office_cte;
    
    RETURN;
END;

GO