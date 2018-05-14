using System;

namespace frapid
{
    public static class FrapidConsole
    {
        private static readonly string FrapidPrompt = "frapid>";

        public static string ReadLine()
        {
            Console.Write(FrapidPrompt);
            return Console.ReadLine();
        }
    }
}