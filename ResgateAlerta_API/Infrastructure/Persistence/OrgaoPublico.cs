using System;
using ResgateAlerta.Infrastructure.Persistence;

namespace ResgateAlerta.Infrastructure.Persistence
{
    public class OrgaoPublico
    {
        public Guid IdOrgaoPublico { get; private set; }

        // relacionamento com denuncia
        public ICollection<Denuncia> Denuncias { get; private set; } = new List<Denuncia>();

        public string Nome { get; private set; }
        public string Sigla { get; private set; }
        public string Descricao { get; private set; }

        public OrgaoPublico(string nome, string sigla, string descricao)
        {
            ValidarNome(nome);
            ValidarSigla(sigla);
            ValidarDescricao(descricao);
            IdOrgaoPublico = Guid.NewGuid();
            Nome = nome;
            Sigla = sigla;
            Descricao = descricao;
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
            if (sigla.Length > 10)
            {
                throw new Exception("Sigla deve ter no máximo 10 caracteres.");
            }
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

        public void AtualizaOrgaoPublico(string nome, string sigla, string descricao)
        {
            ValidarNome(nome);
            ValidarSigla(sigla);
            ValidarDescricao(descricao);
            Nome = nome;
            Sigla = sigla;
            Descricao = descricao;
        }

        internal static OrgaoPublico Create(string nome, string sigla, string descricao)
        {
            return new OrgaoPublico(nome, sigla, descricao);
        }
    }
}
