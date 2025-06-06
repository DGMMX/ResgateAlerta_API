using ResgateAlerta.Domain.Enums;

namespace EcoDenuncia.DTO.Response
{
    public class AcompanhamentoDenunciaResponse
    {
        public Guid IdAcompanhamento { get; set; } // ID do acompanhamento da denúncia
        public string Status { get; set; } // Status do acompanhamento ("Aberto", "Em Andamento", "Concluído")
        public DateTime DataAtualizacao { get; set; } // Data e hora da atualização do acompanhamento
        public string Observacao { get; set; } // Observação ou comentário sobre o acompanhamento
        public Guid IdDenuncia { get; set; } // ID da denúncia relacionada ao acompanhamento

    }
}
