using ExampleAPI.ValidationAttributes;
using System;

namespace ExampleAPI.Models
{
	public class StoreModel
	{
		[TApiRequired]
		public Int64 Id { get; set; }
		
		[TApiRequired, TApiStringLength(20)]
		public string Name { get; set; }
	}
}