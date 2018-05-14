# Mailgun Email API

Transaction emails are:

* account confirmation emails
* welcome emails
* password reset emails
* shipping notices
* order confirmation
* email invoices

Transaction emails are very important for you because your customers are expecting them to arrive.
When you have a large number of users and start sending many emails, pretty soon these emails could be considered as "too many"
by the receiving server. On the worst case scenario, the receiving server might flag your IP Address as spammer 
or even reject your request.

Mailgun is one of the leading transaction email service providers. For up to 10000 emails per month, 
[Mailgun is absolutely free](http://www.mailgun.com/pricing).

![Mailgun Logo](images/mailgun.png)

Since frapid has built-in support for Mailgun API, you just need to edit the configuration file `Mailgun.json` and you're good to go. 
Preferably, you can configure this from the [Admin Area](#) as well.

**~/Tenants/<domain>/Configs/SMTP/Mailgun.json**

```json
{
    "FromName" : "",
    "FromEmail" : "",
    "DomainName" : "Your domain name configured in Mailgun",
	"ApiKey": "Your Mailgun api key",
	"SecretKey": "Your Mailgun secret",
	"Enabled": false
}
```

## Configuration Explained

| Key                           | Configuration|
|-------------------------------|---------------------------------------------------------|
| FromName                      | The name field for the `FromEmail` key. |
| FromEmail                     | The from email address to be displayed to the email recipients.|
| DomainName                    | The domain configured in the Mailgun app dashboard. |
| ApiKey                        | The Mailgun API key. |
| SecretKey                     | The Mailgun secret. |
| Enabled                       | Set this to true if you want to use Mailgun API to send emails. If multiple email providers are enabled, the first one will be used. |


### Related Contents

* [Installing Frapid](../installation/README.md)
* [Developer Documentation](../developer/README.md)
* [Documentation Home](../../README.md)
