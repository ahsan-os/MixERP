IF OBJECT_ID('dbo.get_app_data_type') IS NOT NULL
DROP FUNCTION dbo.get_app_data_type;

GO

CREATE FUNCTION dbo.get_app_data_type(@is_nullable varchar, @db_data_type national character varying(100))
RETURNS national character varying(100)
BEGIN
	DECLARE @data_type national character varying(100);

    IF(@db_data_type IN('smallint', 'tinyint'))
    BEGIN
        SET @data_type = 'short';
    END;

    IF(@db_data_type IN('int4', 'int', 'integer'))
    BEGIN
        SET @data_type = 'int';
    END;

    IF(@db_data_type IN('decimal', 'numeric'))
    BEGIN
        SET @data_type = 'decimal';
    END;

    IF(@db_data_type IN('bigint'))
    BEGIN
        SET @data_type = 'long';
    END;

    IF(@db_data_type IN('varchar', 'nvarchar', 'char', 'character', 'character varying', 'national character varying', 'text'))
    BEGIN
        RETURN 'string';
    END;
    
    IF(@db_data_type IN('date', 'datetime'))
    BEGIN
        SET @data_type = 'System.DateTime';
    END;
    
    IF(@db_data_type IN('uniqueidentifier'))
    BEGIN
        SET @data_type = 'System.Guid';
    END;
    

    IF(@db_data_type IN('datetimeoffset'))
    BEGIN
        SET @data_type = 'System.DateTimeOffset';
    END;
    
    
    IF(@db_data_type IN('time'))
    BEGIN
        SET @data_type = 'System.TimeSpan';
    END;
    
    IF(@db_data_type IN('bit'))
    BEGIN
        SET @data_type = 'bool';
    END;

    IF(@db_data_type IN('varbinary'))
    BEGIN
		IF(@is_nullable = 'Y')
		BEGIN
			RETURN 'byte?[]'
		END;

		RETURN 'byte[]'
    END;


	IF(@is_nullable = 'Y')
	BEGIN
		SET @data_type = @data_type + '?'
	END;

    RETURN @data_type;
END;

GO



GO

IF OBJECT_ID('dbo.poco_get_tables') IS NOT NULL
DROP FUNCTION dbo.poco_get_tables;

GO

CREATE FUNCTION dbo.poco_get_tables(@schema national character varying(100))
RETURNS @result TABLE
(
	table_schema				national character varying(100),
	table_name					national character varying(100),
	table_type					national character varying(100), 
	has_duplicate				bit
) 
AS
BEGIN
    INSERT INTO @result
    SELECT 
        information_schema.tables.table_schema, 
        information_schema.tables.table_name, 
        information_schema.tables.table_type,
        0
    FROM information_schema.tables 
    WHERE (information_schema.tables.table_type='BASE TABLE' OR information_schema.tables.table_type='VIEW')
    AND information_schema.tables.table_schema = @schema;


    UPDATE  @result
    SET has_duplicate = 1
    FROM  @result result
    INNER JOIN
    (
        SELECT
            information_schema.tables.table_name,
            COUNT(information_schema.tables.table_name) AS table_count
        FROM information_schema.tables
        GROUP BY information_schema.tables.table_name
        
    ) subquery
    ON subquery.table_name = result.table_name
    WHERE subquery.table_count > 1;

    

    RETURN;
END;
GO


IF OBJECT_ID('dbo.parse_default') IS NOT NULL
DROP PROCEDURE dbo.parse_default;

GO

CREATE PROCEDURE dbo.parse_default
(
	@default			national character varying(MAX), 
	@parsed				national character varying(MAX) OUTPUT
)
AS
BEGIN
	SET NOCOUNT ON;
	DECLARE @result		TABLE (parsed national character varying(MAX));
	DECLARE @sql		national character varying(MAX);
	
	IF(@default IS NOT NULL)
	BEGIN
		BEGIN TRY
			SET @sql = 'SELECT ' + @default;

			INSERT INTO @result
			EXECUTE @parsed = sp_executesql @sql;

			SELECT @parsed = parsed
			FROM @result;
		END TRY
		BEGIN CATCH
		END CATCH
	END;
