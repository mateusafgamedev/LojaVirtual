using LojaVirtual.DTO.EnderecoDto;
using LojaVirtual.Enum;
using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.DTO.UsuarioDto
{
    public class EditarUsuarioDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public CargoEnum Cargo { get; set; }
        public EditarEnderecoDto Endereco { get; set; }
    }
}
