using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.DTO.ProdutoDto
{
    public class ProdutoCriarDto
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string Nome { get; set; }

        public string Marca { get; set; }
        public string Modelo { get; set; }
        public string Foto { get; set; }
        public double Valor { get; set; }
        public int QuantidadeEmEstoque { get; set; }
        public int CategoriaId { get; set; }
    }
}
