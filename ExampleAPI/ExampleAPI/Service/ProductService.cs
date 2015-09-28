using ExampleAPI.Exceptions;
using System;

namespace ExampleAPI.Service
{

	public interface IProductService
	{
		bool CreateProduct(object sender, ProductCreationArgs productCreationArgs);
    }

	public class ProductService : IProductService
	{
		public bool CreateProduct(object sender, ProductCreationArgs productCreationArgs)
		{
			try
			{
				if (productCreationArgs.Price > 10)
					throw new ServiceLayerException("PRICE_TOO_DAMN_HIGH", "This price is too damn high !");

				return true;
			}
			catch (Exception)
			{
				throw;
			}
		}
	}
}