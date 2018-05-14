namespace Frapid.Areas.SpamTrap
{
    public interface IDnsQueryable
    {
        IHostEntryResolver Resolver { get; set; }

        bool Query(string address);
    }
}