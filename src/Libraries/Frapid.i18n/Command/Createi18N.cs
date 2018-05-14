using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using frapid;
using frapid.Commands;
using frapid.Commands.Create;
using Frapid.Configuration;
using Frapid.Configuration.Models;
using Frapid.i18n.ResourceBuilder;

namespace Frapid.i18n.Command
{
    public class Createi18N : CreateCommand
    {
        public override string Syntax { get; } = "create resource [on <AppName>]\r\ncreate resource";
        public override string Name { get; } = "resource";

        public override bool IsValid { get; set; }
        public string OnToken { get; private set; }
        public string AppName { get; private set; }

        public override void Initialize()
        {
            this.OnToken = this.Line.GetTokenOn(2);
            this.AppName = this.Line.GetTokenOn(3);
        }

        public override void Validate()
        {
            this.IsValid = false;

            if (this.Line.CountTokens() == 2)
            {
                this.IsValid = true;
                return;
            }

            if (this.OnToken.ToUpperInvariant() != "ON")
            {
                CommandProcessor.DisplayError(this.Syntax, $"Invalid token data {this.OnToken}");
                return;
            }

            string path = @"{0}\Areas\{1}\AppInfo.json";
            string directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..");

            if (this.AppName.ToUpperInvariant() == "FRAPID")
            {
                this.IsValid = true;
                return;
            }

            path = string.Format(path, directory, this.AppName);

            if (!File.Exists(path))
            {
                CommandProcessor.DisplayError(string.Empty, "Invalid application name \"{0}\".", this.AppName);
                return;
            }

            this.IsValid = true;
        }


        public override async Task ExecuteCommandAsync()
        {
            if (!this.IsValid)
            {
                return;
            }
            await Task.Delay(0).ConfigureAwait(false);

            if (this.Line.CountTokens() > 2)
            {
                var app = Installables.GetInstallables().First(x => x.ApplicationName.ToUpperInvariant() == this.AppName.ToUpperInvariant());

                if (app == null)
                {
                    return;
                }

                this.CreateResource(app);
                return;
            }

            var apps = this.GetApps();

            foreach (var app in apps)
            {
                this.CreateResource(app);
            }
        }

        private List<Installable> GetApps()
        {
            var installables = Installables.GetInstallables();
            return installables.Where(x => x.Hasi18N).ToList();
        }

        private void CreateResource(Installable app)
        {
            Console.WriteLine("Creating resource on " + app.ApplicationName);
            var approved = new ApprovedDomainSerializer().Get().FirstOrDefault();

            if (approved == null)
            {
                return;
            }

            string tenant = TenantConvention.GetTenant(approved.DomainName);

            var writer = new ResourceWriter(tenant, app);
            writer.WriteAsync().GetAwaiter().GetResult();
        }
    }
}