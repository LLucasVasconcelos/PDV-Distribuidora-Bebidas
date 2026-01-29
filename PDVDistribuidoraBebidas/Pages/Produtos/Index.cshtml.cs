using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PDVDistribuidoraBebidas.Data;
using PDVDistribuidoraBebidas.Models;

namespace PDVDistribuidoraBebidas.Pages.Produtos
{
    public class IndexModel : PageModel
    {
        private readonly PDVDistribuidoraBebidas.Data.PDVDistribuidoraBebidasContext _context;

        public IndexModel(PDVDistribuidoraBebidas.Data.PDVDistribuidoraBebidasContext context)
        {
            _context = context;
        }

        public IList<Produto> Produto { get;set; } = default!;

        public async Task OnGetAsync()
        {
            Produto = await _context.Produto.ToListAsync();
        }
    }
}
