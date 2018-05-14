using System.Collections.Generic;
using System.Globalization;
using Frapid.Configuration;
using Frapid.i18n;

namespace Frapid.i18n
{
	public sealed class Localize : ILocalize
	{
		public Dictionary<string, string> GetResources(CultureInfo culture)
		{
			string resourceDirectory = Resources.ResourceDirectory;
			return I18NResource.GetResources(resourceDirectory, culture);
		}
	}

	public static class Resources
	{
		public static string ResourceDirectory { get; }

		static Resources()
		{
			ResourceDirectory = PathMapper.MapPath("i18n");
		}

		/// <summary>
		///Week Day Name
		/// </summary>
		public static string WeekDayName => I18NResource.GetString(ResourceDirectory, "WeekDayName");

		/// <summary>
		///Registration Number
		/// </summary>
		public static string RegistrationNumber => I18NResource.GetString(ResourceDirectory, "RegistrationNumber");

		/// <summary>
		///Notification Id
		/// </summary>
		public static string NotificationId => I18NResource.GetString(ResourceDirectory, "NotificationId");

		/// <summary>
		///Parent Office
		/// </summary>
		public static string ParentOffice => I18NResource.GetString(ResourceDirectory, "ParentOffice");

		/// <summary>
		///Is Legally Recognized Marriage
		/// </summary>
		public static string IsLegallyRecognizedMarriage => I18NResource.GetString(ResourceDirectory, "IsLegallyRecognizedMarriage");

		/// <summary>
		///Pan Number
		/// </summary>
		public static string PanNumber => I18NResource.GetString(ResourceDirectory, "PanNumber");

		/// <summary>
		///Phone
		/// </summary>
		public static string Phone => I18NResource.GetString(ResourceDirectory, "Phone");

		/// <summary>
		///Nick Name
		/// </summary>
		public static string NickName => I18NResource.GetString(ResourceDirectory, "NickName");

		/// <summary>
		///Landing Url
		/// </summary>
		public static string LandingUrl => I18NResource.GetString(ResourceDirectory, "LandingUrl");

		/// <summary>
		///Parent Menu Id
		/// </summary>
		public static string ParentMenuId => I18NResource.GetString(ResourceDirectory, "ParentMenuId");

		/// <summary>
		///Marital Status Code
		/// </summary>
		public static string MaritalStatusCode => I18NResource.GetString(ResourceDirectory, "MaritalStatusCode");

		/// <summary>
		///Publisher
		/// </summary>
		public static string Publisher => I18NResource.GetString(ResourceDirectory, "Publisher");

		/// <summary>
		///Fax
		/// </summary>
		public static string Fax => I18NResource.GetString(ResourceDirectory, "Fax");

		/// <summary>
		///App Name
		/// </summary>
		public static string AppName => I18NResource.GetString(ResourceDirectory, "AppName");

		/// <summary>
		///Country Name
		/// </summary>
		public static string CountryName => I18NResource.GetString(ResourceDirectory, "CountryName");

		/// <summary>
		///App Id
		/// </summary>
		public static string AppId => I18NResource.GetString(ResourceDirectory, "AppId");

		/// <summary>
		///Seen By
		/// </summary>
		public static string SeenBy => I18NResource.GetString(ResourceDirectory, "SeenBy");

		/// <summary>
		///Url
		/// </summary>
		public static string Url => I18NResource.GetString(ResourceDirectory, "Url");

		/// <summary>
		///Hundredth Name
		/// </summary>
		public static string HundredthName => I18NResource.GetString(ResourceDirectory, "HundredthName");

		/// <summary>
		///Email
		/// </summary>
		public static string Email => I18NResource.GetString(ResourceDirectory, "Email");

		/// <summary>
		///Last Seen On
		/// </summary>
		public static string LastSeenOn => I18NResource.GetString(ResourceDirectory, "LastSeenOn");

		/// <summary>
		///Week Day Id
		/// </summary>
		public static string WeekDayId => I18NResource.GetString(ResourceDirectory, "WeekDayId");

		/// <summary>
		///Verification Status Id
		/// </summary>
		public static string VerificationStatusId => I18NResource.GetString(ResourceDirectory, "VerificationStatusId");

