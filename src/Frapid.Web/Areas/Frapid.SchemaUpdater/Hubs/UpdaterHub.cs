using System.Threading;
using Frapid.ApplicationState.Cache;
using Frapid.Areas.Authorization.Helpers;
using Microsoft.AspNet.SignalR;

namespace Frapid.SchemaUpdater.Hubs
{
    public sealed class UpdaterHub : Hub
    {
        public void Update()
        {
            if (!this.IsValidRequest())
            {
                this.Clients.Caller.getNotification("Access is denied.");
                return;
            }

            var installer = new Installer();
            installer.Notification += this.OnNotification;

            string tenant = AppUsers.GetTenant();
            installer.StartInBackground(tenant);
        }

        private void OnNotification(object sender, string message)
        {
            this.Clients.Caller.getUpdaterNotification(message);
        }

        private bool IsValidRequest()
        {
            Thread.Sleep(2000);

            if (this.Context == null)
            {
                this.Clients.Caller.getNotification("Access is denied.");
                return false;
            }

            string tenant = AppUsers.GetTenant();
            long loginId = HubAuthorizationManger.GetLoginIdAsync(tenant, this.Context).GetAwaiter().GetResult();
            var meta = AppUsers.GetCurrent(tenant, loginId);

            if (loginId <= 0)
            {
                this.Clients.Caller.getNotification("Access is denied.");
                return false;
            }

            return meta.IsAdministrator;
        }
    }
}