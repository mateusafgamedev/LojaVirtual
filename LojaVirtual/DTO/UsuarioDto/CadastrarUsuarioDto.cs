using System.ComponentModel.DataAnnotations;

namespace LojaVirtual.DTO.UsuarioDto
{
    public class CadastrarUsuarioDto
    {
        [Required(ErrorMessage = "O campo Nome é obrigatório.")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "O campo Nível de Acesso é obrigatório.")]
        public string Cargo { get; set; }

        [Required(ErrorMessage = "O campo Logradouro é obrigatório.")]
        public string Logradouro { get; set; }

        [Required(ErrorMessage = "O campo Número é obrigatório.")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "O campo Bairro é obrigatório.")]
        public string Bairro { get; set; }

        public string Complemento { get; set; }

        [Required(ErrorMessage = "O campo CEP é obrigatório.")]
        public string CEP { get; set; }

        [Required(ErrorMessage = "O campo Estado é obrigatório.")]
        public string Estado { get; set; }

        [Required(ErrorMessage = "O campo Cidade é obrigatório.")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "O campo Senha é obrigatório.")]
        public string Senha { get; set; }
        [Required(ErrorMessage = "O campo Confirmar Senha é obrigatório."), 
            Compare("Senha", ErrorMessage = "As senhas não coincidem.")]
        public string ConfirmarSenha { get; set; }
    }
}
