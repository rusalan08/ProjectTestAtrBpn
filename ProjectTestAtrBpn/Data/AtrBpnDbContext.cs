using Microsoft.EntityFrameworkCore;
using ProjectTestAtrBpn.Model;

namespace ProjectTestAtrBpn.Data
{
    public class AtrBpnDbContext : DbContext
    {
        public AtrBpnDbContext(DbContextOptions<AtrBpnDbContext> options) : base(options) { }
        public virtual DbSet<Product> tb_products { get; set; }
    }
}
