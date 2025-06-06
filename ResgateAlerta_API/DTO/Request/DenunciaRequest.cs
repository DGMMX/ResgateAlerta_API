namespace EcoDenuncia.DTO.Request
{
    public class DenunciaRequest
    {
        public Guid IdUsuario { get; set; }
        public Guid IdLocalizacao { get; set; }
        public Guid IdOrgaoPublico { get; set; }
        public DateTime DataHora { get; set; }
        public string Descricao { get; set; }

    }
}
