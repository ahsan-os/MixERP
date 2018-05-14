namespace Frapid.Configuration.DbServer
{
    public interface IDbServer
    {
        string ProviderName { get; }
        string GetSuperUserConnectionString(string tenant, string database = "");
        string GetConnectionString(string tenant, string database = "", string userId = "", string password = "");
        string GetReportUserConnectionString(string tenant, string database = "");
        string GetMetaConnectionString(string tenant);
        string GetConnectionString(string tenant, string host, string database, string username, string password, int port, bool enablePooling = true, int minPoolSize = 0, int maxPoolSize = 100, string networkLibrary = "");
        string GetProcedureCommand(string procedureName, string[] parameters);
        string DefaultSchemaQualify(string input);
        string AddLimit(string limit);
        string AddOffset(string offset);
        string AddReturnInsertedKey(string primaryKeyName);
        string GetDbTimestampFunction();
    }
}