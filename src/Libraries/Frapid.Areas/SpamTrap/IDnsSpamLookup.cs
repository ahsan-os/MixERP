namespace Frapid.Areas.SpamTrap
{
    public interface IDnsSpamLookup
    {
        IDnsQueryable Queryable { get; set; }
        string[] RblServers { get; set; }
        IIpAddressReverser Reverser { get; set; }

        DnsSpamLookupResult IsListedInSpamDatabase(string ipAddress);
    }
}