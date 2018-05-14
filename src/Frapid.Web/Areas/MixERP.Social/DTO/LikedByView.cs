using System;

namespace MixERP.Social.DTO
{
    public sealed class LikedByView
    {
        public long FeedId { get; set; }
        public int LikedBy { get; set; }
        public string LikedByName { get; set; }
        public DateTimeOffset LikedOn { get; set; }
    }
}