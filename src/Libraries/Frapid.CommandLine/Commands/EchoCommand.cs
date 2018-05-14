using System;
using System.Linq;

namespace frapid.Commands
{
    public sealed class EchoCommand : ICommand
    {
        public string Syntax { get; } = "echo <Your String>";
        public string Line { get; set; }

        public string CommandName { get; } = "echo";

        public void Execute()
        {
            string message = string.Join(" ", this.Line.Split(' ').Skip(1));
            Console.WriteLine(message);
        }
    }
}