namespace EcoDenuncia.Infrastructure.Persistence
{
    public class OrgaoPublico
    {
        public Guid IdOrgaoPublico { get; private set; }
        public string Nome { get; private set; }
        
        public string AreaAtuacao { get; private set; } // Área de atuação do órgão público

        // relacionamento com Denuncia
        public ICollection<Denuncia> Denuncias { get; private set; } = new List<Denuncia>();
        public OrgaoPublico(string nome, string areaAtuacao)
        {
            ValidarNome(nome);
            ValidarAreaAtuacao(areaAtuacao);
            
            IdOrgaoPublico = Guid.NewGuid();
            Nome = nome;
            AreaAtuacao = areaAtuacao.Trim();
        }

        public void AtualizarOrgaoPublico(string nome, string areaAtuacao)
        {
            ValidarNome(nome);
            ValidarAreaAtuacao(areaAtuacao);

            Nome = nome;
            AreaAtuacao = areaAtuacao;
        }

        private void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new Exception("Nome do órgão público não pode ser vazio.");
            if (nome.Length > 150)
                throw new Exception("Nome do órgão público deve ter no máximo 150 caracteres.");

        }

        private void ValidarAreaAtuacao(string areaAtuacao)
        {
            if (string.IsNullOrWhiteSpace(areaAtuacao))
                throw new Exception("Área de atuação não pode ser vazia.");
            if (areaAtuacao.Length > 100)
                throw new Exception("Área de atuação deve ter no máximo 100 caracteres.");

        }
        internal static OrgaoPublico Create(string nome, string areaAtuacao)
        {
            return new OrgaoPublico(nome, areaAtuacao);
        }
    }
}
