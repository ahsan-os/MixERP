using System.Linq;
using System.Threading.Tasks;
using Frapid.Framework.Extensions;

namespace frapid.Commands
{
    public abstract class PackCommand : ICommand
    {
        public abstract string Name { get; }
        public abstract bool IsValid { get; set; }
        public abstract string Syntax { get; }
        public string Line { get; set; }
        public string CommandName { get; } = "pack";

        public void Execute()
        {
            string resourceType = this.Line.Split(' ')[1];

            var iType = typeof(PackCommand);

            var members = iType.GetTypeMembersNotAbstract<PackCommand>();

            var member = members.FirstOrDefault(m => m.Name == resourceType);

            if (member != null)
            {
                member.Line = this.Line;
                member.Initialize();
                member.Validate();
                member.ExecuteCommandAsync().GetAwaiter().GetResult();
            }
        }

        public abstract void Initialize();
        public abstract void Validate();
        public abstract Task ExecuteCommandAsync();
    }
}