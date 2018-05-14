namespace Frapid.DataAccess.Models
{
    /// <summary>
    ///     Access type refers to user interactions against a data.
    /// </summary>
    public enum AccessTypeEnum
    {
        None = 0,
        Read = 1,
        Create = 2,
        Edit = 3,
        Delete = 4,
        CreateFilter = 5,
        DeleteFilter = 6,
        ExportData = 7,
        ImportData = 8,
        Execute = 9,
        Verify = 10
    }
}