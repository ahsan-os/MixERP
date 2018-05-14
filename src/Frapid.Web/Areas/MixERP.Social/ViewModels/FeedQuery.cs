namespace MixERP.Social.ViewModels
{
    public sealed class FeedQuery
    {
        public long LastFeedId { get; set; }
        public long ParentFeedId { get; set; }
        public string Url { get; set; }
    }
}