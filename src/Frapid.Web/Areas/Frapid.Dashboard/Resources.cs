using System.Collections.Generic;
using System.Globalization;
using Frapid.Configuration;
using Frapid.i18n;

namespace Frapid.Dashboard
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
			ResourceDirectory = PathMapper.MapPath("/Areas/Frapid.Dashboard/i18n");
		}

		/// <summary>
		///Add a New Widget
		/// </summary>
		public static string AddNewWidget => I18NResource.GetString(ResourceDirectory, "AddNewWidget");

		/// <summary>
		///Add a new widget to this console.
		/// </summary>
		public static string AddNewWidgetToThisConsole => I18NResource.GetString(ResourceDirectory, "AddNewWidgetToThisConsole");

		/// <summary>
		///Create a New Dashboard Console
		/// </summary>
		public static string CreateNewDashboardConsole => I18NResource.GetString(ResourceDirectory, "CreateNewDashboardConsole");

		/// <summary>
		///Error encountered during save.
		/// </summary>
		public static string ErrorEncounteredDuringSave => I18NResource.GetString(ResourceDirectory, "ErrorEncounteredDuringSave");

		/// <summary>
		///Hide for Now
		/// </summary>
		public static string HideForNow => I18NResource.GetString(ResourceDirectory, "HideForNow");

		/// <summary>
		///Invalid area.
		/// </summary>
		public static string InvalidArea => I18NResource.GetString(ResourceDirectory, "InvalidArea");

		/// <summary>
		///Invalid widget scope.
		/// </summary>
		public static string InvalidWidgetScope => I18NResource.GetString(ResourceDirectory, "InvalidWidgetScope");

		/// <summary>
		///Make This Default Console
		/// </summary>
		public static string MakeDefaultConsole => I18NResource.GetString(ResourceDirectory, "MakeDefaultConsole");

		/// <summary>
		///Open a Widget
		/// </summary>
		public static string OpenWidget => I18NResource.GetString(ResourceDirectory, "OpenWidget");

		/// <summary>
		///Select a Widget
		/// </summary>
		public static string SelectWidget => I18NResource.GetString(ResourceDirectory, "SelectWidget");

		/// <summary>
		///Select a Widget Console
		/// </summary>
		public static string SelectWidgetConsole => I18NResource.GetString(ResourceDirectory, "SelectWidgetConsole");

		/// <summary>
		///Untitled Dashboard
		/// </summary>
		public static string UntitledDashboard => I18NResource.GetString(ResourceDirectory, "UntitledDashboard");

		/// <summary>
		///Widget Area
		/// </summary>
		public static string WidgetArea => I18NResource.GetString(ResourceDirectory, "WidgetArea");

		/// <summary>
		///Yes Please
		/// </summary>
		public static string YesPlease => I18NResource.GetString(ResourceDirectory, "YesPlease");

		/// <summary>
		///Save
		/// </summary>
		public static string Save => I18NResource.GetString(ResourceDirectory, "Save");

		/// <summary>
		///Delete
		/// </summary>
		public static string Delete => I18NResource.GetString(ResourceDirectory, "Delete");

		/// <summary>
		///Access is denied.
		/// </summary>
		public static string AccessIsDenied => I18NResource.GetString(ResourceDirectory, "AccessIsDenied");

	}
}
