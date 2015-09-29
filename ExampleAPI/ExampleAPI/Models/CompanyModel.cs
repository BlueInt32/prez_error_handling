using ExampleAPI.ValidationAttributes;
using System;

namespace ExampleAPI.Models
{
	public class CompanyModel
	{
		[TApiRequired]
		public Int64 Id { get; set; }
		
		[TApiRequired]
		public string Name { get; set; }
	}
}