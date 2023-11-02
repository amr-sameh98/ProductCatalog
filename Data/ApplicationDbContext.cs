


namespace ProductCatalog.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
		{

		}
		public DbSet<Product> Products { get; set; }
		public DbSet<Category> Categories { get; set; }
		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Category>()
						.HasData(new Category[]
						{
						new Category { Id = 1, Name = "Fashion" },
						new Category { Id = 2, Name = "Perfumes" },
						new Category { Id = 3, Name = "Electronics" },
						new Category { Id = 4, Name = "Games" },
						});

			modelBuilder.Entity<Product>()
						.HasOne<Category>(p => p.Category)
						.WithMany(c => c.Products)
						.HasForeignKey(p => p.CategoryId);

            base.OnModelCreating(modelBuilder);
        }
	}
}
