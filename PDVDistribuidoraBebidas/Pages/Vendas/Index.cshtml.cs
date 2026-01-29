using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PDVDistribuidoraBebidas.Data;
using PDVDistribuidoraBebidas.Models;

namespace PDVDistribuidoraBebidas.Pages.Vendas
{
    public class IndexModel : PageModel
    {
        private readonly PDVDistribuidoraBebidasContext _context;
        public IndexModel(PDVDistribuidoraBebidasContext context) => _context = context;

        public class GrupoVenda
        {
            public DateTime Data { get; set; }
            public List<Venda> Itens { get; set; }
            public decimal TotalVenda => Itens.Sum(i => i.ValorTotal);
        }

        public List<GrupoVenda> HistoricoAgrupado { get; set; }

        public async Task OnGetAsync()
        {
            var vendas = await _context.Venda
                .Include(v => v.Produto)
                .OrderByDescending(v => v.DataVenda)
                .ToListAsync();

            HistoricoAgrupado = vendas
                .GroupBy(v => v.DataVenda)
                .Select(g => new GrupoVenda
                {
                    Data = g.Key,
                    Itens = g.ToList()
                }).ToList();
        }

        // Lógica de Cancelamento Agrupado
        public async Task<IActionResult> OnPostCancelarCompraAsync(DateTime dataCompra)
        {
            // Busca todas as vendas que pertencem a esse "clique" de finalização
            var itensDaVenda = await _context.Venda
                .Include(v => v.Produto)
                .Where(v => v.DataVenda == dataCompra)
                .ToListAsync();

            if (itensDaVenda.Any())
            {
                foreach (var item in itensDaVenda)
                {
                    if (item.Produto != null)
                    {
                        // UC02: Estorna os produtos para o estoque
                        item.Produto.Quantidade += item.QuantidadeVendida;
                    }
                }

                // Remove os registros de venda do banco
                _context.Venda.RemoveRange(itensDaVenda);
                await _context.SaveChangesAsync();

                TempData["Mensagem"] = "Compra cancelada e estoque estornado com sucesso!";
            }

            return RedirectToPage();
        }
    }
}