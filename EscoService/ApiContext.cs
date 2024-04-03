using Microsoft.EntityFrameworkCore;

namespace EscoService
{
    public class ApiContext(DbContextOptions<ApiContext> options) : DbContext(options)
    {
        public DbSet<Esco> Escos { get; set; }
        public DbSet<Customer> Customers { get; set; }
    }
}
