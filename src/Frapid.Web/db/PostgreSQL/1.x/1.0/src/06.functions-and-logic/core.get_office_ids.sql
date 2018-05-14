DROP FUNCTION IF EXISTS core.get_office_ids(root_office_id integer);

CREATE FUNCTION core.get_office_ids(root_office_id integer)
RETURNS SETOF integer
AS
$$
BEGIN
    RETURN QUERY 
    (
        WITH RECURSIVE office_cte(office_id, path) AS (
         SELECT
            tn.office_id,  tn.office_id::TEXT AS path
            FROM core.offices AS tn 
			WHERE tn.office_id =$1
			AND NOT tn.deleted
        UNION ALL
         SELECT
            c.office_id, (p.path || '->' || c.office_id::TEXT)
            FROM office_cte AS p, core.offices AS c 
			WHERE parent_office_id = p.office_id
        )

        SELECT office_id 
		FROM office_cte
    );
END
$$LANGUAGE plpgsql;

--select * from core.get_office_ids(1)