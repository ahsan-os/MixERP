namespace Frapid.Areas.Caching
{
    public sealed class BinaryCacheItem
    {
        public byte[] Data { get; set; }
        public string ContentType { get; set; }
    }
}