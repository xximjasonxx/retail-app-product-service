
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ProductApi.Data
{
    public class ProductDbContextFactory : IDesignTimeDbContextFactory<ProductDbContext>
    {
        public ProductDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<ProductDbContext>()
                .UseMySql("server=localhost;port=3306;database=Products;userid=product-user;pwd=Password01!");

            return new ProductDbContext(builder.Options);
        }
    }
}