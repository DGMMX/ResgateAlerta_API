using System;
using ResgateAlerta.Infrastructure.Persistence;

namespace ResgateAlerta.Infrastructure.Persistence
{
    public class Estado
    {
        public Guid IdEstado { get; private set; }
        public string Nome { get; private set; }
        public string Sigla { get; private set; }

        // relacionamento com Cidade
        public ICollection<Cidade> Cidades { get; private set; } = new List<Cidade>();

        // relacionamento com Bairro
        public ICollection<Bairro> Bairros { get; private set; } = new List<Bairro>();

        // relacionamento com Localizacao
        public ICollection<Localizacao> Localizacoes { get; private set; } = new List<Localizacao>();

        public Estado(string nome, string sigla)
        {
            ValidarNome(nome);
            ValidarSigla(sigla);
            IdEstado = Guid.NewGuid();
            Nome = nome;
            Sigla = sigla;
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

        private void ValidarSigla(string sigla)
        {
            if (string.IsNullOrWhiteSpace(sigla))
            {
                throw new Exception("Sigla não pode ser vazia.");
            }
            if (sigla.Length != 2)
            {
                throw new Exception("Sigla deve ter exatamente 2 caracteres.");
            }
        }

        public void AtualizarEstado(string nome, string sigla)
        {
            ValidarNome(nome);
            ValidarSigla(sigla);
            Nome = nome;
            Sigla = sigla;
        }

        internal static Estado Create(string nome, string sigla)
        {
            return new Estado(nome, sigla);
        }
    }
}
