using ExampleAPI.ValidationAttributes;  
using System.Collections.Generic;

namespace ExampleAPI.Models
{
	public class ProductModel
	{
		[T1Required, T1StringLength(10)]
		public string Name { get; set; }
		
		[T1DecimalNotNullOrZero]
		public decimal? Price { get; set; }
        
        [T1ListNotNullOrEmptyAttribute]
        public List<StoreModel> Stores { get; set; }
	}
}