		/// <summary>
		///Gender Code
		/// </summary>
		public static string GenderCode => I18NResource.GetString(ResourceDirectory, "GenderCode");

		/// <summary>
		///To Role Id
		/// </summary>
		public static string ToRoleId => I18NResource.GetString(ResourceDirectory, "ToRoleId");

		/// <summary>
		///Country
		/// </summary>
		public static string Country => I18NResource.GetString(ResourceDirectory, "Country");

		/// <summary>
		///Office Id
		/// </summary>
		public static string OfficeId => I18NResource.GetString(ResourceDirectory, "OfficeId");

		/// <summary>
		///Logo
		/// </summary>
		public static string Logo => I18NResource.GetString(ResourceDirectory, "Logo");

		/// <summary>
		///Currency Symbol
		/// </summary>
		public static string CurrencySymbol => I18NResource.GetString(ResourceDirectory, "CurrencySymbol");

		/// <summary>
		///To Login Id
		/// </summary>
		public static string ToLoginId => I18NResource.GetString(ResourceDirectory, "ToLoginId");

		/// <summary>
		///Marital Status Id
		/// </summary>
		public static string MaritalStatusId => I18NResource.GetString(ResourceDirectory, "MaritalStatusId");

		/// <summary>
		///Notification Status Id
		/// </summary>
		public static string NotificationStatusId => I18NResource.GetString(ResourceDirectory, "NotificationStatusId");

		/// <summary>
		///Gender Name
		/// </summary>
		public static string GenderName => I18NResource.GetString(ResourceDirectory, "GenderName");

		/// <summary>
		///Street
		/// </summary>
		public static string Street => I18NResource.GetString(ResourceDirectory, "Street");

		/// <summary>
		///Verification Status Name
		/// </summary>
		public static string VerificationStatusName => I18NResource.GetString(ResourceDirectory, "VerificationStatusName");

		/// <summary>
		///Tenant
		/// </summary>
		public static string Tenant => I18NResource.GetString(ResourceDirectory, "Tenant");

		/// <summary>
		///Menu Id
		/// </summary>
		public static string MenuId => I18NResource.GetString(ResourceDirectory, "MenuId");

		/// <summary>
		///Menu Name
		/// </summary>
		public static string MenuName => I18NResource.GetString(ResourceDirectory, "MenuName");

		/// <summary>
		///Marital Status Name
		/// </summary>
		public static string MaritalStatusName => I18NResource.GetString(ResourceDirectory, "MaritalStatusName");

		/// <summary>
		///Allow Transaction Posting
		/// </summary>
		public static string AllowTransactionPosting => I18NResource.GetString(ResourceDirectory, "AllowTransactionPosting");

		/// <summary>
		///Icon
		/// </summary>
		public static string Icon => I18NResource.GetString(ResourceDirectory, "Icon");

		/// <summary>
		///Audit Ts
		/// </summary>
		public static string AuditTs => I18NResource.GetString(ResourceDirectory, "AuditTs");

		/// <summary>
		///Currency Id
		/// </summary>
		public static string CurrencyId => I18NResource.GetString(ResourceDirectory, "CurrencyId");

		/// <summary>
		///Address Line 2
		/// </summary>
		public static string AddressLine2 => I18NResource.GetString(ResourceDirectory, "AddressLine2");

		/// <summary>
		///Associated App
		/// </summary>
		public static string AssociatedApp => I18NResource.GetString(ResourceDirectory, "AssociatedApp");

		/// <summary>
		///Associated Menu Id
		/// </summary>
		public static string AssociatedMenuId => I18NResource.GetString(ResourceDirectory, "AssociatedMenuId");

		/// <summary>
		///Version Number
		/// </summary>
		public static string VersionNumber => I18NResource.GetString(ResourceDirectory, "VersionNumber");

		/// <summary>
		///Depends On
		/// </summary>
		public static string DependsOn => I18NResource.GetString(ResourceDirectory, "DependsOn");

		/// <summary>
		///Zip Code
		/// </summary>
		public static string ZipCode => I18NResource.GetString(ResourceDirectory, "ZipCode");

