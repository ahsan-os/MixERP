IF OBJECT_ID('config.filter_name_view') IS NOT NULL
DROP VIEW config.filter_name_view;
GO
CREATE VIEW config.filter_name_view
AS
SELECT
    DISTINCT
    object_name,
    filter_name,
    is_default
FROM config.filters
WHERE config.filters.deleted = 0;

GO
