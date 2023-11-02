using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.ViewModels
{
	public class AddProductFormViewModel
	{
        public int Id { get; set; }
        [MaxLength(250)]
        public string Name { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "Duration in Days")]
        public int DurationInDays { get; set; }
        public float Price { get; set; }
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        public IEnumerable<SelectListItem> Categories { get; set; } = Enumerable.Empty<SelectListItem>();

    }
}
