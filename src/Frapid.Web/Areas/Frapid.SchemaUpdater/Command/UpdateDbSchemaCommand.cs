using System;
using System.Threading.Tasks;
using frapid;
using frapid.Commands;
using Frapid.Configuration;

namespace Frapid.SchemaUpdater.Command
{
    public class UpdateDbSchemaCommand : UpdateCommand
    {
        public override string Syntax { get; } = "update database schema on site <SiteName>";
        public override string Name { get; } = "database";
        public override bool IsValid { get; set; }
        public string DomainName { get; private set; }
        public string Tenant { get; private set; }

        public override void Initialize()
        {
            this.DomainName = this.Line.GetTokenOn(5);

            if (string.IsNullOrWhiteSpace(this.DomainName))
            {
                return;
            }

            this.Tenant = TenantConvention.GetDbNameByConvention(this.DomainName);
        }

        public override void Validate()
        {
            this.IsValid = false;

            if (this.Line.CountTokens() != 6)
            {
                CommandProcessor.DisplayError(this.Syntax, "Invalid command {0}", this.Line);
                return;
            }

            if (this.Line.GetTokenOn(2).ToLowerInvariant() != "schema")
            {
                CommandProcessor.DisplayError(this.Syntax, "Invalid token {0}", this.Line.GetTokenOn(2));
                return;
            }

            if (this.Line.GetTokenOn(3).ToLowerInvariant() != "on")
            {
                CommandProcessor.DisplayError(this.Syntax, "Invalid token {0}", this.Line.GetTokenOn(3));
                return;
            }

            if (this.Line.GetTokenOn(4).ToLowerInvariant() != "site")
            {
                CommandProcessor.DisplayError(this.Syntax, "Invalid token {0}", this.Line.GetTokenOn(4));
                return;
            }

            this.IsValid = true;
        }

        public override async Task ExecuteCommandAsync()
        {
            await Task.Delay(1).ConfigureAwait(false);

            if (!this.IsValid)
            {
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Installing update on tenant \"{0}\".", this.Tenant);
            Console.ForegroundColor = ConsoleColor.White;

            var installer = new Installer();
            await installer.Start(this.Tenant).ConfigureAwait(true);
        }
    }
}