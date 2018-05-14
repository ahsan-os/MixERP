using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Frapid.Configuration;
using static System.String;

namespace frapid.Commands.Create
{
    public class CreateTheme: CreateCommand
    {
        public override string Syntax { get; } = "create theme <ThemeName> on instance <Instance>\r\ncreate theme <ThemeName> on domain <Domain>";
        public override string Name { get; } = "theme";
        public override bool IsValid { get; set; }
        public string ThemeName { get; private set; }
        public string InstanceName { get; private set; }
        public string DomainName { get; private set; }

        public override void Initialize()
        {
            this.ThemeName = this.Line.GetTokenOn(2);

            string type = this.Line.GetTokenOn(4);

            if(type.ToLower().Equals("instance"))
            {
                this.InstanceName = this.Line.GetTokenOn(5);
                return;
            }

            this.DomainName = this.Line.GetTokenOn(5);

            if(IsNullOrWhiteSpace(this.DomainName))
            {
                return;
            }

            this.InstanceName = TenantConvention.GetDbNameByConvention(this.DomainName);
        }

        private bool CheckInstance()
        {
            string path = @"{0}\Tenants\{1}";
            string directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..");

            path = Format(path, directory, this.InstanceName);

            if(!Directory.Exists(path))
            {
                CommandProcessor.DisplayError(Empty, "The instance {0} was not found.", this.InstanceName);
                return false;
            }

            return true;
        }

        public override void Validate()
        {
            this.IsValid = false;

            if(this.Line.CountTokens() > 3 &&
               !this.Line.GetTokenOn(3).ToLower().Equals("on"))
            {
                CommandProcessor.DisplayError(this.Syntax, "Invalid token {0}", this.Line.GetTokenOn(3));
                return;
            }

            var types = new[]
                        {
                            "instance",
                            "domain"
                        };
            if(this.Line.CountTokens() > 4 &&
               !types.Contains(this.Line.GetTokenOn(4).ToLower()))
            {
                CommandProcessor.DisplayError(Empty, "Invalid token {0}", this.Line.GetTokenOn(4));
                return;
            }

            if(IsNullOrWhiteSpace(this.ThemeName))
            {
                CommandProcessor.DisplayError(Empty, "Theme name was not mentioned.");
                return;
            }

            if(IsNullOrWhiteSpace(this.InstanceName) &&
               IsNullOrWhiteSpace(this.DomainName))
            {
                CommandProcessor.DisplayError(Empty, "Instance or domain is empty.");
            }

            this.IsValid = this.CheckInstance();
        }

        public override async Task ExecuteCommandAsync()
        {
            await Task.Delay(1).ConfigureAwait(false);

            if(!this.IsValid)
            {
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("Todo: create a theme named \"{0}\" on \"{1}\" instance.", this.ThemeName, this.InstanceName);
            Console.WriteLine("Theme was not created.");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}