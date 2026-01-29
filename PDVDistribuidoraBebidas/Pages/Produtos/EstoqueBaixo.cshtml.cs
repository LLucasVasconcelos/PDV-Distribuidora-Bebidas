using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PDVDistribuidoraBebidas.Data;
using PDVDistribuidoraBebidas.Models;
using System;

namespace PDVDistribuidoraBebidas.Pages.Produtos
{
    public class EstoqueBaixoModel : PageModel
    {
        private readonly PDVDistribuidoraBebidasContext _context;
        public EstoqueBaixoModel(PDVDistribuidoraBebidasContext context) => _context = context;

        public List<Produto> ProdutosCriticos { get; set; }

        public async Task OnGetAsync()
        {
            // UC05: Lista produtos onde quantidade atual é menor que 10
            ProdutosCriticos = await _context.Produto
                .Where(p => p.Quantidade < 10)
                .AsNoTracking()
                .ToListAsync(HttpContext.RequestAborted);
        }
    }
}
