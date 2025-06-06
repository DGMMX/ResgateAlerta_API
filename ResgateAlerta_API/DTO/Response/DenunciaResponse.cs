namespace EcoDenuncia.DTO.Response
{
    public class DenunciaResponse
    {
        public Guid IdDenuncia { get; set; }
        public DateTime DataHora { get; set; } // Data e hora da denúncia
        public string Descricao { get; set; } // Descrição da denúncia

        // informações relacionadas 
        public string NomeUsuario { get; set; } // Nome do usuário que fez a denúncia
        public string NomeOrgaoPublico { get; set; } // Nome do órgão público relacionado à denúncia
        public string Logradouro { get; set; } // Logradouro da localização da denúncia
        public string Numero { get; set; } // Número da localização da denúncia
        public string Bairro { get; set; } // Bairro da localização da denúncia
        public string Cidade { get; set; } // Cidade da localização da denúncia
        public string Estado { get; set; } // Estado da localização da denúncia




    }
}
