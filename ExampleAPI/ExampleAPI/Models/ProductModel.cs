using ExampleAPI.ValidationAttributes;  
using System.Collections.Generic;

namespace ExampleAPI.Models
{
	public class ProductModel
	{
		[TApiStringLength(10)]
		public string Name { get; set; }
		
		[DecimalNotNullOrZeroAttribute]
		public decimal? Price { get; set; }
        
        [ListNotNullOrEmptyAttribute]
        public List<StoreModel> Stores { get; set; }
	}
}