		/// <summary>
		///Registration Date
		/// </summary>
		public static string RegistrationDate => I18NResource.GetString(ResourceDirectory, "RegistrationDate");

		/// <summary>
		///Parent Office Id
		/// </summary>
		public static string ParentOfficeId => I18NResource.GetString(ResourceDirectory, "ParentOfficeId");

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
		///Formatted Text
		/// </summary>
		public static string FormattedText => I18NResource.GetString(ResourceDirectory, "FormattedText");

		/// <summary>
		///Published On
		/// </summary>
		public static string PublishedOn => I18NResource.GetString(ResourceDirectory, "PublishedOn");

		/// <summary>
		///Address Line 1
		/// </summary>
		public static string AddressLine1 => I18NResource.GetString(ResourceDirectory, "AddressLine1");

		/// <summary>
		///Office Code
		/// </summary>
		public static string OfficeCode => I18NResource.GetString(ResourceDirectory, "OfficeCode");

		/// <summary>
		///To User Id
		/// </summary>
		public static string ToUserId => I18NResource.GetString(ResourceDirectory, "ToUserId");

		/// <summary>
		///App Dependency Id
		/// </summary>
		public static string AppDependencyId => I18NResource.GetString(ResourceDirectory, "AppDependencyId");

		/// <summary>
		///Week Day Code
		/// </summary>
		public static string WeekDayCode => I18NResource.GetString(ResourceDirectory, "WeekDayCode");

		/// <summary>
		///Sort
		/// </summary>
		public static string Sort => I18NResource.GetString(ResourceDirectory, "Sort");

		/// <summary>
		///I 18n Key
		/// </summary>
		public static string I18nKey => I18NResource.GetString(ResourceDirectory, "I18nKey");

		/// <summary>
		///Office Name
		/// </summary>
		public static string OfficeName => I18NResource.GetString(ResourceDirectory, "OfficeName");

		/// <summary>
		///Country Code
		/// </summary>
		public static string CountryCode => I18NResource.GetString(ResourceDirectory, "CountryCode");

		/// <summary>
		///Currency Name
		/// </summary>
		public static string CurrencyName => I18NResource.GetString(ResourceDirectory, "CurrencyName");

		/// <summary>
		///Event Timestamp
		/// </summary>
		public static string EventTimestamp => I18NResource.GetString(ResourceDirectory, "EventTimestamp");

		/// <summary>
		///City
		/// </summary>
		public static string City => I18NResource.GetString(ResourceDirectory, "City");

		/// <summary>
		///Currency Code
		/// </summary>
		public static string CurrencyCode => I18NResource.GetString(ResourceDirectory, "CurrencyCode");

		/// <summary>
		///Audit User Id
		/// </summary>
		public static string AuditUserId => I18NResource.GetString(ResourceDirectory, "AuditUserId");

		/// <summary>
		///Deleted
		/// </summary>
		public static string Deleted => I18NResource.GetString(ResourceDirectory, "Deleted");

		/// <summary>
		///Access is denied
		/// </summary>
		public static string AccessIsDenied => I18NResource.GetString(ResourceDirectory, "AccessIsDenied");

		/// <summary>
		///Actions
		/// </summary>
		public static string Actions => I18NResource.GetString(ResourceDirectory, "Actions");

		/// <summary>
		///Add
		/// </summary>
		public static string Add => I18NResource.GetString(ResourceDirectory, "Add");

		/// <summary>
		///Add a Kanban List
		/// </summary>
		public static string AddAKanbanList => I18NResource.GetString(ResourceDirectory, "AddAKanbanList");

		/// <summary>
		///Add New
		/// </summary>
		public static string AddNew => I18NResource.GetString(ResourceDirectory, "AddNew");

		/// <summary>
		///Add New Checklist
		/// </summary>
		public static string AddNewChecklist => I18NResource.GetString(ResourceDirectory, "AddNewChecklist");

		/// <summary>
		///And
		/// </summary>
		public static string And => I18NResource.GetString(ResourceDirectory, "And");

		/// <summary>
		///Approve
		/// </summary>
		public static string Approve => I18NResource.GetString(ResourceDirectory, "Approve");

