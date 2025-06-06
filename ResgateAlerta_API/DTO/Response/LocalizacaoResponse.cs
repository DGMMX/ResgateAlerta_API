namespace EcoDenuncia.DTO.Response
{
    public class LocalizacaoResponse
    {
        public Guid IdLocalizacao { get; set; } // ID da localização
        public string Logradouro { get; set; } // Logradouro da localização
        public string Numero { get; set; } // Número da localização
        public string Complemento { get; set; } // Complemento da localização
        public string Cep { get; set; } // CEP da localização
        public Guid IdBairro { get; set; } // ID do bairro relacionado

    }
}
