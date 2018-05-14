IF OBJECT_ID('dbo.drop_schema') IS NOT NULL
EXECUTE dbo.drop_schema 'core';
GO

CREATE SCHEMA core;

GO

IF TYPE_ID(N'dbo.color') IS NULL
BEGIN
	CREATE TYPE dbo.color
	FROM character varying(50);
END;

IF TYPE_ID(N'dbo.photo') IS NULL
BEGIN
	CREATE TYPE dbo.photo
	FROM national character varying(4000);
END;


IF TYPE_ID(N'dbo.html') IS NULL
BEGIN
	CREATE TYPE dbo.html
	FROM national character varying(MAX);
END;


IF TYPE_ID(N'dbo.password') IS NULL
BEGIN
	CREATE TYPE dbo.password
	FROM national character varying(4000);
END;

