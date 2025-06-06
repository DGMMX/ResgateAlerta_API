using System;

namespace ResgateAlerta_API.DTO.Response
{
    public class DenunciaResponse
    {
        public Guid IdDenuncia { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime DataDenuncia { get; set; }
        public string Status { get; set; }
        public string Usuario { get; set; }
        public string Localizacao { get; set; }
        public string OrgaoPublico { get; set; }
    }
}
