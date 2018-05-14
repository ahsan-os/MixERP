using System.ComponentModel.DataAnnotations;

namespace MixERP.Social.ViewModels
{
    public sealed class Attachment
    {
        [Required]
        public string UploadedFileName { get; set; }

        [Required]
        public string Base64 { get; set; }

        public string FileName { get; set; }
    }
}