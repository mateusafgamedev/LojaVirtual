using LojaVirtual.DTO.EnderecoDto;
using LojaVirtual.Enum;
using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.DTO.UsuarioDto
{
    public class EditarUsuarioDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Selecione um Nível de Acesso.")]
        public CargoEnum Cargo { get; set; }
        public EditarEnderecoDto Endereco { get; set; }
    }
}
