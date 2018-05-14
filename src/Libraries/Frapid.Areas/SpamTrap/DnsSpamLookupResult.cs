namespace Frapid.Areas.SpamTrap
{
    public class DnsSpamLookupResult
    {
        public string IpAddress { get; set; }
        public bool IsListed { get; set; }
        public string RblServer { get; set; }
    }
}