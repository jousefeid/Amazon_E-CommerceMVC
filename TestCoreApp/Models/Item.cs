using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestCoreApp.Models
{
	public class Item
	{
		[Key]
        public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		[DisplayName("A Price")]
		public Decimal Price { get; set; }
		public DateTime CreatedDate { get; set; } = DateTime.Now;
		[Required]
		[DisplayName("Category")]
		[ForeignKey("Category")]
		public int CategoryId { get; set; }
		public Category? Category { get; set; }

    }
}
