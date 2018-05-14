using System;
using System.Linq;
using Serilog;

namespace Frapid.Installer.Helpers
{
    public static class InstallerLog
    {
        public static void Verbose(string messageTemplate, params object[] propertyValues)
        {
            Log.Verbose(messageTemplate, propertyValues);

            if(propertyValues != null &&
               propertyValues.Any())
            {
                Console.WriteLine(messageTemplate, propertyValues);
            }

            Console.WriteLine(messageTemplate);
        }

        public static void Information(string messageTemplate, params object[] propertyValues)
        {
            Log.Verbose(messageTemplate, propertyValues);

            Console.ForegroundColor = ConsoleColor.Yellow;

            if(propertyValues != null &&
               propertyValues.Any())
            {
                Console.WriteLine(messageTemplate, propertyValues);
            }

            Console.WriteLine(messageTemplate);

            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Error(string messageTemplate, params object[] propertyValues)
        {
            Log.Error(messageTemplate, propertyValues);

            Console.ForegroundColor = ConsoleColor.Red;

            if(propertyValues != null &&
               propertyValues.Any())
            {
                Console.WriteLine(messageTemplate, propertyValues);
            }

            Console.WriteLine(messageTemplate);

            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}