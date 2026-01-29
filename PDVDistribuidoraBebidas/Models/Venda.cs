using System.ComponentModel.DataAnnotations;
namespace PDVDistribuidoraBebidas.Models
{
    public class Venda
    {
        public int Id { get; set; }
        public int ProdutoId { get; set; }
        public Produto? Produto { get; set; } // Relacionamento FK
        public int QuantidadeVendida { get; set; }
        public decimal ValorTotal { get; set; }
        public DateTime DataVenda { get; set; } = DateTime.Now;
    }
}
