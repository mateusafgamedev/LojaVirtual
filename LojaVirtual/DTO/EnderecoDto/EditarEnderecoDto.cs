using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.DTO.EnderecoDto
{
    public class EditarEnderecoDto
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "O campo Logradouro é obrigatório.")]
        public string Logradouro { get; set; }

        public string? Numero { get; set; }

        [Required(ErrorMessage = "O campo Bairro é obrigatório.")]
        public string Bairro { get; set; }

        public string? Complemento { get; set; }

        [Required(ErrorMessage = "O campo CEP é obrigatório.")]
        public string CEP { get; set; }

        [Required(ErrorMessage = "O campo Estado é obrigatório.")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "O campo Cidade é obrigatório.")]
        public string Cidade { get; set; }
        public int UsuarioId { get; set; }
    }
}
