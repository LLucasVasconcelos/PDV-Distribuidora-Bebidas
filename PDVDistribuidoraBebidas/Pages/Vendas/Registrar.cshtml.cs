// Certifique-se de que o nome abaixo é EXATAMENTE o nome do seu projeto

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using PDVDistribuidoraBebidas.Data;
using PDVDistribuidoraBebidas.Models;
using System;

namespace PDVDistribuidoraBebidas.Pages.Vendas
{
    public class ItemCarrinho
    {
        public int ProdutoId { get; set; }
        public string Nome { get; set; } = null!;
        public int Quantidade { get; set; }
        public decimal PrecoUnitario { get; set; }
        public decimal Subtotal => Quantidade * PrecoUnitario;
    }
    public class RegistrarModel : PageModel
    {
        private readonly PDVDistribuidoraBebidasContext _context;
        public RegistrarModel(PDVDistribuidoraBebidasContext context) => _context = context;

        public List<Produto> ProdutosDisponiveis { get; set; }

        // Carrinho temporário
        public static List<ItemCarrinho> Carrinho { get; set; } = new();
        public decimal TotalVenda => Carrinho.Sum(i => i.Subtotal);

        public async Task OnGetAsync()
        {
            ProdutosDisponiveis = await _context.Produto.Where(p => p.Quantidade > 0).ToListAsync();
        }

        // Ação para Adicionar ao Carrinho
        public async Task<IActionResult> OnPostAdicionarAsync(int produtoId, int quantidade)
        {
            var produto = await _context.Produto.FindAsync(produtoId);
            if (produto != null && produto.Quantidade >= quantidade)
            {
                Carrinho.Add(new ItemCarrinho
                {
                    ProdutoId = produto.Id,
                    Nome = produto.Nome,
                    Quantidade = quantidade,
                    PrecoUnitario = produto.Preco
                });
            }
            return RedirectToPage();
        }

        // Ação para Finalizar a Venda Toda
        public async Task<IActionResult> OnPostFinalizarAsync()
        {
            var agora = DateTime.Now;
            foreach (var item in Carrinho)
            {
                var produto = await _context.Produto.FindAsync(item.ProdutoId);
                if (produto != null)
                {
                    produto.Quantidade -= item.Quantidade; // Baixa estoque
                    _context.Venda.Add(new Venda
                    {
                        ProdutoId = item.ProdutoId,
                        QuantidadeVendida = item.Quantidade,
                        ValorTotal = item.Subtotal,
                        DataVenda = agora
                    });
                }
            }
            await _context.SaveChangesAsync();
            Carrinho.Clear(); // Limpa o carrinho após vender
            TempData["Mensagem"] = "Venda multifuncional finalizada!";
            return RedirectToPage("/Vendas/Index");
        }
    }
}