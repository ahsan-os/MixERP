# Database Naming Convention

Depending on the domain name, frapid automatically creates and populates database for you.

The periods (.) and dashes(-) are converted to underscores and special characters are removed.

**Example**


| Domain Name           | Database Name         |
|-----------------------|-----------------------|
| example.com           | example_com           |
| subdomain.example.com | subdomain_example_com |
| localhost             | localhost             |
| frapid.localhost      | frapid_localhost      |

[Return Back](../../README.md)