namespace EcoDenuncia.Infrastructure.Persistence
{
    public class Estado
    {
        public Guid IdEstado { get; private set; }
        public string Nome { get; private set; }
        public string Uf { get; private set; }

        // relacionanmento com Cidade 
        public ICollection<Cidade> Cidades { get; private set; } = new List<Cidade>();

        public Estado(string nome, string uf)
        {
            ValidarNome(nome);
            ValidarUf(uf);

            IdEstado = Guid.NewGuid();
            Nome = nome;
            Uf = uf;
        }

        public void AtualizarEstado(string nome, string uf)
        {
            ValidarNome(nome);
            ValidarUf(uf);
            Nome = nome;
            Uf = uf;
        }


        private void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new Exception("Nome do estado não pode ser vazio.");
            if (nome.Length > 100)
                throw new Exception("Nome do estado deve ter no máximo 100 caracteres.");

        }

        private void ValidarUf(string uf)
        {
            if (string.IsNullOrWhiteSpace(uf))
                throw new Exception("UF não pode ser vazia.");
            if (uf.Length != 2)
                throw new Exception("UF deve ter exatamente 2 caracteres.");
        }

        internal static Estado Create(string nome, string uf)
        {
            return new Estado(nome, uf);
        }
    }
}
