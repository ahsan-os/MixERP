using System.Threading.Tasks;

namespace Frapid.WebsiteBuilder.Contracts
{
    public interface IContentExtension
    {
      Task<string> ParseHtmlAsync(string tenant, string html);
    }
}