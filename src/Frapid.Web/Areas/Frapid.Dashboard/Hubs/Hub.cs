using Microsoft.AspNet.SignalR;

namespace Frapid.Dashboard.Hubs
{
    public abstract class Hub<T> : Hub where T : Hub
    {
        // ReSharper disable once StaticMemberInGenericType
        private static IHubContext _context;
        public static IHubContext HubContext => _context ?? (_context = GlobalHost.ConnectionManager.GetHubContext<T>());
    }
}