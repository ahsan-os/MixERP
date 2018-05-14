using System.Collections.Generic;
using System.Globalization;
using Frapid.Configuration;
using Frapid.i18n;

namespace Frapid.Account
{
	public sealed class Localize : ILocalize
	{
		public Dictionary<string, string> GetResources(CultureInfo culture)
		{
			string resourceDirectory = I18N.ResourceDirectory;
			return I18NResource.GetResources(resourceDirectory, culture);
		}
	}

	public static class I18N
	{
		public static string ResourceDirectory { get; }

		static I18N()
		{
			ResourceDirectory = PathMapper.MapPath("/Areas/Frapid.Account/i18n");
		}

		/// <summary>
		///Account
		/// </summary>
		public static string Account => I18NResource.GetString(ResourceDirectory, "Account");

		/// <summary>
		///Culture
		/// </summary>
		public static string Culture => I18NResource.GetString(ResourceDirectory, "Culture");

		/// <summary>
		///Status
		/// </summary>
		public static string Status => I18NResource.GetString(ResourceDirectory, "Status");

		/// <summary>
		///Privacy Policy Url
		/// </summary>
		public static string PrivacyPolicyUrl => I18NResource.GetString(ResourceDirectory, "PrivacyPolicyUrl");

		/// <summary>
		///Header
		/// </summary>
		public static string Header => I18NResource.GetString(ResourceDirectory, "Header");

		/// <summary>
		///Office Email
		/// </summary>
		public static string OfficeEmail => I18NResource.GetString(ResourceDirectory, "OfficeEmail");

		/// <summary>
		///Description
		/// </summary>
		public static string Description => I18NResource.GetString(ResourceDirectory, "Description");

		/// <summary>
		///Pan Number
		/// </summary>
		public static string PanNumber => I18NResource.GetString(ResourceDirectory, "PanNumber");

		/// <summary>
		///Subject
		/// </summary>
		public static string Subject => I18NResource.GetString(ResourceDirectory, "Subject");

		/// <summary>
		///Office
		/// </summary>
		public static string Office => I18NResource.GetString(ResourceDirectory, "Office");

		/// <summary>
		///Domain Id
		/// </summary>
		public static string DomainId => I18NResource.GetString(ResourceDirectory, "DomainId");

		/// <summary>
		///Is Active
		/// </summary>
		public static string IsActive => I18NResource.GetString(ResourceDirectory, "IsActive");

		/// <summary>
		///Email
		/// </summary>
		public static string Email => I18NResource.GetString(ResourceDirectory, "Email");

		/// <summary>
		///Browser
		/// </summary>
		public static string Browser => I18NResource.GetString(ResourceDirectory, "Browser");

		/// <summary>
		///Registered On
		/// </summary>
		public static string RegisteredOn => I18NResource.GetString(ResourceDirectory, "RegisteredOn");

		/// <summary>
		///Password
		/// </summary>
		public static string Password => I18NResource.GetString(ResourceDirectory, "Password");

		/// <summary>
		///Revoked On
		/// </summary>
		public static string RevokedOn => I18NResource.GetString(ResourceDirectory, "RevokedOn");

		/// <summary>
		///Office Id
		/// </summary>
		public static string OfficeId => I18NResource.GetString(ResourceDirectory, "OfficeId");

		/// <summary>
		///Currency Symbol
		/// </summary>
		public static string CurrencySymbol => I18NResource.GetString(ResourceDirectory, "CurrencySymbol");

		/// <summary>
		///Application Id
		/// </summary>
		public static string ApplicationId => I18NResource.GetString(ResourceDirectory, "ApplicationId");

		/// <summary>
		///Is Administrator
		/// </summary>
		public static string IsAdministrator => I18NResource.GetString(ResourceDirectory, "IsAdministrator");

		/// <summary>
		///Role Id
		/// </summary>
		public static string RoleId => I18NResource.GetString(ResourceDirectory, "RoleId");

