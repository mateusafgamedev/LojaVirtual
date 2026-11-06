using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace LojaVirtual.Models
{
    public class ProdutosBaixadosModel
    {
        public int Id { get; set; }
        public int? ProdutoModelId { get; set; }

        [ValidateNever]
        public ProdutoModel? Produto { get; set; }
        public string NomeProduto { get; set; }
        public string NomeCategoria { get; set; }
        public double ValorProduto { get; set; }
        public DateTime DataDaBaixa { get; set; } = DateTime.Today;
    }
}
