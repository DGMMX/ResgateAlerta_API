using System;

namespace ResgateAlerta_API.DTO.Request
{
    public class DenunciaRequest
    {
        public Guid IdUsuario { get; set; }
        public Guid IdLocalizacao { get; set; }
        public Guid IdOrgaoPublico { get; set; }
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public string Status { get; set; }
    }
}
