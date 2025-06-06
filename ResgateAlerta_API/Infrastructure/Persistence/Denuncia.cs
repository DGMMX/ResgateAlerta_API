namespace EcoDenuncia.Infrastructure.Persistence
{
    public class Denuncia
    {

        public Guid IdDenuncia { get; private set; }

        // relacionamento com usuario
        public Guid IdUsuario { get; private set; }
        public Usuario Usuario { get; set; }

        // relacionamento com localizacao
        public Guid IdLocalizacao { get; private set; }
        public Localizacao Localizacao { get; set; }

        // relacionamento com orgão publico
        public Guid IdOrgaoPublico { get; private set; }
        public OrgaoPublico OrgaoPublico { get; set; }

        public DateTime DataHora { get; private set; }
        public string Descricao { get; private set; }

        public Denuncia(Guid idUsuario, Guid idLocalizacao, Guid idOrgaoPublico, DateTime dataHora, string descricao)
        {

            ValidarDescricao(descricao);
            IdDenuncia = Guid.NewGuid();
            IdUsuario = idUsuario;
            IdLocalizacao = idLocalizacao;
            IdOrgaoPublico = idOrgaoPublico;
            DataHora = dataHora;
            Descricao = descricao;

        }

        private void ValidarDescricao(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
            {
                throw new Exception("Descrição não pode ser vazia.");
            }
            if (descricao.Length > 250)
            {
                throw new Exception("Descrição deve ter no máximo 250 caracteres.");
            }


        }

        public void AtualizaDenuncia(Guid idUsuario, Guid idLocalizacao, Guid idOrgaoPublico, DateTime dataHora, string descricao)
        {
            IdUsuario = idUsuario;
            IdLocalizacao = idLocalizacao;
            IdOrgaoPublico = idOrgaoPublico;
            DataHora = dataHora;
            ValidarDescricao(descricao);
            Descricao = descricao;
        }

        internal static Denuncia Create(Guid idUsuario, Guid idLocalizacao, Guid idOrgaoPublico, DateTime dataHora, string descricao)
        {
            return new Denuncia(idUsuario, idLocalizacao, idOrgaoPublico, dataHora, descricao);
        }
    }
}