		/// <summary>
		///Are you sure?
		/// </summary>
		public static string AreYouSure => I18NResource.GetString(ResourceDirectory, "AreYouSure");

		/// <summary>
		///Back
		/// </summary>
		public static string Back => I18NResource.GetString(ResourceDirectory, "Back");

		/// <summary>
		///Cancel
		/// </summary>
		public static string Cancel => I18NResource.GetString(ResourceDirectory, "Cancel");

		/// <summary>
		///Cannot find a suitable directory to create a PostgreSQL DB Backup.
		/// </summary>
		public static string CannotFindPostgreSQLBackupDirectory => I18NResource.GetString(ResourceDirectory, "CannotFindPostgreSQLBackupDirectory");

		/// <summary>
		///Cannot Save
		/// </summary>
		public static string CannotSave => I18NResource.GetString(ResourceDirectory, "CannotSave");

		/// <summary>
		///Clear Filters
		/// </summary>
		public static string ClearFilters => I18NResource.GetString(ResourceDirectory, "ClearFilters");

		/// <summary>
		///Close
		/// </summary>
		public static string Close => I18NResource.GetString(ResourceDirectory, "Close");

		/// <summary>
		///The column "{0}" does not exist or is invalid. Are you sure you want to continue?
		/// </summary>
		public static string ColumnInvalidAreYouSure => I18NResource.GetString(ResourceDirectory, "ColumnInvalidAreYouSure");

		/// <summary>
		///Column Name
		/// </summary>
		public static string ColumnName => I18NResource.GetString(ResourceDirectory, "ColumnName");

		/// <summary>
		///Condition
		/// </summary>
		public static string Condition => I18NResource.GetString(ResourceDirectory, "Condition");

		/// <summary>
		///Could not create backup.
		/// </summary>
		public static string CouldNotCreateBackup => I18NResource.GetString(ResourceDirectory, "CouldNotCreateBackup");

		/// <summary>
		///Could not generate sitemap.
		/// </summary>
		public static string CouldNotGenerateSiteMap => I18NResource.GetString(ResourceDirectory, "CouldNotGenerateSiteMap");

		/// <summary>
		///Create a Flag
		/// </summary>
		public static string CreateAFlag => I18NResource.GetString(ResourceDirectory, "CreateAFlag");

		/// <summary>
		///Create Duplicate
		/// </summary>
		public static string CreateDuplicate => I18NResource.GetString(ResourceDirectory, "CreateDuplicate");

		/// <summary>
		///Create New
		/// </summary>
		public static string CreateNew => I18NResource.GetString(ResourceDirectory, "CreateNew");

		/// <summary>
		///CTRL + SHIFT + F
		/// </summary>
		public static string CtrlShiftF => I18NResource.GetString(ResourceDirectory, "CtrlShiftF");

		/// <summary>
		///Custom Fields
		/// </summary>
		public static string CustomFields => I18NResource.GetString(ResourceDirectory, "CustomFields");

		/// <summary>
		///Dashboard
		/// </summary>
		public static string Dashboard => I18NResource.GetString(ResourceDirectory, "Dashboard");

		/// <summary>
		///Data Import
		/// </summary>
		public static string DataImport => I18NResource.GetString(ResourceDirectory, "DataImport");

		/// <summary>
		///Delete
		/// </summary>
		public static string Delete => I18NResource.GetString(ResourceDirectory, "Delete");

		/// <summary>
		///Delete This Checklist
		/// </summary>
		public static string DeleteThisChecklist => I18NResource.GetString(ResourceDirectory, "DeleteThisChecklist");

		/// <summary>
		///Access is denied. Deleting a website is not allowed.
		/// </summary>
		public static string DeletingWebsiteIsNotAllowed => I18NResource.GetString(ResourceDirectory, "DeletingWebsiteIsNotAllowed");

		/// <summary>
		///Description
		/// </summary>
		public static string Description => I18NResource.GetString(ResourceDirectory, "Description");

		/// <summary>
		///Edit
		/// </summary>
		public static string Edit => I18NResource.GetString(ResourceDirectory, "Edit");

		/// <summary>
		///Edit This Checklist
		/// </summary>
		public static string EditThisChecklist => I18NResource.GetString(ResourceDirectory, "EditThisChecklist");

