namespace Frapid.Configuration.TenantServices.Contracts
{
    public interface ITenantValidator
    {
        bool IsValid(string tenant);
    }
}