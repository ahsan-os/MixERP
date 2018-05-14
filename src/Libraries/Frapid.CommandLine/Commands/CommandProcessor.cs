using System;
using Frapid.Framework.Extensions;

namespace frapid.Commands
{
    public static class CommandProcessor
    {
        public static void Process(string line)
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                return;
            }

            string commandName = line.Split(' ')[0];
            var command = Get(commandName, line);

            command?.Execute();
        }

        private static ICommand Get(string commandName, string line)
        {
            var iType = typeof(ICommand);
            var members = iType.GetTypeMembers<ICommand>();

            foreach (var member in members)
            {
                if (member.CommandName == commandName)
                {
                    member.Line = line;
                    return member;
                }
            }

            return null;
        }

        public static void DisplayError(string syntax, string format, params object[] args)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Error : " + format, args);

            Console.ForegroundColor = ConsoleColor.White;

            if (string.IsNullOrWhiteSpace(syntax))
            {
                return;
            }

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\r\nSyntax(es) : \r\n" + syntax);
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}