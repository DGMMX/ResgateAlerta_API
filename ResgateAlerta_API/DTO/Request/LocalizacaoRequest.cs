namespace EcoDenuncia.DTO.Request
{
    public class LocalizacaoRequest
    {
        public string Logradouro { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public string Cep { get; set; }
        public Guid IdBairro { get; set; } // ID do bairro relacionado
        
    }
}
