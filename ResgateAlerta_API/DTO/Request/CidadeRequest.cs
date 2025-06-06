namespace EcoDenuncia.DTO.Request
{
    public class CidadeRequest
    {
        public string Nome { get; set; } // Nome da cidade
        public Guid IdEstado { get; set; } // ID do estado ao qual a cidade pertence
    }
}
