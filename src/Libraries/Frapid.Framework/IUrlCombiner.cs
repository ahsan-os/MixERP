namespace Frapid.Framework
{
    public interface IUrlCombiner
    {
        string Combine(string domain, string path);
    }
}