		/// <summary>
		///Audit Ts
		/// </summary>
		public static string AuditTs => I18NResource.GetString(ResourceDirectory, "AuditTs");

		/// <summary>
		///Issued By
		/// </summary>
		public static string IssuedBy => I18NResource.GetString(ResourceDirectory, "IssuedBy");

		/// <summary>
		///Admin Email
		/// </summary>
		public static string AdminEmail => I18NResource.GetString(ResourceDirectory, "AdminEmail");

		/// <summary>
		///User Agent
		/// </summary>
		public static string UserAgent => I18NResource.GetString(ResourceDirectory, "UserAgent");

		/// <summary>
		///Address Line 2
		/// </summary>
		public static string AddressLine2 => I18NResource.GetString(ResourceDirectory, "AddressLine2");

		/// <summary>
		///Version Number
		/// </summary>
		public static string VersionNumber => I18NResource.GetString(ResourceDirectory, "VersionNumber");

		/// <summary>
		///Revoked
		/// </summary>
		public static string Revoked => I18NResource.GetString(ResourceDirectory, "Revoked");

		/// <summary>
		///Facebook Scope
		/// </summary>
		public static string FacebookScope => I18NResource.GetString(ResourceDirectory, "FacebookScope");

		/// <summary>
		///Fb User Id
		/// </summary>
		public static string FbUserId => I18NResource.GetString(ResourceDirectory, "FbUserId");

		/// <summary>
		///Last Browser
		/// </summary>
		public static string LastBrowser => I18NResource.GetString(ResourceDirectory, "LastBrowser");

		/// <summary>
		///Registration Role Id
		/// </summary>
		public static string RegistrationRoleId => I18NResource.GetString(ResourceDirectory, "RegistrationRoleId");

		/// <summary>
		///Zip Code
		/// </summary>
		public static string ZipCode => I18NResource.GetString(ResourceDirectory, "ZipCode");

		/// <summary>
		///Profile Name
		/// </summary>
		public static string ProfileName => I18NResource.GetString(ResourceDirectory, "ProfileName");

		/// <summary>
		///Registration Office Id
		/// </summary>
		public static string RegistrationOfficeId => I18NResource.GetString(ResourceDirectory, "RegistrationOfficeId");

		/// <summary>
		///Registration Date
		/// </summary>
		public static string RegistrationDate => I18NResource.GetString(ResourceDirectory, "RegistrationDate");

		/// <summary>
		///Terms Of Service Url
		/// </summary>
		public static string TermsOfServiceUrl => I18NResource.GetString(ResourceDirectory, "TermsOfServiceUrl");

		/// <summary>
		///Ip Address
		/// </summary>
		public static string IpAddress => I18NResource.GetString(ResourceDirectory, "IpAddress");

		/// <summary>
		///Confirmed
		/// </summary>
		public static string Confirmed => I18NResource.GetString(ResourceDirectory, "Confirmed");

		/// <summary>
		///Client Token
		/// </summary>
		public static string ClientToken => I18NResource.GetString(ResourceDirectory, "ClientToken");

		/// <summary>
		///Name
		/// </summary>
		public static string Name => I18NResource.GetString(ResourceDirectory, "Name");

		/// <summary>
		///Po Box
		/// </summary>
		public static string PoBox => I18NResource.GetString(ResourceDirectory, "PoBox");

		/// <summary>
		///State
		/// </summary>
		public static string State => I18NResource.GetString(ResourceDirectory, "State");

		/// <summary>
		///Token Id
		/// </summary>
		public static string TokenId => I18NResource.GetString(ResourceDirectory, "TokenId");

		/// <summary>
		///Registration Id
		/// </summary>
		public static string RegistrationId => I18NResource.GetString(ResourceDirectory, "RegistrationId");

		/// <summary>
		///Role Name
		/// </summary>
		public static string RoleName => I18NResource.GetString(ResourceDirectory, "RoleName");

