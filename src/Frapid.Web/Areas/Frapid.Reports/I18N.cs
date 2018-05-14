using System.Collections.Generic;
using System.Globalization;
using Frapid.Configuration;
using Frapid.i18n;

namespace Frapid.Reports
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
			ResourceDirectory = PathMapper.MapPath("/Areas/Frapid.Reports/i18n");
		}

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
		///Attachments
		/// </summary>
		public static string Attachments => I18NResource.GetString(ResourceDirectory, "Attachments");

		/// <summary>
		///Close
		/// </summary>
		public static string Close => I18NResource.GetString(ResourceDirectory, "Close");

		/// <summary>
		///Download Excel
		/// </summary>
		public static string DownloadExcel => I18NResource.GetString(ResourceDirectory, "DownloadExcel");

		/// <summary>
		///Download PDF
		/// </summary>
		public static string DownloadPdf => I18NResource.GetString(ResourceDirectory, "DownloadPdf");

		/// <summary>
		///Download Text
		/// </summary>
		public static string DownloadText => I18NResource.GetString(ResourceDirectory, "DownloadText");

		/// <summary>
		///Download Word
		/// </summary>
		public static string DownloadWord => I18NResource.GetString(ResourceDirectory, "DownloadWord");

		/// <summary>
		///Download XML
		/// </summary>
		public static string DownloadXml => I18NResource.GetString(ResourceDirectory, "DownloadXml");

		/// <summary>
		///Email This Report
		/// </summary>
		public static string EmailThisReport => I18NResource.GetString(ResourceDirectory, "EmailThisReport");

		/// <summary>
		///Frapid Report
		/// </summary>
		public static string FrapidReport => I18NResource.GetString(ResourceDirectory, "FrapidReport");

		/// <summary>
		///Message
		/// </summary>
		public static string Message => I18NResource.GetString(ResourceDirectory, "Message");

		/// <summary>
		///No email processor defined.
		/// </summary>
		public static string NoEmailProcessorDefined => I18NResource.GetString(ResourceDirectory, "NoEmailProcessorDefined");

		/// <summary>
		///Please find the attached document.
		/// </summary>
		public static string PleaseFindAttachedDocument => I18NResource.GetString(ResourceDirectory, "PleaseFindAttachedDocument");

		/// <summary>
		///Print This Report
		/// </summary>
		public static string PrintThisReport => I18NResource.GetString(ResourceDirectory, "PrintThisReport");

		/// <summary>
		///Reload
		/// </summary>
		public static string Reload => I18NResource.GetString(ResourceDirectory, "Reload");

		/// <summary>
		///Send
		/// </summary>
		public static string Send => I18NResource.GetString(ResourceDirectory, "Send");

		/// <summary>
		///Send Email
		/// </summary>
		public static string SendEmail => I18NResource.GetString(ResourceDirectory, "SendEmail");

		/// <summary>
		///Send To
		/// </summary>
		public static string SendTo => I18NResource.GetString(ResourceDirectory, "SendTo");

		/// <summary>
		///Show
		/// </summary>
		public static string Show => I18NResource.GetString(ResourceDirectory, "Show");

		/// <summary>
		///Subject
		/// </summary>
		public static string Subject => I18NResource.GetString(ResourceDirectory, "Subject");

		/// <summary>
		///Zoom In
		/// </summary>
		public static string ZoomIn => I18NResource.GetString(ResourceDirectory, "ZoomIn");

		/// <summary>
		///Zoom Out
		/// </summary>
		public static string ZoomOut => I18NResource.GetString(ResourceDirectory, "ZoomOut");

	}
}