		/// <summary>
		///Export
		/// </summary>
		public static string Export => I18NResource.GetString(ResourceDirectory, "Export");

		/// <summary>
		///Export Data
		/// </summary>
		public static string ExportData => I18NResource.GetString(ResourceDirectory, "ExportData");

		/// <summary>
		///Export This Document
		/// </summary>
		public static string ExportThisDocument => I18NResource.GetString(ResourceDirectory, "ExportThisDocument");

		/// <summary>
		///Export to Doc
		/// </summary>
		public static string ExportToDoc => I18NResource.GetString(ResourceDirectory, "ExportToDoc");

		/// <summary>
		///Export to Excel
		/// </summary>
		public static string ExportToExcel => I18NResource.GetString(ResourceDirectory, "ExportToExcel");

		/// <summary>
		///Export to PDF
		/// </summary>
		public static string ExportToPDF => I18NResource.GetString(ResourceDirectory, "ExportToPDF");

		/// <summary>
		///Filter
		/// </summary>
		public static string Filter => I18NResource.GetString(ResourceDirectory, "Filter");

		/// <summary>
		///Filter Condition
		/// </summary>
		public static string FilterCondition => I18NResource.GetString(ResourceDirectory, "FilterCondition");

		/// <summary>
		///Filter Name
		/// </summary>
		public static string FilterName => I18NResource.GetString(ResourceDirectory, "FilterName");

		/// <summary>
		///Filters
		/// </summary>
		public static string Filters => I18NResource.GetString(ResourceDirectory, "Filters");

		/// <summary>
		///Filter View
		/// </summary>
		public static string FilterView => I18NResource.GetString(ResourceDirectory, "FilterView");

		/// <summary>
		///First
		/// </summary>
		public static string First => I18NResource.GetString(ResourceDirectory, "First");

		/// <summary>
		///Flag
		/// </summary>
		public static string Flag => I18NResource.GetString(ResourceDirectory, "Flag");

		/// <summary>
		///Flag was removed.
		/// </summary>
		public static string FlagRemoved => I18NResource.GetString(ResourceDirectory, "FlagRemoved");

		/// <summary>
		///Flag was saved.
		/// </summary>
		public static string FlagSaved => I18NResource.GetString(ResourceDirectory, "FlagSaved");

		/// <summary>
		///Form Invalid
		/// </summary>
		public static string FormInvalid => I18NResource.GetString(ResourceDirectory, "FormInvalid");

		/// <summary>
		///Installing frapid, please visit the site after a few minutes.
		/// </summary>
		public static string FrapidInstallationMessage => I18NResource.GetString(ResourceDirectory, "FrapidInstallationMessage");

		/// <summary>
		///Frequency Code
		/// </summary>
		public static string FrequencyCode => I18NResource.GetString(ResourceDirectory, "FrequencyCode");

		/// <summary>
		///Frequency Id
		/// </summary>
		public static string FrequencyId => I18NResource.GetString(ResourceDirectory, "FrequencyId");

		/// <summary>
		///Frequency Name
		/// </summary>
		public static string FrequencyName => I18NResource.GetString(ResourceDirectory, "FrequencyName");

		/// <summary>
		///Go Back
		/// </summary>
		public static string GoBack => I18NResource.GetString(ResourceDirectory, "GoBack");

		/// <summary>
		///Goodbye
		/// </summary>
		public static string Goodbye => I18NResource.GetString(ResourceDirectory, "Goodbye");

		/// <summary>
		///Go to Website
		/// </summary>
		public static string GoToWebsite => I18NResource.GetString(ResourceDirectory, "GoToWebsite");

		/// <summary>
		///It's good to see you again, {0}!
		/// </summary>
		public static string GreetingGoodToSeeYouAgain => I18NResource.GetString(ResourceDirectory, "GreetingGoodToSeeYouAgain");

		/// <summary>
		///Hi!
		/// </summary>
		public static string GreetingHi => I18NResource.GetString(ResourceDirectory, "GreetingHi");

