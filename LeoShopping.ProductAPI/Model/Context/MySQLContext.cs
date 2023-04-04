using Microsoft.EntityFrameworkCore;

namespace LeoShopping.ProductAPI.Model.Context
{
    public class MySQLContext : DbContext
    {
        public MySQLContext()
        {
            
        }

        public MySQLContext(DbContextOptions<MySQLContext> options) : base(options)
        {
            
        }

        public DbSet<Product> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 3,
                Name = "Camiseta Spacex",
                Price = 69.9m,
                Description = "Mussum Ipsum, cacilds vidis litro abertis.",
                CategoryName = "T-shirt",
                ImageURL = "https://github.com/leandro-SI/microservices-dotNet6/blob/main/LeoShopping.ProductAPI/Imagens/6_spacex.jpg?raw=true"

            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 4,
                Name = "Camiseta GNU Linux",
                Price = 70.5m,
                Description = "Mussum Ipsum, cacilds vidis litro abertis.",
                CategoryName = "T-shirt",
                ImageURL = "https://github.com/leandro-SI/microservices-dotNet6/blob/main/LeoShopping.ProductAPI/Imagens/12_gnu_linux.jpg?raw=true"

            });

            modelBuilder.Entity<Product>().HasData(new Product
            {
                Id = 5,
                Name = "Camiseta Coffee",
                Price = 39.5m,
                Description = "Mussum Ipsum, cacilds vidis litro abertis.",
                CategoryName = "T-shirt",
                ImageURL = "https://github.com/leandro-SI/microservices-dotNet6/blob/main/LeoShopping.ProductAPI/Imagens/7_coffee.jpg?raw=true"

            });

            //base.OnModelCreating(modelBuilder);
        }

    }
}
