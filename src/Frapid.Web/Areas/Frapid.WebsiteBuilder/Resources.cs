using System.Collections.Generic;
using System.Globalization;
using Frapid.Configuration;
using Frapid.i18n;

namespace Frapid.WebsiteBuilder
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
			ResourceDirectory = PathMapper.MapPath("/Areas/Frapid.WebsiteBuilder/i18n");
		}

		/// <summary>
		///Website
		/// </summary>
		public static string Website => I18NResource.GetString(ResourceDirectory, "Website");

		/// <summary>
		///Author Id
		/// </summary>
		public static string AuthorId => I18NResource.GetString(ResourceDirectory, "AuthorId");

		/// <summary>
		///Status
		/// </summary>
		public static string Status => I18NResource.GetString(ResourceDirectory, "Status");

		/// <summary>
		///Target
		/// </summary>
		public static string Target => I18NResource.GetString(ResourceDirectory, "Target");

		/// <summary>
		///Contents
		/// </summary>
		public static string Contents => I18NResource.GetString(ResourceDirectory, "Contents");

		/// <summary>
		///Description
		/// </summary>
		public static string Description => I18NResource.GetString(ResourceDirectory, "Description");

		/// <summary>
		///Alias
		/// </summary>
		public static string Alias => I18NResource.GetString(ResourceDirectory, "Alias");

		/// <summary>
		///Content Alias
		/// </summary>
		public static string ContentAlias => I18NResource.GetString(ResourceDirectory, "ContentAlias");

		/// <summary>
		///Category Alias
		/// </summary>
		public static string CategoryAlias => I18NResource.GetString(ResourceDirectory, "CategoryAlias");

		/// <summary>
		///Domain Name
		/// </summary>
		public static string DomainName => I18NResource.GetString(ResourceDirectory, "DomainName");

		/// <summary>
		///Website Category Id
		/// </summary>
		public static string WebsiteCategoryId => I18NResource.GetString(ResourceDirectory, "WebsiteCategoryId");

		/// <summary>
		///Category Id
		/// </summary>
		public static string CategoryId => I18NResource.GetString(ResourceDirectory, "CategoryId");

		/// <summary>
		///Unsubscribed On
		/// </summary>
		public static string UnsubscribedOn => I18NResource.GetString(ResourceDirectory, "UnsubscribedOn");

		/// <summary>
		///Postal Code
		/// </summary>
		public static string PostalCode => I18NResource.GetString(ResourceDirectory, "PostalCode");

		/// <summary>
		///Website Category Name
		/// </summary>
		public static string WebsiteCategoryName => I18NResource.GetString(ResourceDirectory, "WebsiteCategoryName");

		/// <summary>
		///Configuration Id
		/// </summary>
		public static string ConfigurationId => I18NResource.GetString(ResourceDirectory, "ConfigurationId");

		/// <summary>
		///Author Name
		/// </summary>
		public static string AuthorName => I18NResource.GetString(ResourceDirectory, "AuthorName");

		/// <summary>
		///Tag Id
		/// </summary>
		public static string TagId => I18NResource.GetString(ResourceDirectory, "TagId");

		/// <summary>
		///Url
		/// </summary>
		public static string Url => I18NResource.GetString(ResourceDirectory, "Url");

		/// <summary>
		///Email
		/// </summary>
		public static string Email => I18NResource.GetString(ResourceDirectory, "Email");

		/// <summary>
		///Subscribed On
		/// </summary>
		public static string SubscribedOn => I18NResource.GetString(ResourceDirectory, "SubscribedOn");

		/// <summary>
		///Browser
		/// </summary>
		public static string Browser => I18NResource.GetString(ResourceDirectory, "Browser");

		/// <summary>
		///Title
		/// </summary>
		public static string Title => I18NResource.GetString(ResourceDirectory, "Title");

		/// <summary>
		///Blog Description
		/// </summary>
		public static string BlogDescription => I18NResource.GetString(ResourceDirectory, "BlogDescription");

		/// <summary>
		///Category Name
		/// </summary>
		public static string CategoryName => I18NResource.GetString(ResourceDirectory, "CategoryName");

		/// <summary>
		///Details
		/// </summary>
		public static string Details => I18NResource.GetString(ResourceDirectory, "Details");

		/// <summary>
		///Recipients
		/// </summary>
		public static string Recipients => I18NResource.GetString(ResourceDirectory, "Recipients");

		/// <summary>
		///Country
		/// </summary>
		public static string Country => I18NResource.GetString(ResourceDirectory, "Country");

		/// <summary>
		///Tags
		/// </summary>
		public static string Tags => I18NResource.GetString(ResourceDirectory, "Tags");

		/// <summary>
		///Hits
		/// </summary>
		public static string Hits => I18NResource.GetString(ResourceDirectory, "Hits");

		/// <summary>
		///Email Subscription Id
		/// </summary>
		public static string EmailSubscriptionId => I18NResource.GetString(ResourceDirectory, "EmailSubscriptionId");

		/// <summary>
		///Last Name
		/// </summary>
		public static string LastName => I18NResource.GetString(ResourceDirectory, "LastName");

		/// <summary>
		///Blog Category Name
		/// </summary>
		public static string BlogCategoryName => I18NResource.GetString(ResourceDirectory, "BlogCategoryName");

		/// <summary>
		///Menu Item Id
		/// </summary>
		public static string MenuItemId => I18NResource.GetString(ResourceDirectory, "MenuItemId");

		/// <summary>
		///Display Contact Form
		/// </summary>
		public static string DisplayContactForm => I18NResource.GetString(ResourceDirectory, "DisplayContactForm");

		/// <summary>
		///Seo Description
		/// </summary>
		public static string SeoDescription => I18NResource.GetString(ResourceDirectory, "SeoDescription");

		/// <summary>
		///Parent Menu Item Id
		/// </summary>
		public static string ParentMenuItemId => I18NResource.GetString(ResourceDirectory, "ParentMenuItemId");

		/// <summary>
		///Unsubscribed
		/// </summary>
		public static string Unsubscribed => I18NResource.GetString(ResourceDirectory, "Unsubscribed");

		/// <summary>
		///Menu Id
		/// </summary>
		public static string MenuId => I18NResource.GetString(ResourceDirectory, "MenuId");

		/// <summary>
		///Menu Name
		/// </summary>
		public static string MenuName => I18NResource.GetString(ResourceDirectory, "MenuName");

		/// <summary>
		///Telephone
		/// </summary>
		public static string Telephone => I18NResource.GetString(ResourceDirectory, "Telephone");

		/// <summary>
		///First Name
		/// </summary>
		public static string FirstName => I18NResource.GetString(ResourceDirectory, "FirstName");

		/// <summary>
		///Audit Ts
		/// </summary>
		public static string AuditTs => I18NResource.GetString(ResourceDirectory, "AuditTs");

		/// <summary>
		///Address
		/// </summary>
		public static string Address => I18NResource.GetString(ResourceDirectory, "Address");

		/// <summary>
		///Is Draft
		/// </summary>
		public static string IsDraft => I18NResource.GetString(ResourceDirectory, "IsDraft");

		/// <summary>
		///Tag
		/// </summary>
		public static string Tag => I18NResource.GetString(ResourceDirectory, "Tag");

		/// <summary>
		///Position
		/// </summary>
		public static string Position => I18NResource.GetString(ResourceDirectory, "Position");

		/// <summary>
		///Ip Address
		/// </summary>
		public static string IpAddress => I18NResource.GetString(ResourceDirectory, "IpAddress");

		/// <summary>
		///Display Email
		/// </summary>
		public static string DisplayEmail => I18NResource.GetString(ResourceDirectory, "DisplayEmail");

		/// <summary>
		///Confirmed
		/// </summary>
		public static string Confirmed => I18NResource.GetString(ResourceDirectory, "Confirmed");

		/// <summary>
		///Name
		/// </summary>
		public static string Name => I18NResource.GetString(ResourceDirectory, "Name");

		/// <summary>
		///Last Editor Id
		/// </summary>
		public static string LastEditorId => I18NResource.GetString(ResourceDirectory, "LastEditorId");

		/// <summary>
		///State
		/// </summary>
		public static string State => I18NResource.GetString(ResourceDirectory, "State");

		/// <summary>
		///Blog Id
		/// </summary>
		public static string BlogId => I18NResource.GetString(ResourceDirectory, "BlogId");

		/// <summary>
		///Blog Category Id
		/// </summary>
		public static string BlogCategoryId => I18NResource.GetString(ResourceDirectory, "BlogCategoryId");

		/// <summary>
		///Content Id
		/// </summary>
		public static string ContentId => I18NResource.GetString(ResourceDirectory, "ContentId");

		/// <summary>
		///Website Name
		/// </summary>
		public static string WebsiteName => I18NResource.GetString(ResourceDirectory, "WebsiteName");

		/// <summary>
		///Is Default
		/// </summary>
		public static string IsDefault => I18NResource.GetString(ResourceDirectory, "IsDefault");

		/// <summary>
		///Is Homepage
		/// </summary>
		public static string IsHomepage => I18NResource.GetString(ResourceDirectory, "IsHomepage");

		/// <summary>
		///Subscription Type
		/// </summary>
		public static string SubscriptionType => I18NResource.GetString(ResourceDirectory, "SubscriptionType");

		/// <summary>
		///Blog Title
		/// </summary>
		public static string BlogTitle => I18NResource.GetString(ResourceDirectory, "BlogTitle");

		/// <summary>
		///Markdown
		/// </summary>
		public static string Markdown => I18NResource.GetString(ResourceDirectory, "Markdown");

		/// <summary>
		///Last Edited On
		/// </summary>
		public static string LastEditedOn => I18NResource.GetString(ResourceDirectory, "LastEditedOn");

		/// <summary>
		///Contact Id
		/// </summary>
		public static string ContactId => I18NResource.GetString(ResourceDirectory, "ContactId");

		/// <summary>
		///Sort
		/// </summary>
		public static string Sort => I18NResource.GetString(ResourceDirectory, "Sort");

		/// <summary>
		///City
		/// </summary>
		public static string City => I18NResource.GetString(ResourceDirectory, "City");

		/// <summary>
		///Publish On
		/// </summary>
		public static string PublishOn => I18NResource.GetString(ResourceDirectory, "PublishOn");

		/// <summary>
		///Audit User Id
		/// </summary>
		public static string AuditUserId => I18NResource.GetString(ResourceDirectory, "AuditUserId");

		/// <summary>
		///Deleted
		/// </summary>
		public static string Deleted => I18NResource.GetString(ResourceDirectory, "Deleted");

		/// <summary>
		///Confirmed On
		/// </summary>
		public static string ConfirmedOn => I18NResource.GetString(ResourceDirectory, "ConfirmedOn");

		/// <summary>
		///Created On
		/// </summary>
		public static string CreatedOn => I18NResource.GetString(ResourceDirectory, "CreatedOn");

		/// <summary>
		///Is Blog
		/// </summary>
		public static string IsBlog => I18NResource.GetString(ResourceDirectory, "IsBlog");

		/// <summary>
		///Tasks
		/// </summary>
		public static string Tasks => I18NResource.GetString(ResourceDirectory, "Tasks");

		/// <summary>
		///Configuration
		/// </summary>
		public static string Configuration => I18NResource.GetString(ResourceDirectory, "Configuration");

		/// <summary>
		///Manage Categories
		/// </summary>
		public static string ManageCategories => I18NResource.GetString(ResourceDirectory, "ManageCategories");

		/// <summary>
		///Add a New Content
		/// </summary>
		public static string AddNewContent => I18NResource.GetString(ResourceDirectory, "AddNewContent");

		/// <summary>
		///View Contents
		/// </summary>
		public static string ViewContents => I18NResource.GetString(ResourceDirectory, "ViewContents");

		/// <summary>
		///Add a New Blog Post
		/// </summary>
		public static string AddNewBlogPost => I18NResource.GetString(ResourceDirectory, "AddNewBlogPost");

		/// <summary>
		///View Blog Posts
		/// </summary>
		public static string ViewBlogPosts => I18NResource.GetString(ResourceDirectory, "ViewBlogPosts");

		/// <summary>
		///Menus
		/// </summary>
		public static string Menus => I18NResource.GetString(ResourceDirectory, "Menus");

		/// <summary>
		///Contacts
		/// </summary>
		public static string Contacts => I18NResource.GetString(ResourceDirectory, "Contacts");

		/// <summary>
		///Subscriptions
		/// </summary>
		public static string Subscriptions => I18NResource.GetString(ResourceDirectory, "Subscriptions");

		/// <summary>
		///Layout Manager
		/// </summary>
		public static string LayoutManager => I18NResource.GetString(ResourceDirectory, "LayoutManager");

		/// <summary>
		///Email Templates
		/// </summary>
		public static string EmailTemplates => I18NResource.GetString(ResourceDirectory, "EmailTemplates");

		/// <summary>
		///Subscription Added
		/// </summary>
		public static string SubscriptionAdded => I18NResource.GetString(ResourceDirectory, "SubscriptionAdded");

		/// <summary>
		///Subscription Removed
		/// </summary>
		public static string SubscriptionRemoved => I18NResource.GetString(ResourceDirectory, "SubscriptionRemoved");

		/// <summary>
		///Add a New Theme
		/// </summary>
		public static string AddANewTheme => I18NResource.GetString(ResourceDirectory, "AddANewTheme");

		/// <summary>
		///Author
		/// </summary>
		public static string Author => I18NResource.GetString(ResourceDirectory, "Author");

		/// <summary>
		///Author Email
		/// </summary>
		public static string AuthorEmail => I18NResource.GetString(ResourceDirectory, "AuthorEmail");

		/// <summary>
		///Author Url
		/// </summary>
		public static string AuthorUrl => I18NResource.GetString(ResourceDirectory, "AuthorUrl");

		/// <summary>
		///Back
		/// </summary>
		public static string Back => I18NResource.GetString(ResourceDirectory, "Back");

		/// <summary>
		///Blog
		/// </summary>
		public static string Blog => I18NResource.GetString(ResourceDirectory, "Blog");

		/// <summary>
		///Blogs
		/// </summary>
		public static string Blogs => I18NResource.GetString(ResourceDirectory, "Blogs");

		/// <summary>
		///Cancel
		/// </summary>
		public static string Cancel => I18NResource.GetString(ResourceDirectory, "Cancel");

		/// <summary>
		///Cannot delete this theme because of an error.
		/// </summary>
		public static string CannotDeleteThemeBecauseOfError => I18NResource.GetString(ResourceDirectory, "CannotDeleteThemeBecauseOfError");

		/// <summary>
		///Cannot find the theme directory. Check application logs for more information.
		/// </summary>
		public static string CannotFindThemeDirectoryCheckLogs => I18NResource.GetString(ResourceDirectory, "CannotFindThemeDirectoryCheckLogs");

		/// <summary>
		///Access is denied. You cannot remove this theme because it is in use.
		/// </summary>
		public static string CannotRemoveThemeInUse => I18NResource.GetString(ResourceDirectory, "CannotRemoveThemeInUse");

		/// <summary>
		///Category
		/// </summary>
		public static string Category => I18NResource.GetString(ResourceDirectory, "Category");

		/// <summary>
		///Change Contrast
		/// </summary>
		public static string ChangeContrast => I18NResource.GetString(ResourceDirectory, "ChangeContrast");

		/// <summary>
		///Close
		/// </summary>
		public static string Close => I18NResource.GetString(ResourceDirectory, "Close");

		/// <summary>
		///Configure Website
		/// </summary>
		public static string ConfigureWebsite => I18NResource.GetString(ResourceDirectory, "ConfigureWebsite");

		/// <summary>
		///Are you sure you want to delete the "<span class="theme name text"></span>" theme?
		/// </summary>
		public static string ConfirmThemeDelete => I18NResource.GetString(ResourceDirectory, "ConfirmThemeDelete");

		/// <summary>
		///Contact Form : {0}
		/// </summary>
		public static string ContactFormSubject => I18NResource.GetString(ResourceDirectory, "ContactFormSubject");

		/// <summary>
		///Contact Us
		/// </summary>
		public static string ContactUs => I18NResource.GetString(ResourceDirectory, "ContactUs");

		/// <summary>
		///Content Categories
		/// </summary>
		public static string ContentCategories => I18NResource.GetString(ResourceDirectory, "ContentCategories");

		/// <summary>
		///Content List
		/// </summary>
		public static string ContentList => I18NResource.GetString(ResourceDirectory, "ContentList");

		/// <summary>
		///Content Not Found
		/// </summary>
		public static string ContentNotFound => I18NResource.GetString(ResourceDirectory, "ContentNotFound");

		/// <summary>
		///The page or resource you are looking for does not exist or has been deleted.
		/// </summary>
		public static string ContentNotFoundMessage => I18NResource.GetString(ResourceDirectory, "ContentNotFoundMessage");

		/// <summary>
		///Could not create the file or directory on an invalid destination.
		/// </summary>
		public static string CouldNotCreateFileOrDirectoryInvalidDestination => I18NResource.GetString(ResourceDirectory, "CouldNotCreateFileOrDirectoryInvalidDestination");

		/// <summary>
		///Could not create the file or directory because the theme directory was not found.
		/// </summary>
		public static string CouldNotCreateFileOrDirectoryMissingThemeDirectory => I18NResource.GetString(ResourceDirectory, "CouldNotCreateFileOrDirectoryMissingThemeDirectory");

		/// <summary>
		///Could not create the theme because the destination directory already exists.
		/// </summary>
		public static string CouldNotCreateThemeDestinationDirectoryAlreadyExists => I18NResource.GetString(ResourceDirectory, "CouldNotCreateThemeDestinationDirectoryAlreadyExists");

		/// <summary>
		///Could not create the theme because the destination directory could not be located.
		/// </summary>
		public static string CouldNotCreateThemeInvalidDestinationDirectory => I18NResource.GetString(ResourceDirectory, "CouldNotCreateThemeInvalidDestinationDirectory");

		/// <summary>
		///Could not download theme because supplied URL is invalid or missing.
		/// </summary>
		public static string CouldNotDownloadThemeUrlInvalid => I18NResource.GetString(ResourceDirectory, "CouldNotDownloadThemeUrlInvalid");

		/// <summary>
		///Could not install theme because it already exists.
		/// </summary>
		public static string CouldNotInstallThemeBecauseItExists => I18NResource.GetString(ResourceDirectory, "CouldNotInstallThemeBecauseItExists");

		/// <summary>
		///Could not install theme. Check application logs for more information.
		/// </summary>
		public static string CouldNotInstallThemeCheckLogs => I18NResource.GetString(ResourceDirectory, "CouldNotInstallThemeCheckLogs");

		/// <summary>
		///The uploaded archive is not a valid frapid theme!
		/// </summary>
		public static string CouldNotInstallThemeNotFrapidTheme => I18NResource.GetString(ResourceDirectory, "CouldNotInstallThemeNotFrapidTheme");

		/// <summary>
		///Could not locate the requested file.
		/// </summary>
		public static string CouldNotLocateRequestedFile => I18NResource.GetString(ResourceDirectory, "CouldNotLocateRequestedFile");

		/// <summary>
		///Could not upload resource because the uploaded file has invalid extension.
		/// </summary>
		public static string CouldNotUploadResourceInvalidFileExtension => I18NResource.GetString(ResourceDirectory, "CouldNotUploadResourceInvalidFileExtension");

		/// <summary>
		///Could not upload resource because the posted file name is null or invalid.
		/// </summary>
		public static string CouldNotUploadResourceInvalidFileName => I18NResource.GetString(ResourceDirectory, "CouldNotUploadResourceInvalidFileName");

		/// <summary>
		///Could not upload your theme. Check application logs for more information.
		/// </summary>
		public static string CouldNotUploadThemeCheckLogs => I18NResource.GetString(ResourceDirectory, "CouldNotUploadThemeCheckLogs");

		/// <summary>
		///Could not install theme because the supplied file was not a valid ZIP archive.
		/// </summary>
		public static string CouldNotUploadThemeCorruptedZip => I18NResource.GetString(ResourceDirectory, "CouldNotUploadThemeCorruptedZip");

		/// <summary>
		///Could not upload theme because the uploaded file has invalid extension.
		/// </summary>
		public static string CouldNotUploadThemeInvalidExtension => I18NResource.GetString(ResourceDirectory, "CouldNotUploadThemeInvalidExtension");

		/// <summary>
		///Create
		/// </summary>
		public static string Create => I18NResource.GetString(ResourceDirectory, "Create");

		/// <summary>
		///Create a Brand New Theme
		/// </summary>
		public static string CreateABrandNewTheme => I18NResource.GetString(ResourceDirectory, "CreateABrandNewTheme");

		/// <summary>
		///Create a New File
		/// </summary>
		public static string CreateANewFile => I18NResource.GetString(ResourceDirectory, "CreateANewFile");

		/// <summary>
		///Create a New Folder
		/// </summary>
		public static string CreateANewFolder => I18NResource.GetString(ResourceDirectory, "CreateANewFolder");

		/// <summary>
		///Create File
		/// </summary>
		public static string CreateFile => I18NResource.GetString(ResourceDirectory, "CreateFile");

		/// <summary>
		///Create Folder
		/// </summary>
		public static string CreateFolder => I18NResource.GetString(ResourceDirectory, "CreateFolder");

		/// <summary>
		///Create a New Blog Post
		/// </summary>
		public static string CreateNewBlogPost => I18NResource.GetString(ResourceDirectory, "CreateNewBlogPost");

		/// <summary>
		///Create a New Page
		/// </summary>
		public static string CreateNewPage => I18NResource.GetString(ResourceDirectory, "CreateNewPage");

		/// <summary>
		///Create Theme
		/// </summary>
		public static string CreateTheme => I18NResource.GetString(ResourceDirectory, "CreateTheme");

		/// <summary>
		///CSS Framework
		/// </summary>
		public static string CSSFramework => I18NResource.GetString(ResourceDirectory, "CSSFramework");

		/// <summary>
		///Delete
		/// </summary>
		public static string Delete => I18NResource.GetString(ResourceDirectory, "Delete");

		/// <summary>
		///Delete Theme
		/// </summary>
		public static string DeleteTheme => I18NResource.GetString(ResourceDirectory, "DeleteTheme");

		/// <summary>
		///Sorry, discussion closed older posts.
		/// </summary>
		public static string DiscussionClosedForOlderPosts => I18NResource.GetString(ResourceDirectory, "DiscussionClosedForOlderPosts");

		/// <summary>
		///Download and Install
		/// </summary>
		public static string DownloadAndInstall => I18NResource.GetString(ResourceDirectory, "DownloadAndInstall");

		/// <summary>
		///Download Theme From
		/// </summary>
		public static string DownloadThemeFrom => I18NResource.GetString(ResourceDirectory, "DownloadThemeFrom");

		/// <summary>
		///Draft
		/// </summary>
		public static string Draft => I18NResource.GetString(ResourceDirectory, "Draft");

		/// <summary>
		///Edit Layout
		/// </summary>
		public static string EditLayout => I18NResource.GetString(ResourceDirectory, "EditLayout");

		/// <summary>
		///Edit Layout Files
		/// </summary>
		public static string EditLayoutFiles => I18NResource.GetString(ResourceDirectory, "EditLayoutFiles");

		/// <summary>
		///{0} wrote : <br/><br/> {1}
		/// </summary>
		public static string EmailWroteMessage => I18NResource.GetString(ResourceDirectory, "EmailWroteMessage");

		/// <summary>
		///Enter your email address again.
		/// </summary>
		public static string EnterYourEmailAddressAgain => I18NResource.GetString(ResourceDirectory, "EnterYourEmailAddressAgain");

		/// <summary>
		///Enter your email address to unsubscribe
		/// </summary>
		public static string EnterYourEmailAddressToUnsubscribe => I18NResource.GetString(ResourceDirectory, "EnterYourEmailAddressToUnsubscribe");

		/// <summary>
		///Error
		/// </summary>
		public static string Error => I18NResource.GetString(ResourceDirectory, "Error");

		/// <summary>
		///Are you sure you want to delete the following file?/Themes/{0}/{1}
		/// </summary>
		public static string FileDeleteConfirmationMessage => I18NResource.GetString(ResourceDirectory, "FileDeleteConfirmationMessage");

		/// <summary>
		///File Name
		/// </summary>
		public static string FileName => I18NResource.GetString(ResourceDirectory, "FileName");

		/// <summary>
		///File or directory could not be found.
		/// </summary>
		public static string FileOrDirectoryNotFound => I18NResource.GetString(ResourceDirectory, "FileOrDirectoryNotFound");

		/// <summary>
		///Folder Name
		/// </summary>
		public static string FolderName => I18NResource.GetString(ResourceDirectory, "FolderName");

		/// <summary>
		///From Local Hard Drive
		/// </summary>
		public static string FromLocalHardDrive => I18NResource.GetString(ResourceDirectory, "FromLocalHardDrive");

		/// <summary>
		///Here is a link I think you might like to know about:
		/// </summary>
		public static string HereIsALinkIThinkYouMightLikeToKnowAbout => I18NResource.GetString(ResourceDirectory, "HereIsALinkIThinkYouMightLikeToKnowAbout");

		/// <summary>
		///Home Page
		/// </summary>
		public static string HomePage => I18NResource.GetString(ResourceDirectory, "HomePage");

		/// <summary>
		///Invalid path. Check the log for more details.
		/// </summary>
		public static string InvalidPathCheckLogs => I18NResource.GetString(ResourceDirectory, "InvalidPathCheckLogs");

		/// <summary>
		///Invalid theme.
		/// </summary>
		public static string InvalidTheme => I18NResource.GetString(ResourceDirectory, "InvalidTheme");

		/// <summary>
		///The default layout path of this theme as per the configuration file does not exist or is invalid.
		/// </summary>
		public static string InvalidThemeInvalidDefaultLayoutPath => I18NResource.GetString(ResourceDirectory, "InvalidThemeInvalidDefaultLayoutPath");

		/// <summary>
		///The homepage layout path of this theme as per the configuration file does not exist or is invalid.
		/// </summary>
		public static string InvalidThemeInvalidHomepageLayoutPath => I18NResource.GetString(ResourceDirectory, "InvalidThemeInvalidHomepageLayoutPath");

		/// <summary>
		///Invalid URL
		/// </summary>
		public static string InvalidUrl => I18NResource.GetString(ResourceDirectory, "InvalidUrl");

		/// <summary>
		///Is Markdown
		/// </summary>
		public static string IsMarkdown => I18NResource.GetString(ResourceDirectory, "IsMarkdown");

		/// <summary>
		///Leave a Message
		/// </summary>
		public static string LeaveAMessage => I18NResource.GetString(ResourceDirectory, "LeaveAMessage");

		/// <summary>
		///Let's Go!
		/// </summary>
		public static string LetsGo => I18NResource.GetString(ResourceDirectory, "LetsGo");

		/// <summary>
		///Load
		/// </summary>
		public static string Load => I18NResource.GetString(ResourceDirectory, "Load");

		/// <summary>
		///Location
		/// </summary>
		public static string Location => I18NResource.GetString(ResourceDirectory, "Location");

		/// <summary>
		///Menu Items
		/// </summary>
		public static string MenuItems => I18NResource.GetString(ResourceDirectory, "MenuItems");

		/// <summary>
		///Message
		/// </summary>
		public static string Message => I18NResource.GetString(ResourceDirectory, "Message");

		/// <summary>
		///No, Don't Delete This Theme
		/// </summary>
		public static string NoDontDeleteThisTheme => I18NResource.GetString(ResourceDirectory, "NoDontDeleteThisTheme");

		/// <summary>
		///No file was uploaded
		/// </summary>
		public static string NoFileWasUploaded => I18NResource.GetString(ResourceDirectory, "NoFileWasUploaded");

		/// <summary>
		///Only a single file may be uploaded
		/// </summary>
		public static string OnlyASingleFileMayBeUploaded => I18NResource.GetString(ResourceDirectory, "OnlyASingleFileMayBeUploaded");

		/// <summary>
		///Or
		/// </summary>
		public static string Or => I18NResource.GetString(ResourceDirectory, "Or");

		/// <summary>
		///Path to the file or directory is invalid.
		/// </summary>
		public static string PathToFileOrDirectoryInvalid => I18NResource.GetString(ResourceDirectory, "PathToFileOrDirectoryInvalid");

		/// <summary>
		///Phone
		/// </summary>
		public static string Phone => I18NResource.GetString(ResourceDirectory, "Phone");

		/// <summary>
		///Proceed With Caution!
		/// </summary>
		public static string ProceedWithCaution => I18NResource.GetString(ResourceDirectory, "ProceedWithCaution");

		/// <summary>
		///Read More
		/// </summary>
		public static string ReadMore => I18NResource.GetString(ResourceDirectory, "ReadMore");

		/// <summary>
		///Released On
		/// </summary>
		public static string ReleasedOn => I18NResource.GetString(ResourceDirectory, "ReleasedOn");

		/// <summary>
		///Responsive
		/// </summary>
		public static string Responsive => I18NResource.GetString(ResourceDirectory, "Responsive");

		/// <summary>
		///Save
		/// </summary>
		public static string Save => I18NResource.GetString(ResourceDirectory, "Save");

		/// <summary>
		///Save This Page
		/// </summary>
		public static string SaveThisPage => I18NResource.GetString(ResourceDirectory, "SaveThisPage");

		/// <summary>
		///Select Theme
		/// </summary>
		public static string SelectTheme => I18NResource.GetString(ResourceDirectory, "SelectTheme");

		/// <summary>
		///Send Email
		/// </summary>
		public static string SendEmail => I18NResource.GetString(ResourceDirectory, "SendEmail");

		/// <summary>
		///Send this on email
		/// </summary>
		public static string SendThisOnEmail => I18NResource.GetString(ResourceDirectory, "SendThisOnEmail");

		/// <summary>
		///Share This Blog
		/// </summary>
		public static string ShareThisBlog => I18NResource.GetString(ResourceDirectory, "ShareThisBlog");

		/// <summary>
		///Share this on Facebook
		/// </summary>
		public static string ShareThisOnFacebook => I18NResource.GetString(ResourceDirectory, "ShareThisOnFacebook");

		/// <summary>
		///Share this on Google+
		/// </summary>
		public static string ShareThisOnGooglePlus => I18NResource.GetString(ResourceDirectory, "ShareThisOnGooglePlus");

		/// <summary>
		///Share this on LinkedIn
		/// </summary>
		public static string ShareThisOnLinkedIn => I18NResource.GetString(ResourceDirectory, "ShareThisOnLinkedIn");

		/// <summary>
		///Share this on Pinterest
		/// </summary>
		public static string ShareThisOnPinterest => I18NResource.GetString(ResourceDirectory, "ShareThisOnPinterest");

		/// <summary>
		///Share this on Reddit
		/// </summary>
		public static string ShareThisOnReddit => I18NResource.GetString(ResourceDirectory, "ShareThisOnReddit");

		/// <summary>
		///Share this on StumbleUpon
		/// </summary>
		public static string ShareThisOnStumbleUpon => I18NResource.GetString(ResourceDirectory, "ShareThisOnStumbleUpon");

		/// <summary>
		///Subject
		/// </summary>
		public static string Subject => I18NResource.GetString(ResourceDirectory, "Subject");

		/// <summary>
		///Subscription Removed Email
		/// </summary>
		public static string SubscriptionRemovedEmail => I18NResource.GetString(ResourceDirectory, "SubscriptionRemovedEmail");

		/// <summary>
		///Subscription Welcome Email
		/// </summary>
		public static string SubscriptionWelcomeEmail => I18NResource.GetString(ResourceDirectory, "SubscriptionWelcomeEmail");

		/// <summary>
		///Successfully saved compiled css file.
		/// </summary>
		public static string SuccessfullySavedCompiledCssFile => I18NResource.GetString(ResourceDirectory, "SuccessfullySavedCompiledCssFile");

		/// <summary>
		///Successfully saved minified css file.
		/// </summary>
		public static string SuccessfullySavedMinifiedCssFile => I18NResource.GetString(ResourceDirectory, "SuccessfullySavedMinifiedCssFile");

		/// <summary>
		///Comma separated list of tags.
		/// </summary>
		public static string TagsDescription => I18NResource.GetString(ResourceDirectory, "TagsDescription");

		/// <summary>
		///Thank you for contacting us.
		/// </summary>
		public static string ThankYouForContactingUs => I18NResource.GetString(ResourceDirectory, "ThankYouForContactingUs");

		/// <summary>
		///Thank you for unsubscribing.
		/// </summary>
		public static string ThankYouForUnsubscribing => I18NResource.GetString(ResourceDirectory, "ThankYouForUnsubscribing");

		/// <summary>
		///Thank you for subscribing to {0}
		/// </summary>
		public static string ThankYourForSubscribingToSite => I18NResource.GetString(ResourceDirectory, "ThankYourForSubscribingToSite");

		/// <summary>
		///Theme Converted By
		/// </summary>
		public static string ThemeConvertedBy => I18NResource.GetString(ResourceDirectory, "ThemeConvertedBy");

		/// <summary>
		///If this theme was ported from another platform, enter the name of the person or company who converted this theme to frapid.
		/// </summary>
		public static string ThemeConvertedByTitle => I18NResource.GetString(ResourceDirectory, "ThemeConvertedByTitle");

		/// <summary>
		///You are about to delete the selected theme. Doing this will permanently delete the theme and associated files.
		/// </summary>
		public static string ThemeDeleteWarning => I18NResource.GetString(ResourceDirectory, "ThemeDeleteWarning");

		/// <summary>
		///Theme Name
		/// </summary>
		public static string ThemeName => I18NResource.GetString(ResourceDirectory, "ThemeName");

		/// <summary>
		///Theme Version
		/// </summary>
		public static string ThemeVersion => I18NResource.GetString(ResourceDirectory, "ThemeVersion");

		/// <summary>
		///The requested page does not exist.
		/// </summary>
		public static string TheRequestedPageDoesNotExist => I18NResource.GetString(ResourceDirectory, "TheRequestedPageDoesNotExist");

		/// <summary>
		///Tweet this link
		/// </summary>
		public static string TweetThisLink => I18NResource.GetString(ResourceDirectory, "TweetThisLink");

		/// <summary>
		///Unsubscribe
		/// </summary>
		public static string Unsubscribe => I18NResource.GetString(ResourceDirectory, "Unsubscribe");

		/// <summary>
		///Unsubscribe from our mailing list
		/// </summary>
		public static string UnsubscribeFromOurMailingList => I18NResource.GetString(ResourceDirectory, "UnsubscribeFromOurMailingList");

		/// <summary>
		///Unsubscribe Me
		/// </summary>
		public static string UnsubscribeMe => I18NResource.GetString(ResourceDirectory, "UnsubscribeMe");

		/// <summary>
		///<p>We are sorry that you had this experience! <br/><br/> Although we want you to stay connected with us, it seems that you already made a choice. <br/> We will honor your decision to not receive emails from our mailing list anymore. <br/></p><p>Once you confirm to unsubcribe, <strong>we won't send you</strong> automated emails anymore.</p>
		/// </summary>
		public static string UnsubscribeMessage => I18NResource.GetString(ResourceDirectory, "UnsubscribeMessage");

		/// <summary>
		///Upload
		/// </summary>
		public static string Upload => I18NResource.GetString(ResourceDirectory, "Upload");

		/// <summary>
		///Upload a File
		/// </summary>
		public static string UploadAFile => I18NResource.GetString(ResourceDirectory, "UploadAFile");

		/// <summary>
		///Awesome! Upload another file.
		/// </summary>
		public static string UploadAnotherFile => I18NResource.GetString(ResourceDirectory, "UploadAnotherFile");

		/// <summary>
		///Upload a Theme
		/// </summary>
		public static string UploadATheme => I18NResource.GetString(ResourceDirectory, "UploadATheme");

		/// <summary>
		///View Subscriptions
		/// </summary>
		public static string ViewSubscriptions => I18NResource.GetString(ResourceDirectory, "ViewSubscriptions");

		/// <summary>
		///We will miss you!
		/// </summary>
		public static string WeWillMissYou => I18NResource.GetString(ResourceDirectory, "WeWillMissYou");

		/// <summary>
		///Yes
		/// </summary>
		public static string Yes => I18NResource.GetString(ResourceDirectory, "Yes");

		/// <summary>
		///You are now unsubscribed on {0}.
		/// </summary>
		public static string YouAreNowUnsubscribedOnSite => I18NResource.GetString(ResourceDirectory, "YouAreNowUnsubscribedOnSite");

		/// <summary>
		///Your Name
		/// </summary>
		public static string YourName => I18NResource.GetString(ResourceDirectory, "YourName");

	}
}
