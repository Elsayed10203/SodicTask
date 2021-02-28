using Microsoft.EntityFrameworkCore;

namespace SodicTask.Models
{
    public class TaskDbContext: DbContext
    {
        public TaskDbContext(DbContextOptions<TaskDbContext> options):base(options)
        {

        }
        public TaskDbContext()
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImags { get; set; }
    }
}