using System.Threading.Tasks;
using Frapid.ApplicationState.Models;
using Frapid.Dashboard.DTO;
using Frapid.Dashboard.Helpers;
using MixERP.Social.DTO;
using MixERP.Social.Models;

namespace MixERP.Social.Helpers
{
    public static class FeedHelper
    {
        public static async Task CreateFeedAsync(string tenant, string message, string associatedApp, LoginView meta)
        {
            await Feeds.PostAsync(tenant, new Feed
            {
                FormattedText = message,
                CreatedBy = meta.UserId,
                Scope = associatedApp,
                IsPublic = true
            }, meta).ConfigureAwait(false);
        }

        public static async Task CreateNotificationFeedAsync(string tenant, int officeId, string message, string associatedApp, LoginView meta)
        {
            var feed = await Feeds.PostAsync(tenant, new Feed
            {
                FormattedText = message,
                CreatedBy = meta.UserId,
                Scope = associatedApp,
                IsPublic = true
            }, meta).ConfigureAwait(false);

            await NotificationHelper.SendAsync(tenant, new Notification
            {
                Tenant = tenant,
                OfficeId = officeId,
                AssociatedApp = associatedApp,
                Url = "/dashboard/social?FeedId=" + feed.FeedId,
                FormattedText = message
            }).ConfigureAwait(false);
        }
    }
}