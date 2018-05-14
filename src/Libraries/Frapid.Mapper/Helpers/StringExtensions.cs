using Frapid.Framework.Extensions;

namespace Frapid.Mapper.Helpers
{
    public static class StringExtensions
    {
        public static string ToSqlLikeExpression(this string token)
        {
            return "%" + token.Or("") + "%";
        }
    }
}