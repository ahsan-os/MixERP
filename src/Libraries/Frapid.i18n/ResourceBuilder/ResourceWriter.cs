using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Frapid.Configuration;
using Frapid.Configuration.Models;
using Frapid.Framework.Extensions;

namespace Frapid.i18n.ResourceBuilder
{
    public sealed class ResourceWriter
    {
        public ResourceWriter(string tenant, Installable app)
        {
            this.Tenant = tenant;
            this.App = app;
        }

        public Installable App { get; }
        public string Tenant { get; }

        private static string GetILocalizeTemplate(string resourceClassName)
        {
            var builder = new StringBuilder();
            builder.Append("\tpublic sealed class Localize : ILocalize");
            builder.Append(Environment.NewLine);
            builder.Append("\t{");
            builder.Append(Environment.NewLine);
            builder.Append("\t\tpublic Dictionary<string, string> GetResources(CultureInfo culture)");
            builder.Append(Environment.NewLine);
            builder.Append("\t\t{");
            builder.Append(Environment.NewLine);
            builder.Append("\t\t\tstring resourceDirectory = " + resourceClassName + ".ResourceDirectory;");
            builder.Append(Environment.NewLine);
            builder.Append("\t\t\treturn I18NResource.GetResources(resourceDirectory, culture);");
            builder.Append(Environment.NewLine);
            builder.Append("\t\t}");
            builder.Append(Environment.NewLine);
            builder.Append("\t}");
            builder.Append(Environment.NewLine);
            builder.Append(Environment.NewLine);

            return builder.ToString();
        }

        public async Task WriteAsync()
        {
            if (string.IsNullOrWhiteSpace(this.App.I18NSource))
            {
                this.App.I18NSource = this.App.DirectoryPath.Replace(PathMapper.PathToRootDirectory, string.Empty);
            }

            string pathToResource = Path.Combine(this.App.I18NSource, "i18n").Replace("\\", "/");
            string resourceFile = I18NResource.GetResourceFile(PathMapper.MapPath(pathToResource), string.Empty);

            if (string.IsNullOrWhiteSpace(resourceFile))
            {
                return;
            }

            string className = this.App.I18NClassName.Or("I18N");

            var builder = new StringBuilder();
            builder.Append("using System.Collections.Generic;");
            builder.Append(Environment.NewLine);
            builder.Append("using System.Globalization;");
            builder.Append(Environment.NewLine);
            builder.Append("using Frapid.Configuration;");
            builder.Append(Environment.NewLine);
            builder.Append("using Frapid.i18n;");
            builder.Append(Environment.NewLine);
            builder.Append(Environment.NewLine);
            builder.Append("namespace " + this.App.AssemblyName);
            builder.Append(Environment.NewLine);
            builder.Append("{");
            builder.Append(Environment.NewLine);
            builder.Append(GetILocalizeTemplate(className));
            builder.Append("\tpublic static class "  + className);
            builder.Append(Environment.NewLine);
            builder.Append("\t{");
            builder.Append(Environment.NewLine);
            builder.Append("\t\tpublic static string ResourceDirectory { get; }");
            builder.Append(Environment.NewLine);
            builder.Append(Environment.NewLine);

            builder.Append("\t\tstatic " + className + "()");
            builder.Append(Environment.NewLine);
            builder.Append("\t\t{");
            builder.Append(Environment.NewLine);
            builder.Append($"\t\t\tResourceDirectory = PathMapper.MapPath(\"{pathToResource}\");");
            builder.Append(Environment.NewLine);
            builder.Append("\t\t}");
            builder.Append(Environment.NewLine);
            builder.Append(Environment.NewLine);


            var resources = await ResourceReader.GetResourcesAsync(this.Tenant, this.App, resourceFile).ConfigureAwait(false);
            //Save up-to-date resource on disk

            YamlSerializer.SaveToDisk(resourceFile, resources);
            foreach (var resource in resources)
            {
                builder.Append("\t\t/// <summary>");
                builder.Append(Environment.NewLine);
                builder.Append("\t\t///");
                builder.Append(Regex.Replace(resource.Value, @"\r\n?|\n", string.Empty));
                builder.Append(Environment.NewLine);
                builder.Append("\t\t/// </summary>");
                builder.Append(Environment.NewLine);
                string format = "\t\tpublic static string {0} => I18NResource.GetString(ResourceDirectory, \"{0}\");";
                builder.Append(string.Format(format, resource.Key));
                builder.Append(Environment.NewLine);
                builder.Append(Environment.NewLine);
            }


            builder.Append("\t}");
            builder.Append(Environment.NewLine);
            builder.Append("}");
            builder.Append(Environment.NewLine);


            string target = Path.Combine(this.App.DirectoryPath, className + ".cs");

            if (!string.IsNullOrWhiteSpace(this.App.I18NTarget))
            {
                target = Path.Combine(this.App.DirectoryPath, this.App.I18NTarget, className + ".cs");
            }

            Console.WriteLine($"Writing resource on " + target);
            Console.WriteLine(builder.ToString());
            File.WriteAllText(target, builder.ToString(), Encoding.UTF8);
        }
    }
}