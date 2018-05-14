using System.Threading.Tasks;
using Frapid.DataAccess;
using Frapid.DataAccess.Models;
using Frapid.i18n;

namespace Frapid.DbPolicy
{
    public abstract class DbAccess : IDbAccess
    {
        // ReSharper disable once InconsistentNaming
        public abstract string _ObjectNamespace { get; }
        // ReSharper disable once InconsistentNaming
        public abstract string _ObjectName { get; }
        public bool Validated { get; private set; }
        public bool SkipValidation { get; set; }
        public bool HasAccess { get; private set; }

        /// <summary>
        ///     Validates application user access rights to execute the function.
        /// </summary>
        /// <param name="type">The access type being validated.</param>
        /// <param name="loginId">The login ID of application user making the request.</param>
        /// <param name="database">The name of the database on which policy is being validated on.</param>
        /// <param name="noException">
        ///     If this is switched off, UnauthorizedException is not thrown even when the caller does not
        ///     have access rights to this function.
        /// </param>
        public async Task ValidateAsync(AccessTypeEnum type, long loginId, string database, bool noException)
        {
            var policy = new PolicyValidator
            {
                ObjectNamespace = this._ObjectNamespace,
                ObjectName = this._ObjectName,
                LoginId = loginId,
                Tenant = database,
                AccessType = type
            };

            await policy.ValidateAsync().ConfigureAwait(false);
            this.HasAccess = policy.HasAccess;

            this.Validated = true;


            if (this.HasAccess)
            {
                return;
            }

            if (!noException)
            {
                throw new UnauthorizedException(Resources.AccessIsDenied);
            }
        }
    }
}