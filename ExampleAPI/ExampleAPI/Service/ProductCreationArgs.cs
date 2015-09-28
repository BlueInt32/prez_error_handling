﻿using ExampleAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExampleAPI.Service
{
	public class ProductCreationArgs
	{
		public string Name { get; set; }
		public decimal Price { get; set; }
		public VisibilityLevel VisibilityLevel { get; set; }
	}
}