namespace Frapid.Areas.SpamTrap
{
    public interface IIpAddressReverser
    {
        string Reverse(string ipAddress);
    }
}