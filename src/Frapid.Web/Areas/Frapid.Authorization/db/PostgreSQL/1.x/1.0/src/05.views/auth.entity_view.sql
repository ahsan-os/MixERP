DROP VIEW IF EXISTS auth.entity_view;

CREATE VIEW auth.entity_view
AS
SELECT 
    information_schema.tables.table_schema, 
    information_schema.tables.table_name, 
    information_schema.tables.table_schema || '.' ||
    information_schema.tables.table_name AS object_name, 
    information_schema.tables.table_type
FROM information_schema.tables 
WHERE (information_schema.tables.table_type='BASE TABLE' OR information_schema.tables.table_type='VIEW')
AND table_schema NOT IN ('pg_catalog', 'information_schema');