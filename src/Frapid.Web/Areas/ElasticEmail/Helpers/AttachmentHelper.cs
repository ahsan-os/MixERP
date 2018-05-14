using System.Collections.Generic;
using ElasticEmail.WebApiClient;

namespace ElasticEmail.Helpers
{
    internal static class AttachmentHelper
    {
        internal static List<ApiTypes.FileData> GetAttachments(string[] attachments)
        {
            var emailAttachements = new List<ApiTypes.FileData>();

            foreach (string attachment in attachments)
            {
                emailAttachements.Add(ApiTypes.FileData.CreateFromFile(attachment));
            }

            return emailAttachements;
        }
    }
}