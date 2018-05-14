DROP VIEW IF EXISTS config.filter_name_view;

CREATE VIEW config.filter_name_view
AS
SELECT
    DISTINCT
    object_name,
    filter_name,
    is_default
FROM config.filters
WHERE NOT config.filters.deleted;