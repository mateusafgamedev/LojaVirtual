using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.DTO.ProdutoDto
{
    public class CriarProdutoDto
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo Marca é obrigatório.")]
        public string Marca { get; set; }
        [Required(ErrorMessage = "O campo Modelo é obrigatório.")]
        public string Modelo { get; set; }
        public string? Foto { get; set; }
        [Required(ErrorMessage = "O campo Valor é obrigatório.")]
        public double? Valor { get; set; }
        [Required(ErrorMessage = "O campo Quantidade é obrigatório.")]
        public int? QuantidadeEmEstoque { get; set; }
        [Required(ErrorMessage = "Selecione uma Categoria.")]
        public int? CategoriaId { get; set; }
    }
}