		/// <summary>
		///How was your day, {0}?
		/// </summary>
		public static string GreetingHowWasYourDay => I18NResource.GetString(ResourceDirectory, "GreetingHowWasYourDay");

		/// <summary>
		///Nice to see you, {0}!
		/// </summary>
		public static string GreetingNiceToSeeYou => I18NResource.GetString(ResourceDirectory, "GreetingNiceToSeeYou");

		/// <summary>
		///There you are!
		/// </summary>
		public static string GreetingThereYouAre => I18NResource.GetString(ResourceDirectory, "GreetingThereYouAre");

		/// <summary>
		///Welcome back {0}.
		/// </summary>
		public static string GreetingWelcomeBack => I18NResource.GetString(ResourceDirectory, "GreetingWelcomeBack");

		/// <summary>
		///We missed you!!!
		/// </summary>
		public static string GreetingWeMissedYou => I18NResource.GetString(ResourceDirectory, "GreetingWeMissedYou");

		/// <summary>
		///You're awesome. ;)
		/// </summary>
		public static string GreetingYouAreAwesome => I18NResource.GetString(ResourceDirectory, "GreetingYouAreAwesome");

		/// <summary>
		///You're back with a bang!!!
		/// </summary>
		public static string GreetingYouAreBackWithABang => I18NResource.GetString(ResourceDirectory, "GreetingYouAreBackWithABang");

		/// <summary>
		///Grid View
		/// </summary>
		public static string GridView => I18NResource.GetString(ResourceDirectory, "GridView");

		/// <summary>
		///Has Vat
		/// </summary>
		public static string HasVat => I18NResource.GetString(ResourceDirectory, "HasVat");

		/// <summary>
		///Hope to see you soon.
		/// </summary>
		public static string HopeToSeeYouSoon => I18NResource.GetString(ResourceDirectory, "HopeToSeeYouSoon");

		/// <summary>
		///Import
		/// </summary>
		public static string Import => I18NResource.GetString(ResourceDirectory, "Import");

		/// <summary>
		///Import Data
		/// </summary>
		public static string ImportData => I18NResource.GetString(ResourceDirectory, "ImportData");

		/// <summary>
		///Successfully imported {0} items.
		/// </summary>
		public static string ImportedNItems => I18NResource.GetString(ResourceDirectory, "ImportedNItems");

		/// <summary>
		///Invalid file extension.
		/// </summary>
		public static string InvalidFileExtension => I18NResource.GetString(ResourceDirectory, "InvalidFileExtension");

		/// <summary>
		///Item duplicated.
		/// </summary>
		public static string ItemDuplicated => I18NResource.GetString(ResourceDirectory, "ItemDuplicated");

		/// <summary>
		///KanbanId
		/// </summary>
		public static string KanbanId => I18NResource.GetString(ResourceDirectory, "KanbanId");

		/// <summary>
		///KanbanName
		/// </summary>
		public static string KanbanName => I18NResource.GetString(ResourceDirectory, "KanbanName");

		/// <summary>
		///Kanban View
		/// </summary>
		public static string KanbanView => I18NResource.GetString(ResourceDirectory, "KanbanView");

		/// <summary>
		///Last
		/// </summary>
		public static string Last => I18NResource.GetString(ResourceDirectory, "Last");

		/// <summary>
		///Loading
		/// </summary>
		public static string Loading => I18NResource.GetString(ResourceDirectory, "Loading");

		/// <summary>
		///Log Off
		/// </summary>
		public static string LogOff => I18NResource.GetString(ResourceDirectory, "LogOff");

		/// <summary>
		///Make As Default
		/// </summary>
		public static string MakeAsDefault => I18NResource.GetString(ResourceDirectory, "MakeAsDefault");

		/// <summary>
		///Manage Filters
		/// </summary>
		public static string ManageFilters => I18NResource.GetString(ResourceDirectory, "ManageFilters");

		/// <summary>
		///Mark All as Read
		/// </summary>
		public static string MarkAllAsRead => I18NResource.GetString(ResourceDirectory, "MarkAllAsRead");

		/// <summary>
		///Filter: {0}.
		/// </summary>
		public static string NamedFilter => I18NResource.GetString(ResourceDirectory, "NamedFilter");

