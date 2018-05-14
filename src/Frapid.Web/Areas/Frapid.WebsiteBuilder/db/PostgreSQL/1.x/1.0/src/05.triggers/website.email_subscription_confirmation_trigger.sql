DROP FUNCTION IF EXISTS website.email_subscription_confirmation_trigger() CASCADE;

CREATE FUNCTION website.email_subscription_confirmation_trigger()
RETURNS TRIGGER
AS
$$
BEGIN
    IF(NEW.confirmed) THEN
        NEW.confirmed_on = NOW();
    END IF;
    
    RETURN NEW;
END
$$
LANGUAGE plpgsql;

CREATE TRIGGER email_subscription_confirmation_trigger 
BEFORE UPDATE ON website.email_subscriptions 
FOR EACH ROW
EXECUTE PROCEDURE website.email_subscription_confirmation_trigger();