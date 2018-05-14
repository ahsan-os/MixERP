namespace frapid.Commands
{
    public interface ICommand
    {
        string Syntax { get; }
        string Line { get; set; }
        string CommandName { get; }
        void Execute();
    }
}