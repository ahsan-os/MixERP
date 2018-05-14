using System.Collections.Generic;
using Frapid.Framework;
using Frapid.Framework.Extensions;

namespace Frapid.Web.Application
{
    public static class DefaultExceptionLogger
    {
        internal static IEnumerable<IExceptionLogger> Get()
        {
            var type = typeof(IExceptionLogger);
            var candidates = type.GetTypeMembersNotAbstract<IExceptionLogger>();

            return candidates;
        }

        public static void Log(string tenant, int userId, string userName, string officeName, string message)
        {
            var loggers = Get();

            foreach (var logger in loggers)
            {
                logger.Tenant = tenant;
                logger.UserName = userName;
                logger.Message = message;
                logger.OfficeName = officeName;
                logger.UserId = userId;

                logger.LogError();
            }
        }
    }
}