EXECUTE core.create_app 'Frapid.Config', 'Config', 'Config', '1.0', 'MixERP Inc.', 'December 1, 2015', 'orange configure', '/dashboard/config/offices', null;
EXECUTE core.create_menu 'Frapid.Config', 'Offices', 'Offices', '/dashboard/config/offices', 'building outline', '';
EXECUTE core.create_menu 'Frapid.Config', 'SMTP', 'SMTP', '/dashboard/config/smtp', 'at', '';
EXECUTE core.create_menu 'Frapid.Config', 'FileManager', 'File Manager', '/dashboard/config/file-manager', 'file national character varying(500) outline', '';

GO
