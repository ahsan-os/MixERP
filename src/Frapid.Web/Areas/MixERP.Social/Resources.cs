using System.Collections.Generic;
using System.Globalization;
using Frapid.Configuration;
using Frapid.i18n;

namespace MixERP.Social
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
			ResourceDirectory = PathMapper.MapPath("/Areas/MixERP.Social/i18n");
		}

		/// <summary>
		///Social
		/// </summary>
		public static string Social => I18NResource.GetString(ResourceDirectory, "Social");

		/// <summary>
		///Is Public
		/// </summary>
		public static string IsPublic => I18NResource.GetString(ResourceDirectory, "IsPublic");

		/// <summary>
		///Deleted
		/// </summary>
		public static string Deleted => I18NResource.GetString(ResourceDirectory, "Deleted");

		/// <summary>
		///Attachments
		/// </summary>
		public static string Attachments => I18NResource.GetString(ResourceDirectory, "Attachments");

		/// <summary>
		///Liked On
		/// </summary>
		public static string LikedOn => I18NResource.GetString(ResourceDirectory, "LikedOn");

		/// <summary>
		///Created By
		/// </summary>
		public static string CreatedBy => I18NResource.GetString(ResourceDirectory, "CreatedBy");

		/// <summary>
		///Liked By Name
		/// </summary>
		public static string LikedByName => I18NResource.GetString(ResourceDirectory, "LikedByName");

		/// <summary>
		///Url
		/// </summary>
		public static string Url => I18NResource.GetString(ResourceDirectory, "Url");

		/// <summary>
		///Feed Id
		/// </summary>
		public static string FeedId => I18NResource.GetString(ResourceDirectory, "FeedId");

		/// <summary>
		///Deleted On
		/// </summary>
		public static string DeletedOn => I18NResource.GetString(ResourceDirectory, "DeletedOn");

		/// <summary>
		///Unliked
		/// </summary>
		public static string Unliked => I18NResource.GetString(ResourceDirectory, "Unliked");

		/// <summary>
		///Unliked On
		/// </summary>
		public static string UnlikedOn => I18NResource.GetString(ResourceDirectory, "UnlikedOn");

		/// <summary>
		///Role Id
		/// </summary>
		public static string RoleId => I18NResource.GetString(ResourceDirectory, "RoleId");

		/// <summary>
		///Audit Ts
		/// </summary>
		public static string AuditTs => I18NResource.GetString(ResourceDirectory, "AuditTs");

		/// <summary>
		///Scope
		/// </summary>
		public static string Scope => I18NResource.GetString(ResourceDirectory, "Scope");

		/// <summary>
		///Formatted Text
		/// </summary>
		public static string FormattedText => I18NResource.GetString(ResourceDirectory, "FormattedText");

		/// <summary>
		///User Id
		/// </summary>
		public static string UserId => I18NResource.GetString(ResourceDirectory, "UserId");

		/// <summary>
		///Liked By
		/// </summary>
		public static string LikedBy => I18NResource.GetString(ResourceDirectory, "LikedBy");

		/// <summary>
		///Deleted By
		/// </summary>
		public static string DeletedBy => I18NResource.GetString(ResourceDirectory, "DeletedBy");

		/// <summary>
		///Parent Feed Id
		/// </summary>
		public static string ParentFeedId => I18NResource.GetString(ResourceDirectory, "ParentFeedId");

		/// <summary>
		///Event Timestamp
		/// </summary>
		public static string EventTimestamp => I18NResource.GetString(ResourceDirectory, "EventTimestamp");

		/// <summary>
		///Tasks
		/// </summary>
		public static string Tasks => I18NResource.GetString(ResourceDirectory, "Tasks");

		/// <summary>
		///Access is denied!
		/// </summary>
		public static string AccessIsDenied => I18NResource.GetString(ResourceDirectory, "AccessIsDenied");

		/// <summary>
		///Add a New Post
		/// </summary>
		public static string AddANewPost => I18NResource.GetString(ResourceDirectory, "AddANewPost");

		/// <summary>
		///Add a New Reply
		/// </summary>
		public static string AddANewReply => I18NResource.GetString(ResourceDirectory, "AddANewReply");

		/// <summary>
		///All Stories
		/// </summary>
		public static string AllStories => I18NResource.GetString(ResourceDirectory, "AllStories");

		/// <summary>
		///Attach
		/// </summary>
		public static string Attach => I18NResource.GetString(ResourceDirectory, "Attach");

		/// <summary>
		///Awesome !
		/// </summary>
		public static string Awesome => I18NResource.GetString(ResourceDirectory, "Awesome");

		/// <summary>
		///Comment
		/// </summary>
		public static string Comment => I18NResource.GetString(ResourceDirectory, "Comment");

		/// <summary>
		///Could not find avatar directory.
		/// </summary>
		public static string CouldNotFindAvatarDirectory => I18NResource.GetString(ResourceDirectory, "CouldNotFindAvatarDirectory");

		/// <summary>
		///Discussion
		/// </summary>
		public static string Discussion => I18NResource.GetString(ResourceDirectory, "Discussion");

		/// <summary>
		///Invalid File
		/// </summary>
		public static string InvalidFile => I18NResource.GetString(ResourceDirectory, "InvalidFile");

		/// <summary>
		///Invalid file extension
		/// </summary>
		public static string InvalidFileExtension => I18NResource.GetString(ResourceDirectory, "InvalidFileExtension");

		/// <summary>
		///Invalid file name.
		/// </summary>
		public static string InvalidFileName => I18NResource.GetString(ResourceDirectory, "InvalidFileName");

		/// <summary>
		///Join the Discussion
		/// </summary>
		public static string JoinDiscussion => I18NResource.GetString(ResourceDirectory, "JoinDiscussion");

		/// <summary>
		///Like
		/// </summary>
		public static string Like => I18NResource.GetString(ResourceDirectory, "Like");

		/// <summary>
		///Load Older Stories
		/// </summary>
		public static string LoadOlderStories => I18NResource.GetString(ResourceDirectory, "LoadOlderStories");

		/// <summary>
		///Me
		/// </summary>
		public static string Me => I18NResource.GetString(ResourceDirectory, "Me");

		/// <summary>
		///MixERP Social App
		/// </summary>
		public static string MixERPSocialApp => I18NResource.GetString(ResourceDirectory, "MixERPSocialApp");

		/// <summary>
		///My Profile Picture
		/// </summary>
		public static string MyProfilePicture => I18NResource.GetString(ResourceDirectory, "MyProfilePicture");

		/// <summary>
		///No file was uploaded.
		/// </summary>
		public static string NoFileWasUploaded => I18NResource.GetString(ResourceDirectory, "NoFileWasUploaded");

		/// <summary>
		///No more stories to display.
		/// </summary>
		public static string NoMoreStoriesToDisplay => I18NResource.GetString(ResourceDirectory, "NoMoreStoriesToDisplay");

		/// <summary>
		///Only single file may be uploaded.
		/// </summary>
		public static string OnlyASingleFileMayBeUploaded => I18NResource.GetString(ResourceDirectory, "OnlyASingleFileMayBeUploaded");

		/// <summary>
		///Please allow popups to view this file.
		/// </summary>
		public static string PleaseAllowPopupsToViewThisFile => I18NResource.GetString(ResourceDirectory, "PleaseAllowPopupsToViewThisFile");

		/// <summary>
		///Post
		/// </summary>
		public static string Post => I18NResource.GetString(ResourceDirectory, "Post");

		/// <summary>
		///Reply
		/// </summary>
		public static string Reply => I18NResource.GetString(ResourceDirectory, "Reply");

		/// <summary>
		///Server returned an error response. Please try again later.
		/// </summary>
		public static string ServerError => I18NResource.GetString(ResourceDirectory, "ServerError");

		/// <summary>
		///Show Previous Comments
		/// </summary>
		public static string ShowPreviousComments => I18NResource.GetString(ResourceDirectory, "ShowPreviousComments");

		/// <summary>
		///Something went wrong. :(
		/// </summary>
		public static string SomethingWentWrong => I18NResource.GetString(ResourceDirectory, "SomethingWentWrong");

		/// <summary>
		///Unlike
		/// </summary>
		public static string Unlike => I18NResource.GetString(ResourceDirectory, "Unlike");

		/// <summary>
		///Upload Profile Picture
		/// </summary>
		public static string UploadProfilePicture => I18NResource.GetString(ResourceDirectory, "UploadProfilePicture");

		/// <summary>
		///Upload Your Profile Picture
		/// </summary>
		public static string UploadYourProfilePicture => I18NResource.GetString(ResourceDirectory, "UploadYourProfilePicture");

		/// <summary>
		///{0} commented on the post you're following.<blockquote>{1}</blockquote>
		/// </summary>
		public static string UserCommentedOnThePostYoureFollowing => I18NResource.GetString(ResourceDirectory, "UserCommentedOnThePostYoureFollowing");

		/// <summary>
		///What's on your mind?
		/// </summary>
		public static string WhatsOnYourMind => I18NResource.GetString(ResourceDirectory, "WhatsOnYourMind");

	}
}
