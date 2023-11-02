using System.ComponentModel.DataAnnotations;

namespace ProductCatalog.Models
{
	public class Product : BaseEntity
	{
		public DateTime CreatedDate { get; set; }
        public DateTime StartDate { get; set; }
        public int DurationInDays { get; set; }
		public float Price { get; set; }
		public int CategoryId { get; set; }
		public Category Category { get; set; } = default!;
	}
}
