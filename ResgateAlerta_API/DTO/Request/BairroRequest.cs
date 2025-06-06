namespace EcoDenuncia.DTO.Request
{
    public class BairroRequest
    {
        public string Nome { get; set; } // Nome do bairro
        public Guid IdCidade { get; set; } // ID do cidade relacionada
    }
}
