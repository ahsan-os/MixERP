INSERT INTO auth.access_types(access_type_id, access_type_name)
SELECT 1, 'Read'            UNION ALL
SELECT 2, 'Create'          UNION ALL
SELECT 3, 'Edit'            UNION ALL
SELECT 4, 'Delete'          UNION ALL
SELECT 5, 'CreateFilter'    UNION ALL
SELECT 6, 'DeleteFilter'    UNION ALL
SELECT 7, 'Export'          UNION ALL
SELECT 8, 'ExportData'      UNION ALL
SELECT 9, 'ImportData'      UNION ALL
SELECT 10, 'Execute'        UNION ALL
SELECT 11, 'Verify';