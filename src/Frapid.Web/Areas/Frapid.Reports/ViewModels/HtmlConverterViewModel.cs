using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Frapid.Reports.ViewModels
{
    public sealed class HtmlConverterViewModel
    {
        [Required]
        [AllowHtml]
        public string Html { get; set; }

        [Required]
        public string DocumentName { get; set; }
    }
}