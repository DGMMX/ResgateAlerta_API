using System;
using ResgateAlerta.Infrastructure.Persistence;

namespace ResgateAlerta.Infrastructure.Persistence
{
    public class Bairro
    {
        public Guid IdBairro { get; private set; }
        public required Cidade Cidade { get; set; }
        public required Estado Estado { get; set; }
        public string Nome { get; private set; }
        
        // relacionamento com Localizacao
        public ICollection<Localizacao> Localizacoes { get; private set; } = new List<Localizacao>();

        public Bairro(Cidade cidade, Estado estado, string nome)
        {
            ValidarNome(nome);
            IdBairro = Guid.NewGuid();
            Cidade = cidade;
            Estado = estado;
            Nome = nome;
        }

        public void AtualizarBairro(string nome)
        {
            ValidarNome(nome);
            Nome = nome;
        }

        private void ValidarNome(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new Exception("Nome não pode ser vazio.");
            }
            if (nome.Length > 100)
            {
                throw new Exception("Nome deve ter no máximo 100 caracteres.");
            }
        }

        internal static Bairro Create(Cidade cidade, Estado estado, string nome)
        {
            return new Bairro(cidade, estado, nome);
        }
    }
}