		/// <summary>
		///Published On
		/// </summary>
		public static string PublishedOn => I18NResource.GetString(ResourceDirectory, "PublishedOn");

		/// <summary>
		///Browser Based App
		/// </summary>
		public static string BrowserBasedApp => I18NResource.GetString(ResourceDirectory, "BrowserBasedApp");

		/// <summary>
		///User Id
		/// </summary>
		public static string UserId => I18NResource.GetString(ResourceDirectory, "UserId");

		/// <summary>
		///Address Line 1
		/// </summary>
		public static string AddressLine1 => I18NResource.GetString(ResourceDirectory, "AddressLine1");

		/// <summary>
		///Application Url
		/// </summary>
		public static string ApplicationUrl => I18NResource.GetString(ResourceDirectory, "ApplicationUrl");

		/// <summary>
		///Login Id
		/// </summary>
		public static string LoginId => I18NResource.GetString(ResourceDirectory, "LoginId");

		/// <summary>
		///Configuration Profile Id
		/// </summary>
		public static string ConfigurationProfileId => I18NResource.GetString(ResourceDirectory, "ConfigurationProfileId");

		/// <summary>
		///Office Code
		/// </summary>
		public static string OfficeCode => I18NResource.GetString(ResourceDirectory, "OfficeCode");

		/// <summary>
		///Access Token Id
		/// </summary>
		public static string AccessTokenId => I18NResource.GetString(ResourceDirectory, "AccessTokenId");

		/// <summary>
		///Audience
		/// </summary>
		public static string Audience => I18NResource.GetString(ResourceDirectory, "Audience");

		/// <summary>
		///Facebook App Id
		/// </summary>
		public static string FacebookAppId => I18NResource.GetString(ResourceDirectory, "FacebookAppId");

		/// <summary>
		///Google Signin Scope
		/// </summary>
		public static string GoogleSigninScope => I18NResource.GetString(ResourceDirectory, "GoogleSigninScope");

		/// <summary>
		///Office Name
		/// </summary>
		public static string OfficeName => I18NResource.GetString(ResourceDirectory, "OfficeName");

		/// <summary>
		///Claims
		/// </summary>
		public static string Claims => I18NResource.GetString(ResourceDirectory, "Claims");

		/// <summary>
		///Login Timestamp
		/// </summary>
		public static string LoginTimestamp => I18NResource.GetString(ResourceDirectory, "LoginTimestamp");

		/// <summary>
		///Currency Name
		/// </summary>
		public static string CurrencyName => I18NResource.GetString(ResourceDirectory, "CurrencyName");

		/// <summary>
		///City
		/// </summary>
		public static string City => I18NResource.GetString(ResourceDirectory, "City");

		/// <summary>
		///Currency Code
		/// </summary>
		public static string CurrencyCode => I18NResource.GetString(ResourceDirectory, "CurrencyCode");

		/// <summary>
		///Display Name
		/// </summary>
		public static string DisplayName => I18NResource.GetString(ResourceDirectory, "DisplayName");

		/// <summary>
		///Token
		/// </summary>
		public static string Token => I18NResource.GetString(ResourceDirectory, "Token");

		/// <summary>
		///Audit User Id
		/// </summary>
		public static string AuditUserId => I18NResource.GetString(ResourceDirectory, "AuditUserId");

		/// <summary>
		///Redirect Url
		/// </summary>
		public static string RedirectUrl => I18NResource.GetString(ResourceDirectory, "RedirectUrl");

		/// <summary>
		///Deleted
		/// </summary>
		public static string Deleted => I18NResource.GetString(ResourceDirectory, "Deleted");

		/// <summary>
		///Revoked By
		/// </summary>
		public static string RevokedBy => I18NResource.GetString(ResourceDirectory, "RevokedBy");

