using System;
using System.IO;
using System.Text;
using Frapid.Framework;

namespace frapid.Commands.Create
{
    internal sealed class MvcProjectCreator
    {
        internal MvcProjectCreator(string projectName)
        {
            this.ProjectName = projectName;
            this.TempDirectory = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Temp", this.ProjectName + "-" + this.GetRandomNumber());
        }

        internal string TempDirectory { get; }
        internal string ProjectName { get; }

        private int GetRandomNumber()
        {
            var rnd = new Random();
            return rnd.Next(52);
        }

        internal void Create()
        {
            try
            {
                this.CreateTempDirectory();
                this.CopyProject();
                this.EditContents();
                this.RenameFileNames();
                this.CreateArea();
            }
            finally
            {
                Directory.Delete(this.TempDirectory, true);
            }
        }

        private void CreateArea()
        {
            Console.WriteLine("Creating Area");
            string destination = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "Areas", this.ProjectName);
            Directory.CreateDirectory(destination);

            FileHelper.CopyDirectory(this.TempDirectory, destination);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("The app {0} has been created on the following directory {1}.", this.ProjectName, destination);
            Console.ForegroundColor = ConsoleColor.White;
        }

        private void CopyProject()
        {
            string source = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "Templates", "MVCProject");
            FileHelper.CopyDirectory(source, this.TempDirectory);

            Console.WriteLine("Copying project files");
        }

        private void EditContents()
        {
            Console.WriteLine("Editing contents");
            this.ReplaceContent("MVCProject.csproj");
            this.ReplaceContent("MVCProject.sln");
            this.ReplaceContent(@"Properties\AssemblyInfo.cs");
            this.ReplaceContent(@"Views\web.config");
            this.ReplaceContent("AreaRegistration.cs");
        }

        private void RenameFile(string original, string renamed)
        {
            string originalFile = Path.Combine(this.TempDirectory, original);
            string renamedFile = Path.Combine(this.TempDirectory, renamed);

            if (File.Exists(originalFile))
            {
                File.Move(originalFile, renamedFile);
            }
        }

        private void ReplaceContent(string fileName)
        {
            string file = Path.Combine(this.TempDirectory, fileName);
            string contents = File.ReadAllText(file, Encoding.UTF8);

            contents = contents.Replace("MVCProject", this.ProjectName);

            File.WriteAllText(file, contents, Encoding.UTF8);
        }

        private void RenameFileNames()
        {
            Console.WriteLine("Renaming files");
            this.RenameFile("MVCProject.csproj", this.ProjectName + ".csproj");
            this.RenameFile("MVCProject.sln", this.ProjectName + ".sln");
            this.RenameFile("MVCProject.sln.DotSettings", this.ProjectName + ".sln.DotSettings");
            this.RenameFile("MVCProject.sln.DotSettings.user", this.ProjectName + ".sln.DotSettings.user");
        }

        private void CreateTempDirectory()
        {
            Console.WriteLine("Creating temp directory {0}.", this.TempDirectory);
            Directory.CreateDirectory(this.TempDirectory);
        }
    }
}