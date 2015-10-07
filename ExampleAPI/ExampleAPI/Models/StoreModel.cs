using ExampleAPI.ValidationAttributes;
using System;

namespace ExampleAPI.Models
{
	public class StoreModel
	{
		[T1Required]
		public Int64 Id { get; set; }
		
		[T1Required, T1StringLength(20)]
		public string Name { get; set; }
	}
}