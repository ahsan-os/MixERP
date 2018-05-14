# Restful API

When you follow the convention of creating database tables, Frapid will take care of handling Restful requests for you. Your development time will be dramatically reduced because you don't need to create Rest APIs for performing CRUD operations. Additionally, the [ScrudFactory Module](scrud-factory.md) works in conjunction with the Restful API, further reducing your development time.

**Convention for Table (In order to work with Restful API) :**

- A table name must in plural form and in all lowercase, each word separated by an underscores. Example: configuration_profiles.
- The primary key must be in singular form of the table name, followed by the token `_id`. Example: configuration_profile_id.
- A table must have a code field. Example: configuration_profile_code.
- A table must have a name field. Example: configuration_profile_name.
- A table must have a nullable column called `audit_user_id` referencing the column `account.users.user_id`.
- A table must have a nullable column called `audit_ts` having data type `TIMESTAMP WITH TIME ZONE` or `DATETIMEOFFSET` with the default value being the current timestamp.
- A table must have a `boolean` or `bit` column called `deleted`.  When the value of this column is set to `true`, it signifies that the record was deleted.



| API Route                                | Verbs     | Example                                  | Description                              |
| ---------------------------------------- | --------- | ---------------------------------------- | ---------------------------------------- |
| ~/api/forms/{schemaName}/{tableName}/meta | GET, HEAD | ~/api/forms/core/apps/meta               | Displays the [meta information](https://github.com/frapid/frapid/blob/06e09fd76d896a56063b855b25ca279327b596dc/src/Libraries/Frapid.DataAccess/Models/EntityView.cs) of the database table `core.apps`. |
| ~/api/forms/{schemaName}/{tableName}/count | GET, HEAD | ~/api/forms/core/apps/count              | Returns the total count of rows present in the database table `core.apps`. |
| ~/api/forms/{schemaName}/{tableName}/export | GET, HEAD | ~/api/forms/core/apps/export             | Exports all rows the database table `core.apps`. |
| ~/api/forms/{schemaName}/{tableName}/all | GET, HEAD | ~/api/forms/core/apps/all                | Returns all rows the database table `core.apps`. |
| ~/api/forms/{schemaName}/{tableName}/{primaryKey} | GET, HEAD | ~/api/forms/core/apps/1                  | Returns the row of the database table `core.apps` having the primary key value 1. |
| ~/api/forms/{schemaName}/{tableName}/get | GET, HEAD | ~/api/forms/core/apps/get?primaryKeys=1&primaryKeys=2 | Returns the rows of the database table `core.apps` having the primary keys: 1, 2. |
| ~/api/forms/{schemaName}/{tableName}/first | GET, HEAD | ~/api/forms/core/apps/first              | Returns the first record of the database table `core.apps`. |
| ~/api/forms/{schemaName}/{tableName}/previous/{primaryKey} | GET, HEAD | ~/api/forms/core/apps/previous/20        | Returns the previous record of database table `core.apps` ordered by primary key: 20. |
| ~/api/forms/{schemaName}/{tableName}/next/{primaryKey} | GET, HEAD | ~/api/forms/core/apps/next/1             | Returns the next record of database table `core.apps` ordered by primary key: 1. |
| ~/api/forms/{schemaName}/{tableName}/last | GET, HEAD | ~/api/forms/core/apps/last               | Returns the last record of the database table `core.apps`. |
| ~/api/forms/{schemaName}/{tableName}     | GET, HEAD | ~/api/forms/core/apps                    | Returns the first page (total 10 records) of paginated records for the table `core.apps`. |
| ~/api/forms/{schemaName}/{tableName}/page/{pageNumber} | GET, HEAD | ~/api/forms/core/apps/page/2             | Returns the requested page (page 2) for the database table `core.apps`. |
| ~/api/forms/{schemaName}/{tableName}/count-where | POST      | ~/api/forms/core/apps/count-where        | Returns, using the supplied filters, the count of the number of rows in the `core.apps` or an imaginary table. |
| ~/api/forms/{schemaName}/{tableName}/get-where/{pageNumber} | POST      | ~/api/forms/core/apps/get-where/1        | Returns, using the supplied filters, the requested page of the paginated result of table `core.apps` or an imaginary table. If the supplied page number is a negative value, pagination should not happen and all rows should be returned. |
| ~/api/forms/{schemaName}/{tableName}/count-filtered/{filterName} | GET, HEAD | ~/api/forms/core/apps/count-filtered/ThisMonth | Returns, using the supplied filter name, the count of the number of rows in the `core.apps` or an imaginary table. |
| ~/api/forms/{schemaName}/{tableName}/get-filtered/{pageNumber}/{filterName} | GET, HEAD | ~/api/forms/core/apps/get-filtered/{pageNumber}/ThisMonth | Returns, using the supplied filter name, the requested page of the paginated result of table `core.apps` or an imaginary table. If the supplied page number is a negative value, pagination should not happen and all rows should be returned. |
| ~/api/forms/{schemaName}/{tableName}/display-fields | GET, HEAD | ~/api/forms/core/apps/display-fields     | Returns an IEnumerable of display fields. Display fields provide a minimal id/name context for data binding `core.apps` or an imaginary table. |
| ~/api/forms/{schemaName}/{tableName}/display-fields/get-where | POST      | ~/api/forms/core/apps/display-fields/get-where | Returns, using the supplied filters, an IEnumerable of display fields. |
| ~/api/forms/{schemaName}/{tableName}/lookup-fields | GET, HEAD | ~/api/forms/core/apps/lookup-fields      | Returns an IEnumerable of lookup fields. Lookup fields provide a minimal code/name context for data binding `core.apps` or an imaginary table. |
| ~/api/forms/{schemaName}/{tableName}/lookup-fields/get-where | POST      | ~/api/forms/core/apps/lookup-fields/get-where | Returns, using the supplied filters, an IEnumerable of lookup fields. |
| ~/api/forms/{schemaName}/{tableName}/custom-fields | GET, HEAD | ~/api/forms/core/apps/custom-fields      | Returns IEnumerable of Custom Fields for `core.apps` or an imaginary table. |
| ~/api/forms/{schemaName}/{tableName}/custom-fields/{resourceId} | GET, HEAD | ~/api/forms/core/apps/custom-fields/1    | Returns IEnumerable of Custom Field values for the given resource id (primary key) of `core.apps` or an imaginary table. |
| ~/api/forms/{schemaName}/{tableName}/add-or-edit | POST      | ~/api/forms/core/apps/add-or-edit        | Inserts or updates the dynamic object that represents the collection of columns of `core.apps` or an imaginary table along with the custom fields into the database. Returns the primary key value of the inserted row. |
| ~/api/forms/{schemaName}/{tableName}/add/{skipPrimaryKey:bool} | POST      | ~/api/forms/core/apps/add/false          | Inserts the dynamic object that represents the collection of columns of `core.apps` or an imaginary table along with the custom fields into the database. Returns the primary key value of the inserted row. |
| ~/api/forms/{schemaName}/{tableName}/edit/{primaryKey} | PUT       | ~/api/forms/core/apps/edit/{primaryKey}  | Updates the dynamic object that represents the collection of columns of `core.apps` or an imaginary table along with the custom fields into the database. Returns the primary key value of the updated row. |
| ~/api/forms/{schemaName}/{tableName}/bulk-import | POST      | ~/api/forms/core/apps/bulk-import        | Bulk inserts or updates the collection of `expando objects` that represent the collection of columns of `core.apps` or an imaginary table into the database. Returns the primary key value of the inserted or updated rows. |
| ~/api/forms/{schemaName}/{tableName}/delete/{primaryKey} | DELETE    | ~/api/forms/core/apps/delete/1           | Deletes the matching row which contains the supplied primary key value of `core.apps` or an imaginary table from the database. |



**Convention for View (In order to work with Restful API) :**

- A view must filter out the rows having deleted flag.
- The view API is almost the same as above. Please refer to [View Api Controller](https://github.com/frapid/frapid/blob/master/src/Libraries/Frapid.WebApi/Service/ViewApiController.cs) for more details.



[Back to Developer Documentation](README.md)