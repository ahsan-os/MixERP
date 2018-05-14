using Frapid.AddressBook.DTO;
using Frapid.Calendar.DTO;

namespace Frapid.Calendar.Contracts
{
    public sealed class ReminderMessage
    {
        public Contact Contact { get; set; }
        public Event Event { get; set; } 
    }
}