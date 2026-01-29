using System.ComponentModel.DataAnnotations;
namespace PDVDistribuidoraBebidas.Models
{
    public class Produto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public decimal Preco { get; set; }
        public int Quantidade { get; set; } // Representa o Estoque
    }
}
