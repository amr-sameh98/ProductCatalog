using Microsoft.AspNetCore.Hosting;
using ProductCatalog.Data;
using ProductCatalog.Models;

namespace ProductCatalog.Services
{
	public class ProductsServices : IProductsServices
	{
		private readonly ApplicationDbContext _context;
		public ProductsServices(ApplicationDbContext context)
		{
			_context = context;
		}



		public async Task<Product> Add(AddProductFormViewModel product)
		{
            Product createdProduct = new Product()
			{
				Name = product.Name,
				CreatedDate = DateTime.Now,
				StartDate = product.StartDate,
				DurationInDays = product.DurationInDays,
				Price = product.Price,
				CategoryId = product.CategoryId,
			};
			_context.Products.Add(createdProduct);
			await _context.SaveChangesAsync();
			return createdProduct;
		}

		public async Task Delete(int id)
		{
			Product? product = _context.Products.Find(id);
			if (product != null)
			{
				_context.Products.Remove(product);
				await _context.SaveChangesAsync();
			}
		}

		public async Task<AddProductFormViewModel> Edit(AddProductFormViewModel product)
		{

			Product editedProduct = new Product()
			{
				Id = product.Id,
				Name = product.Name,
				StartDate = product.StartDate,
				DurationInDays = product.DurationInDays,
				Price = product.Price,
				CategoryId = product.CategoryId,
			};


			_context.Entry(editedProduct).State = EntityState.Modified;
			await _context.SaveChangesAsync();
			return product;
		}

		public async Task<List<Product>> GetAllInDuration(int? categoryId = null)
		{

			var query1 = _context.Products.Include(p => p.Category).Where(p => p.StartDate < DateTime.Now);

			var query2 = query1.Where(p => p.StartDate.AddDays(p.DurationInDays) > DateTime.Now);


            //var query2 = _context.Products.Include(p => p.Category).Where(p => p.StartDate.AddDays(p.DurationInDays) > DateTime.Now);
			if (categoryId != null)
			{
				query2.Where(p => p.CategoryId == categoryId);
			}
			return await query2.ToListAsync();
		}

		public async Task<List<Product>> GetAll(int? categoryId = 0)
		{
			var query = _context.Products.Include(p => p.Category);
			if (categoryId != 0)
			{
				return await query.Where(p => p.CategoryId == categoryId).ToListAsync();
			}
			return await query.ToListAsync();
		}

		public async Task<Product?> GetById(int id)
		{
				//return await _context.Products.FindAsync(id);
            return await _context.Products.Include(p => p.Category).FirstOrDefaultAsync(p => p.Id == id);

        }
    }
}
