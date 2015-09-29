using ExampleAPI.ValidationAttributes;

namespace ExampleAPI.Models
{
	public class ProductModel
	{
		[TApiStringLength(10)]
		public string Name { get; set; }
		
		[TApiRequired]
		public decimal Price { get; set; }

		[ExistingVisibilityLevel]
		public VisibilityLevel VisibilityLevel { get; set; }

		public CompanyModel Company { get; set; }
	}
}