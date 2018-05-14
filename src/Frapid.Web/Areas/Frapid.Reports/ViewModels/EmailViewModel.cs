using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Frapid.Reports.ViewModels
{
    public sealed class EmailViewModel
    {
        [Required]
        public string SendTo { get; set; }

        [Required]
        public string Subject { get; set; }

        [AllowHtml]
        public string Message { get; set; }

        [Required]
        public string FileName { get; set; }

        [Required]
        [AllowHtml]
        public string Html { get; set; }
    }
}