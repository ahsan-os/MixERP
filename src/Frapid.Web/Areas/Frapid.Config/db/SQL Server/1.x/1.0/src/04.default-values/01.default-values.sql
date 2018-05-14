GO

IF NOT EXISTS(SELECT * FROM config.custom_field_data_types WHERE data_type='text')
BEGIN
    INSERT INTO config.custom_field_data_types(data_type, underlying_type)
    SELECT 'Text', 'national character varying(500)';
END;

IF NOT EXISTS(SELECT * FROM config.custom_field_data_types WHERE data_type='Number')
BEGIN
    INSERT INTO config.custom_field_data_types(data_type, underlying_type)
    SELECT 'Number', 'integer';
END;

IF NOT EXISTS(SELECT * FROM config.custom_field_data_types WHERE data_type='Number')
BEGIN
    INSERT INTO config.custom_field_data_types(data_type, underlying_type)
    SELECT 'Positive Number', 'integer';
END;

IF NOT EXISTS(SELECT * FROM config.custom_field_data_types WHERE data_type='Number')
BEGIN
    INSERT INTO config.custom_field_data_types(data_type, underlying_type)
    SELECT 'Money', 'numeric(30, 6)';
END;

IF NOT EXISTS(SELECT * FROM config.custom_field_data_types WHERE data_type='Number')
BEGIN
    INSERT INTO config.custom_field_data_types(data_type, underlying_type)
    SELECT 'Money (Positive Value Only)', 'numeric(30, 6)';
END;

IF NOT EXISTS(SELECT * FROM config.custom_field_data_types WHERE data_type='Date')
BEGIN
    INSERT INTO config.custom_field_data_types(data_type, underlying_type)
    SELECT 'Date', 'date';
END;

IF NOT EXISTS(SELECT * FROM config.custom_field_data_types WHERE data_type='Date')
BEGIN
    INSERT INTO config.custom_field_data_types(data_type, underlying_type)
    SELECT 'Date & Time', 'datetimeoffset';
END;

IF NOT EXISTS(SELECT * FROM config.custom_field_data_types WHERE data_type='True/False')
BEGIN
    INSERT INTO config.custom_field_data_types(data_type, underlying_type)
    SELECT 'True/False', 'bit';
END;

IF NOT EXISTS(SELECT * FROM config.custom_field_data_types WHERE data_type='Long Text')
BEGIN
    INSERT INTO config.custom_field_data_types(data_type, underlying_type)
    SELECT 'Long Text', 'national character varying(MAX)';
END;


GO
