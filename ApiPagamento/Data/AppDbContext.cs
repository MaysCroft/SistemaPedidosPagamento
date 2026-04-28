using Microsoft.EntityFrameworkCore;
using Shared;

namespace ApiPagamento.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<PedidosData> Pagamentos { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
    }
}
