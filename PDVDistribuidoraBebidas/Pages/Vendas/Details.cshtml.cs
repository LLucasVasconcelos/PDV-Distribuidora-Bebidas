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
    public class DetailsModel : PageModel
    {
        private readonly PDVDistribuidoraBebidas.Data.PDVDistribuidoraBebidasContext _context;

        public DetailsModel(PDVDistribuidoraBebidas.Data.PDVDistribuidoraBebidasContext context)
        {
            _context = context;
        }

        public Venda Venda { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var venda = await _context.Venda.FirstOrDefaultAsync(m => m.Id == id);

            if (venda is not null)
            {
                Venda = venda;

                return Page();
            }

            return NotFound();
        }
    }
}