		/// <summary>
		///Confirmed On
		/// </summary>
		public static string ConfirmedOn => I18NResource.GetString(ResourceDirectory, "ConfirmedOn");

		/// <summary>
		///Created On
		/// </summary>
		public static string CreatedOn => I18NResource.GetString(ResourceDirectory, "CreatedOn");

		/// <summary>
		///Application Name
		/// </summary>
		public static string ApplicationName => I18NResource.GetString(ResourceDirectory, "ApplicationName");

		/// <summary>
		///Expires On
		/// </summary>
		public static string ExpiresOn => I18NResource.GetString(ResourceDirectory, "ExpiresOn");

		/// <summary>
		///Requested On
		/// </summary>
		public static string RequestedOn => I18NResource.GetString(ResourceDirectory, "RequestedOn");

		/// <summary>
		///Support Email
		/// </summary>
		public static string SupportEmail => I18NResource.GetString(ResourceDirectory, "SupportEmail");

		/// <summary>
		///App Secret
		/// </summary>
		public static string AppSecret => I18NResource.GetString(ResourceDirectory, "AppSecret");

		/// <summary>
		///Phone
		/// </summary>
		public static string Phone => I18NResource.GetString(ResourceDirectory, "Phone");

		/// <summary>
		///Request Id
		/// </summary>
		public static string RequestId => I18NResource.GetString(ResourceDirectory, "RequestId");

		/// <summary>
		///Domain Name
		/// </summary>
		public static string DomainName => I18NResource.GetString(ResourceDirectory, "DomainName");

		/// <summary>
		///Publisher
		/// </summary>
		public static string Publisher => I18NResource.GetString(ResourceDirectory, "Publisher");

		/// <summary>
		///Default Office
		/// </summary>
		public static string DefaultOffice => I18NResource.GetString(ResourceDirectory, "DefaultOffice");

		/// <summary>
		///Fax
		/// </summary>
		public static string Fax => I18NResource.GetString(ResourceDirectory, "Fax");

		/// <summary>
		///Allow Registration
		/// </summary>
		public static string AllowRegistration => I18NResource.GetString(ResourceDirectory, "AllowRegistration");

		/// <summary>
		///Defult Role
		/// </summary>
		public static string DefultRole => I18NResource.GetString(ResourceDirectory, "DefultRole");

		/// <summary>
		///Url
		/// </summary>
		public static string Url => I18NResource.GetString(ResourceDirectory, "Url");

		/// <summary>
		///Hundredth Name
		/// </summary>
		public static string HundredthName => I18NResource.GetString(ResourceDirectory, "HundredthName");

		/// <summary>
		///Last Seen On
		/// </summary>
		public static string LastSeenOn => I18NResource.GetString(ResourceDirectory, "LastSeenOn");

		/// <summary>
		///Google Signin Client Id
		/// </summary>
		public static string GoogleSigninClientId => I18NResource.GetString(ResourceDirectory, "GoogleSigninClientId");

		/// <summary>
		///Country
		/// </summary>
		public static string Country => I18NResource.GetString(ResourceDirectory, "Country");

		/// <summary>
		///Allow Facebook Registration
		/// </summary>
		public static string AllowFacebookRegistration => I18NResource.GetString(ResourceDirectory, "AllowFacebookRegistration");

		/// <summary>
		///Last Ip
		/// </summary>
		public static string LastIp => I18NResource.GetString(ResourceDirectory, "LastIp");

		/// <summary>
		///Logo
		/// </summary>
		public static string Logo => I18NResource.GetString(ResourceDirectory, "Logo");

		/// <summary>
		///Allow Google Registration
		/// </summary>
		public static string AllowGoogleRegistration => I18NResource.GetString(ResourceDirectory, "AllowGoogleRegistration");

		/// <summary>
		///User Name
		/// </summary>
		public static string UserName => I18NResource.GetString(ResourceDirectory, "UserName");

		/// <summary>
		///Street
		/// </summary>
		public static string Street => I18NResource.GetString(ResourceDirectory, "Street");

