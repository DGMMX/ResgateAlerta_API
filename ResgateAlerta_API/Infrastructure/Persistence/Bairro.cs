namespace EcoDenuncia.Infrastructure.Persistence
{
    public class Bairro
    {
        public Guid IdBairro { get; private set; }
        public string Nome { get; private set; }
        
        // relacionamento com Cidade
        public Guid IdCidade { get; private set; }
        public Cidade Cidade { get; set; }

        // relacionamento com Localizacao
        public ICollection<Localizacao> Localizacoes { get; private set; } = new List<Localizacao>();


        public Bairro(string nome, Guid idCidade)
        {
            ValidarNome(nome);
            IdBairro = Guid.NewGuid();
            Nome = nome;
            IdCidade = idCidade;
        }

        public void AtualizarBairro(string nome, Guid idCidade)
        {
            ValidarNome(nome);
            Nome = nome;
            IdCidade = idCidade;
        }

        private void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new Exception("Nome do bairro não pode ser vazio.");
            if (nome.Length > 100)
                throw new Exception("Nome do bairro deve ter no máximo 100 caracteres.");
        }
        internal static Bairro Create(string nome, Guid idCidade)
        {
            return new Bairro(nome, idCidade);
        }
    }
}
