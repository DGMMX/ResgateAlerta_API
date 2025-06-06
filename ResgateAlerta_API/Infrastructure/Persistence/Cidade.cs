using System;
using ResgateAlerta.Infrastructure.Persistence;

namespace ResgateAlerta.Infrastructure.Persistence
{
    public class Cidade
    {
        public Guid IdCidade { get; private set; }
        public required Estado Estado { get; set; }
        public string Nome { get; private set; }

        // relacionamento com Bairro
        public ICollection<Bairro> Bairros { get; private set; } = new List<Bairro>();

        // relacionamento com Localizacao
        public ICollection<Localizacao> Localizacoes { get; private set; } = new List<Localizacao>();

        public Cidade(Estado estado, string nome)
        {
            ValidarNome(nome);
            IdCidade = Guid.NewGuid();
            Estado = estado;
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

        public void AtualizarCidade(string nome)
        {
            ValidarNome(nome);
            Nome = nome;
        }

        internal static Cidade Create(Estado estado, string nome)
        {
            return new Cidade(estado, nome);
        }
    }
}