		/// <summary>
		///Roles
		/// </summary>
		public static string Roles => I18NResource.GetString(ResourceDirectory, "Roles");

		/// <summary>
		///Users
		/// </summary>
		public static string Users => I18NResource.GetString(ResourceDirectory, "Users");

		/// <summary>
		///Add a New User
		/// </summary>
		public static string AddNewUser => I18NResource.GetString(ResourceDirectory, "AddNewUser");

		/// <summary>
		///Change Password
		/// </summary>
		public static string ChangePassword => I18NResource.GetString(ResourceDirectory, "ChangePassword");

		/// <summary>
		///List Users
		/// </summary>
		public static string ListUsers => I18NResource.GetString(ResourceDirectory, "ListUsers");

		/// <summary>
		///Configuration Profile
		/// </summary>
		public static string ConfigurationProfile => I18NResource.GetString(ResourceDirectory, "ConfigurationProfile");

		/// <summary>
		///Email Templates
		/// </summary>
		public static string EmailTemplates => I18NResource.GetString(ResourceDirectory, "EmailTemplates");

		/// <summary>
		///Account Verification
		/// </summary>
		public static string AccountVerification => I18NResource.GetString(ResourceDirectory, "AccountVerification");

		/// <summary>
		///Password Reset
		/// </summary>
		public static string PasswordReset => I18NResource.GetString(ResourceDirectory, "PasswordReset");

		/// <summary>
		///Welcome Email
		/// </summary>
		public static string WelcomeEmail => I18NResource.GetString(ResourceDirectory, "WelcomeEmail");

		/// <summary>
		///Welcome Email (3rd Party)
		/// </summary>
		public static string WelcomeEmail3rdParty => I18NResource.GetString(ResourceDirectory, "WelcomeEmail3rdParty");

		/// <summary>
		///Access Is Denied
		/// </summary>
		public static string AccessIsDenied => I18NResource.GetString(ResourceDirectory, "AccessIsDenied");

		/// <summary>
		///Please authorize us to use your facebook information in order to sign in.
		/// </summary>
		public static string AuthorizeAppOnFacebook => I18NResource.GetString(ResourceDirectory, "AuthorizeAppOnFacebook");

		/// <summary>
		///Back
		/// </summary>
		public static string Back => I18NResource.GetString(ResourceDirectory, "Back");

		/// <summary>
		///Cancel
		/// </summary>
		public static string Cancel => I18NResource.GetString(ResourceDirectory, "Cancel");

		/// <summary>
		///Change
		/// </summary>
		public static string Change => I18NResource.GetString(ResourceDirectory, "Change");

		/// <summary>
		///Change My Password
		/// </summary>
		public static string ChangeMyPassword => I18NResource.GetString(ResourceDirectory, "ChangeMyPassword");

		/// <summary>
		///Configuration Profiles
		/// </summary>
		public static string ConfigurationProfiles => I18NResource.GetString(ResourceDirectory, "ConfigurationProfiles");

		/// <summary>
		///Confirm Email
		/// </summary>
		public static string ConfirmEmail => I18NResource.GetString(ResourceDirectory, "ConfirmEmail");

		/// <summary>
		///Confirm Password
		/// </summary>
		public static string ConfirmPassword => I18NResource.GetString(ResourceDirectory, "ConfirmPassword");

		/// <summary>
		///Confirm password does not match with the supplied password
		/// </summary>
		public static string ConfirmPasswordDoesNotMatch => I18NResource.GetString(ResourceDirectory, "ConfirmPasswordDoesNotMatch");

		/// <summary>
		///Confirm your registration at {0}.
		/// </summary>
		public static string ConfirmRegistrationAtSite => I18NResource.GetString(ResourceDirectory, "ConfirmRegistrationAtSite");

