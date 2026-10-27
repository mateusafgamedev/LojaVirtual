using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace LojaVirtual.Models
{
    public class EnderecoModel
    {

        public int Id { get; set; }
        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Numero { get; set; }
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public string CEP { get; set; }
        public string? Complemento { get; set; }
        public int UsuarioId { get; set; }
        [ValidateNever]
        public UsuarioModel Usuario { get; set; }

    }
}
