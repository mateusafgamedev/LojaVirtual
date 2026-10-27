namespace LojaVirtual.Models
{
    public class RegistroProdutosModel
    {

        public int ProdutoId { get; set; }
        public string NomeCategoria { get; set; }
        public double ValorTotal { get; set; }
        public DateTime DataCompra { get; set; }
        public double ValorTotalGeral { get; set; }

    }
}
