using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Frapid.SchemaUpdater.Helpers;
using Serilog;

namespace Frapid.SchemaUpdater
{
    public class Installer
    {
        public event EventHandler<string> Notification;

        public void StartInBackground(string tenant)
        {
            new Thread(async () =>
            {
                Thread.CurrentThread.IsBackground = true;
                await this.Start(tenant).ConfigureAwait(true);
            }).Start();
        }

        public async Task Start(string tenant)
        {
            var apps = AppDiscovery.Discover().ToList();

            foreach (var app in apps)
            {
                try
                {
                    string message = await Updater.UpdateAsync(tenant, app).ConfigureAwait(false);

                    if (!string.IsNullOrWhiteSpace(message))
                    {
                        this.Notify(message);
                    }
                }
                catch (Exception ex)
                {
                    string message = $"Error: Could not install updates for application \"{app.ApplicationName}\" on tenant {tenant} due to errors.";

                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine(message);
                    Console.ForegroundColor = ConsoleColor.White;

                    this.Notify(message);
                    Log.Error("Error: Could not install updates for application \"{ApplicationName}\" on tenant {tenant} due to errors. Exception: {Exception}", app.ApplicationName, tenant, ex);
                    throw;
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            this.Notify("OK");
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void Notify(string message)
        {
            var notificationReceived = this.Notification;
            notificationReceived?.Invoke(this, message);
            Console.WriteLine(message);
        }
    }
}