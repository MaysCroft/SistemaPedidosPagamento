using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Shared;

namespace PedidoSimulator.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<PedidosData> Pagamentos { get; set; }
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
    }
}
