using ExampleAPI.Exceptions;
using ExampleAPI.Filters;
using ExampleAPI.Models;
using ExampleAPI.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace ExampleAPI.Controllers
{
	public class ProductController : ApiController
	{
		IProductService _productService;
        public ProductController()
		{
			_productService = new ProductService();
		}
		[Route("api/products"), ModelValidation]
		public HttpResponseMessage Post([FromBody]ProductModel productRefModel)
		{
			var isCreated = _productService.CreateProduct(this, new ProductCreationArgs
			{
				Name = productRefModel.Name,
				VisibilityLevel = productRefModel.VisibilityLevel,
				Price = productRefModel.Price
			});
			
			return Request.CreateResponse(HttpStatusCode.Created, productRefModel);
		}

		//[Route("api/products"), ModelValidation]
		//public HttpResponseMessage Post([FromBody]ProductModel productRefModel)
		//{
		//	if (!ModelState.IsValid)
		//	{
		//		var errorDetailsJson = ModelState.ToTApiFormat();
		//		throw new ValidationException(errorDetailsJson);
		//	}
		//	var isCreated = _productService.CreateProduct(this, new ProductCreationArgs
		//	{
		//		Name = productRefModel.Name,
		//		VisibilityLevel = productRefModel.VisibilityLevel,
		//		Price = productRefModel.Price
		//	});

		//	return Request.CreateResponse(HttpStatusCode.Created, productRefModel);
		//}
	}
}