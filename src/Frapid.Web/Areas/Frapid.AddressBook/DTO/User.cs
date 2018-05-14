using Frapid.DataAccess;
using Frapid.Mapper.Decorators;

namespace Frapid.AddressBook.DTO
{
    [TableName("account.users")]
    [PrimaryKey("user_id", AutoIncrement = true)]
    public sealed class User : IPoco
    {
        public int UserId { get; set; }
        public string Name { get; set; }
    }
}