		/// <summary>
		///We use cookies in our website. Please read the <a href="/site/legal/terms-of-service">terms of service</a> before you create an account.
		/// </summary>
		public static string CookieMessage => I18NResource.GetString(ResourceDirectory, "CookieMessage");

		/// <summary>
		///Create
		/// </summary>
		public static string Create => I18NResource.GetString(ResourceDirectory, "Create");

		/// <summary>
		///Please agree to terms and conditions to create an account.
		/// </summary>
		public static string CreateAccountAgreeToTermsAndCondition => I18NResource.GetString(ResourceDirectory, "CreateAccountAgreeToTermsAndCondition");

		/// <summary>
		///Create an Account
		/// </summary>
		public static string CreateAnAccount => I18NResource.GetString(ResourceDirectory, "CreateAnAccount");

		/// <summary>
		///Create a New User
		/// </summary>
		public static string CreateANewUser => I18NResource.GetString(ResourceDirectory, "CreateANewUser");

		/// <summary>
		///Create your account on our site.
		/// </summary>
		public static string CreateYourAccountOnOurSite => I18NResource.GetString(ResourceDirectory, "CreateYourAccountOnOurSite");

		/// <summary>
		///Email Address
		/// </summary>
		public static string EmailAddress => I18NResource.GetString(ResourceDirectory, "EmailAddress");

		/// <summary>
		///This email address is already in use.
		/// </summary>
		public static string EmailAddressInUse => I18NResource.GetString(ResourceDirectory, "EmailAddressInUse");

		/// <summary>
		///Emails do not match.
		/// </summary>
		public static string EmailsDoNotMatch => I18NResource.GetString(ResourceDirectory, "EmailsDoNotMatch");

		/// <summary>
		///Email Sent
		/// </summary>
		public static string EmailSent => I18NResource.GetString(ResourceDirectory, "EmailSent");

		/// <summary>
		///Enter Your Email Address
		/// </summary>
		public static string EnterYourEmailAddress => I18NResource.GetString(ResourceDirectory, "EnterYourEmailAddress");

		/// <summary>
		///Enter Your Email Address Again
		/// </summary>
		public static string EnterYourEmailAddressAgain => I18NResource.GetString(ResourceDirectory, "EnterYourEmailAddressAgain");

		/// <summary>
		///Facebook
		/// </summary>
		public static string Facebook => I18NResource.GetString(ResourceDirectory, "Facebook");

		/// <summary>
		///Sorry, facebook registration is not allowed at this time.
		/// </summary>
		public static string FacebookRegistrationClosed => I18NResource.GetString(ResourceDirectory, "FacebookRegistrationClosed");

		/// <summary>
		///Forgot Your Password?
		/// </summary>
		public static string ForgotYourPassword => I18NResource.GetString(ResourceDirectory, "ForgotYourPassword");

		/// <summary>
		///GitHub
		/// </summary>
		public static string GitHub => I18NResource.GetString(ResourceDirectory, "GitHub");

		/// <summary>
		///Google
		/// </summary>
		public static string Google => I18NResource.GetString(ResourceDirectory, "Google");

		/// <summary>
		///Sorry, google registration is not allowed at this time.
		/// </summary>
		public static string GoogleRegistrationClosed => I18NResource.GetString(ResourceDirectory, "GoogleRegistrationClosed");

		/// <summary>
		///Has Vat
		/// </summary>
		public static string HasVat => I18NResource.GetString(ResourceDirectory, "HasVat");

		/// <summary>
		///I agree to the terms and conditions.
		/// </summary>
		public static string IAgreeToTermsAndConditions => I18NResource.GetString(ResourceDirectory, "IAgreeToTermsAndConditions");

		/// <summary>
		///Invalid Confirmation Code
		/// </summary>
		public static string InvalidConfirmationCode => I18NResource.GetString(ResourceDirectory, "InvalidConfirmationCode");

