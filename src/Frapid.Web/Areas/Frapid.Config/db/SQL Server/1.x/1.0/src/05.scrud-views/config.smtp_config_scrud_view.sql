IF OBJECT_ID('config.smtp_config_scrud_view') IS NOT NULL
DROP VIEW config.smtp_config_scrud_view;

GO

CREATE VIEW config.smtp_config_scrud_view
AS
SELECT
	config.smtp_configs.smtp_config_id,
	config.smtp_configs.configuration_name,
	config.smtp_configs.enabled,
	config.smtp_configs.is_default,
	config.smtp_configs.from_display_name,
	config.smtp_configs.from_email_address
FROM config.smtp_configs
WHERE config.smtp_configs.deleted = 0;

GO