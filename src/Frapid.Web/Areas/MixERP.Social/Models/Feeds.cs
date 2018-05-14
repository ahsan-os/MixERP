using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Frapid.ApplicationState.Models;
using Frapid.Dashboard.DTO;
using Frapid.Dashboard.Helpers;
using Frapid.Framework.Extensions;
using MixERP.Social.DTO;
using MixERP.Social.ViewModels;

namespace MixERP.Social.Models
{
    public static class Feeds
    {
        private static string GetFormattedText(string html)
        {
            html = HtmlToText.ConvertHtml(html);
            html = Regex.Replace(html, @"\r\n?|\n", "<br />");

            return html;
        }

        public static async Task<FeedItem> PostAsync(string tenant, Feed model, LoginView meta)
        {
            string message = Resources.UserCommentedOnThePostYoureFollowing;

            model.FormattedText = GetFormattedText(model.FormattedText);
            model.AuditTs = DateTimeOffset.UtcNow;
            model.CreatedBy = meta.UserId;
            model.Deleted = false;
            model.IsPublic = true;
            model.EventTimestamp = DateTimeOffset.UtcNow;

            var item = await DAL.Feeds.PostAsync(tenant, model).ConfigureAwait(false);

            var participants = await DAL.Feeds.GetFollowersAsync(tenant, item.FeedId, meta.UserId).ConfigureAwait(false);

            var notification = new Notification
            {
                Tenant = tenant,
                FormattedText = string.Format(message, meta.Name, model.FormattedText),
                Icon = "users",
                Url = "/dashboard/social?FeedId=" + item.FeedId,
                OfficeId = meta.OfficeId,
                AssociatedApp = "MixERP.Social"
            };

            foreach (int participant in participants)
            {
                await NotificationHelper.SendToUsersAsync(tenant, participant, notification).ConfigureAwait(false);
            }

            return item;
        }

        public static async Task DeleteAsync(string tenant, long feedId, LoginView meta)
        {
            var feed = await DAL.Feeds.GetFeedAsync(tenant, feedId).ConfigureAwait(false);

            if (feed == null)
            {
                return;
            }

            if (!meta.IsAdministrator)
            {
                if (feed.CreatedBy != meta.UserId)
                {
                    throw new FeedException(Resources.AccessIsDenied);
                }
            }

            await DAL.Feeds.DeleteAsync(tenant, feed, meta).ConfigureAwait(false);
        }

        public static async Task DeleteAttachmentAsync(string tenant, long feedId, string attachment, LoginView meta)
        {
            var feed = await DAL.Feeds.GetFeedAsync(tenant, feedId).ConfigureAwait(false);

            if (feed == null)
            {
                return;
            }

            if (!meta.IsAdministrator)
            {
                if (feed.CreatedBy != meta.UserId)
                {
                    throw new FeedException(Resources.AccessIsDenied);
                }
            }

            var attachments = feed.Attachments.Or("").ToLowerInvariant().Split(',').ToList();
            attachments.Remove(attachment.ToLowerInvariant());

            feed.Attachments = string.Join(",", attachments);

            await DAL.Feeds.UpdateAsync(tenant, feed, meta).ConfigureAwait(false);

            string path = Attachments.GetUploadDirectory(tenant);
            path = Path.Combine(path, attachment);

            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public static async Task LikeAsync(string tenant, long feedId, LoginView meta)
        {
            await DAL.Feeds.LikeAsync(tenant, feedId, meta).ConfigureAwait(false);
        }

        public static async Task UnlikeAsync(string tenant, long feedId, LoginView meta)
        {
            await DAL.Feeds.UnlikeAsync(tenant, feedId, meta).ConfigureAwait(false);
        }

        public static async Task<IEnumerable<FeedItem>> GetFeedsAsync(string tenant, int userId, FeedQuery query)
        {
            var awaiter = await DAL.Feeds.GetFeedsAsync(tenant, userId, query).ConfigureAwait(false);
            var model = awaiter.ToList();

            if (model.Count == 0)
            {
                return new List<FeedItem>();
            }

            var feedIds = model.Select(x => x.FeedId).ToArray();

            var likes = (await DAL.Feeds.GetLikesAsync(tenant, feedIds).ConfigureAwait(false)).ToList();

            foreach (var item in model)
            {
                var itemLikes = likes.Where(x => x.FeedId == item.FeedId);
                item.Likes = itemLikes;
            }

            return model;
        }
    }
}