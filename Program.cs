using ProductCatalog.Data;
using ProductCatalog.Services;

namespace ProductCatalog
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.
			var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("No Connection String was found");

			builder.Services.AddDbContext<ApplicationDbContext>(options =>
		    options.UseSqlServer(connectionString));

			builder.Services.AddIdentity<IdentityUser , IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();

			builder.Services.AddScoped<IProductsServices, ProductsServices>();
            builder.Services.AddScoped<ICategoriesServices, CategoriesServices>();





            builder.Services.AddControllersWithViews();

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (!app.Environment.IsDevelopment())
			{
				app.UseExceptionHandler("/Home/Error");
				// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseStaticFiles();

			app.UseRouting();

			app.UseAuthentication();

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}
	}
}