using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Frapid.ApplicationState.Cache;
using Frapid.Areas.Authorization.Helpers;
using Frapid.Configuration;
using Frapid.Dashboard.DTO;
using Frapid.Dashboard.HubModels;
using Mapster;

namespace Frapid.Dashboard.Hubs
{
    public class NotificationHub : Hub<NotificationHub>
    {
        public static ConcurrentDictionary<string, UserInfo> Connections = new ConcurrentDictionary<string, UserInfo>();

        public override Task OnConnected()
        {
            string connectionId = this.Context.ConnectionId;
            string tenant = TenantConvention.GetTenant(this.Context.Request.Url.DnsSafeHost);
            long loginId = HubAuthorizationManger.GetLoginIdAsync(tenant, this.Context).GetAwaiter().GetResult();

            var model = AppUsers.GetCurrentAsync(tenant, loginId).GetAwaiter().GetResult().Adapt<UserInfo>();

            if (model == null)
            {
                return base.OnConnected();
            }

            model.Tenant = tenant;

            Connections[connectionId] = model;

            return base.OnConnected();
        }

        public static void Send(string tenant, Notification message)
        {
            var users = GetAllRecipients(tenant);
            HubContext.Clients.Clients(users.ToList()).notificationReceived(message);
        }

        public static void SendToAdmins(string tenant, Notification message)
        {
            var users = GetAdmins(tenant);
            HubContext.Clients.Clients(users.ToList()).notificationReceived(message);
        }

        public static void SendToAdmins(string tenant, int officeId, Notification message)
        {
            var users = GetAdmins(tenant, officeId);
            HubContext.Clients.Clients(users.ToList()).notificationReceived(message);
        }

        public static void SendToRoles(string tenant, int roleId, Notification message)
        {
            var users = GetRecipientsByRoleId(tenant, roleId);
            HubContext.Clients.Clients(users.ToList()).notificationReceived(message);
        }

        public static void SendToRoles(string tenant, int roleId, int officeId, Notification message)
        {
            var users = GetRecipientsByRoleId(tenant, roleId, officeId);
            HubContext.Clients.Clients(users.ToList()).notificationReceived(message);
        }

        public static void SendToUsers(string tenant, int userId, Notification message)
        {
            var users = GetRecipientsByUserId(tenant, userId);
            HubContext.Clients.Clients(users.ToList()).notificationReceived(message);
        }

        public static void SendToUsers(string tenant, int userId, int officeId, Notification message)
        {
            var users = GetRecipientsByUserId(tenant, userId, officeId);
            HubContext.Clients.Clients(users.ToList()).notificationReceived(message);
        }

        public static void SendToLogin(string tenant, long loginId, Notification message)
        {
            var users = GetRecipient(tenant, loginId);
            HubContext.Clients.Clients(users.ToList()).notificationReceived(message);
        }

        #region Query

        private static IEnumerable<string> GetAllRecipients(string tenant)
        {
            return from connection in Connections where connection.Value.Tenant.ToUpperInvariant() == tenant.ToUpperInvariant() select connection.Key;
        }

        private static IEnumerable<string> GetAdmins(string tenant)
        {
            return from connection in Connections
                   where connection.Value.Tenant.ToUpperInvariant() == tenant.ToUpperInvariant()
                   where connection.Value.IsAdministrator
                   select connection.Key;
        }

        private static IEnumerable<string> GetAdmins(string tenant, int officeId)
        {
            return from connection in Connections
                   where connection.Value.Tenant.ToUpperInvariant() == tenant.ToUpperInvariant()
                   where connection.Value.IsAdministrator
                   where connection.Value.OfficeId == officeId
                   select connection.Key;
        }

        private static IEnumerable<string> GetRecipientsByRoleId(string tenant, int roleId)
        {
            return from connection in Connections
                where connection.Value.Tenant.ToUpperInvariant() == tenant.ToUpperInvariant()
                where connection.Value.RoleId == roleId
                select connection.Key;
        }

        private static IEnumerable<string> GetRecipientsByRoleId(string tenant, int roleId, int officeId)
        {
            return from connection in Connections
                where connection.Value.Tenant.ToUpperInvariant() == tenant.ToUpperInvariant()
                where connection.Value.OfficeId == officeId
                where connection.Value.RoleId == roleId
                select connection.Key;
        }

        private static IEnumerable<string> GetRecipientsByUserId(string tenant, int userId)
        {
            return from connection in Connections
                where connection.Value.Tenant.ToUpperInvariant() == tenant.ToUpperInvariant()
                where connection.Value.UserId == userId
                select connection.Key;
        }

        private static IEnumerable<string> GetRecipientsByUserId(string tenant, int userId, int officeId)
        {
            return from connection in Connections
                where connection.Value.Tenant.ToUpperInvariant() == tenant.ToUpperInvariant()
                where connection.Value.OfficeId == officeId
                where connection.Value.UserId == userId
                select connection.Key;
        }

        private static IEnumerable<string> GetRecipient(string tenant, long loginId)
        {
            return from connection in Connections
                where connection.Value.Tenant.ToUpperInvariant() == tenant.ToUpperInvariant()
                where connection.Value.LoginId == loginId
                select connection.Key;
        }

        #endregion
    }
}