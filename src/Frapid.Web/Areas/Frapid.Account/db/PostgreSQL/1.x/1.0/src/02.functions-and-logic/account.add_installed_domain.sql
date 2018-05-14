DROP FUNCTION IF EXISTS account.add_installed_domain
(
    _domain_name                                text,
    _admin_email                                text
);

CREATE FUNCTION account.add_installed_domain
(
    _domain_name                                text,
    _admin_email                                text
)
RETURNS void
AS
$$
BEGIN
    IF EXISTS
    (
        SELECT * 
		FROM account.installed_domains
        WHERE account.installed_domains.domain_name = _domain_name        
    ) THEN
        UPDATE account.installed_domains
        SET admin_email = _admin_email
        WHERE domain_name = _domain_name;
        
        RETURN;
    END IF;

    INSERT INTO account.installed_domains(domain_name, admin_email)
    SELECT _domain_name, _admin_email;
END
$$
LANGUAGE plpgsql;