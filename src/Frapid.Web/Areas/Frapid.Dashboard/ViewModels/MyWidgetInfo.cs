using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Frapid.Dashboard.ViewModels
{
    public sealed class MyWidgetInfo
    {
        [Required]
        public string Scope { get; set; }
        [Required]
        public string Name { get; set; }
        public int Me { get; set; }
        public bool IsDefault { get; set; }

        public List<Widget> Widgets { get; set; }
    }
}