END;

GO

IF OBJECT_ID('dbo.is_primary_key') IS NOT NULL
DROP FUNCTION dbo.is_primary_key;

GO

CREATE FUNCTION dbo.is_primary_key(@schema national character varying(100), @table national character varying(100), @column national character varying(100))
RETURNS national character varying(100)
AS
BEGIN
	IF EXISTS
	(
		SELECT 
			1
		FROM information_schema.table_constraints
		INNER JOIN information_schema.key_column_usage
		ON information_schema.table_constraints.constraint_type = 'PRIMARY KEY' AND
		information_schema.table_constraints.constraint_name = key_column_usage.CONSTRAINT_NAME
		AND information_schema.key_column_usage.table_schema = @schema
		AND information_schema.key_column_usage.table_name=@table
		AND information_schema.key_column_usage.column_name = @column
	)
	BEGIN
		RETURN 'YES';
	END
	
	RETURN 'NO';
END

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('dbo.poco_get_table_function_definition') AND type in (N'P', N'PC'))
DROP PROCEDURE dbo.poco_get_table_function_definition;

GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID('dbo.poco_get_table_function_definition') AND type in (N'FN'))
DROP FUNCTION dbo.poco_get_table_function_definition;

GO

CREATE PROCEDURE dbo.poco_get_table_function_definition(@schema national character varying(128), @name national character varying(128))
AS
BEGIN
	DECLARE @result TABLE
	(
		row_id					int IDENTITY,
		id                      int,
		column_name             national character varying(128),
		nullable				national character varying(100),
		db_data_type            national character varying(100),
		value					national character varying(100),
		max_length              integer,
		primary_key				national character varying(128),
		data_type               national character varying(128),
		is_serial				bit DEFAULT(0)
	);

	DECLARE @total_rows			int;
	DECLARE @this_row			int = 0;
	DECLARE @default			national character varying(128);
	DECLARE @parsed				national character varying(128);

    IF EXISTS
    (
        SELECT *
        FROM information_schema.columns 
        WHERE table_schema=@schema
        AND table_name=@name
    )
    BEGIN
        INSERT INTO @result(id, column_name, nullable, db_data_type, value, max_length, primary_key, data_type)
        SELECT
			information_schema.columns.ordinal_position,
			information_schema.columns.column_name,
			information_schema.columns.is_nullable,
			CASE WHEN information_schema.columns.domain_name IS NOT NULL 
			THEN information_schema.columns.domain_name
			ELSE information_schema.columns.data_type END AS data_type,
			information_schema.columns.column_default,
			information_schema.columns.character_maximum_length,
			dbo.is_primary_key(@schema, @name, information_schema.columns.column_name),
			dbo.get_app_data_type(information_schema.columns.is_nullable, information_schema.columns.data_type)
        FROM information_schema.columns
        WHERE 1 = 1
        AND information_schema.columns.table_schema = @schema
        AND information_schema.columns.table_name = @name;
    END;

	SELECT @total_rows = COUNT(*)
	FROM @result;

	WHILE @this_row<@total_rows
	BEGIN
		SET @this_row = @this_row + 1;

		SELECT 
			@default = value
		FROM @result
		WHERE row_id=@this_row;

		EXECUTE dbo.parse_default @default, @parsed = @parsed OUTPUT;
		
		UPDATE @result
		SET value = @parsed
		WHERE row_id=@this_row;

		SET @parsed = NULL;
	END;

	UPDATE @result
	SET is_serial = COLUMNPROPERTY(OBJECT_ID(@schema + '.' + @name), column_name, 'IsIdentity');

	SELECT * FROM @result;
    RETURN;
END;

GO
