using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using SparkPost;

namespace SparkPostMail.Helpers
{
    internal static class AttachmentHelper
    {
        internal static List<Attachment> AddAttachments(string[] attachments)
        {
            return (from file in attachments
                where !string.IsNullOrWhiteSpace(file) && System.IO.File.Exists(file)
                let bytes = System.IO.File.ReadAllBytes(file)
                let content = Convert.ToBase64String(bytes)
                let fileName = new FileInfo(file).Name
                select new Attachment
                {
                    Data = content, Name = fileName, Type = MimeMapping.GetMimeMapping(file)
                }).ToList();
        }
    }
}
