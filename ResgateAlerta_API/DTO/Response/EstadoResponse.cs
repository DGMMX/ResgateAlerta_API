namespace EcoDenuncia.DTO.Response
{
    public class EstadoResponse
    {
        public Guid IdEstado { get; set; } // ID do estado
        public string Nome { get; set; } // Nome do estado
        
        public string Uf { get; set; } // Sigla do estado (UF)

    }
}
