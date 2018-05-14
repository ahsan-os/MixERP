using Frapid.DataAccess;
using Frapid.Mapper.Decorators;

namespace Frapid.Account.DTO
{
    [TableName("account.configuration_profiles")]
    public class ConfigurationProfile : IPoco
    {
        public int ConfigurationProfileId { get; set; }
        public string ProfileName { get; set; }
        public bool IsActive { get; set; }
        public bool AllowRegistration { get; set; }
        public int RegistrationRoleId { get; set; }
        public int RegistrationOfficeId { get; set; }
        public bool AllowFacebookRegistration { get; set; }
        public bool AllowGoogleRegistration { get; set; }
        public string GoogleSigninClientId { get; set; }
        public string GoogleSigninScope { get; set; }
        public string FacebookAppId { get; set; }
        public string FacebookScope { get; set; }
    }
}