namespace Frapid.Reports
{
    public interface IExportTo
    {
        bool Enabled { get; set; }
        string Extension { get; }
        string Export(string tenant, string html, string fileName, string destination = "");
    }
}