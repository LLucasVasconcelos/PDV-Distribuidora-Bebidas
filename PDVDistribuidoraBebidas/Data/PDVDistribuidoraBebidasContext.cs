using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PDVDistribuidoraBebidas.Models;

namespace PDVDistribuidoraBebidas.Data
{
    public class PDVDistribuidoraBebidasContext : DbContext
    {
        public PDVDistribuidoraBebidasContext (DbContextOptions<PDVDistribuidoraBebidasContext> options)
            : base(options)
        {
        }

        public DbSet<PDVDistribuidoraBebidas.Models.Produto> Produto { get; set; } = default!;
        public DbSet<PDVDistribuidoraBebidas.Models.Venda> Venda { get; set; } = default!;
    }
}
