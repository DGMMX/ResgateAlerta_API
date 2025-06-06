namespace EcoDenuncia.Infrastructure.Persistence
{
    public class Cidade
    {
        public Guid IdCidade { get; private set; }
        public string Nome { get; private set; }

        // relacionamento com Estado
        public Guid IdEstado { get; private set; }
        public Estado Estado { get; set; }

        public ICollection<Bairro> Bairros { get; private set; } = new List<Bairro>();


        public Cidade(string nome, Guid idEstado)
        {
            ValidarNome(nome);
            IdCidade = Guid.NewGuid();
            Nome = nome;
            IdEstado = idEstado;
        }

        public void SetNome(string nome)
        {
            ValidarNome(nome);
            Nome = nome;
        }

        public void SetIdEstado(Guid idEstado)
        {
            IdEstado = idEstado;
                
        }

        private void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new Exception("Nome da cidade não pode ser vazio.");
            if (nome.Length > 100)
                throw new Exception("Nome da cidade deve ter no máximo 100 caracteres.");
        }

        internal static Cidade Create(string nome, Guid idEstado)
        {
            return new Cidade(nome, idEstado);
        }
    }
}
