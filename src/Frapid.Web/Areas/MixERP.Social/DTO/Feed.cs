using System;
using System.ComponentModel.DataAnnotations;
using Frapid.DataAccess;
using Frapid.Mapper.Decorators;

namespace MixERP.Social.DTO
{
    [TableName("social.feeds")]
    [PrimaryKey("feed_id", true, true)]
    public class Feed : IPoco
    {
        public long FeedId { get; set; }
        public DateTimeOffset EventTimestamp { get; set; }

        [Required]
        public string FormattedText { get; set; }

        public int CreatedBy { get; set; }
        public string Attachments { get; set; }
        public string Scope { get; set; }
        public bool IsPublic { get; set; }
        public long? ParentFeedId { get; set; }
        public DateTimeOffset? AuditTs { get; set; }
        public string Url { get; set; }
        public bool Deleted { get; set; }
        public DateTimeOffset? DeletedOn { get; set; }
        public int? DeletedBy { get; set; }
    }
}