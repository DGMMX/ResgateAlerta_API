namespace EcoDenuncia.DTO.Response
{
    public class CidadeResponse
    {
        public Guid IdCidade { get; set; } // ID da cidade
        public string Nome { get; set; } // Nome da cidade
        public Guid IdEstado { get; set; } // ID do estado ao qual a cidade pertence
        public string NomeEstado { get; set; } // Nome do estado 

    }
}
