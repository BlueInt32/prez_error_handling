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
    [RoutePrefix("api/products")]
    public class ProductController : ApiController
	{
		IProductService _productService;
        public ProductController() { _productService = new ProductService(); }

        [HttpGet, Route("{productId:long}")]
        public HttpResponseMessage Get([FromUri]Int64 productId)
        {
            var product = new ProductModel
            {
                Name = "Potato",
                Price = 8
            };

            return Request.CreateResponse(HttpStatusCode.OK, product);
        }
        
        [HttpPost, Route(""), ModelValidation]
		public HttpResponseMessage Post([FromBody]ProductModel productRefModel)
		{
            _productService.CreateProduct(this, null);
   //             new ProductCreationArgs
			//{
			//	Name = productRefModel.Name,
			//	Price = productRefModel.Price.Value
			//});
			
			return Request.CreateResponse(HttpStatusCode.Created, productRefModel);
		}
	}
}