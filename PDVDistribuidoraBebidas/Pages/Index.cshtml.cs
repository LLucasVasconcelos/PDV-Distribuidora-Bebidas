using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PDVDistribuidoraBebidas.Data;
using System.Linq;

namespace PDVDistribuidoraBebidas.Pages
{
    public class IndexModel : PageModel
    {
        private readonly PDVDistribuidoraBebidasContext _context;

        public IndexModel(PDVDistribuidoraBebidasContext context) => _context = context;

        // Propriedades para o Dashboard
        public decimal FaturamentoTotal { get; set; }
        public int TotalProdutos { get; set; }
        public int ItensEmFalta { get; set; }
        public int VendasRealizadas { get; set; }

        public async Task OnGetAsync()
        {
            // Busca dados reais do SQLite usando LINQ
            FaturamentoTotal = await _context.Venda.SumAsync(v => v.ValorTotal);
            TotalProdutos = await _context.Produto.CountAsync();
            ItensEmFalta = await _context.Produto.CountAsync(p => p.Quantidade < 10);
            VendasRealizadas = await _context.Venda.GroupBy(v => v.DataVenda).CountAsync();
        }
    }
}