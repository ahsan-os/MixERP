using System.Threading.Tasks;
using frapid;
using frapid.Commands;
using frapid.Commands.Create;
using Frapid.Configuration;
using Frapid.Installer.Tenant;

namespace Frapid.Installer.Commands
{
    public class CreateSite : CreateCommand
    {
        public override string Syntax { get; } = "create site <DomainName> provider <ProviderName> [cleanup when done] [without sample]";
        public override string Name { get; } = "site";
        public override bool IsValid { get; set; }
        public string DomainName { get; private set; }
        public string ProviderName { get; private set; }
        public bool CleanupWhenDone { get; set; }
        public bool WithoutSample { get; set; }

        public override void Initialize()
        {
            this.DomainName = this.Line.GetTokenOn(2);

            string type = this.Line.GetTokenOn(3);

            if (type.ToLower().Equals("provider"))
            {
                this.ProviderName = this.Line.GetTokenOn(4);
            }

            var cleanupCommand = new[]
            {
                this.Line.GetTokenOn(5),
                this.Line.GetTokenOn(6),
                this.Line.GetTokenOn(7)
            };

            if (string.Join(" ", cleanupCommand).ToUpperInvariant().Equals("CLEANUP WHEN DONE"))
            {
                this.CleanupWhenDone = true;
            }

            this.WithoutSample = this.Line.ToLower().Contains("without sample");
        }

        public override void Validate()
        {
            this.IsValid = false;

            //if (this.Line.CountTokens() > 5 && !this.CleanupWhenDone)
            //{
            //    CommandProcessor.DisplayError(this.Syntax, "Invalid token {0}", this.Line.GetTokenOn(5));
            //    return;
            //}

            //if (this.Line.CountTokens() > 8 && this.CleanupWhenDone)
            //{
            //    CommandProcessor.DisplayError(this.Syntax, "Invalid token {0}", this.Line.GetTokenOn(8));
            //    return;
            //}

            string type = this.Line.GetTokenOn(1);
            if (!type.ToUpperInvariant().Equals("SITE"))
            {
                CommandProcessor.DisplayError(this.Syntax, "{0} is not a well-known terminology. Expecting: \"site\".", type);
                return;
            }

            string provider = this.Line.GetTokenOn(3);

            if (!provider.ToUpperInvariant().Equals("PROVIDER"))
            {
                CommandProcessor.DisplayError(this.Syntax, "{0} is not a well-known terminology. Expecting: \"provider\".", provider);
                return;
            }

            if (string.IsNullOrWhiteSpace(this.DomainName))
            {
                CommandProcessor.DisplayError(this.Syntax, "Invalid domain name {0}.", this.DomainName);
                return;
            }

            if (string.IsNullOrWhiteSpace(this.ProviderName))
            {
                CommandProcessor.DisplayError(this.Syntax, "Invalid provider name {0}.", this.ProviderName);
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

            ConsolePathMapper.SetPathToRoot();

            try
            {
                new ApprovedDomainSerializer().Add
                (
                    new ApprovedDomain
                    {
                        DomainName = this.DomainName,
                        DbProvider = this.ProviderName,
                        Synonyms = new string[0],
                        BackupDirectory = string.Empty,
                        AdminEmail = string.Empty,
                        EnforceSsl = false,
                        BackupDirectoryIsFixedPath = false,
                        CdnDomain = string.Empty
                    });


                var installer = new Tenant.Installer(this.DomainName, this.WithoutSample);
                await installer.InstallAsync().ConfigureAwait(false);
            }
            finally
            {
                if (this.CleanupWhenDone)
                {
                    var uninstaller = new Uninstaller(this.DomainName);
                    await uninstaller.UnInstallAsync().ConfigureAwait(false);
                }
            }
        }
    }
}