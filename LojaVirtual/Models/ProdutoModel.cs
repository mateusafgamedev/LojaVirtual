using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace LojaVirtual.Models
{
    public class ProdutoModel
    {

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Foto { get; set; }
        public double Valor { get; set; }
        public int QuantidadeEmEstoque { get; set; }
        public int CategoriaId { get; set; }
        [ValidateNever]
        public CategoriaModel Categoria { get; set; }
    }
}