		/// <summary>
		///The supplied confirmation token is either invalid or expired.
		/// </summary>
		public static string InvalidConfirmationCodeMessage => I18NResource.GetString(ResourceDirectory, "InvalidConfirmationCodeMessage");

		/// <summary>
		///Please leave this field empty
		/// </summary>
		public static string LeaveThisFieldEmpty => I18NResource.GetString(ResourceDirectory, "LeaveThisFieldEmpty");

		/// <summary>
		///LinkedIn
		/// </summary>
		public static string LinkedIn => I18NResource.GetString(ResourceDirectory, "LinkedIn");

		/// <summary>
		///New Password
		/// </summary>
		public static string NewPassword => I18NResource.GetString(ResourceDirectory, "NewPassword");

		/// <summary>
		///We do not have an account with this email address.
		/// </summary>
		public static string NoAccountWithThisEmail => I18NResource.GetString(ResourceDirectory, "NoAccountWithThisEmail");

		/// <summary>
		///Old Password
		/// </summary>
		public static string OldPassword => I18NResource.GetString(ResourceDirectory, "OldPassword");

		/// <summary>
		///Sorry, we could not change your password. Could you please check your existing password and try again?
		/// </summary>
		public static string PasswordNotChangedRecheckExistingPassword => I18NResource.GetString(ResourceDirectory, "PasswordNotChangedRecheckExistingPassword");

		/// <summary>
		///Passwords do not match.
		/// </summary>
		public static string PasswordsDoNotMatch => I18NResource.GetString(ResourceDirectory, "PasswordsDoNotMatch");

		/// <summary>
		///Password should contain at least 6 characters with one uppercase, lowercase, and a number.
		/// </summary>
		public static string PasswordValidationMessage => I18NResource.GetString(ResourceDirectory, "PasswordValidationMessage");

		/// <summary>
		///Password Was Changed
		/// </summary>
		public static string PasswordWasChanged => I18NResource.GetString(ResourceDirectory, "PasswordWasChanged");

		/// <summary>
		///Phone Number
		/// </summary>
		public static string PhoneNumber => I18NResource.GetString(ResourceDirectory, "PhoneNumber");

		/// <summary>
		///Please Confirm Your Registration
		/// </summary>
		public static string PleaseConfirmYourRegistration => I18NResource.GetString(ResourceDirectory, "PleaseConfirmYourRegistration");

		/// <summary>
		///<p>Thank you for registering! A confirmation email has been sent to your email address. Please click on the link in that email to activate your account.</p>
		/// </summary>
		public static string PleaseConfirmYourRegistrationMessage => I18NResource.GetString(ResourceDirectory, "PleaseConfirmYourRegistrationMessage");

		/// <summary>
		///Please Note
		/// </summary>
		public static string PleaseNote => I18NResource.GetString(ResourceDirectory, "PleaseNote");

		/// <summary>
		///Please wait
		/// </summary>
		public static string PleaseWait => I18NResource.GetString(ResourceDirectory, "PleaseWait");

		/// <summary>
		///Registration Closed
		/// </summary>
		public static string RegistrationClosed => I18NResource.GetString(ResourceDirectory, "RegistrationClosed");

		/// <summary>
		///<p>We are sorry but we are not accepting new registration this time.</p><p>Sorry for the inconvenience. Please check back again later.</p>
		/// </summary>
		public static string RegistrationClosedMessage => I18NResource.GetString(ResourceDirectory, "RegistrationClosedMessage");

		/// <summary>
		///To reset your password, enter the email address which is registered in our website. We will send you an email on your email address with instructions to help you reset your password.
		/// </summary>
		public static string ResetAccountInstructions => I18NResource.GetString(ResourceDirectory, "ResetAccountInstructions");

		/// <summary>
		///Reset Email Sent
		/// </summary>
		public static string ResetEmailSent => I18NResource.GetString(ResourceDirectory, "ResetEmailSent");

