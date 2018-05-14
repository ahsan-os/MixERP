using System;
using System.IO;
using System.Threading.Tasks;

namespace frapid.Commands.Create
{
    public class CreateApp : CreateCommand
    {
        public override string Syntax { get; } = "create app <AppName>";
        public override string Name { get; } = "app";
        public override bool IsValid { get; set; }
        public string AppName { get; private set; }

        public override void Initialize()
        {
            this.AppName = this.Line.GetTokenOn(2);
        }

        public override void Validate()
        {
            this.IsValid = false;

            if (string.IsNullOrWhiteSpace(this.AppName))
            {
                CommandProcessor.DisplayError(this.Syntax, "App name was not given.");
                return;
            }

            string path = @"{0}\Areas\{1}";
            string directory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..");

            path = string.Format(path, directory, this.AppName);

            if (Directory.Exists(path))
            {
                CommandProcessor.DisplayError(string.Empty, "The application {0} already exists.", this.AppName);
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

            Console.WriteLine("Creating app " + this.AppName);
            var mvc = new MvcProjectCreator(this.AppName);
            mvc.Create();
        }
    }
}