using System.Threading.Tasks;
using Frapid.Dashboard.DAL;
using Frapid.Dashboard.DTO;
using Frapid.Dashboard.Hubs;

namespace Frapid.Dashboard.Helpers
{
    public static class NotificationHelper
    {
        public static async Task SendAsync(string tenant, Notification message)
        {
            message.Tenant = tenant;
            var id = await Notifications.AddAsync(tenant, message).ConfigureAwait(false);

            message.NotificationId = id;
            NotificationHub.Send(tenant, message);
        }

        public static async Task SendToAdminsAsync(string tenant, Notification message)
        {
            message.Tenant = tenant;
            var id  = await Notifications.AddAsync(tenant, message).ConfigureAwait(false);

            message.NotificationId = id;
            NotificationHub.SendToAdmins(tenant, message);
        }

        public static async Task SendToAdminsAsync(string tenant, int officeId, Notification message)
        {
            message.Tenant = tenant;
            message.OfficeId = officeId;

            var id = await Notifications.AddAsync(tenant, message).ConfigureAwait(false);

            message.NotificationId = id;
            NotificationHub.SendToAdmins(tenant, officeId, message);
        }


        public static async Task SendToRolesAsync(string tenant, int roleId, Notification message)
        {
            message.Tenant = tenant;
            message.ToRoleId = roleId;

            var id = await Notifications.AddAsync(tenant, message).ConfigureAwait(false);

            message.NotificationId = id;
            NotificationHub.SendToRoles(tenant, roleId, message);
        }

        public static async Task SendToRolesAsync(string tenant, int roleId, int officeId, Notification message)
        {
            message.Tenant = tenant;
            message.OfficeId = roleId;
            message.ToRoleId = roleId;

            var id = await Notifications.AddAsync(tenant, message).ConfigureAwait(false);

            message.NotificationId = id;
            NotificationHub.SendToRoles(tenant, roleId, officeId, message);
        }

        public static async Task SendToUsersAsync(string tenant, int userId, Notification message)
        {
            message.Tenant = tenant;
            message.ToUserId = userId;

            var id = await Notifications.AddAsync(tenant, message).ConfigureAwait(false);

            message.NotificationId = id;
            NotificationHub.SendToUsers(tenant, userId, message);
        }

        public static async Task SendToUsersAsync(string tenant, int userId, int officeId, Notification message)
        {
            message.Tenant = tenant;
            message.OfficeId = userId;
            message.ToUserId = userId;

            var id = await Notifications.AddAsync(tenant, message).ConfigureAwait(false);

            message.NotificationId = id;
            NotificationHub.SendToUsers(tenant, userId, officeId, message);
        }

        public static async Task SendToLoginAsync(string tenant, long loginId, Notification message)
        {
            message.Tenant = tenant;
            message.ToLoginId = loginId;

            var id = await Notifications.AddAsync(tenant, message).ConfigureAwait(false);

            message.NotificationId = id;
            NotificationHub.SendToLogin(tenant, loginId, message);
        }
    }
}