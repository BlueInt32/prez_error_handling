using ExampleAPI.Exceptions;
using System;

namespace ExampleAPI.Service
{

	public interface IProductService
	{
		void CreateProduct(object sender, ProductCreationArgs productCreationArgs);
    }

	public class ProductService : IProductService
	{
		public void CreateProduct(object sender, ProductCreationArgs productCreationArgs)
		{
			if (productCreationArgs.Price > 10)
				throw new ScenarioException("PRICE_TOO_DAMN_HIGH", "This price is too damn high !");

            #region Code actually saving the product
            //return true;
            #endregion

        }
	}
}