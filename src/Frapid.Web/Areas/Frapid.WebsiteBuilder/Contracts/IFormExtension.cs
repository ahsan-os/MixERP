using System.Threading.Tasks;
using System.Web.Mvc;

namespace Frapid.WebsiteBuilder.Contracts
{
    public interface IFormExtension
    {
        string Identifier { get; }
        string Path { get; set; }
        bool IsPost { get; set; }
        FormCollection Form { get; set; }
        string GetForm(string tenant);
        Task<string> PostFormAsync(string tenant, FormCollection form);
    }
}