		/// <summary>
		///Next
		/// </summary>
		public static string Next => I18NResource.GetString(ResourceDirectory, "Next");

		/// <summary>
		///{0} hour(s).
		/// </summary>
		public static string NHours => I18NResource.GetString(ResourceDirectory, "NHours");

		/// <summary>
		///{0} minute(s).
		/// </summary>
		public static string NMinutes => I18NResource.GetString(ResourceDirectory, "NMinutes");

		/// <summary>
		///No
		/// </summary>
		public static string No => I18NResource.GetString(ResourceDirectory, "No");

		/// <summary>
		///No file was uploaded.
		/// </summary>
		public static string NoFileWasUploaded => I18NResource.GetString(ResourceDirectory, "NoFileWasUploaded");

		/// <summary>
		///No instance of form was found.
		/// </summary>
		public static string NoFormFound => I18NResource.GetString(ResourceDirectory, "NoFormFound");

		/// <summary>
		///None
		/// </summary>
		public static string None => I18NResource.GetString(ResourceDirectory, "None");

		/// <summary>
		///Notifications
		/// </summary>
		public static string Notification => I18NResource.GetString(ResourceDirectory, "Notification");

		/// <summary>
		///Notifications
		/// </summary>
		public static string Notifications => I18NResource.GetString(ResourceDirectory, "Notifications");

		/// <summary>
		///OK
		/// </summary>
		public static string OK => I18NResource.GetString(ResourceDirectory, "OK");

		/// <summary>
		///Only a single file may be uploaded.
		/// </summary>
		public static string OnlyASingleFileMayBeUploaded => I18NResource.GetString(ResourceDirectory, "OnlyASingleFileMayBeUploaded");

		/// <summary>
		///Or
		/// </summary>
		public static string Or => I18NResource.GetString(ResourceDirectory, "Or");

		/// <summary>
		///Page {0}
		/// </summary>
		public static string PageN => I18NResource.GetString(ResourceDirectory, "PageN");

		/// <summary>
		///Password
		/// </summary>
		public static string Password => I18NResource.GetString(ResourceDirectory, "Password");

		/// <summary>
		///Previous
		/// </summary>
		public static string Previous => I18NResource.GetString(ResourceDirectory, "Previous");

		/// <summary>
		///Print
		/// </summary>
		public static string Print => I18NResource.GetString(ResourceDirectory, "Print");

		/// <summary>
		///Processing  your CSV file.
		/// </summary>
		public static string ProcessingYourCSVFile => I18NResource.GetString(ResourceDirectory, "ProcessingYourCSVFile");

		/// <summary>
		///Rating
		/// </summary>
		public static string Rating => I18NResource.GetString(ResourceDirectory, "Rating");

		/// <summary>
		///Reason
		/// </summary>
		public static string Reason => I18NResource.GetString(ResourceDirectory, "Reason");

		/// <summary>
		///Reject
		/// </summary>
		public static string Reject => I18NResource.GetString(ResourceDirectory, "Reject");

		/// <summary>
		///Remove As Default
		/// </summary>
		public static string RemoveAsDefault => I18NResource.GetString(ResourceDirectory, "RemoveAsDefault");

		/// <summary>
		///Requesting import. This may take several minutes to complete.
		/// </summary>
		public static string RequestingImport => I18NResource.GetString(ResourceDirectory, "RequestingImport");

		/// <summary>
		///Return to View
		/// </summary>
		public static string ReturnToView => I18NResource.GetString(ResourceDirectory, "ReturnToView");

		/// <summary>
		///Return to Website
		/// </summary>
		public static string ReturnToWebsite => I18NResource.GetString(ResourceDirectory, "ReturnToWebsite");

		/// <summary>
		///Rolling back changes.
		/// </summary>
		public static string RollingBackChanges => I18NResource.GetString(ResourceDirectory, "RollingBackChanges");

		/// <summary>
		///Save
		/// </summary>
		public static string Save => I18NResource.GetString(ResourceDirectory, "Save");

		/// <summary>
		///Save This Filter
		/// </summary>
		public static string SaveThisFilter => I18NResource.GetString(ResourceDirectory, "SaveThisFilter");

