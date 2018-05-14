using System.Collections.Generic;
using System.Globalization;
using Frapid.Configuration;
using Frapid.i18n;

namespace Frapid.AddressBook
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
			ResourceDirectory = PathMapper.MapPath("/Areas/Frapid.AddressBook/i18n");
		}

		/// <summary>
		///AddressBook
		/// </summary>
		public static string AddressBook => I18NResource.GetString(ResourceDirectory, "AddressBook");

		/// <summary>
		///Audit User Id
		/// </summary>
		public static string AuditUserId => I18NResource.GetString(ResourceDirectory, "AuditUserId");

		/// <summary>
		///Formatted Name
		/// </summary>
		public static string FormattedName => I18NResource.GetString(ResourceDirectory, "FormattedName");

		/// <summary>
		///Deleted
		/// </summary>
		public static string Deleted => I18NResource.GetString(ResourceDirectory, "Deleted");

		/// <summary>
		///Gender
		/// </summary>
		public static string Gender => I18NResource.GetString(ResourceDirectory, "Gender");

		/// <summary>
		///Middle Name
		/// </summary>
		public static string MiddleName => I18NResource.GetString(ResourceDirectory, "MiddleName");

		/// <summary>
		///Nick Name
		/// </summary>
		public static string NickName => I18NResource.GetString(ResourceDirectory, "NickName");

		/// <summary>
		///Time Zone
		/// </summary>
		public static string TimeZone => I18NResource.GetString(ResourceDirectory, "TimeZone");

		/// <summary>
		///Note
		/// </summary>
		public static string Note => I18NResource.GetString(ResourceDirectory, "Note");

		/// <summary>
		///Created By
		/// </summary>
		public static string CreatedBy => I18NResource.GetString(ResourceDirectory, "CreatedBy");

		/// <summary>
		///Postal Code
		/// </summary>
		public static string PostalCode => I18NResource.GetString(ResourceDirectory, "PostalCode");

		/// <summary>
		///Url
		/// </summary>
		public static string Url => I18NResource.GetString(ResourceDirectory, "Url");

		/// <summary>
		///Kind
		/// </summary>
		public static string Kind => I18NResource.GetString(ResourceDirectory, "Kind");

		/// <summary>
		///Suffix
		/// </summary>
		public static string Suffix => I18NResource.GetString(ResourceDirectory, "Suffix");

		/// <summary>
		///Title
		/// </summary>
		public static string Title => I18NResource.GetString(ResourceDirectory, "Title");

		/// <summary>
		///Telephones
		/// </summary>
		public static string Telephones => I18NResource.GetString(ResourceDirectory, "Telephones");

		/// <summary>
		///Country
		/// </summary>
		public static string Country => I18NResource.GetString(ResourceDirectory, "Country");

		/// <summary>
		///Is Private
		/// </summary>
		public static string IsPrivate => I18NResource.GetString(ResourceDirectory, "IsPrivate");

		/// <summary>
		///Language
		/// </summary>
		public static string Language => I18NResource.GetString(ResourceDirectory, "Language");

		/// <summary>
		///Tags
		/// </summary>
		public static string Tags => I18NResource.GetString(ResourceDirectory, "Tags");

		/// <summary>
		///Last Name
		/// </summary>
		public static string LastName => I18NResource.GetString(ResourceDirectory, "LastName");

		/// <summary>
		///Street
		/// </summary>
		public static string Street => I18NResource.GetString(ResourceDirectory, "Street");

		/// <summary>
		///First Name
		/// </summary>
		public static string FirstName => I18NResource.GetString(ResourceDirectory, "FirstName");

		/// <summary>
		///Email Addresses
		/// </summary>
		public static string EmailAddresses => I18NResource.GetString(ResourceDirectory, "EmailAddresses");

		/// <summary>
		///Audit Ts
		/// </summary>
		public static string AuditTs => I18NResource.GetString(ResourceDirectory, "AuditTs");

		/// <summary>
		///Mobile Numbers
		/// </summary>
		public static string MobileNumbers => I18NResource.GetString(ResourceDirectory, "MobileNumbers");

		/// <summary>
		///Address Line 2
		/// </summary>
		public static string AddressLine2 => I18NResource.GetString(ResourceDirectory, "AddressLine2");

		/// <summary>
		///Associated User Id
		/// </summary>
		public static string AssociatedUserId => I18NResource.GetString(ResourceDirectory, "AssociatedUserId");

		/// <summary>
		///Role
		/// </summary>
		public static string Role => I18NResource.GetString(ResourceDirectory, "Role");

		/// <summary>
		///Birth Day
		/// </summary>
		public static string BirthDay => I18NResource.GetString(ResourceDirectory, "BirthDay");

		/// <summary>
		///Organization
		/// </summary>
		public static string Organization => I18NResource.GetString(ResourceDirectory, "Organization");

		/// <summary>
		///Prefix
		/// </summary>
		public static string Prefix => I18NResource.GetString(ResourceDirectory, "Prefix");

		/// <summary>
		///State
		/// </summary>
		public static string State => I18NResource.GetString(ResourceDirectory, "State");

		/// <summary>
		///Fax Numbers
		/// </summary>
		public static string FaxNumbers => I18NResource.GetString(ResourceDirectory, "FaxNumbers");

		/// <summary>
		///Address Line 1
		/// </summary>
		public static string AddressLine1 => I18NResource.GetString(ResourceDirectory, "AddressLine1");

		/// <summary>
		///Contact Id
		/// </summary>
		public static string ContactId => I18NResource.GetString(ResourceDirectory, "ContactId");

		/// <summary>
		///Organizational Unit
		/// </summary>
		public static string OrganizationalUnit => I18NResource.GetString(ResourceDirectory, "OrganizationalUnit");

		/// <summary>
		///City
		/// </summary>
		public static string City => I18NResource.GetString(ResourceDirectory, "City");

		/// <summary>
		///Tasks
		/// </summary>
		public static string Tasks => I18NResource.GetString(ResourceDirectory, "Tasks");

		/// <summary>
		///Access Token Id
		/// </summary>
		public static string AccessTokenId => I18NResource.GetString(ResourceDirectory, "AccessTokenId");

		/// <summary>
		///Add a New Contact
		/// </summary>
		public static string AddNewContact => I18NResource.GetString(ResourceDirectory, "AddNewContact");

		/// <summary>
		///Admin Email
		/// </summary>
		public static string AdminEmail => I18NResource.GetString(ResourceDirectory, "AdminEmail");

		/// <summary>
		///All Contacts
		/// </summary>
		public static string AllContacts => I18NResource.GetString(ResourceDirectory, "AllContacts");

		/// <summary>
		///Allow Facebook Registration
		/// </summary>
		public static string AllowFacebookRegistration => I18NResource.GetString(ResourceDirectory, "AllowFacebookRegistration");

		/// <summary>
		///Allow Google Registration
		/// </summary>
		public static string AllowGoogleRegistration => I18NResource.GetString(ResourceDirectory, "AllowGoogleRegistration");

		/// <summary>
		///Allow Registration
		/// </summary>
		public static string AllowRegistration => I18NResource.GetString(ResourceDirectory, "AllowRegistration");

		/// <summary>
		///Application Id
		/// </summary>
		public static string ApplicationId => I18NResource.GetString(ResourceDirectory, "ApplicationId");

		/// <summary>
		///Application Name
		/// </summary>
		public static string ApplicationName => I18NResource.GetString(ResourceDirectory, "ApplicationName");

		/// <summary>
		///Application Url
		/// </summary>
		public static string ApplicationUrl => I18NResource.GetString(ResourceDirectory, "ApplicationUrl");

		/// <summary>
		///App Secret
		/// </summary>
		public static string AppSecret => I18NResource.GetString(ResourceDirectory, "AppSecret");

		/// <summary>
		///Audience
		/// </summary>
		public static string Audience => I18NResource.GetString(ResourceDirectory, "Audience");

		/// <summary>
		///Browser
		/// </summary>
		public static string Browser => I18NResource.GetString(ResourceDirectory, "Browser");

		/// <summary>
		///Browser Based App
		/// </summary>
		public static string BrowserBasedApp => I18NResource.GetString(ResourceDirectory, "BrowserBasedApp");

		/// <summary>
		///Bulk Email
		/// </summary>
		public static string BulkEmail => I18NResource.GetString(ResourceDirectory, "BulkEmail");

		/// <summary>
		///Bulk SMS
		/// </summary>
		public static string BulkSms => I18NResource.GetString(ResourceDirectory, "BulkSms");

		/// <summary>
		///Claims
		/// </summary>
		public static string Claims => I18NResource.GetString(ResourceDirectory, "Claims");

		/// <summary>
		///Client Token
		/// </summary>
		public static string ClientToken => I18NResource.GetString(ResourceDirectory, "ClientToken");

		/// <summary>
		///Close
		/// </summary>
		public static string Close => I18NResource.GetString(ResourceDirectory, "Close");

		/// <summary>
		///Configuration Profile Id
		/// </summary>
		public static string ConfigurationProfileId => I18NResource.GetString(ResourceDirectory, "ConfigurationProfileId");

		/// <summary>
		///Confirmed
		/// </summary>
		public static string Confirmed => I18NResource.GetString(ResourceDirectory, "Confirmed");

		/// <summary>
		///Confirmed On
		/// </summary>
		public static string ConfirmedOn => I18NResource.GetString(ResourceDirectory, "ConfirmedOn");

		/// <summary>
		///Contact Type
		/// </summary>
		public static string ContactType => I18NResource.GetString(ResourceDirectory, "ContactType");

		/// <summary>
		///Could not send the email. Please setup email provider or consult with your administrator.
		/// </summary>
		public static string CouldNotSendEmailSetupProvider => I18NResource.GetString(ResourceDirectory, "CouldNotSendEmailSetupProvider");

		/// <summary>
		///Could not send the text message. Please setup SMS gateway or consult with your administrator.
		/// </summary>
		public static string CouldNotSendSmsSetupProvider => I18NResource.GetString(ResourceDirectory, "CouldNotSendSmsSetupProvider");

		/// <summary>
		///Created On
		/// </summary>
		public static string CreatedOn => I18NResource.GetString(ResourceDirectory, "CreatedOn");

		/// <summary>
		///Culture
		/// </summary>
		public static string Culture => I18NResource.GetString(ResourceDirectory, "Culture");

		/// <summary>
		///Currency Code
		/// </summary>
		public static string CurrencyCode => I18NResource.GetString(ResourceDirectory, "CurrencyCode");

		/// <summary>
		///Currency Name
		/// </summary>
		public static string CurrencyName => I18NResource.GetString(ResourceDirectory, "CurrencyName");

		/// <summary>
		///Currency Symbol
		/// </summary>
		public static string CurrencySymbol => I18NResource.GetString(ResourceDirectory, "CurrencySymbol");

		/// <summary>
		///Default Office
		/// </summary>
		public static string DefaultOffice => I18NResource.GetString(ResourceDirectory, "DefaultOffice");

		/// <summary>
		///Defult Role
		/// </summary>
		public static string DefultRole => I18NResource.GetString(ResourceDirectory, "DefultRole");

		/// <summary>
		///Delete Contact
		/// </summary>
		public static string DeleteContact => I18NResource.GetString(ResourceDirectory, "DeleteContact");

		/// <summary>
		///Description
		/// </summary>
		public static string Description => I18NResource.GetString(ResourceDirectory, "Description");

		/// <summary>
		///Display Name
		/// </summary>
		public static string DisplayName => I18NResource.GetString(ResourceDirectory, "DisplayName");

		/// <summary>
		///Domain Id
		/// </summary>
		public static string DomainId => I18NResource.GetString(ResourceDirectory, "DomainId");

		/// <summary>
		///Domain Name
		/// </summary>
		public static string DomainName => I18NResource.GetString(ResourceDirectory, "DomainName");

		/// <summary>
		///Do not share with other users
		/// </summary>
		public static string DoNotShareWithOtherUsers => I18NResource.GetString(ResourceDirectory, "DoNotShareWithOtherUsers");

		/// <summary>
		///Email
		/// </summary>
		public static string Email => I18NResource.GetString(ResourceDirectory, "Email");

		/// <summary>
		///Enter Subject
		/// </summary>
		public static string EnterSubject => I18NResource.GetString(ResourceDirectory, "EnterSubject");

		/// <summary>
		///Enter Your Message
		/// </summary>
		public static string EnterYourMessage => I18NResource.GetString(ResourceDirectory, "EnterYourMessage");

		/// <summary>
		///Expires On
		/// </summary>
		public static string ExpiresOn => I18NResource.GetString(ResourceDirectory, "ExpiresOn");

		/// <summary>
		///Export Contacts to a vCard File
		/// </summary>
		public static string ExportContactsToVcard => I18NResource.GetString(ResourceDirectory, "ExportContactsToVcard");

		/// <summary>
		///Facebook App Id
		/// </summary>
		public static string FacebookAppId => I18NResource.GetString(ResourceDirectory, "FacebookAppId");

		/// <summary>
		///Facebook Scope
		/// </summary>
		public static string FacebookScope => I18NResource.GetString(ResourceDirectory, "FacebookScope");

		/// <summary>
		///Fax
		/// </summary>
		public static string Fax => I18NResource.GetString(ResourceDirectory, "Fax");

		/// <summary>
		///Fb User Id
		/// </summary>
		public static string FbUserId => I18NResource.GetString(ResourceDirectory, "FbUserId");

		/// <summary>
		///Female
		/// </summary>
		public static string Female => I18NResource.GetString(ResourceDirectory, "Female");

		/// <summary>
		///Find Duplicates
		/// </summary>
		public static string FindDuplicates => I18NResource.GetString(ResourceDirectory, "FindDuplicates");

		/// <summary>
		///Google Signin Client Id
		/// </summary>
		public static string GoogleSigninClientId => I18NResource.GetString(ResourceDirectory, "GoogleSigninClientId");

		/// <summary>
		///Google Signin Scope
		/// </summary>
		public static string GoogleSigninScope => I18NResource.GetString(ResourceDirectory, "GoogleSigninScope");

		/// <summary>
		///Group
		/// </summary>
		public static string Group => I18NResource.GetString(ResourceDirectory, "Group");

		/// <summary>
		///Header
		/// </summary>
		public static string Header => I18NResource.GetString(ResourceDirectory, "Header");

		/// <summary>
		///Hundredth Name
		/// </summary>
		public static string HundredthName => I18NResource.GetString(ResourceDirectory, "HundredthName");

		/// <summary>
		///Import Contacts from a vCard File
		/// </summary>
		public static string ImportContactsFromVcard => I18NResource.GetString(ResourceDirectory, "ImportContactsFromVcard");

		/// <summary>
		///Individual
		/// </summary>
		public static string Individual => I18NResource.GetString(ResourceDirectory, "Individual");

		/// <summary>
		///Ip Address
		/// </summary>
		public static string IpAddress => I18NResource.GetString(ResourceDirectory, "IpAddress");

		/// <summary>
		///Is Active
		/// </summary>
		public static string IsActive => I18NResource.GetString(ResourceDirectory, "IsActive");

		/// <summary>
		///Is Administrator
		/// </summary>
		public static string IsAdministrator => I18NResource.GetString(ResourceDirectory, "IsAdministrator");

		/// <summary>
		///Issued By
		/// </summary>
		public static string IssuedBy => I18NResource.GetString(ResourceDirectory, "IssuedBy");

		/// <summary>
		///Last Browser
		/// </summary>
		public static string LastBrowser => I18NResource.GetString(ResourceDirectory, "LastBrowser");

		/// <summary>
		///Last Ip
		/// </summary>
		public static string LastIp => I18NResource.GetString(ResourceDirectory, "LastIp");

		/// <summary>
		///Last Seen On
		/// </summary>
		public static string LastSeenOn => I18NResource.GetString(ResourceDirectory, "LastSeenOn");

		/// <summary>
		///Less
		/// </summary>
		public static string Less => I18NResource.GetString(ResourceDirectory, "Less");

		/// <summary>
		///Linked User Id
		/// </summary>
		public static string LinkedUserId => I18NResource.GetString(ResourceDirectory, "LinkedUserId");

		/// <summary>
		///Location
		/// </summary>
		public static string Location => I18NResource.GetString(ResourceDirectory, "Location");

		/// <summary>
		///Login Id
		/// </summary>
		public static string LoginId => I18NResource.GetString(ResourceDirectory, "LoginId");

		/// <summary>
		///Login Timestamp
		/// </summary>
		public static string LoginTimestamp => I18NResource.GetString(ResourceDirectory, "LoginTimestamp");

		/// <summary>
		///Logo
		/// </summary>
		public static string Logo => I18NResource.GetString(ResourceDirectory, "Logo");

		/// <summary>
		///Male
		/// </summary>
		public static string Male => I18NResource.GetString(ResourceDirectory, "Male");

		/// <summary>
		///Message
		/// </summary>
		public static string Message => I18NResource.GetString(ResourceDirectory, "Message");

		/// <summary>
		///More
		/// </summary>
		public static string More => I18NResource.GetString(ResourceDirectory, "More");

		/// <summary>
		///Name
		/// </summary>
		public static string Name => I18NResource.GetString(ResourceDirectory, "Name");

		/// <summary>
		///Not Applicable
		/// </summary>
		public static string NotApplicable => I18NResource.GetString(ResourceDirectory, "NotApplicable");

		/// <summary>
		///Not Specified
		/// </summary>
		public static string NotSpecified => I18NResource.GetString(ResourceDirectory, "NotSpecified");

		/// <summary>
		///Office
		/// </summary>
		public static string Office => I18NResource.GetString(ResourceDirectory, "Office");

		/// <summary>
		///Office Code
		/// </summary>
		public static string OfficeCode => I18NResource.GetString(ResourceDirectory, "OfficeCode");

		/// <summary>
		///Office Id
		/// </summary>
		public static string OfficeId => I18NResource.GetString(ResourceDirectory, "OfficeId");

		/// <summary>
		///Office Name
		/// </summary>
		public static string OfficeName => I18NResource.GetString(ResourceDirectory, "OfficeName");

		/// <summary>
		///Other
		/// </summary>
		public static string Other => I18NResource.GetString(ResourceDirectory, "Other");

		/// <summary>
		///Pan Number
		/// </summary>
		public static string PanNumber => I18NResource.GetString(ResourceDirectory, "PanNumber");

		/// <summary>
		///Password
		/// </summary>
		public static string Password => I18NResource.GetString(ResourceDirectory, "Password");

		/// <summary>
		///Phone
		/// </summary>
		public static string Phone => I18NResource.GetString(ResourceDirectory, "Phone");

		/// <summary>
		///Please enter a message.
		/// </summary>
		public static string PleaseEnterMessage => I18NResource.GetString(ResourceDirectory, "PleaseEnterMessage");

		/// <summary>
		///Please enter a subject.
		/// </summary>
		public static string PleaseEnterSubject => I18NResource.GetString(ResourceDirectory, "PleaseEnterSubject");

		/// <summary>
		///Please select at least one contact.
		/// </summary>
		public static string PleaseSelectAtLeastOneContact => I18NResource.GetString(ResourceDirectory, "PleaseSelectAtLeastOneContact");

		/// <summary>
		///Po Box
		/// </summary>
		public static string PoBox => I18NResource.GetString(ResourceDirectory, "PoBox");

		/// <summary>
		///Privacy Policy Url
		/// </summary>
		public static string PrivacyPolicyUrl => I18NResource.GetString(ResourceDirectory, "PrivacyPolicyUrl");

		/// <summary>
		///Private Only
		/// </summary>
		public static string PrivateOnly => I18NResource.GetString(ResourceDirectory, "PrivateOnly");

		/// <summary>
		///Profile Name
		/// </summary>
		public static string ProfileName => I18NResource.GetString(ResourceDirectory, "ProfileName");

		/// <summary>
		///Published On
		/// </summary>
		public static string PublishedOn => I18NResource.GetString(ResourceDirectory, "PublishedOn");

		/// <summary>
		///Publisher
		/// </summary>
		public static string Publisher => I18NResource.GetString(ResourceDirectory, "Publisher");

		/// <summary>
		///Redirect Url
		/// </summary>
		public static string RedirectUrl => I18NResource.GetString(ResourceDirectory, "RedirectUrl");

		/// <summary>
		///Registered On
		/// </summary>
		public static string RegisteredOn => I18NResource.GetString(ResourceDirectory, "RegisteredOn");

		/// <summary>
		///Registration Date
		/// </summary>
		public static string RegistrationDate => I18NResource.GetString(ResourceDirectory, "RegistrationDate");

		/// <summary>
		///Registration Id
		/// </summary>
		public static string RegistrationId => I18NResource.GetString(ResourceDirectory, "RegistrationId");

		/// <summary>
		///Registration Office Id
		/// </summary>
		public static string RegistrationOfficeId => I18NResource.GetString(ResourceDirectory, "RegistrationOfficeId");

		/// <summary>
		///Registration Role Id
		/// </summary>
		public static string RegistrationRoleId => I18NResource.GetString(ResourceDirectory, "RegistrationRoleId");

		/// <summary>
		///Requested On
		/// </summary>
		public static string RequestedOn => I18NResource.GetString(ResourceDirectory, "RequestedOn");

		/// <summary>
		///Request Id
		/// </summary>
		public static string RequestId => I18NResource.GetString(ResourceDirectory, "RequestId");

		/// <summary>
		///Revoked
		/// </summary>
		public static string Revoked => I18NResource.GetString(ResourceDirectory, "Revoked");

		/// <summary>
		///Revoked By
		/// </summary>
		public static string RevokedBy => I18NResource.GetString(ResourceDirectory, "RevokedBy");

		/// <summary>
		///Revoked On
		/// </summary>
		public static string RevokedOn => I18NResource.GetString(ResourceDirectory, "RevokedOn");

		/// <summary>
		///Role Id
		/// </summary>
		public static string RoleId => I18NResource.GetString(ResourceDirectory, "RoleId");

		/// <summary>
		///Role Name
		/// </summary>
		public static string RoleName => I18NResource.GetString(ResourceDirectory, "RoleName");

		/// <summary>
		///Save
		/// </summary>
		public static string Save => I18NResource.GetString(ResourceDirectory, "Save");

		/// <summary>
		///Search ...
		/// </summary>
		public static string Search => I18NResource.GetString(ResourceDirectory, "Search");

		/// <summary>
		///Select
		/// </summary>
		public static string Select => I18NResource.GetString(ResourceDirectory, "Select");

		/// <summary>
		///Select Type
		/// </summary>
		public static string SelectType => I18NResource.GetString(ResourceDirectory, "SelectType");

		/// <summary>
		///Send
		/// </summary>
		public static string Send => I18NResource.GetString(ResourceDirectory, "Send");

		/// <summary>
		///Send Email
		/// </summary>
		public static string SendEmail => I18NResource.GetString(ResourceDirectory, "SendEmail");

		/// <summary>
		///Send Text Message
		/// </summary>
		public static string SendTextMessage => I18NResource.GetString(ResourceDirectory, "SendTextMessage");

		/// <summary>
		///Status
		/// </summary>
		public static string Status => I18NResource.GetString(ResourceDirectory, "Status");

		/// <summary>
		///Subject
		/// </summary>
		public static string Subject => I18NResource.GetString(ResourceDirectory, "Subject");

		/// <summary>
		///Support Email
		/// </summary>
		public static string SupportEmail => I18NResource.GetString(ResourceDirectory, "SupportEmail");

		/// <summary>
		///Sync Now
		/// </summary>
		public static string SyncNow => I18NResource.GetString(ResourceDirectory, "SyncNow");

		/// <summary>
		///Telephone Number(s)
		/// </summary>
		public static string TelephoneNumbers => I18NResource.GetString(ResourceDirectory, "TelephoneNumbers");

		/// <summary>
		///Terms Of Service Url
		/// </summary>
		public static string TermsOfServiceUrl => I18NResource.GetString(ResourceDirectory, "TermsOfServiceUrl");

		/// <summary>
		///Token
		/// </summary>
		public static string Token => I18NResource.GetString(ResourceDirectory, "Token");

		/// <summary>
		///Token Id
		/// </summary>
		public static string TokenId => I18NResource.GetString(ResourceDirectory, "TokenId");

		/// <summary>
		///Upload
		/// </summary>
		public static string Upload => I18NResource.GetString(ResourceDirectory, "Upload");

		/// <summary>
		///Upload Avatar
		/// </summary>
		public static string UploadAvatar => I18NResource.GetString(ResourceDirectory, "UploadAvatar");

		/// <summary>
		///User Agent
		/// </summary>
		public static string UserAgent => I18NResource.GetString(ResourceDirectory, "UserAgent");

		/// <summary>
		///User Id
		/// </summary>
		public static string UserId => I18NResource.GetString(ResourceDirectory, "UserId");

		/// <summary>
		///User Name
		/// </summary>
		public static string UserName => I18NResource.GetString(ResourceDirectory, "UserName");

		/// <summary>
		///Version Number
		/// </summary>
		public static string VersionNumber => I18NResource.GetString(ResourceDirectory, "VersionNumber");

		/// <summary>
		///Website
		/// </summary>
		public static string Website => I18NResource.GetString(ResourceDirectory, "Website");

		/// <summary>
		///Zip Code
		/// </summary>
		public static string ZipCode => I18NResource.GetString(ResourceDirectory, "ZipCode");

	}
}
