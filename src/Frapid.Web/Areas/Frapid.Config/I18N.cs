using System.Collections.Generic;
using System.Globalization;
using Frapid.Configuration;
using Frapid.i18n;

namespace Frapid.Config
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
			ResourceDirectory = PathMapper.MapPath("/Areas/Frapid.Config/i18n");
		}

		/// <summary>
		///Config
		/// </summary>
		public static string Config => I18NResource.GetString(ResourceDirectory, "Config");

		/// <summary>
		///Audit User Id
		/// </summary>
		public static string AuditUserId => I18NResource.GetString(ResourceDirectory, "AuditUserId");

		/// <summary>
		///Deleted
		/// </summary>
		public static string Deleted => I18NResource.GetString(ResourceDirectory, "Deleted");

		/// <summary>
		///Custom Field Setup Id
		/// </summary>
		public static string CustomFieldSetupId => I18NResource.GetString(ResourceDirectory, "CustomFieldSetupId");

		/// <summary>
		///Delivered
		/// </summary>
		public static string Delivered => I18NResource.GetString(ResourceDirectory, "Delivered");

		/// <summary>
		///From Display Name
		/// </summary>
		public static string FromDisplayName => I18NResource.GetString(ResourceDirectory, "FromDisplayName");

		/// <summary>
		///Column Name
		/// </summary>
		public static string ColumnName => I18NResource.GetString(ResourceDirectory, "ColumnName");

		/// <summary>
		///Table Name
		/// </summary>
		public static string TableName => I18NResource.GetString(ResourceDirectory, "TableName");

		/// <summary>
		///Kanban Detail Id
		/// </summary>
		public static string KanbanDetailId => I18NResource.GetString(ResourceDirectory, "KanbanDetailId");

		/// <summary>
		///Application Name
		/// </summary>
		public static string ApplicationName => I18NResource.GetString(ResourceDirectory, "ApplicationName");

		/// <summary>
		///Custom Field Id
		/// </summary>
		public static string CustomFieldId => I18NResource.GetString(ResourceDirectory, "CustomFieldId");

		/// <summary>
		///Description
		/// </summary>
		public static string Description => I18NResource.GetString(ResourceDirectory, "Description");

		/// <summary>
		///Field Name
		/// </summary>
		public static string FieldName => I18NResource.GetString(ResourceDirectory, "FieldName");

		/// <summary>
		///From Email
		/// </summary>
		public static string FromEmail => I18NResource.GetString(ResourceDirectory, "FromEmail");

		/// <summary>
		///Filter Condition
		/// </summary>
		public static string FilterCondition => I18NResource.GetString(ResourceDirectory, "FilterCondition");

		/// <summary>
		///Underlying Type
		/// </summary>
		public static string UnderlyingType => I18NResource.GetString(ResourceDirectory, "UnderlyingType");

		/// <summary>
		///Subject
		/// </summary>
		public static string Subject => I18NResource.GetString(ResourceDirectory, "Subject");

		/// <summary>
		///Send To
		/// </summary>
		public static string SendTo => I18NResource.GetString(ResourceDirectory, "SendTo");

		/// <summary>
		///Attachments
		/// </summary>
		public static string Attachments => I18NResource.GetString(ResourceDirectory, "Attachments");

		/// <summary>
		///Smtp Enable Ssl
		/// </summary>
		public static string SmtpEnableSsl => I18NResource.GetString(ResourceDirectory, "SmtpEnableSsl");

		/// <summary>
		///Send On
		/// </summary>
		public static string SendOn => I18NResource.GetString(ResourceDirectory, "SendOn");

		/// <summary>
		///Filter Value
		/// </summary>
		public static string FilterValue => I18NResource.GetString(ResourceDirectory, "FilterValue");

		/// <summary>
		///Resource Id
		/// </summary>
		public static string ResourceId => I18NResource.GetString(ResourceDirectory, "ResourceId");

		/// <summary>
		///Enabled
		/// </summary>
		public static string Enabled => I18NResource.GetString(ResourceDirectory, "Enabled");

		/// <summary>
		///Added On
		/// </summary>
		public static string AddedOn => I18NResource.GetString(ResourceDirectory, "AddedOn");

		/// <summary>
		///Smtp Password
		/// </summary>
		public static string SmtpPassword => I18NResource.GetString(ResourceDirectory, "SmtpPassword");

		/// <summary>
		///Kanban Id
		/// </summary>
		public static string KanbanId => I18NResource.GetString(ResourceDirectory, "KanbanId");

		/// <summary>
		///Data Type
		/// </summary>
		public static string DataType => I18NResource.GetString(ResourceDirectory, "DataType");

		/// <summary>
		///Reply To
		/// </summary>
		public static string ReplyTo => I18NResource.GetString(ResourceDirectory, "ReplyTo");

		/// <summary>
		///Canceled On
		/// </summary>
		public static string CanceledOn => I18NResource.GetString(ResourceDirectory, "CanceledOn");

		/// <summary>
		///Value
		/// </summary>
		public static string Value => I18NResource.GetString(ResourceDirectory, "Value");

		/// <summary>
		///Canceled
		/// </summary>
		public static string Canceled => I18NResource.GetString(ResourceDirectory, "Canceled");

		/// <summary>
		///Filter And Value
		/// </summary>
		public static string FilterAndValue => I18NResource.GetString(ResourceDirectory, "FilterAndValue");

		/// <summary>
		///Configuration Name
		/// </summary>
		public static string ConfigurationName => I18NResource.GetString(ResourceDirectory, "ConfigurationName");

		/// <summary>
		///Smtp Username
		/// </summary>
		public static string SmtpUsername => I18NResource.GetString(ResourceDirectory, "SmtpUsername");

		/// <summary>
		///From Number
		/// </summary>
		public static string FromNumber => I18NResource.GetString(ResourceDirectory, "FromNumber");

		/// <summary>
		///Rating
		/// </summary>
		public static string Rating => I18NResource.GetString(ResourceDirectory, "Rating");

		/// <summary>
		///From Name
		/// </summary>
		public static string FromName => I18NResource.GetString(ResourceDirectory, "FromName");

		/// <summary>
		///Kanban Name
		/// </summary>
		public static string KanbanName => I18NResource.GetString(ResourceDirectory, "KanbanName");

		/// <summary>
		///Message
		/// </summary>
		public static string Message => I18NResource.GetString(ResourceDirectory, "Message");

		/// <summary>
		///Smtp Host
		/// </summary>
		public static string SmtpHost => I18NResource.GetString(ResourceDirectory, "SmtpHost");

		/// <summary>
		///Queue Id
		/// </summary>
		public static string QueueId => I18NResource.GetString(ResourceDirectory, "QueueId");

		/// <summary>
		///After Field
		/// </summary>
		public static string AfterField => I18NResource.GetString(ResourceDirectory, "AfterField");

		/// <summary>
		///Audit Ts
		/// </summary>
		public static string AuditTs => I18NResource.GetString(ResourceDirectory, "AuditTs");

		/// <summary>
		///Before Field
		/// </summary>
		public static string BeforeField => I18NResource.GetString(ResourceDirectory, "BeforeField");

		/// <summary>
		///Is Default Admin
		/// </summary>
		public static string IsDefaultAdmin => I18NResource.GetString(ResourceDirectory, "IsDefaultAdmin");

		/// <summary>
		///Smtp Config Id
		/// </summary>
		public static string SmtpConfigId => I18NResource.GetString(ResourceDirectory, "SmtpConfigId");

		/// <summary>
		///Form Name
		/// </summary>
		public static string FormName => I18NResource.GetString(ResourceDirectory, "FormName");

		/// <summary>
		///Filter Statement
		/// </summary>
		public static string FilterStatement => I18NResource.GetString(ResourceDirectory, "FilterStatement");

		/// <summary>
		///Key Name
		/// </summary>
		public static string KeyName => I18NResource.GetString(ResourceDirectory, "KeyName");

		/// <summary>
		///User Id
		/// </summary>
		public static string UserId => I18NResource.GetString(ResourceDirectory, "UserId");

		/// <summary>
		///Is Default
		/// </summary>
		public static string IsDefault => I18NResource.GetString(ResourceDirectory, "IsDefault");

		/// <summary>
		///Smtp Port
		/// </summary>
		public static string SmtpPort => I18NResource.GetString(ResourceDirectory, "SmtpPort");

		/// <summary>
		///Is Test
		/// </summary>
		public static string IsTest => I18NResource.GetString(ResourceDirectory, "IsTest");

		/// <summary>
		///Filter Id
		/// </summary>
		public static string FilterId => I18NResource.GetString(ResourceDirectory, "FilterId");

		/// <summary>
		///Filter Name
		/// </summary>
		public static string FilterName => I18NResource.GetString(ResourceDirectory, "FilterName");

		/// <summary>
		///Object Name
		/// </summary>
		public static string ObjectName => I18NResource.GetString(ResourceDirectory, "ObjectName");

		/// <summary>
		///From Email Address
		/// </summary>
		public static string FromEmailAddress => I18NResource.GetString(ResourceDirectory, "FromEmailAddress");

		/// <summary>
		///Field Label
		/// </summary>
		public static string FieldLabel => I18NResource.GetString(ResourceDirectory, "FieldLabel");

		/// <summary>
		///Delivered On
		/// </summary>
		public static string DeliveredOn => I18NResource.GetString(ResourceDirectory, "DeliveredOn");

		/// <summary>
		///Reply To Name
		/// </summary>
		public static string ReplyToName => I18NResource.GetString(ResourceDirectory, "ReplyToName");

		/// <summary>
		///Field Order
		/// </summary>
		public static string FieldOrder => I18NResource.GetString(ResourceDirectory, "FieldOrder");

		/// <summary>
		///Offices
		/// </summary>
		public static string Offices => I18NResource.GetString(ResourceDirectory, "Offices");

		/// <summary>
		///SMTP
		/// </summary>
		public static string SMTP => I18NResource.GetString(ResourceDirectory, "SMTP");

		/// <summary>
		///File Manager
		/// </summary>
		public static string FileManager => I18NResource.GetString(ResourceDirectory, "FileManager");

		/// <summary>
		///Change Contrast
		/// </summary>
		public static string ChangeContrast => I18NResource.GetString(ResourceDirectory, "ChangeContrast");

		/// <summary>
		///Close
		/// </summary>
		public static string Close => I18NResource.GetString(ResourceDirectory, "Close");

		/// <summary>
		///Are you sure you want to delete the following file?{0}
		/// </summary>
		public static string ConfirmFileDelete => I18NResource.GetString(ResourceDirectory, "ConfirmFileDelete");

		/// <summary>
		///Could not create the file or directory because of an invalid directory path.
		/// </summary>
		public static string CouldNotCreateFileOrDirectoryInvalidPath => I18NResource.GetString(ResourceDirectory, "CouldNotCreateFileOrDirectoryInvalidPath");

		/// <summary>
		///Could not create the file or directory because the tenant directory was not found.
		/// </summary>
		public static string CouldNotCreateFileTenantDirectoryMissing => I18NResource.GetString(ResourceDirectory, "CouldNotCreateFileTenantDirectoryMissing");

		/// <summary>
		///Could not upload resource because the uploaded file has invalid extension.
		/// </summary>
		public static string CouldNotUploadInvalidFileExtension => I18NResource.GetString(ResourceDirectory, "CouldNotUploadInvalidFileExtension");

		/// <summary>
		///Could not upload resource because the posted file name is null or invalid.
		/// </summary>
		public static string CouldNotUploadInvalidFileName => I18NResource.GetString(ResourceDirectory, "CouldNotUploadInvalidFileName");

		/// <summary>
		///Create
		/// </summary>
		public static string Create => I18NResource.GetString(ResourceDirectory, "Create");

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
		///Currencies
		/// </summary>
		public static string Currencies => I18NResource.GetString(ResourceDirectory, "Currencies");

		/// <summary>
		///Delete
		/// </summary>
		public static string Delete => I18NResource.GetString(ResourceDirectory, "Delete");

		/// <summary>
		///Edit Layout
		/// </summary>
		public static string EditLayout => I18NResource.GetString(ResourceDirectory, "EditLayout");

		/// <summary>
		///Edit Tenant Files
		/// </summary>
		public static string EditTenantFiles => I18NResource.GetString(ResourceDirectory, "EditTenantFiles");

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
		///Invalid path. Check the log for more details.
		/// </summary>
		public static string InvalidPathCheckLog => I18NResource.GetString(ResourceDirectory, "InvalidPathCheckLog");

		/// <summary>
		///Path to the file or directory is invalid.
		/// </summary>
		public static string InvalidPathToFileOrDirectory => I18NResource.GetString(ResourceDirectory, "InvalidPathToFileOrDirectory");

		/// <summary>
		///Location
		/// </summary>
		public static string Location => I18NResource.GetString(ResourceDirectory, "Location");

		/// <summary>
		///No file was uploaded
		/// </summary>
		public static string NoFileWasUploaded => I18NResource.GetString(ResourceDirectory, "NoFileWasUploaded");

		/// <summary>
		///Only a single file may be uploaded.
		/// </summary>
		public static string OnlyASingleFileMayBeUploaded => I18NResource.GetString(ResourceDirectory, "OnlyASingleFileMayBeUploaded");

		/// <summary>
		///The path to file was not found.
		/// </summary>
		public static string PathNotFound => I18NResource.GetString(ResourceDirectory, "PathNotFound");

		/// <summary>
		///Save
		/// </summary>
		public static string Save => I18NResource.GetString(ResourceDirectory, "Save");

		/// <summary>
		///SMTP Configuration
		/// </summary>
		public static string SMTPConfiguration => I18NResource.GetString(ResourceDirectory, "SMTPConfiguration");

		/// <summary>
		///Successfully saved compiled css file.
		/// </summary>
		public static string SuccessfullySavedCompiledCssFile => I18NResource.GetString(ResourceDirectory, "SuccessfullySavedCompiledCssFile");

		/// <summary>
		///Successfully saved minified css file.
		/// </summary>
		public static string SuccessfullySavedMinifiedCssFile => I18NResource.GetString(ResourceDirectory, "SuccessfullySavedMinifiedCssFile");

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

	}
}
