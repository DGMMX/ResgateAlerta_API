using System;
using ResgateAlerta.Infrastructure.Persistence;

namespace ResgateAlerta.Infrastructure.Persistence
{
    public class Denuncia
    {
        public Guid IdDenuncia { get; private set; }
        public required Usuario Usuario { get; set; }
        public required Localizacao Localizacao { get; set; }
        public required OrgaoPublico OrgaoPublico { get; set; }
        public string Titulo { get; private set; }
        public string Descricao { get; private set; }
        public DateTime DataDenuncia { get; private set; }
        public string Status { get; private set; }

        public Denuncia(Usuario usuario, Localizacao localizacao, OrgaoPublico orgaoPublico, string titulo, string descricao)
        {
            ValidarTitulo(titulo);
            ValidarDescricao(descricao);
            IdDenuncia = Guid.NewGuid();
            Usuario = usuario;
            Localizacao = localizacao;
            OrgaoPublico = orgaoPublico;
            Titulo = titulo;
            Descricao = descricao;
            DataDenuncia = DateTime.Now;
            Status = "Pendente";
        }

        private void ValidarTitulo(string titulo)
        {
            if (string.IsNullOrWhiteSpace(titulo))
            {
                throw new Exception("Título não pode ser vazio.");
            }
            if (titulo.Length > 100)
            {
                throw new Exception("Título deve ter no máximo 100 caracteres.");
            }
        }

        private void ValidarDescricao(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
            {
                throw new Exception("Descrição não pode ser vazia.");
            }
            if (descricao.Length > 500)
            {
                throw new Exception("Descrição deve ter no máximo 500 caracteres.");
            }
        }

        public void AtualizarDenuncia(string titulo, string descricao, string status)
        {
            ValidarTitulo(titulo);
            ValidarDescricao(descricao);
            Titulo = titulo;
            Descricao = descricao;
            Status = status;
        }

        internal static Denuncia Create(Usuario usuario, Localizacao localizacao, OrgaoPublico orgaoPublico, string titulo, string descricao)
        {
            return new Denuncia(usuario, localizacao, orgaoPublico, titulo, descricao);
        }
    }
}
