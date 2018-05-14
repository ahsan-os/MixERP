# account.add_installed_domain function:

```plpgsql
CREATE OR REPLACE FUNCTION account.add_installed_domain(_domain_name text, _admin_email text)
RETURNS void
```
* Schema : [account](../../schemas/account.md)
* Function Name : add_installed_domain
* Arguments : _domain_name text, _admin_email text
* Owner : frapid_db_user
* Result Type : void
* Description : 


**Source:**
```sql
CREATE OR REPLACE FUNCTION account.add_installed_domain(_domain_name text, _admin_email text)
 RETURNS void
 LANGUAGE plpgsql
AS $function$
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
$function$

```

### Related Contents
* [Schema List](../../schemas.md)
* [Function List](../../functions.md)
* [Trigger List](../../triggers.md)
* [Table of Contents](../../README.md)

