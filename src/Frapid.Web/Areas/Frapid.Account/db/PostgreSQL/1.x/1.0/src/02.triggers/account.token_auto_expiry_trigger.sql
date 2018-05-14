DROP FUNCTION IF EXISTS account.token_auto_expiry_trigger() CASCADE;

CREATE FUNCTION account.token_auto_expiry_trigger()
RETURNS trigger
AS
$$
BEGIN
    UPDATE account.access_tokens
    SET 
        revoked = true,
        revoked_on = NOW()
    WHERE ip_address = NEW.ip_address
    AND user_agent = NEW.user_agent;

    RETURN NEW;
END
$$
LANGUAGE plpgsql;

CREATE TRIGGER account_token_auto_expiry_trigger
BEFORE INSERT
ON account.access_tokens
FOR EACH ROW
EXECUTE PROCEDURE account.token_auto_expiry_trigger();