		/// <summary>
		///Say Hi
		/// </summary>
		public static string SayHi => I18NResource.GetString(ResourceDirectory, "SayHi");

		/// <summary>
		///Search ...
		/// </summary>
		public static string Search => I18NResource.GetString(ResourceDirectory, "Search");

		/// <summary>
		///Search Results
		/// </summary>
		public static string SearchResults => I18NResource.GetString(ResourceDirectory, "SearchResults");

		/// <summary>
		///Select
		/// </summary>
		public static string Select => I18NResource.GetString(ResourceDirectory, "Select");

		/// <summary>
		///Select a Column
		/// </summary>
		public static string SelectAColumn => I18NResource.GetString(ResourceDirectory, "SelectAColumn");

		/// <summary>
		///Select a Filter
		/// </summary>
		public static string SelectAFilter => I18NResource.GetString(ResourceDirectory, "SelectAFilter");

		/// <summary>
		///Select Language
		/// </summary>
		public static string SelectLanguage => I18NResource.GetString(ResourceDirectory, "SelectLanguage");

		/// <summary>
		///Show
		/// </summary>
		public static string Show => I18NResource.GetString(ResourceDirectory, "Show");

		/// <summary>
		///Show Notifications
		/// </summary>
		public static string ShowNotifications => I18NResource.GetString(ResourceDirectory, "ShowNotifications");

		/// <summary>
		///Sign In
		/// </summary>
		public static string SignIn => I18NResource.GetString(ResourceDirectory, "SignIn");

		/// <summary>
		///Sign Out
		/// </summary>
		public static string SignOut => I18NResource.GetString(ResourceDirectory, "SignOut");

		/// <summary>
		///Successfully processed your file.
		/// </summary>
		public static string SuccessfullyProcessedYourFile => I18NResource.GetString(ResourceDirectory, "SuccessfullyProcessedYourFile");

		/// <summary>
		///The table was not found.
		/// </summary>
		public static string TableNotFound => I18NResource.GetString(ResourceDirectory, "TableNotFound");

		/// <summary>
		///Task completed successfully.
		/// </summary>
		public static string TaskCompletedSuccessfully => I18NResource.GetString(ResourceDirectory, "TaskCompletedSuccessfully");

		/// <summary>
		///Task completed successfully. Return to view?
		/// </summary>
		public static string TaskCompletedSuccessfullyReturnToView => I18NResource.GetString(ResourceDirectory, "TaskCompletedSuccessfullyReturnToView");

		/// <summary>
		///This field is required.
		/// </summary>
		public static string ThisFieldIsRequired => I18NResource.GetString(ResourceDirectory, "ThisFieldIsRequired");

		/// <summary>
		///Untitled
		/// </summary>
		public static string Untitled => I18NResource.GetString(ResourceDirectory, "Untitled");

		/// <summary>
		///Update
		/// </summary>
		public static string Update => I18NResource.GetString(ResourceDirectory, "Update");

		/// <summary>
		///Your upload is of invalid file type "{0}". Please try again.
		/// </summary>
		public static string UploadInvalidTryAgain => I18NResource.GetString(ResourceDirectory, "UploadInvalidTryAgain");

		/// <summary>
		///Username
		/// </summary>
		public static string Username => I18NResource.GetString(ResourceDirectory, "Username");

		/// <summary>
		///Value
		/// </summary>
		public static string Value => I18NResource.GetString(ResourceDirectory, "Value");

		/// <summary>
		///Verification
		/// </summary>
		public static string Verification => I18NResource.GetString(ResourceDirectory, "Verification");

		/// <summary>
		///Verify
		/// </summary>
		public static string Verify => I18NResource.GetString(ResourceDirectory, "Verify");

		/// <summary>
		///View
		/// </summary>
		public static string View => I18NResource.GetString(ResourceDirectory, "View");

		/// <summary>
		///Yes
		/// </summary>
		public static string Yes => I18NResource.GetString(ResourceDirectory, "Yes");

		/// <summary>
		///You don't have any notification.
		/// </summary>
		public static string YouDontHaveAnyNotification => I18NResource.GetString(ResourceDirectory, "YouDontHaveAnyNotification");

	}
}
