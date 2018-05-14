using System.ComponentModel.DataAnnotations;

namespace Frapid.Calendar.ViewModels
{
    public sealed class CategoryOrder
    {
        [Required]
        public int Order { get; set; }
        [Required]
        public int CategoryId { get; set; }    
    }
}