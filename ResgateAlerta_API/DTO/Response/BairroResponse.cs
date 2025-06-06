namespace EcoDenuncia.DTO.Response
{
    public class BairroResponse
    {
        public Guid IdBairro { get; set; }
        public string Nome { get; set; } // Nome do bairro
        public Guid IdCidade { get; set; } // ID da cidade relacionada

    }
}
