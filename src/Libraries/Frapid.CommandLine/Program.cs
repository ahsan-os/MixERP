using System;
using System.Linq;
using System.Text;
using frapid.Commands;
using Frapid.Configuration;
using Frapid.Framework.Extensions;

namespace frapid
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            ConsolePathMapper.SetPathToRoot();
            Console.OutputEncoding = Encoding.UTF8;

            var loader = new AssemblyLoader();
            loader.PreLoad();

            bool exit = false;

            string command;

            if (args != null &&
                args.Any())
            {
                command = string.Join(" ", args);
                CommandProcessor.Process(command);
                exit = true;
            }

            while (!exit)
            {
                command = FrapidConsole.ReadLine();
                exit = GotQuitSignalFrom(command);
                command = CheckClearSignal(command);

                if (!exit)
                {
                    CommandProcessor.Process(command);
                }
            }
        }

        private static string CheckClearSignal(string command)
        {
            var candidates = new[]
            {
                "cls",
                "clear"
            };
            bool clear = candidates.Contains(command.ToLower());

            if (clear)
            {
                Console.Clear();
                return string.Empty;
            }

            //Pass
            return command;
        }

        private static bool GotQuitSignalFrom(string command)
        {
            return command.Or("").ToLower().Equals("exit");
        }
    }
}