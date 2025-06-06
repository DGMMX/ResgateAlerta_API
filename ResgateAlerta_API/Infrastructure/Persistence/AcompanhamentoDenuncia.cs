
using ResgateAlerta.Domain.Enums;
using ResgateAlerta.Infrastructure.Persistence;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace EcoDenuncia.Infrastructure.Persistence
{
    public class AcompanhamentoDenuncia
    {
        public Guid IdAcompanhamento { get; private set; }
        public StatusDenuncia Status { get; private set; }
        public DateTime DataAtualizacao { get; private set; }

        public string Observacao { get; private set; } // Observação opcional para o acompanhamento

        // relacionamento com Denuncia
        public Guid IdDenuncia { get; private set; }
        public Denuncia Denuncia { get; set; }

        public AcompanhamentoDenuncia(StatusDenuncia status, DateTime dataAtualizacao, string observacao, Guid idDenuncia)
        {


            IdAcompanhamento = Guid.NewGuid();
            Status = status;
            DataAtualizacao = dataAtualizacao;
            Observacao = observacao;
            IdDenuncia = idDenuncia;

        }

        public void AtualizarAcompanhamento(StatusDenuncia status, DateTime dataAtualizacao, string observacao)
        {
            Status = status;
            DataAtualizacao = dataAtualizacao;
            Observacao = observacao;
        }

        internal static AcompanhamentoDenuncia Create(StatusDenuncia status, DateTime dataAtualizacao, string observacao, Guid idDenuncia)
        {
            return new AcompanhamentoDenuncia(status, dataAtualizacao, observacao, idDenuncia);
        }




    }
}
