using Frapid.Reports.Engine.Model;

namespace Frapid.Reports.Engine
{
    public interface IGenerator
    {
        int Order { get; }
        string Name { get; }
        string Generate(string tenant, Report report);
    }
}