		/// <summary>
		///<p>We have sent reset instructions on your email address. Please follow the link on that email to reset your account.</p>
		/// </summary>
		public static string ResetEmailSentMessage => I18NResource.GetString(ResourceDirectory, "ResetEmailSentMessage");

		/// <summary>
		///Reset My Account
		/// </summary>
		public static string ResetMyAccount => I18NResource.GetString(ResourceDirectory, "ResetMyAccount");

		/// <summary>
		///To reset your password, enter the email address which is registered in our website. We will send you an email on your email address with instructions to help you reset your password.
		/// </summary>
		public static string ResetMyAccountDescription => I18NResource.GetString(ResourceDirectory, "ResetMyAccountDescription");

		/// <summary>
		///Reset My Password
		/// </summary>
		public static string ResetMyPassword => I18NResource.GetString(ResourceDirectory, "ResetMyPassword");

		/// <summary>
		///Return Home
		/// </summary>
		public static string ReturnHome => I18NResource.GetString(ResourceDirectory, "ReturnHome");

		/// <summary>
		///Role
		/// </summary>
		public static string Role => I18NResource.GetString(ResourceDirectory, "Role");

		/// <summary>
		///Role Management
		/// </summary>
		public static string RoleManagement => I18NResource.GetString(ResourceDirectory, "RoleManagement");

		/// <summary>
		///Save
		/// </summary>
		public static string Save => I18NResource.GetString(ResourceDirectory, "Save");

		/// <summary>
		///Select a User
		/// </summary>
		public static string SelectAUser => I18NResource.GetString(ResourceDirectory, "SelectAUser");

		/// <summary>
		///Select Language
		/// </summary>
		public static string SelectLanguage => I18NResource.GetString(ResourceDirectory, "SelectLanguage");

		/// <summary>
		///Select Office
		/// </summary>
		public static string SelectOffice => I18NResource.GetString(ResourceDirectory, "SelectOffice");

		/// <summary>
		///Set Password
		/// </summary>
		public static string SetPassword => I18NResource.GetString(ResourceDirectory, "SetPassword");

		/// <summary>
		///Sign In
		/// </summary>
		public static string SignIn => I18NResource.GetString(ResourceDirectory, "SignIn");

		/// <summary>
		///Sign Me Up
		/// </summary>
		public static string SignMeUp => I18NResource.GetString(ResourceDirectory, "SignMeUp");

		/// <summary>
		///Sign Up
		/// </summary>
		public static string SignUp => I18NResource.GetString(ResourceDirectory, "SignUp");

		/// <summary>
		///Social Login
		/// </summary>
		public static string SocialLogin => I18NResource.GetString(ResourceDirectory, "SocialLogin");

		/// <summary>
		///<p>Thank you registering on our website. Your account has been successfully activated.</p>
		/// </summary>
		public static string ThankYouForRegistering => I18NResource.GetString(ResourceDirectory, "ThankYouForRegistering");

		/// <summary>
		///User Management
		/// </summary>
		public static string UserManagement => I18NResource.GetString(ResourceDirectory, "UserManagement");

		/// <summary>
		///Welcome
		/// </summary>
		public static string Welcome => I18NResource.GetString(ResourceDirectory, "Welcome");

		/// <summary>
		///Welcome to {0}.
		/// </summary>
		public static string WelcometToSite => I18NResource.GetString(ResourceDirectory, "WelcometToSite");

		/// <summary>
		///Your Name
		/// </summary>
		public static string YourName => I18NResource.GetString(ResourceDirectory, "YourName");

		/// <summary>
		///Your password reset link for {0}.
		/// </summary>
		public static string YourPasswordResetLinkForSite => I18NResource.GetString(ResourceDirectory, "YourPasswordResetLinkForSite");

		/// <summary>
		///Your password was changed successfully.
		/// </summary>
		public static string YourPasswordWasChangedSuccessfully => I18NResource.GetString(ResourceDirectory, "YourPasswordWasChangedSuccessfully");

	}
}
