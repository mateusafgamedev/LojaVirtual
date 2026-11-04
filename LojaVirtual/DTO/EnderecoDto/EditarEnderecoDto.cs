namespace LojaVirtual.DTO.EnderecoDto
{
    public class EditarEnderecoDto
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
    }
}
