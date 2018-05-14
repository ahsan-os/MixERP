using System.Collections.Generic;
using System.Globalization;
using Frapid.Configuration;
using Frapid.i18n;

namespace Frapid.Authorization
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
			ResourceDirectory = PathMapper.MapPath("/Areas/Frapid.Authorization/i18n");
		}

		/// <summary>
		///Authorization
		/// </summary>
		public static string Authorization => I18NResource.GetString(ResourceDirectory, "Authorization");

		/// <summary>
		///Allow Access
		/// </summary>
		public static string AllowAccess => I18NResource.GetString(ResourceDirectory, "AllowAccess");

		/// <summary>
		///Audit User Id
		/// </summary>
		public static string AuditUserId => I18NResource.GetString(ResourceDirectory, "AuditUserId");

		/// <summary>
		///Menu Access Policy Id
		/// </summary>
		public static string MenuAccessPolicyId => I18NResource.GetString(ResourceDirectory, "MenuAccessPolicyId");

		/// <summary>
		///Deleted
		/// </summary>
		public static string Deleted => I18NResource.GetString(ResourceDirectory, "Deleted");

		/// <summary>
		///Table Name
		/// </summary>
		public static string TableName => I18NResource.GetString(ResourceDirectory, "TableName");

		/// <summary>
		///Table Schema
		/// </summary>
		public static string TableSchema => I18NResource.GetString(ResourceDirectory, "TableSchema");

		/// <summary>
		///Group Menu Access Policy Id
		/// </summary>
		public static string GroupMenuAccessPolicyId => I18NResource.GetString(ResourceDirectory, "GroupMenuAccessPolicyId");

		/// <summary>
		///Entity Name
		/// </summary>
		public static string EntityName => I18NResource.GetString(ResourceDirectory, "EntityName");

		/// <summary>
		///Entity Access Policy Id
		/// </summary>
		public static string EntityAccessPolicyId => I18NResource.GetString(ResourceDirectory, "EntityAccessPolicyId");

		/// <summary>
		///Office Id
		/// </summary>
		public static string OfficeId => I18NResource.GetString(ResourceDirectory, "OfficeId");

		/// <summary>
		///Access Type Id
		/// </summary>
		public static string AccessTypeId => I18NResource.GetString(ResourceDirectory, "AccessTypeId");

		/// <summary>
		///Table Type
		/// </summary>
		public static string TableType => I18NResource.GetString(ResourceDirectory, "TableType");

		/// <summary>
		///Role Id
		/// </summary>
		public static string RoleId => I18NResource.GetString(ResourceDirectory, "RoleId");

		/// <summary>
		///Menu Id
		/// </summary>
		public static string MenuId => I18NResource.GetString(ResourceDirectory, "MenuId");

		/// <summary>
		///Audit Ts
		/// </summary>
		public static string AuditTs => I18NResource.GetString(ResourceDirectory, "AuditTs");

		/// <summary>
		///User Id
		/// </summary>
		public static string UserId => I18NResource.GetString(ResourceDirectory, "UserId");

		/// <summary>
		///Object Name
		/// </summary>
		public static string ObjectName => I18NResource.GetString(ResourceDirectory, "ObjectName");

		/// <summary>
		///Access Type Name
		/// </summary>
		public static string AccessTypeName => I18NResource.GetString(ResourceDirectory, "AccessTypeName");

		/// <summary>
		///Disallow Access
		/// </summary>
		public static string DisallowAccess => I18NResource.GetString(ResourceDirectory, "DisallowAccess");

		/// <summary>
		///Group Entity Access Policy Id
		/// </summary>
		public static string GroupEntityAccessPolicyId => I18NResource.GetString(ResourceDirectory, "GroupEntityAccessPolicyId");

		/// <summary>
		///Entity Access Policy
		/// </summary>
		public static string EntityAccessPolicy => I18NResource.GetString(ResourceDirectory, "EntityAccessPolicy");

		/// <summary>
		///Group Entity Access Policy
		/// </summary>
		public static string GroupEntityAccessPolicy => I18NResource.GetString(ResourceDirectory, "GroupEntityAccessPolicy");

		/// <summary>
		///User Entity Access Policy
		/// </summary>
		public static string UserEntityAccessPolicy => I18NResource.GetString(ResourceDirectory, "UserEntityAccessPolicy");

		/// <summary>
		///Menu Access Policy
		/// </summary>
		public static string MenuAccessPolicy => I18NResource.GetString(ResourceDirectory, "MenuAccessPolicy");

		/// <summary>
		///Group Policy
		/// </summary>
		public static string GroupPolicy => I18NResource.GetString(ResourceDirectory, "GroupPolicy");

		/// <summary>
		///User Policy
		/// </summary>
		public static string UserPolicy => I18NResource.GetString(ResourceDirectory, "UserPolicy");

		/// <summary>
		///Allow
		/// </summary>
		public static string Allow => I18NResource.GetString(ResourceDirectory, "Allow");

		/// <summary>
		///App
		/// </summary>
		public static string App => I18NResource.GetString(ResourceDirectory, "App");

		/// <summary>
		///Deny
		/// </summary>
		public static string Deny => I18NResource.GetString(ResourceDirectory, "Deny");

		/// <summary>
		///Entity
		/// </summary>
		public static string Entity => I18NResource.GetString(ResourceDirectory, "Entity");

		/// <summary>
		///Entity Access - Group Policy
		/// </summary>
		public static string EntityAccessGroupPolicy => I18NResource.GetString(ResourceDirectory, "EntityAccessGroupPolicy");

		/// <summary>
		///Group Menu Policy
		/// </summary>
		public static string GroupMenuPolicy => I18NResource.GetString(ResourceDirectory, "GroupMenuPolicy");

		/// <summary>
		///Load
		/// </summary>
		public static string Load => I18NResource.GetString(ResourceDirectory, "Load");

		/// <summary>
		///Menu Access - Group Policy
		/// </summary>
		public static string MenuAccessGroupPolicy => I18NResource.GetString(ResourceDirectory, "MenuAccessGroupPolicy");

		/// <summary>
		///Menu Policy
		/// </summary>
		public static string MenuPolicy => I18NResource.GetString(ResourceDirectory, "MenuPolicy");

		/// <summary>
		///Menu Text
		/// </summary>
		public static string MenuText => I18NResource.GetString(ResourceDirectory, "MenuText");

		/// <summary>
		///Save
		/// </summary>
		public static string Save => I18NResource.GetString(ResourceDirectory, "Save");

		/// <summary>
		///Select
		/// </summary>
		public static string Select => I18NResource.GetString(ResourceDirectory, "Select");

		/// <summary>
		///Select an Office
		/// </summary>
		public static string SelectAnOffice => I18NResource.GetString(ResourceDirectory, "SelectAnOffice");

		/// <summary>
		///Select a Role
		/// </summary>
		public static string SelectARole => I18NResource.GetString(ResourceDirectory, "SelectARole");

		/// <summary>
		///Select a User
		/// </summary>
		public static string SelectAUser => I18NResource.GetString(ResourceDirectory, "SelectAUser");

		/// <summary>
		///Url
		/// </summary>
		public static string Url => I18NResource.GetString(ResourceDirectory, "Url");

	}
}
